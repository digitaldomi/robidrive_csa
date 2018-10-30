namespace RobotView
{
    partial class ConsoleView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.switchView4 = new RobotView.SwitchView();
            this.switchView3 = new RobotView.SwitchView();
            this.switchView2 = new RobotView.SwitchView();
            this.ledView4 = new RobotView.LedView();
            this.ledView3 = new RobotView.LedView();
            this.ledView2 = new RobotView.LedView();
            this.ledView1 = new RobotView.LedView();
            this.switchView1 = new RobotView.SwitchView();
            this.SuspendLayout();
            // 
            // switchView4
            // 
            this.switchView4.Location = new System.Drawing.Point(522, 59);
            this.switchView4.Name = "switchView4";
            this.switchView4.Size = new System.Drawing.Size(40, 80);
            this.switchView4.State = false;
            this.switchView4.Switch = null;
            this.switchView4.TabIndex = 8;
            this.switchView4.Click += new System.EventHandler(this.switchView4_Click);
            // 
            // switchView3
            // 
            this.switchView3.Location = new System.Drawing.Point(443, 59);
            this.switchView3.Name = "switchView3";
            this.switchView3.Size = new System.Drawing.Size(45, 80);
            this.switchView3.State = false;
            this.switchView3.Switch = null;
            this.switchView3.TabIndex = 7;
            this.switchView3.Click += new System.EventHandler(this.switchView3_Click);
            // 
            // switchView2
            // 
            this.switchView2.Location = new System.Drawing.Point(374, 59);
            this.switchView2.Name = "switchView2";
            this.switchView2.Size = new System.Drawing.Size(40, 80);
            this.switchView2.State = false;
            this.switchView2.Switch = null;
            this.switchView2.TabIndex = 6;
            this.switchView2.Click += new System.EventHandler(this.switchView2_Click);
            // 
            // ledView4
            // 
            this.ledView4.Led = null;
            this.ledView4.Location = new System.Drawing.Point(206, 59);
            this.ledView4.Name = "ledView4";
            this.ledView4.Size = new System.Drawing.Size(60, 60);
            this.ledView4.TabIndex = 4;
            this.ledView4.Click += new System.EventHandler(this.ledView4_Click);
            // 
            // ledView3
            // 
            this.ledView3.Led = null;
            this.ledView3.Location = new System.Drawing.Point(144, 59);
            this.ledView3.Name = "ledView3";
            this.ledView3.Size = new System.Drawing.Size(60, 60);
            this.ledView3.TabIndex = 3;
            // 
            // ledView2
            // 
            this.ledView2.Led = null;
            this.ledView2.Location = new System.Drawing.Point(82, 59);
            this.ledView2.Name = "ledView2";
            this.ledView2.Size = new System.Drawing.Size(60, 60);
            this.ledView2.TabIndex = 2;
            // 
            // ledView1
            // 
            this.ledView1.Led = null;
            this.ledView1.Location = new System.Drawing.Point(26, 59);
            this.ledView1.Name = "ledView1";
            this.ledView1.Size = new System.Drawing.Size(60, 60);
            this.ledView1.TabIndex = 1;
            // 
            // switchView1
            // 
            this.switchView1.Location = new System.Drawing.Point(304, 59);
            this.switchView1.Name = "switchView1";
            this.switchView1.Size = new System.Drawing.Size(40, 80);
            this.switchView1.State = false;
            this.switchView1.Switch = null;
            this.switchView1.TabIndex = 5;
            this.switchView1.Click += new System.EventHandler(this.switchView1_Click);
            // 
            // ConsoleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.switchView4);
            this.Controls.Add(this.switchView3);
            this.Controls.Add(this.switchView2);
            this.Controls.Add(this.ledView4);
            this.Controls.Add(this.ledView3);
            this.Controls.Add(this.ledView2);
            this.Controls.Add(this.ledView1);
            this.Controls.Add(this.switchView1);
            this.Name = "ConsoleView";
            this.Size = new System.Drawing.Size(656, 187);
            this.Click += new System.EventHandler(this.ConsoleView_Click);
            this.ResumeLayout(false);

        }

        #endregion
        private LedView ledView1;
        private LedView ledView2;
        private LedView ledView3;
        private LedView ledView4;

        private SwitchView switchView1;
        private SwitchView switchView2;
        private SwitchView switchView3;
        private SwitchView switchView4;
    }
}
