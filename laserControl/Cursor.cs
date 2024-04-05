namespace laserControl
{
    /// <summary>
    /// a control that indicates a location by crossing two lines over it
    /// </summary>
    internal class Cursor
    {
        public Panel horizontalLine;
        public Panel verticalLine;
        private Point location;
       
        public Cursor(Control parent)
        {
            horizontalLine = new Panel();
            verticalLine = new Panel();
            parent.Controls.AddRange([horizontalLine, verticalLine]);
            horizontalLine.BackColor = Color.Gray;
            verticalLine.BackColor = Color.Gray;
            Hide();
            horizontalLine.Enabled = false;
            verticalLine.Enabled = false;

            verticalLine.Paint += (object? sender, PaintEventArgs e) => verticalLine.Location = new Point(Location.X, 0);
            horizontalLine.Paint += (object? sender, PaintEventArgs e) => horizontalLine.Location = new Point(0, Location.Y);
        }

        public Point Location
        {
            get => location;
            set
            {
                location = value;
                Invalidate();
            }
        }

        public Size Size
        {
            set
            {
                horizontalLine.Size = new Size(value.Width, 1);
                verticalLine.Size = new Size(1, value.Height);
            }
        }

        public void Show()
        {
            horizontalLine.Show();
            verticalLine.Show();
        }

        public void Hide()
        {
            horizontalLine.Hide();
            verticalLine.Hide();
        }

        public void Invalidate()
        {
            horizontalLine.Invalidate();
            verticalLine.Invalidate();
        }
    }
}
