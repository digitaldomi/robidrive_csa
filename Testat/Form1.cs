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
        int objectcount;
        bool is_running = false;
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
            //radarView.TooClose += RadarView_TooClose;
            consoleView1.RobotConsole = robot.RobotConsole;

            commonRunParameters1.AccelerationChanged += AccelerationChanged;
            commonRunParameters1.SpeedChanged += SpeedChanged;

            SpeedChanged(null, EventArgs.Empty); // Default Wert setzen
            AccelerationChanged(null, EventArgs.Empty); // Default Wert setzen
            this.buttonHalt.Click += ButtonHalt_Click;
            this.buttonStop.Click += ButtonStop_Click;

            Thread blinkLed = new Thread(new ThreadStart(blink_led));
            Thread drive = new Thread(new ThreadStart(drive_robot));
            Thread object_det = new Thread(new ThreadStart(object_detect));

            blinkLed.Start();
            drive.Start();
            object_det.Start();

            this.ledblink = false;


        }

       /*private void RadarView_TooClose(object sender, EventArgs e)
        {
            robot.Drive.Stop();
        }*/

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

        private void object_detect()
        {

            float distance = 0;
            int counter= 0;
            bool object_detected = false;
            while (true)
            {
                Thread.Sleep(50);
                distance = this.robot.Radar.Distance;
                if (distance < 700)
                {
                    counter++;
                    if(counter == 5 && object_detected == false)
                    {
                        objectcount++;
                        object_detected = true;
                    }
                }
                else  {
                    object_detected = false;
                    counter = 0;
                }  
            }
        }

        private void drive_robot()
        {
            while (true)
            {

                ledblink = false;
                objectcount = 0;
                robot.Drive.Position = new PositionInfo(0, 0, 0);

                while ((!rc[Switches.Switch2].SwitchEnabled)) { Thread.Sleep(5); }
                objectcount = 0;
                is_running = true;
                ledblink = true;
                this.robot.Drive.RunLine((float)2.5, (float)1, (float)1);

                while (!this.robot.Drive.Done) { Thread.Sleep(5); }
                Thread.Sleep(1000);
                this.robot.Drive.RunTurn(180, 0.2f, 0.2f);
                Thread.Sleep(3000);

                while (!this.robot.Drive.Done) { Thread.Sleep(5); }

                this.robot.Drive.RunLine((float)2.5, (float)1, (float)1);
                Thread.Sleep(1000);

                while (!this.robot.Drive.Done) { Thread.Sleep(5); }
                 
                ledblink = false;
                is_running = false;
                while ((rc[Switches.Switch2].SwitchEnabled)) { Thread.Sleep(5); }
               

            }



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(is_running) labelCount.Text = objectcount.ToString();
        }
    }
}
