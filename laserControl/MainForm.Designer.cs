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
            SuspendLayout();
            // 
            // connectionButton
            // 
            connectionButton.Location = new Point(47, 35);
            connectionButton.Name = "connectionButton";
            connectionButton.Size = new Size(121, 23);
            connectionButton.TabIndex = 0;
            connectionButton.Text = "Connect";
            connectionButton.UseVisualStyleBackColor = true;
            // 
            // serialPortSelector
            // 
            serialPortSelector.FormattingEnabled = true;
            serialPortSelector.Location = new Point(47, 6);
            serialPortSelector.Name = "serialPortSelector";
            serialPortSelector.Size = new Size(121, 23);
            serialPortSelector.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 2;
            label1.Text = "Port";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(serialPortSelector);
            Controls.Add(connectionButton);
            Name = "MainForm";
            Text = "Laser Control";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button connectionButton;
        private ComboBox serialPortSelector;
        private Label label1;
    }
}
