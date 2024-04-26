using System.Numerics;

namespace laserControl
{
    /// <summary>
    /// A control for visualization device state an trajectory preview on screen surface.
    /// </summary>
    internal class ScreenVisualizationPanel
    {
        private CoordinatesPanel coordinatesPanel;
        private int centerMarkSize = 10;
        private int laserMarkSize = 10;
        private Bitmap cachedStaticLayers = new Bitmap(10, 10);
        private LaserDevice laserDevice;
        private Vector2 laserPosition;
        private Vector2 targetLaserPosition;
        private Cursor cursor;
        private Image? background = null;
        private LaserTrajectory laserTrajectory;

        public Image? Background
        {
            get => background;
            set
            {
                background = value;
                redraw();
            }
        }

        public ScreenVisualizationPanel(Panel laserControlPanel, LaserDevice laserDevice, Label label, LaserTrajectory laserTrajectory)
        {
            this.laserTrajectory = laserTrajectory;
            coordinatesPanel = new(laserControlPanel);
            this.laserDevice = laserDevice;
            cursor = new(laserControlPanel);
            
            coordinatesPanel.backgroundPanel.MouseMove += new MouseEventHandler(
                (object? sender, MouseEventArgs e) =>
                {
                    var pos = GetLaserPosition(e.Location);
                    label.Text = $"({pos.X}m; {pos.Y}m)";
                    label.Location = new(e.Location.X + 10, e.Location.Y + 1);
                    cursor.Location = e.Location;
                }
            );

            laserControlPanel.MouseLeave += (object? sender, EventArgs e) => cursor.Hide();
            laserControlPanel.MouseEnter += (object? sender, EventArgs e) => cursor.Show();
            laserControlPanel.Resize += (object? sender, EventArgs e) => cursor.Size = laserControlPanel.Size;

            coordinatesPanel.backgroundPanel.Resize += (object? sender, EventArgs e) => redraw();
        }

        public Vector2 GetLaserPosition(Point mousePoint)
        {
            return coordinatesPanel.GetCoordinates(mousePoint) * laserDevice.Profile.ScreenSize;
        }

        private void drawDynamicLayer(Action<Graphics> draw)
        {
            Bitmap backroundBitmap = new(cachedStaticLayers);
            var backgroundGraphics = Graphics.FromImage(backroundBitmap);
            draw(backgroundGraphics);
            coordinatesPanel.backgroundPanel.BackgroundImage = backroundBitmap;
        }

        private void visualizeLaserPosition(Graphics graphics, Vector2 laserPosition, Pen pen)
        {
            DrawLaserMark(graphics, coordinatesPanel.GetPanelPoint(laserPosition / laserDevice.Profile.ScreenSize), laserMarkSize, pen);
        }

        private void visualizeTrajectory(Graphics graphics)
        {
            var targets = laserTrajectory.NormalizedTargets;
            var f = (int i) => coordinatesPanel.GetPanelPoint(targets[i % targets.Count].Position);
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].Intensity == 0) continue;
                graphics.DrawLine(new(Brushes.White, 3.0f), f(i), f(i + 1));
                graphics.DrawLine(new(Brushes.Black), f(i), f(i + 1));
            }
        }

        private void drawDynamicLayers(Graphics graphics)
        {
            visualizeTrajectory(graphics);
            visualizeLaserPosition(graphics, LaserPosition, new(Brushes.Blue));
            visualizeLaserPosition(graphics, TargetLaserPosition, new(Brushes.Red));
        }

        public Vector2 LaserPosition
        {
            get => laserPosition;
            set
            {
                laserPosition = value;
                drawDynamicLayer(drawDynamicLayers);
            }
        }

        public Vector2 TargetLaserPosition
        {
            get => targetLaserPosition;
            set
            {
                targetLaserPosition = value;
                drawDynamicLayer(drawDynamicLayers);
            }
        }

        private void drawStaticLayers()
        {
            cachedStaticLayers = new Bitmap(coordinatesPanel.backgroundPanel.Width, coordinatesPanel.backgroundPanel.Height);
            Graphics graphics = Graphics.FromImage(cachedStaticLayers);
            graphics.Clear(Color.White);
            var zoneSize = coordinatesPanel.CoordinatesSquareSize();
            var zoneRect = new Rectangle(
                coordinatesPanel.GetPanelPoint(new Vector2(-1, 1)),
                new Size(zoneSize, zoneSize)
            );
            if (background != null)
            {
                graphics.DrawImage(background, zoneRect);
            }
            graphics.DrawRectangle(new Pen(Brushes.LightGray), zoneRect);
            DrawCenter(graphics);
        }

        private void redraw()
        {
            drawStaticLayers();
            drawDynamicLayer(drawDynamicLayers);
        }

        private void DrawCenter(Graphics backgroundGraphics)
        {
            backgroundGraphics.DrawEllipse(
                new(Brushes.Red),
                coordinatesPanel.Center().X - centerMarkSize / 2,
                coordinatesPanel.Center().Y - centerMarkSize / 2,
                centerMarkSize,
                centerMarkSize
            );
        }

        private void DrawLaserMark(Graphics graphics, Point position, int radius, Pen pen)
        {
            graphics.DrawLine(pen,
                position.X - radius,
                position.Y,
                position.X + radius,
                position.Y
            );
            graphics.DrawLine(pen,
                position.X,
                position.Y - radius,
                position.X,
                position.Y + radius
            );
        }
    }
}
