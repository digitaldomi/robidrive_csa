using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RobotView;
using RobotCtrl;

namespace TestMotor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            motorCtrlViewLeft.MotorCtrl = new MotorCtrl(Constants.IOMotorCtrlLeft);
            motorCtrlViewRight.MotorCtrl = new MotorCtrl(Constants.IOMotorCtrlRight);
            driveCtrlView.DriveCtrl = new DriveCtrl(Constants.IODriveCtrl);
        }
    }
}
