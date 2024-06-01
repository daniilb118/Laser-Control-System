namespace laserControl
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            connectionButton = new Button();
            serialPortSelector = new ComboBox();
            label1 = new Label();
            splitContainer1 = new SplitContainer();
            programModeSetter = new ComboBox();
            label4 = new Label();
            targetGridView = new DataGridView();
            label3 = new Label();
            intensitySetter = new NumericUpDown();
            speedSetter = new NumericUpDown();
            label2 = new Label();
            statusStrip1 = new StatusStrip();
            laserPositionLabel = new ToolStripStatusLabel();
            screenVisualizationPanel = new Panel();
            cursorLabel = new Label();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            importDeviceProfileToolStripMenuItem = new ToolStripMenuItem();
            exportDeviceProfileToolStripMenuItem = new ToolStripMenuItem();
            importTrajectoryToolStripMenuItem = new ToolStripMenuItem();
            exportTrajectoryToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            chooseBackgroundToolStripMenuItem = new ToolStripMenuItem();
            clearBackgroundToolStripMenuItem = new ToolStripMenuItem();
            configureDeviceToolStripMenuItem = new ToolStripMenuItem();
            actionToolStripMenuItem = new ToolStripMenuItem();
            moveTo00ToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)targetGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)intensitySetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)speedSetter).BeginInit();
            statusStrip1.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // connectionButton
            // 
            connectionButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            connectionButton.Location = new Point(74, 36);
            connectionButton.Name = "connectionButton";
            connectionButton.Size = new Size(117, 23);
            connectionButton.TabIndex = 0;
            connectionButton.Text = "Connect";
            connectionButton.UseVisualStyleBackColor = true;
            // 
            // serialPortSelector
            // 
            serialPortSelector.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            serialPortSelector.FormattingEnabled = true;
            serialPortSelector.Location = new Point(74, 7);
            serialPortSelector.Name = "serialPortSelector";
            serialPortSelector.Size = new Size(117, 23);
            serialPortSelector.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 10);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 2;
            label1.Text = "Port";
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(12, 27);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(programModeSetter);
            splitContainer1.Panel1.Controls.Add(label4);
            splitContainer1.Panel1.Controls.Add(targetGridView);
            splitContainer1.Panel1.Controls.Add(label3);
            splitContainer1.Panel1.Controls.Add(intensitySetter);
            splitContainer1.Panel1.Controls.Add(speedSetter);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(connectionButton);
            splitContainer1.Panel1.Controls.Add(serialPortSelector);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(statusStrip1);
            splitContainer1.Panel2.Controls.Add(screenVisualizationPanel);
            splitContainer1.Size = new Size(551, 363);
            splitContainer1.SplitterDistance = 194;
            splitContainer1.TabIndex = 3;
            // 
            // programModeSetter
            // 
            programModeSetter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            programModeSetter.FormattingEnabled = true;
            programModeSetter.Location = new Point(74, 65);
            programModeSetter.Name = "programModeSetter";
            programModeSetter.Size = new Size(117, 23);
            programModeSetter.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 68);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 8;
            label4.Text = "Mode";
            // 
            // targetGridView
            // 
            targetGridView.AllowUserToAddRows = false;
            targetGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            targetGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            targetGridView.Location = new Point(3, 152);
            targetGridView.Name = "targetGridView";
            targetGridView.Size = new Size(188, 208);
            targetGridView.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 125);
            label3.Name = "label3";
            label3.Size = new Size(65, 15);
            label3.TabIndex = 6;
            label3.Text = "Intensity %";
            // 
            // intensitySetter
            // 
            intensitySetter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            intensitySetter.Location = new Point(74, 123);
            intensitySetter.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            intensitySetter.Name = "intensitySetter";
            intensitySetter.Size = new Size(117, 23);
            intensitySetter.TabIndex = 5;
            // 
            // speedSetter
            // 
            speedSetter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            speedSetter.Location = new Point(74, 94);
            speedSetter.Name = "speedSetter";
            speedSetter.Size = new Size(117, 23);
            speedSetter.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 96);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 3;
            label2.Text = "Speed";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { laserPositionLabel });
            statusStrip1.Location = new Point(0, 341);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(353, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // laserPositionLabel
            // 
            laserPositionLabel.Name = "laserPositionLabel";
            laserPositionLabel.Size = new Size(0, 17);
            // 
            // screenVisualizationPanel
            // 
            screenVisualizationPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            screenVisualizationPanel.Location = new Point(3, 3);
            screenVisualizationPanel.Name = "screenVisualizationPanel";
            screenVisualizationPanel.Size = new Size(347, 335);
            screenVisualizationPanel.TabIndex = 2;
            // 
            // cursorLabel
            // 
            cursorLabel.AutoSize = true;
            cursorLabel.Location = new Point(124, 115);
            cursorLabel.Name = "cursorLabel";
            cursorLabel.Size = new Size(38, 15);
            cursorLabel.TabIndex = 2;
            cursorLabel.Text = "label2";
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, optionsToolStripMenuItem, actionToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(575, 24);
            menuStrip.TabIndex = 4;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { importDeviceProfileToolStripMenuItem, exportDeviceProfileToolStripMenuItem, importTrajectoryToolStripMenuItem, exportTrajectoryToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // importDeviceProfileToolStripMenuItem
            // 
            importDeviceProfileToolStripMenuItem.Name = "importDeviceProfileToolStripMenuItem";
            importDeviceProfileToolStripMenuItem.Size = new Size(185, 22);
            importDeviceProfileToolStripMenuItem.Text = "Import Device Profile";
            // 
            // exportDeviceProfileToolStripMenuItem
            // 
            exportDeviceProfileToolStripMenuItem.Name = "exportDeviceProfileToolStripMenuItem";
            exportDeviceProfileToolStripMenuItem.Size = new Size(185, 22);
            exportDeviceProfileToolStripMenuItem.Text = "Export Device Profile";
            // 
            // importTrajectoryToolStripMenuItem
            // 
            importTrajectoryToolStripMenuItem.Name = "importTrajectoryToolStripMenuItem";
            importTrajectoryToolStripMenuItem.Size = new Size(185, 22);
            importTrajectoryToolStripMenuItem.Text = "Import Trajectory";
            // 
            // exportTrajectoryToolStripMenuItem
            // 
            exportTrajectoryToolStripMenuItem.Name = "exportTrajectoryToolStripMenuItem";
            exportTrajectoryToolStripMenuItem.Size = new Size(185, 22);
            exportTrajectoryToolStripMenuItem.Text = "Export Trajectory";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { chooseBackgroundToolStripMenuItem, clearBackgroundToolStripMenuItem, configureDeviceToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // chooseBackgroundToolStripMenuItem
            // 
            chooseBackgroundToolStripMenuItem.Name = "chooseBackgroundToolStripMenuItem";
            chooseBackgroundToolStripMenuItem.Size = new Size(181, 22);
            chooseBackgroundToolStripMenuItem.Text = "Choose Background";
            // 
            // clearBackgroundToolStripMenuItem
            // 
            clearBackgroundToolStripMenuItem.Name = "clearBackgroundToolStripMenuItem";
            clearBackgroundToolStripMenuItem.Size = new Size(181, 22);
            clearBackgroundToolStripMenuItem.Text = "Clear Background";
            // 
            // configureDeviceToolStripMenuItem
            // 
            configureDeviceToolStripMenuItem.Name = "configureDeviceToolStripMenuItem";
            configureDeviceToolStripMenuItem.Size = new Size(181, 22);
            configureDeviceToolStripMenuItem.Text = "Configure Device";
            // 
            // actionToolStripMenuItem
            // 
            actionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { moveTo00ToolStripMenuItem });
            actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            actionToolStripMenuItem.Size = new Size(54, 20);
            actionToolStripMenuItem.Text = "Action";
            // 
            // moveTo00ToolStripMenuItem
            // 
            moveTo00ToolStripMenuItem.Name = "moveTo00ToolStripMenuItem";
            moveTo00ToolStripMenuItem.Size = new Size(180, 22);
            moveTo00ToolStripMenuItem.Text = "Move to (0; 0)";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(575, 402);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            Text = "Laser Control";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)targetGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)intensitySetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)speedSetter).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button connectionButton;
        private ComboBox serialPortSelector;
        private Label label1;
        private SplitContainer splitContainer1;
        private Panel screenVisualizationPanel;
        private Label cursorLabel;
        private NumericUpDown speedSetter;
        private Label label2;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem importDeviceProfileToolStripMenuItem;
        private ToolStripMenuItem exportDeviceProfileToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem chooseBackgroundToolStripMenuItem;
        private ToolStripMenuItem clearBackgroundToolStripMenuItem;
        private ToolStripMenuItem actionToolStripMenuItem;
        private ToolStripMenuItem moveTo00ToolStripMenuItem;
        private ToolStripMenuItem configureDeviceToolStripMenuItem;
        private Label label3;
        private NumericUpDown intensitySetter;
        private DataGridView targetGridView;
        private ToolStripMenuItem importTrajectoryToolStripMenuItem;
        private ToolStripMenuItem exportTrajectoryToolStripMenuItem;
        private ComboBox programModeSetter;
        private Label label4;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel laserPositionLabel;
    }
}
