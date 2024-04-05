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
            screenVisualizationPanel = new Panel();
            cursorLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            screenVisualizationPanel.SuspendLayout();
            SuspendLayout();
            // 
            // connectionButton
            // 
            connectionButton.Location = new Point(38, 36);
            connectionButton.Name = "connectionButton";
            connectionButton.Size = new Size(121, 23);
            connectionButton.TabIndex = 0;
            connectionButton.Text = "Connect";
            connectionButton.UseVisualStyleBackColor = true;
            // 
            // serialPortSelector
            // 
            serialPortSelector.FormattingEnabled = true;
            serialPortSelector.Location = new Point(38, 7);
            serialPortSelector.Name = "serialPortSelector";
            serialPortSelector.Size = new Size(121, 23);
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
            splitContainer1.Location = new Point(12, 12);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(connectionButton);
            splitContainer1.Panel1.Controls.Add(serialPortSelector);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(screenVisualizationPanel);
            splitContainer1.Size = new Size(546, 302);
            splitContainer1.SplitterDistance = 193;
            splitContainer1.TabIndex = 3;
            // 
            // screenVisualizationPanel
            // 
            screenVisualizationPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            screenVisualizationPanel.Controls.Add(cursorLabel);
            screenVisualizationPanel.Location = new Point(3, 3);
            screenVisualizationPanel.Name = "screenVisualizationPanel";
            screenVisualizationPanel.Size = new Size(343, 296);
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(570, 326);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            Text = "Laser Control";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            screenVisualizationPanel.ResumeLayout(false);
            screenVisualizationPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button connectionButton;
        private ComboBox serialPortSelector;
        private Label label1;
        private SplitContainer splitContainer1;
        private Panel screenVisualizationPanel;
        private Label cursorLabel;
    }
}
