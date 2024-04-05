using System.Numerics;

namespace laserControl
{
    internal class CoordinatesPanel
    {
        public Panel backgroundPanel;
        public int MaxBorderSize = 25;
        public float ZoneRelativeSize = 0.9f;
        
        public CoordinatesPanel(Panel backgroundPanel)
        {
            this.backgroundPanel = backgroundPanel;
            backgroundPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        public Vector2 Center()
        {
            return new Vector2((backgroundPanel.Size.Width - 1) / 2, (backgroundPanel.Size.Height - 1) / 2);
        }

        public int CoordinatesSquareSize()
        {
            var panelSize = Convert.ToDouble(Math.Min(backgroundPanel.Size.Width, backgroundPanel.Size.Height));
            return Convert.ToInt32(Math.Max(panelSize * ZoneRelativeSize, panelSize - MaxBorderSize * 2));
        }

        public Point GetPanelPoint(Vector2 coordinates)
        {
            return new Point(
                Convert.ToInt32(Center().X + CoordinatesSquareSize() * coordinates.X / 2),
                Convert.ToInt32(Center().Y - CoordinatesSquareSize() * coordinates.Y / 2)
            );
        }

        public Vector2 GetCoordinates(Point displayPoint)
        {
            return new Vector2(
                (float)(displayPoint.X - Center().X) / CoordinatesSquareSize() * 2,
                -(float)(displayPoint.Y - Center().Y) / CoordinatesSquareSize() * 2
            );
        }
    }
}
