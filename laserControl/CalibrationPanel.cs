using System.Numerics;

namespace laserControl
{
    /// <summary>
    /// A control that's bound to a LaserDeviceProfile and helps
    /// to configure a compensation grid data of the profile.
    /// </summary>
    internal class CalibrationPanel
    {
        static private int gridSize => LaserDeviceProfile.CompensationGridSize;

        private CoordinatesPanel coordinatesPanel;
        private LaserDeviceProfile profile;

        public CalibrationPanel(Panel panel, LaserDeviceProfile profile, ToolStripLabel label)
        {
            this.profile = profile;
            coordinatesPanel = new(panel);
            coordinatesPanel.MaxBorderSize = 200;
            coordinatesPanel.ZoneRelativeSize = 0.7f;

            coordinatesPanel.backgroundPanel.Resize += (object? sender, EventArgs e) => Redraw();

            coordinatesPanel.backgroundPanel.MouseDown += (object? sender, MouseEventArgs e) =>
            {
                moveCompensationOffset(coordinatesPanel.GetCoordinates(e.Location));
                Redraw();
            };

            coordinatesPanel.backgroundPanel.MouseMove += (object? sender, MouseEventArgs e) =>
            {
                var position = coordinatesPanel.GetCoordinates(e.Location);
                var offset = (position - gridPoint(closestGridPointIndex(position))) * profile.ScreenSize;
                label.Text = $"Position: ({offset.X}m; {offset.Y}m)";
            };
        }

        public void Redraw()
        {
            var bitmap = new Bitmap(coordinatesPanel.backgroundPanel.Width, coordinatesPanel.backgroundPanel.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            drawGrid(graphics, new Pen(Color.LightGray), vec => vec);
            drawGrid(graphics, new Pen(Color.Red), profile.Compensate);
            drawOffsets(graphics);
            coordinatesPanel.backgroundPanel.BackgroundImage = bitmap;
        }

        private Vector2 gridPoint(int i) => new Vector2(i % gridSize, i / gridSize) / (gridSize - 1) * 2 - Vector2.One;

        private int closestGridPointIndex(Vector2 pointPosition) => Enumerable.Range(0, gridSize* gridSize)
            .MinBy(i => Vector2.Distance(profile.Compensate(gridPoint(i)), pointPosition));

        private void moveCompensationOffset(Vector2 pointPosition)
        {
            int closestPointIndex = closestGridPointIndex(pointPosition);
            profile.CompensationOffsets[closestPointIndex] = pointPosition - gridPoint(closestPointIndex);
        }

        private void drawGrid(Graphics graphics, Pen pen, Func<Vector2, Vector2> transform)
        {
            //left -> right, up -> down numerated grid point sequence
            int[] pointSequence = [0, 1, 2, 3, 7, 6, 5, 4, 8, 9, 10, 11, 15, 14, 13, 12, 8, 4, 0, 1, 5, 9, 13, 14, 10, 6, 2, 3, 7, 11];

            Point[] lines = new Point[pointSequence.Length];

            for (int i = 0; i < pointSequence.Length; i++)
            {
                lines[i] = coordinatesPanel.GetPanelPoint(transform(gridPoint(pointSequence[i])));
            }

            graphics.DrawLines(pen, lines);
        }

        private void drawOffsets(Graphics graphics)
        {
            foreach (int i in Enumerable.Range(0, gridSize * gridSize))
            {
                var offset = profile.CompensationOffsets[i] * profile.ScreenSize;
                var s = $"X: {Math.Round(offset.X, 4)}\nY: {Math.Round(offset.Y, 4)}";
                graphics.DrawString(s, new Font("Arial", 8), Brushes.Black, coordinatesPanel.GetPanelPoint(gridPoint(i)));
            }
        }
    }
}
