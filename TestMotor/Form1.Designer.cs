using RobotView;
namespace TestMotor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        //private System.Windows.Forms.MainMenu mainMenu1;

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
            this.driveCtrlView = new RobotView.DriveCtrlView();
            this.motorCtrlViewLeft = new RobotView.MotorCtrlView();
            this.motorCtrlViewRight = new RobotView.MotorCtrlView();
            this.SuspendLayout();
            // 
            // driveCtrlView
            // 
            
            this.driveCtrlView.DriveCtrl = null;
            
            this.driveCtrlView.Location = new System.Drawing.Point(0, 0);
            this.driveCtrlView.Name = "driveCtrlView";
            
            this.driveCtrlView.Size = new System.Drawing.Size(480, 75);
            this.driveCtrlView.TabIndex = 0;
            
            // 
            // motorCtrlViewLeft
            // 
            
            this.motorCtrlViewLeft.Location = new System.Drawing.Point(477, 81);
            this.motorCtrlViewLeft.MotorCtrl = null;
            this.motorCtrlViewLeft.Name = "motorCtrlViewLeft";
            
            this.motorCtrlViewLeft.Size = new System.Drawing.Size(494, 330);
            this.motorCtrlViewLeft.TabIndex = 1;
            
            // 
            // motorCtrlViewRight
            // 
           
            this.motorCtrlViewRight.Location = new System.Drawing.Point(3, 81);
            this.motorCtrlViewRight.MotorCtrl = null;
            this.motorCtrlViewRight.Name = "motorCtrlViewRight";
            
            this.motorCtrlViewRight.Size = new System.Drawing.Size(480, 330);
            this.motorCtrlViewRight.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(979, 433);
            this.Controls.Add(this.driveCtrlView);
            this.Controls.Add(this.motorCtrlViewLeft);
            this.Controls.Add(this.motorCtrlViewRight);
            
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private DriveCtrlView driveCtrlView;
        private MotorCtrlView motorCtrlViewLeft;
        private MotorCtrlView motorCtrlViewRight;
    }
}

