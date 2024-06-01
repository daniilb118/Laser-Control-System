using System.Numerics;

namespace laserControl
{
    internal class LaserTrajectoryEditor
    {
        private LaserTrajectory laserTrajectory;
        private ScreenVisualizationPanel visualizationPanel;
        private NumericUpDown intensitySetter;

        public LaserTrajectoryEditor(LaserDevice device, DataGridView targetGridView, Panel screenPanel, ToolStripLabel laserPositionLabel, NumericUpDown intensitySetter)
        {
            this.intensitySetter = intensitySetter;

            laserTrajectory = new(targetGridView, device.Profile.ScreenSize);
            visualizationPanel = new(screenPanel, device, laserTrajectory);

            screenPanel.MouseDown += (object? sender, MouseEventArgs e) =>
            {
                var targetPos = AimedLaserPosition(visualizationPanel.GetLaserPosition(e.Location));

                if (e.Button == MouseButtons.Left)
                {
                    if (Control.ModifierKeys.HasFlag(Keys.Alt)) laserTrajectory.SelectedIndex = laserTrajectory.ClosestTargetIndex(targetPos);
                    else laserTrajectory.SelectedTargetPosition = targetPos;
                }
                else if (e.Button == MouseButtons.Right) laserTrajectory.InsertTargetAfterSelected(targetPos);

                TargetLaserPosition = laserTrajectory.SelectedTarget.Position;
                OnUserSelectedTargetChanged(new(targetPos, laserTrajectory.SelectedTarget.Intensity * intensityMultiplier));

                MouseDown(sender, e, targetPos);
            };

            screenPanel.MouseMove += (object? sender, MouseEventArgs e) =>
            {
                visualizationPanel.CursorPosition = AimedLaserPosition(visualizationPanel.GetLaserPosition(e.Location));
                var targetPosition = AimedLaserPosition(visualizationPanel.GetLaserPosition(e.Location));
                visualizationPanel.CursorPosition = targetPosition;
                laserPositionLabel.Text = $"Position: ({targetPosition.X}m; {targetPosition.Y}m)";
            };

            screenPanel.MouseLeave += (object? sender, EventArgs e) => laserPositionLabel.Text = "";

            targetGridView.SelectionChanged += (object? sender, EventArgs e) =>
            {
                TargetLaserPosition = laserTrajectory.SelectedTarget.Position;
            };

            targetGridView.CellMouseDown += (object? sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (e.RowIndex < 0) return;
                var target = laserTrajectory[e.RowIndex];
                OnUserSelectedTargetChanged(new(target.Position, target.Intensity * intensityMultiplier));
            };
        }

        public delegate void OnUserSelectedTargetChangedDelegate(LaserDevice.Target target);

        public OnUserSelectedTargetChangedDelegate OnUserSelectedTargetChanged = delegate { };

        public delegate void OnMouseDown(object? sender, MouseEventArgs e, Vector2 targetPosition);

        public OnMouseDown MouseDown = delegate { };

        public int TrajectoryLength => laserTrajectory.Length;

        public float ScreenSize
        {
            get => laserTrajectory.ScreenSize;
            set => laserTrajectory.ScreenSize = value;
        }

        public int SelectedIndex
        {
            get => laserTrajectory.SelectedIndex;
            set => laserTrajectory.SelectedIndex = value;
        }

        public List<LaserDevice.Target> NormalizedTargets
        {
            get => laserTrajectory.NormalizedTargets;
            set => laserTrajectory.NormalizedTargets = value;
        }

        public IEnumerable<LaserDevice.Target> GetTargets(int begin, int? end) => laserTrajectory.GetTargets(begin, end);

        public Vector2 LaserPosition
        {
            get => visualizationPanel.LaserPosition;
            set => visualizationPanel.LaserPosition = value;
        }

        public Vector2 TargetLaserPosition
        {
            get => visualizationPanel.TargetLaserPosition;
            set => visualizationPanel.TargetLaserPosition = value;
        }

        public Image? Background
        {
            get => visualizationPanel.Background;
            set => visualizationPanel.Background = value;
        }

        private float intensityMultiplier => (float)intensitySetter.Value / 100;

        private Vector2 AimedLaserPosition(Vector2 aimedLaserPosition)
        {
            var targetPosition = laserTrajectory.SelectedTarget.Position;
            var useClosestPoint = Control.ModifierKeys.HasFlag(Keys.Alt);
            var axisXFixed = Control.ModifierKeys.HasFlag(Keys.Control);
            var axisYFixed = Control.ModifierKeys.HasFlag(Keys.Shift);
            if (useClosestPoint) { return laserTrajectory[laserTrajectory.ClosestTargetIndex(aimedLaserPosition)].Position; }
            return new(axisXFixed ? targetPosition.X : aimedLaserPosition.X, axisYFixed ? targetPosition.Y : aimedLaserPosition.Y);
        }
    }
}
