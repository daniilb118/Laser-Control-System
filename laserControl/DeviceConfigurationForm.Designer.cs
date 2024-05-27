namespace laserControl
{
    partial class DeviceConfigurationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            screenSizeSetter = new NumericUpDown();
            screenDistanceSetter = new NumericUpDown();
            mirrorDistanceSetter = new NumericUpDown();
            motorStepsPerRotationSetter = new NumericUpDown();
            axisXInvertedSetter = new CheckBox();
            axisYInvertedSetter = new CheckBox();
            maxSpeedSetter = new NumericUpDown();
            axisXbacklashSetter = new NumericUpDown();
            axisYbacklashSetter = new NumericUpDown();
            applyButton = new Button();
            splitContainer1 = new SplitContainer();
            statusStrip1 = new StatusStrip();
            closestPointOffsetLabel = new ToolStripStatusLabel();
            calibrationBackgroundPanel = new Panel();
            screenPointPositionLabel = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)screenSizeSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)screenDistanceSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mirrorDistanceSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)motorStepsPerRotationSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxSpeedSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)axisXbacklashSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)axisYbacklashSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 5);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 0;
            label1.Text = "Screen size (m)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 34);
            label2.Name = "label2";
            label2.Size = new Size(111, 15);
            label2.TabIndex = 1;
            label2.Text = "Screen distance (m)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(0, 63);
            label3.Name = "label3";
            label3.Size = new Size(109, 15);
            label3.TabIndex = 2;
            label3.Text = "Mirror distance (m)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(0, 92);
            label4.Name = "label4";
            label4.Size = new Size(139, 15);
            label4.TabIndex = 3;
            label4.Text = "Motor Steps Per Rotation";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(0, 120);
            label5.Name = "label5";
            label5.Size = new Size(85, 15);
            label5.TabIndex = 4;
            label5.Text = "Axis X inverted";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(0, 145);
            label6.Name = "label6";
            label6.Size = new Size(85, 15);
            label6.TabIndex = 5;
            label6.Text = "Axis Y inverted";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(0, 171);
            label7.Name = "label7";
            label7.Size = new Size(112, 15);
            label7.TabIndex = 6;
            label7.Text = "Max speed (steps/s)";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(0, 200);
            label8.Name = "label8";
            label8.Size = new Size(126, 15);
            label8.TabIndex = 7;
            label8.Text = "Axis X backlash (steps)";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(0, 229);
            label9.Name = "label9";
            label9.Size = new Size(126, 15);
            label9.TabIndex = 8;
            label9.Text = "Axis Y backlash (steps)";
            // 
            // screenSizeSetter
            // 
            screenSizeSetter.Location = new Point(166, 3);
            screenSizeSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            screenSizeSetter.Name = "screenSizeSetter";
            screenSizeSetter.Size = new Size(90, 23);
            screenSizeSetter.TabIndex = 9;
            // 
            // screenDistanceSetter
            // 
            screenDistanceSetter.Location = new Point(166, 32);
            screenDistanceSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            screenDistanceSetter.Name = "screenDistanceSetter";
            screenDistanceSetter.Size = new Size(90, 23);
            screenDistanceSetter.TabIndex = 10;
            // 
            // mirrorDistanceSetter
            // 
            mirrorDistanceSetter.Location = new Point(166, 61);
            mirrorDistanceSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            mirrorDistanceSetter.Name = "mirrorDistanceSetter";
            mirrorDistanceSetter.Size = new Size(90, 23);
            mirrorDistanceSetter.TabIndex = 11;
            // 
            // motorStepsPerRotationSetter
            // 
            motorStepsPerRotationSetter.Location = new Point(166, 90);
            motorStepsPerRotationSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            motorStepsPerRotationSetter.Name = "motorStepsPerRotationSetter";
            motorStepsPerRotationSetter.Size = new Size(90, 23);
            motorStepsPerRotationSetter.TabIndex = 12;
            // 
            // axisXInvertedSetter
            // 
            axisXInvertedSetter.AutoSize = true;
            axisXInvertedSetter.Location = new Point(166, 119);
            axisXInvertedSetter.Name = "axisXInvertedSetter";
            axisXInvertedSetter.Size = new Size(15, 14);
            axisXInvertedSetter.TabIndex = 13;
            axisXInvertedSetter.UseVisualStyleBackColor = true;
            // 
            // axisYInvertedSetter
            // 
            axisYInvertedSetter.AutoSize = true;
            axisYInvertedSetter.Location = new Point(166, 144);
            axisYInvertedSetter.Name = "axisYInvertedSetter";
            axisYInvertedSetter.Size = new Size(15, 14);
            axisYInvertedSetter.TabIndex = 14;
            axisYInvertedSetter.UseVisualStyleBackColor = true;
            // 
            // maxSpeedSetter
            // 
            maxSpeedSetter.Location = new Point(166, 169);
            maxSpeedSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            maxSpeedSetter.Name = "maxSpeedSetter";
            maxSpeedSetter.Size = new Size(90, 23);
            maxSpeedSetter.TabIndex = 15;
            // 
            // axisXbacklashSetter
            // 
            axisXbacklashSetter.Location = new Point(166, 198);
            axisXbacklashSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            axisXbacklashSetter.Name = "axisXbacklashSetter";
            axisXbacklashSetter.Size = new Size(90, 23);
            axisXbacklashSetter.TabIndex = 16;
            // 
            // axisYbacklashSetter
            // 
            axisYbacklashSetter.Location = new Point(166, 227);
            axisYbacklashSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            axisYbacklashSetter.Name = "axisYbacklashSetter";
            axisYbacklashSetter.Size = new Size(90, 23);
            axisYbacklashSetter.TabIndex = 17;
            // 
            // applyButton
            // 
            applyButton.Location = new Point(166, 256);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(90, 23);
            applyButton.TabIndex = 18;
            applyButton.Text = "Apply";
            applyButton.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(12, 12);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(screenSizeSetter);
            splitContainer1.Panel1.Controls.Add(applyButton);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(axisYbacklashSetter);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(axisXbacklashSetter);
            splitContainer1.Panel1.Controls.Add(label3);
            splitContainer1.Panel1.Controls.Add(maxSpeedSetter);
            splitContainer1.Panel1.Controls.Add(label4);
            splitContainer1.Panel1.Controls.Add(axisYInvertedSetter);
            splitContainer1.Panel1.Controls.Add(label5);
            splitContainer1.Panel1.Controls.Add(axisXInvertedSetter);
            splitContainer1.Panel1.Controls.Add(label6);
            splitContainer1.Panel1.Controls.Add(motorStepsPerRotationSetter);
            splitContainer1.Panel1.Controls.Add(label7);
            splitContainer1.Panel1.Controls.Add(mirrorDistanceSetter);
            splitContainer1.Panel1.Controls.Add(label8);
            splitContainer1.Panel1.Controls.Add(screenDistanceSetter);
            splitContainer1.Panel1.Controls.Add(label9);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(statusStrip1);
            splitContainer1.Panel2.Controls.Add(calibrationBackgroundPanel);
            splitContainer1.Size = new Size(545, 280);
            splitContainer1.SplitterDistance = 263;
            splitContainer1.TabIndex = 19;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { closestPointOffsetLabel, screenPointPositionLabel });
            statusStrip1.Location = new Point(0, 258);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(278, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // closestPointOffsetLabel
            // 
            closestPointOffsetLabel.Name = "closestPointOffsetLabel";
            closestPointOffsetLabel.Size = new Size(0, 17);
            // 
            // calibrationBackgroundPanel
            // 
            calibrationBackgroundPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            calibrationBackgroundPanel.Location = new Point(3, 3);
            calibrationBackgroundPanel.Name = "calibrationBackgroundPanel";
            calibrationBackgroundPanel.Size = new Size(272, 252);
            calibrationBackgroundPanel.TabIndex = 0;
            // 
            // screenPointPositionLabel
            // 
            screenPointPositionLabel.Name = "screenPointPositionLabel";
            screenPointPositionLabel.Size = new Size(0, 17);
            // 
            // DeviceConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(569, 304);
            Controls.Add(splitContainer1);
            Name = "DeviceConfigurationForm";
            Text = "Device Configuration";
            ((System.ComponentModel.ISupportInitialize)screenSizeSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)screenDistanceSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)mirrorDistanceSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)motorStepsPerRotationSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxSpeedSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)axisXbacklashSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)axisYbacklashSetter).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private NumericUpDown screenSizeSetter;
        private NumericUpDown screenDistanceSetter;
        private NumericUpDown mirrorDistanceSetter;
        private NumericUpDown motorStepsPerRotationSetter;
        private CheckBox axisXInvertedSetter;
        private CheckBox axisYInvertedSetter;
        private NumericUpDown maxSpeedSetter;
        private NumericUpDown axisXbacklashSetter;
        private NumericUpDown axisYbacklashSetter;
        private Button applyButton;
        private SplitContainer splitContainer1;
        private Panel calibrationBackgroundPanel;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel closestPointOffsetLabel;
        private ToolStripStatusLabel screenPointPositionLabel;
    }
}