using RobotCtrl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Testat
{
    public partial class Form1 : Form
    {
        #region members
        private Robot robot;
        //private Drive drive;
        RobotConsole rc;
        private bool ledblink;
        private bool currentLedState;
        enum State { WAIT_FOR_START, WAIT_FOR_START_INIT, DRIVE_FORWARD, DRIVE_FORWARD_INIT, TURN, TURN_INIT, DRIVE_BACK, DRIVE_BACK_INIT, STOP, STOP_INIT};
        //private Drive Drive { get; set; }
        #endregion

        #region constructor & destructor
        public Form1()
        {
            InitializeComponent();

            rc = new RobotConsole();
            consoleView1.RobotConsole = rc;
            /*for (int j = 0; j <= 3; j++)
            {
                (rc[Switches.Switch1]).SwitchStateChanged += (o, e) => (rc[Leds.Led1]).LedEnabled = (rc[Switches.Switch1]).SwitchEnabled;
                (rc[Switches.Switch2]).SwitchStateChanged += (o, e) => (rc[Leds.Led2]).LedEnabled = (rc[Switches.Switch2]).SwitchEnabled;
                (rc[Switches.Switch3]).SwitchStateChanged += (o, e) => (rc[Leds.Led3]).LedEnabled = (rc[Switches.Switch3]).SwitchEnabled;
                (rc[Switches.Switch4]).SwitchStateChanged += (o, e) => (rc[Leds.Led4]).LedEnabled = (rc[Switches.Switch4]).SwitchEnabled;
            }*/

            //this.drive = new Drive();
            robot = new Robot();        // neuen Roboter erstellen
            robot.Drive.Power = true;   // Stromversorgung der Motoren (im DriveCtrl) einschalten

            runlineView.Drive = robot.Drive;
            runturnView.Drive = robot.Drive;
            runarcView.Drive = robot.Drive;
            driveView1.Drive = robot.Drive; // DriveView benötigt Drive-Objekt zur Visualisierung


            Init();
            radarView.Radar = robot.Radar;
            radarView.TooClose += RadarView_TooClose;
            consoleView1.RobotConsole = robot.RobotConsole;

            commonRunParameters1.AccelerationChanged += AccelerationChanged;
            commonRunParameters1.SpeedChanged += SpeedChanged;

            SpeedChanged(null, EventArgs.Empty); // Default Wert setzen
            AccelerationChanged(null, EventArgs.Empty); // Default Wert setzen
            this.buttonHalt.Click += ButtonHalt_Click;
            this.buttonStop.Click += ButtonStop_Click;

            Thread blinkLed = new Thread(new ThreadStart(blink_led));
            Thread drive = new Thread(new ThreadStart(drive_robot));

            blinkLed.Start();
            drive.Start();

            this.ledblink = false;
        }

        private void RadarView_TooClose(object sender, EventArgs e)
        {
            robot.Drive.Stop();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            robot.Drive.Stop();
        }

        private void ButtonHalt_Click(object sender, EventArgs e)
        {
            robot.Drive.Halt();
        }
        #endregion

        private void Init()
        {
            runlineView.Speed = commonRunParameters1.Speed;
            runlineView.Acceleration = commonRunParameters1.Acceleration;
            runturnView.Speed = commonRunParameters1.Speed;
            runturnView.Acceleration = commonRunParameters1.Acceleration;
            runarcView.Speed = commonRunParameters1.Speed;
            runarcView.Acceleration = commonRunParameters1.Acceleration;
        }

        #region methods
        private void SpeedChanged(object sender, EventArgs e)
        {
            runlineView.Speed = commonRunParameters1.Speed;
            runturnView.Speed = commonRunParameters1.Speed;
            runarcView.Speed = commonRunParameters1.Speed;


        }
        private void AccelerationChanged(object sender, EventArgs e)
        {
            runlineView.Acceleration = commonRunParameters1.Acceleration;
            runturnView.Acceleration = commonRunParameters1.Acceleration;
            runarcView.Acceleration = commonRunParameters1.Acceleration;
        }



        #endregion

        private void label1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void blink_led()
        {
            while (true)
            {
                Thread.Sleep(500);
                if (ledblink)
                {
                    currentLedState = !currentLedState;
                    (rc[Leds.Led1]).LedEnabled = currentLedState;
                    (rc[Leds.Led2]).LedEnabled = currentLedState;
                    (rc[Leds.Led3]).LedEnabled = currentLedState;
                    (rc[Leds.Led4]).LedEnabled = currentLedState;
                }
                else
                {
                    (rc[Leds.Led1]).LedEnabled = true;
                    (rc[Leds.Led2]).LedEnabled = true;
                    (rc[Leds.Led3]).LedEnabled = true;
                    (rc[Leds.Led4]).LedEnabled = true;
                }
            }
        }

        private void drive_robot()
        {
            while (true)
            {
                //State state = new State();
                //state = State.WAIT_FOR_START_INIT;

                ledblink = false;
                robot.Drive.Position = new PositionInfo(0, 0, 0);

                while ((!rc[Switches.Switch2].SwitchEnabled)) { Thread.Sleep(5); }
                ledblink = true;
                this.robot.Drive.RunLine((float)2.5, (float)0.2, (float)0.2);
                Thread.Sleep(1000);

                while (!this.robot.Drive.Done) { Thread.Sleep(5); }
                Thread.Sleep(1000);
                this.robot.Drive.RunTurn(180, 0.2f, 0.2f);
                Thread.Sleep(3000);

                while (!this.robot.Drive.Done) { Thread.Sleep(5); }

                this.robot.Drive.RunLine((float)2.5, (float)0.2, (float)0.2);
                Thread.Sleep(1000);
                ledblink = false;

                while ((rc[Switches.Switch2].SwitchEnabled)) { Thread.Sleep(5); }
               

            }



            //                state = State.WAIT_FOR_START;
            //            break;

            //Thread.Sleep(500);
            //while (true)
            //{
            //    Thread.Sleep(20);
            //    switch (state)
            //    {
            //        case State.WAIT_FOR_START_INIT:
            //            ledblink = false;
            //            //this.labelCount.Text = "0";
            //            robot.Drive.Position = new PositionInfo(0, 0, 0);
            //            if (!rc[Switches.Switch2].SwitchEnabled)
            //                state = State.WAIT_FOR_START;
            //            break;
            //        case State.WAIT_FOR_START:
            //            if (rc[Switches.Switch2].SwitchEnabled)
            //            {
            //                state = State.DRIVE_FORWARD_INIT;
            //            }
            //            break;
            //        case State.DRIVE_FORWARD_INIT:
            //            ledblink = true;
            //            this.robot.Drive.RunLine((float)2.5, (float)0.2, (float)0.2);
            //            Thread.Sleep(2000);
            //            state = State.DRIVE_FORWARD;
            //            break;
            //        case State.DRIVE_FORWARD:
            //            if (this.robot.Drive.Done)
            //            {
            //                state = State.TURN_INIT;
            //                Thread.Sleep(2000);
            //            }
            //            break;
            //        case State.TURN_INIT:
            //            this.robot.Drive.RunTurn(180,0.2f,0.2f);
            //            Thread.Sleep(2000);
            //            state = State.TURN;
            //            break;
            //        case State.TURN:
            //            if (this.robot.Drive.Done)
            //            {
            //                state = State.DRIVE_BACK_INIT;
            //                Thread.Sleep(2000);
            //            }
            //            break;
            //        case State.DRIVE_BACK_INIT:
            //            this.robot.Drive.RunLine((float)2.5, (float)0.2, (float)0.2);
            //            Thread.Sleep(2000);
            //            state = State.DRIVE_BACK;
            //            break;
            //        case State.DRIVE_BACK:
            //            if (this.robot.Drive.Done)
            //            {
            //                state = State.STOP_INIT;
            //                Thread.Sleep(2000);
            //            }
            //            break;

            //        case State.STOP_INIT:
            //            ledblink = false;
            //            state = State.STOP;
            //            break;
            //        case State.STOP:
            //            state = State.WAIT_FOR_START_INIT;
            //            break;
            //    }
            //}
        }
    }
}
