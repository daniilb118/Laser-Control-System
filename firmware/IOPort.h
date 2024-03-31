#ifndef _IOPORT_h
#define _IOPORT_h

#include <stdint.h>
#include <Arduino.h>
#include "LFIFO.h"

struct FrameHeader {
	uint8_t thisACK; //POV: sender
	uint8_t thatACK; //POV: sender
	bool isAuxiliary; //POV: sender
	uint8_t checksum;
};

template <class Message, size_t queueSize = 32>
class IOPort {
	using MessageQueue = LFIFO<Message, queueSize>;

	struct Frame {
		Message message;
		FrameHeader	header;

		bool validate() const {
			//TODO: implement checksum
			return true;
		}

		void setChecksum() {
			//TODO: implement checksum
		}

		Frame(
			uint8_t thisACK,
			uint8_t thatACK,
			bool isAuxiliary,
			Message message
		) :
			message {message}
		{
			header.thisACK = thisACK;
			header.thatACK = thatACK;
			header.isAuxiliary = isAuxiliary;
			setChecksum();
		}

		Frame() {}
	};

public:
	using OnReceiveFunc = void (*)(Message message);

	void send(const Message& message) {
		messagesToSend.add(message);
	}

	IOPort(OnReceiveFunc onReceiveFunc) :
		onReceiveFunc {onReceiveFunc}
	{
		static_assert(
			sizeof(Frame) == sizeof(Message) + sizeof(FrameHeader),
			"Wrong frame size."
		);
	}

	void tick() {
		receive();
		if (!isLastMessageApproved) {
			if (millis() - timer >= responseTimeoutMs) {
				trySend();
				timer = millis();
			}
		} else {
			trySendNext();
		}
	}

private:
	void sendAuxiliary(const Message& message) {
		Frame frame(
			thisACK,
			thatACK,
			true,
			message
		);
		if (Serial.availableForWrite() >= sizeof(Frame)) {
			Serial.write((uint8_t*)&frame, sizeof(Frame));
		}
	}

	void trySend() {
		if (Serial.availableForWrite() >= sizeof(Frame)) {
			pendingFrame.header.thatACK = thatACK; //thisID doesn't change for no reason but other side can require new "thatID at any time
			Serial.write((uint8_t*)&pendingFrame, sizeof(Frame));
		}
	}
	
	bool trySendNext() {
		if (messagesToSend.available() == 0 || !isLastMessageApproved) {
			return false;
		}
		isLastMessageApproved = false;
		thisACK = next(thisACK);
		pendingFrame = Frame(
			thisACK,
			thatACK,
			false,
			messagesToSend.get()
		);
		messagesToSend.next();
		trySend();
		timer = millis();
		return true;
	}

	void receive() {
		if (Serial.available() < sizeof(Frame)) {
			return;
		}
		Frame frame;
		Serial.readBytes((uint8_t*)&frame, sizeof(Frame));
		if (frame.validate()) {
			process(frame);
		} else {
			discardBuffer();
		}
	}

	void process(const Frame& frame) {
		isLastMessageApproved = (frame.header.thatACK == thisACK) || isLastMessageApproved;			//POV: receiver - this -> header.that
		bool isIncomingMessageNew = frame.header.thisACK != thatACK;	//POV: receiver - that -> header.this
			
		if (isIncomingMessageNew && !frame.header.isAuxiliary) {
			thatACK = frame.header.thisACK;
			onReceiveFunc(frame.message);
		}
		if (!trySendNext() && !frame.header.isAuxiliary) {
			sendAuxiliary(frame.message);
		}
	}

	uint8_t next(uint8_t num) {
		return (num == 255) ? 0 : num + 1;
	}

	void discardBuffer() {
		int hardwareBufferSize = 64;
		uint8_t buf[hardwareBufferSize];
		Serial.readBytes(buf, Serial.available());
	}


	MessageQueue messagesToSend = {};
	Frame pendingFrame;
	uint32_t timer;
	int responseTimeoutMs = 300;
	OnReceiveFunc onReceiveFunc;
	uint8_t thisACK = 0;
	uint8_t thatACK = 0;
	bool isLastMessageApproved = true;
};

#endif
