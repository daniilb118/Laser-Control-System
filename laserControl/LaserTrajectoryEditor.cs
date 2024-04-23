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
            visualizationPanel = new(screenPanel, device, label);

            screenPanel.MouseDown += (object? sender, MouseEventArgs e) =>
            {
                var targetPos = visualizationPanel.GetLaserPosition(e.Location);

                if (e.Button == MouseButtons.Left) laserTrajectory.SelectedTargetPosition = targetPos;
                else if (e.Button == MouseButtons.Right) laserTrajectory.InsertTargetAfterSelected(targetPos);

                visualizationPanel.TargetLaserPosition = laserTrajectory.SelectedTarget.Position;
                OnUserSelectedTargetChanged(new(targetPos, laserTrajectory.SelectedTarget.Intensity * (float)intensitySetter.Value / 100));
            };

            targetGridView.SelectionChanged += (object? sender, EventArgs e) =>
            {
                visualizationPanel.TargetLaserPosition = laserTrajectory.SelectedTarget.Position;
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
    }
}
