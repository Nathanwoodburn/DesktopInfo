namespace DesktopInfo
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            labelremote = new Label();
            labelContainer = new Label();
            updatetimer = new System.Windows.Forms.Timer(components);
            labeluptime = new Label();
            labelsystem = new Label();
            labellocal = new Label();
            labelstats = new Label();
            SuspendLayout();
            // 
            // labelremote
            // 
            labelremote.AutoSize = true;
            labelremote.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            labelremote.Location = new Point(12, 9);
            labelremote.Name = "labelremote";
            labelremote.Size = new Size(160, 32);
            labelremote.TabIndex = 0;
            labelremote.Text = "Docker Status";
            // 
            // labelContainer
            // 
            labelContainer.AutoSize = true;
            labelContainer.Location = new Point(12, 55);
            labelContainer.Name = "labelContainer";
            labelContainer.Size = new Size(83, 15);
            labelContainer.TabIndex = 1;
            labelContainer.Text = "Container info";
            // 
            // updatetimer
            // 
            updatetimer.Enabled = true;
            updatetimer.Interval = 10;
            updatetimer.Tick += updatetimer_TickAsync;
            // 
            // labeluptime
            // 
            labeluptime.AutoSize = true;
            labeluptime.Location = new Point(12, 117);
            labeluptime.Name = "labeluptime";
            labeluptime.Size = new Size(46, 15);
            labeluptime.TabIndex = 2;
            labeluptime.Text = "Uptime";
            // 
            // labelsystem
            // 
            labelsystem.AutoSize = true;
            labelsystem.Location = new Point(12, 170);
            labelsystem.Name = "labelsystem";
            labelsystem.Size = new Size(69, 15);
            labelsystem.TabIndex = 3;
            labelsystem.Text = "System Info";
            // 
            // labellocal
            // 
            labellocal.AutoSize = true;
            labellocal.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            labellocal.Location = new Point(12, 249);
            labellocal.Name = "labellocal";
            labellocal.Size = new Size(139, 32);
            labellocal.TabIndex = 4;
            labellocal.Text = "Local Status";
            // 
            // labelstats
            // 
            labelstats.AutoSize = true;
            labelstats.Location = new Point(12, 281);
            labelstats.Name = "labelstats";
            labelstats.Size = new Size(63, 15);
            labelstats.TabIndex = 5;
            labelstats.Text = "Local Stats";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(328, 450);
            Controls.Add(labelstats);
            Controls.Add(labellocal);
            Controls.Add(labelsystem);
            Controls.Add(labeluptime);
            Controls.Add(labelContainer);
            Controls.Add(labelremote);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            RightToLeftLayout = true;
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            MouseClick += Form1_MouseClick;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelremote;
        private Label labelContainer;
        private System.Windows.Forms.Timer updatetimer;
        private Label labeluptime;
        private Label labelsystem;
        private Label labellocal;
        private Label labelstats;
    }
}