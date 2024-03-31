using System.IO.Ports;
using System.Timers;
using MessageQueue = System.Collections.Concurrent.ConcurrentQueue<byte[]>;

namespace laserControl
{
    /// <summary>
    /// error-redundant, fixed-message-size, two-way communication protocol implementation.
    /// </summary>
    public class IOPort
    {
        public delegate void OnFrameFunc(Frame frame, string who);
        public delegate void OnReceiveFunc(byte[] message);

        private SerialPort serialPort;
        private volatile byte thisACK = 0;
        private volatile byte thatACK = 0;
        private volatile bool isLastMessageApproved = true;
        private MessageQueue messagesToSend = new MessageQueue();
        private static int responseTimeoutMs = 300;
        public int messageSize;
        private Frame pendingFrame;
        private int frameSize;
        public OnFrameFunc OnFrame = new OnFrameFunc((Frame frame, string who) => { });
        public OnReceiveFunc OnReceive { get; set; } = message => {};
        private System.Timers.Timer timer = new System.Timers.Timer();

        public void Send(byte[] message)
        {
            messagesToSend.Enqueue(message);
            if (isLastMessageApproved)
            {
                TrySendNext();
            }
        }

        private void SendAuxiliary()
        {
            var frame = new Frame(
                thisACK,
                thatACK,
                1,
                new byte[messageSize]
            );
            serialPort.Write(frame.GetBytes(), 0, frameSize);
            OnFrame(frame, "this");
        }

        private void TrySend()
        {
            pendingFrame.thatACK = thatACK;
            serialPort.Write(pendingFrame.GetBytes(), 0, frameSize);
            OnFrame(pendingFrame, "this");
        }

        private bool TrySendNext()
        {
            if (messagesToSend.IsEmpty || !isLastMessageApproved)
            {
                return false;
            }
            byte[] message;
            if (messagesToSend.TryDequeue(out message))
            {
                isLastMessageApproved = false;
                thisACK = Next(thisACK);
                pendingFrame = new Frame(
                    thisACK,
                    thatACK,
                    0,
                    message
                );
                TrySend();
                timer.Start();
                return true;
            }
            return false;
        }

        private void Receive()
        {
            if (serialPort.BytesToRead < frameSize)
            {
                return;
            }
            var frameBytes = new byte[frameSize];
            serialPort.Read(frameBytes, 0, frameSize);
            var frame = new Frame(frameBytes);
            OnFrame(frame, "device");
            if (frame.IsValid)
            {
                Process(frame);
            }
            else
            {
                serialPort.DiscardInBuffer();
            }
        }

        private void Process(Frame frame)
        {
            isLastMessageApproved = (frame.thatACK == thisACK) || isLastMessageApproved;    //POV: receiver - this -> header.that
            bool isIncomingMessageNew = frame.thisACK != thatACK;  //POV: receiver - that -> header.this

            if (isLastMessageApproved)
            {
                timer.Stop();
            }

            if (isIncomingMessageNew && frame.isAuxiliary == 0)
            {
                thatACK = frame.thisACK;
                Thread thread = new(() => OnReceive(frame.message));
                thread.Start();
            }
            if (!TrySendNext() && frame.isAuxiliary == 0)
            {
                SendAuxiliary();
            }
        }

        private static byte Next(byte num)
        {
            return (byte)((num == 255) ? 0 : num + 1);
        }

        public IOPort(SerialPort serialPort, int messageSize)
        {
            this.serialPort = serialPort;
            this.messageSize = messageSize;
            frameSize = messageSize + Frame.HeaderSize;
            serialPort.DataReceived += (object sender, SerialDataReceivedEventArgs e) => Receive();
            timer.Interval = responseTimeoutMs;
            timer.AutoReset = true;
            timer.Elapsed += (object? sender, ElapsedEventArgs e) => TrySend();
        }
    }
}
