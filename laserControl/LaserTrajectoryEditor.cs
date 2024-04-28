using System.Numerics;

namespace laserControl
{
    internal class LaserTrajectoryEditor
    {
        private LaserTrajectory laserTrajectory;
        private ScreenVisualizationPanel visualizationPanel;

        public LaserTrajectoryEditor(LaserDevice device, DataGridView targetGridView, Panel screenPanel, Label label, NumericUpDown intensitySetter)
        {
            laserTrajectory = new(targetGridView, device.Profile.ScreenSize);
            visualizationPanel = new(screenPanel, device, label, laserTrajectory);

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
                OnUserSelectedTargetChanged(new(targetPos, laserTrajectory.SelectedTarget.Intensity * (float)intensitySetter.Value / 100));
            };

            screenPanel.MouseMove += (object? sender, MouseEventArgs e) =>
            {
                visualizationPanel.CursorPosition = AimedLaserPosition(visualizationPanel.GetLaserPosition(e.Location));
            };

            targetGridView.SelectionChanged += (object? sender, EventArgs e) =>
            {
                TargetLaserPosition = laserTrajectory.SelectedTarget.Position;
            };
        }

        public delegate void OnUserSelectedTargetChangedDelegate(LaserDevice.Target target);

        public OnUserSelectedTargetChangedDelegate OnUserSelectedTargetChanged = delegate { };

        public float ScreenSize
        {
            get => laserTrajectory.ScreenSize;
            set => laserTrajectory.ScreenSize = value;
        }

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
