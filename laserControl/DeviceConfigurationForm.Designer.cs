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
            ((System.ComponentModel.ISupportInitialize)screenSizeSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)screenDistanceSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mirrorDistanceSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)motorStepsPerRotationSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxSpeedSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)axisXbacklashSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)axisYbacklashSetter).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 14);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 0;
            label1.Text = "Screen size (m)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 43);
            label2.Name = "label2";
            label2.Size = new Size(111, 15);
            label2.TabIndex = 1;
            label2.Text = "Screen distance (m)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 72);
            label3.Name = "label3";
            label3.Size = new Size(109, 15);
            label3.TabIndex = 2;
            label3.Text = "Mirror distance (m)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 101);
            label4.Name = "label4";
            label4.Size = new Size(139, 15);
            label4.TabIndex = 3;
            label4.Text = "Motor Steps Per Rotation";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 129);
            label5.Name = "label5";
            label5.Size = new Size(85, 15);
            label5.TabIndex = 4;
            label5.Text = "Axis X inverted";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 154);
            label6.Name = "label6";
            label6.Size = new Size(85, 15);
            label6.TabIndex = 5;
            label6.Text = "Axis Y inverted";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 180);
            label7.Name = "label7";
            label7.Size = new Size(112, 15);
            label7.TabIndex = 6;
            label7.Text = "Max speed (steps/s)";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 209);
            label8.Name = "label8";
            label8.Size = new Size(126, 15);
            label8.TabIndex = 7;
            label8.Text = "Axis X backlash (steps)";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 238);
            label9.Name = "label9";
            label9.Size = new Size(126, 15);
            label9.TabIndex = 8;
            label9.Text = "Axis Y backlash (steps)";
            // 
            // screenSizeSetter
            // 
            screenSizeSetter.Location = new Point(178, 12);
            screenSizeSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            screenSizeSetter.Name = "screenSizeSetter";
            screenSizeSetter.Size = new Size(120, 23);
            screenSizeSetter.TabIndex = 9;
            // 
            // screenDistanceSetter
            // 
            screenDistanceSetter.Location = new Point(178, 41);
            screenDistanceSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            screenDistanceSetter.Name = "screenDistanceSetter";
            screenDistanceSetter.Size = new Size(120, 23);
            screenDistanceSetter.TabIndex = 10;
            // 
            // mirrorDistanceSetter
            // 
            mirrorDistanceSetter.Location = new Point(178, 70);
            mirrorDistanceSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            mirrorDistanceSetter.Name = "mirrorDistanceSetter";
            mirrorDistanceSetter.Size = new Size(120, 23);
            mirrorDistanceSetter.TabIndex = 11;
            // 
            // motorsStepsPerRotation
            // 
            motorStepsPerRotationSetter.Location = new Point(178, 99);
            motorStepsPerRotationSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            motorStepsPerRotationSetter.Name = "motorsStepsPerRotation";
            motorStepsPerRotationSetter.Size = new Size(120, 23);
            motorStepsPerRotationSetter.TabIndex = 12;
            // 
            // axisXInvertedSetter
            // 
            axisXInvertedSetter.AutoSize = true;
            axisXInvertedSetter.Location = new Point(178, 128);
            axisXInvertedSetter.Name = "axisXInvertedSetter";
            axisXInvertedSetter.Size = new Size(15, 14);
            axisXInvertedSetter.TabIndex = 13;
            axisXInvertedSetter.UseVisualStyleBackColor = true;
            // 
            // axisYInvertedSetter
            // 
            axisYInvertedSetter.AutoSize = true;
            axisYInvertedSetter.Location = new Point(178, 153);
            axisYInvertedSetter.Name = "axisYInvertedSetter";
            axisYInvertedSetter.Size = new Size(15, 14);
            axisYInvertedSetter.TabIndex = 14;
            axisYInvertedSetter.UseVisualStyleBackColor = true;
            // 
            // maxSpeedSetter
            // 
            maxSpeedSetter.Location = new Point(178, 178);
            maxSpeedSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            maxSpeedSetter.Name = "maxSpeedSetter";
            maxSpeedSetter.Size = new Size(120, 23);
            maxSpeedSetter.TabIndex = 15;
            // 
            // axisXbacklashSetter
            // 
            axisXbacklashSetter.Location = new Point(178, 207);
            axisXbacklashSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            axisXbacklashSetter.Name = "axisXbacklashSetter";
            axisXbacklashSetter.Size = new Size(120, 23);
            axisXbacklashSetter.TabIndex = 16;
            // 
            // axisYbacklashSetter
            // 
            axisYbacklashSetter.Location = new Point(178, 236);
            axisYbacklashSetter.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            axisYbacklashSetter.Name = "axisYbacklashSetter";
            axisYbacklashSetter.Size = new Size(120, 23);
            axisYbacklashSetter.TabIndex = 17;
            // 
            // applyButton
            // 
            applyButton.Location = new Point(178, 265);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(120, 23);
            applyButton.TabIndex = 18;
            applyButton.Text = "Apply";
            applyButton.UseVisualStyleBackColor = true;
            // 
            // DeviceConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(311, 301);
            Controls.Add(applyButton);
            Controls.Add(axisYbacklashSetter);
            Controls.Add(axisXbacklashSetter);
            Controls.Add(maxSpeedSetter);
            Controls.Add(axisYInvertedSetter);
            Controls.Add(axisXInvertedSetter);
            Controls.Add(motorStepsPerRotationSetter);
            Controls.Add(mirrorDistanceSetter);
            Controls.Add(screenDistanceSetter);
            Controls.Add(screenSizeSetter);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "DeviceConfigurationForm";
            Text = "Device Configuration";
            ((System.ComponentModel.ISupportInitialize)screenSizeSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)screenDistanceSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)mirrorDistanceSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)motorStepsPerRotationSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxSpeedSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)axisXbacklashSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)axisYbacklashSetter).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
    }
}