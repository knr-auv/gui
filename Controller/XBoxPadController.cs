using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpDX.XInput;
using SharpDX;
using System.Drawing;

namespace Controller
{
    public class XBoxPadController : ControllerBase
    {
        private SharpDX.XInput.Controller controller;
        private SharpDX.XInput.Gamepad gamepad;
        private bool LoopActive;
        private Thread x;
        private double[] controlState;
        private int channels = 5;

        public int deadband = 2500;
        public Point leftThumb, rightThumb = new System.Drawing.Point(0, 0);
        public float leftTrigger, rightTrigger;
        public XBoxPadController()
        {
            controlState = new double[channels];
        }
        public override void StartController(Action<int[]> callback)
        {
            controller = new SharpDX.XInput.Controller(UserIndex.One);
            for (int i = 0; i < channels; i++)
                controlState[i] = 0;

            x = new Thread(() => ControlLoop(callback));
            x.Start();
        }
        public override void StopController()
        {
            if (LoopActive)
            {
                LoopActive = false;
                x.Join();

            }
        }
        private void ControlLoop(Action<int[]> callback)
        {
            LoopActive = true;
            DateTime last = DateTime.Now;
            int freq = controlSettings.ControllerInterval;
            while (LoopActive)
            {
                if ((DateTime.Now - last).Milliseconds >= freq)
                {
                    callback(calculateOutput());
                    last = DateTime.Now;
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        private int[] calculateOutput()
        {
            gamepad = controller.GetState().Gamepad;

            leftThumb.X = (Math.Abs((float)gamepad.LeftThumbX) < deadband) ? 0 : gamepad.LeftThumbX / short.MinValue * -1000;
            leftThumb.Y = (Math.Abs((float)gamepad.LeftThumbY) < deadband) ? 0 : gamepad.LeftThumbY / short.MaxValue * 1000;
            rightThumb.Y = (Math.Abs((float)gamepad.RightThumbX) < deadband) ? 0 : gamepad.RightThumbX / short.MaxValue * 1000;
            rightThumb.X = (Math.Abs((float)gamepad.RightThumbY) < deadband) ? 0 : gamepad.RightThumbY / short.MaxValue * 1000;
            //values are between -1000 and 1000
            leftTrigger = gamepad.LeftTrigger/255*1000;
            rightTrigger = gamepad.RightTrigger/255*1000;
            controlState[0] = rightThumb.X; //roll
            controlState[1] = rightThumb.Y; //pitch
            controlState[2] = leftThumb.X; //yaw
            controlState[3] = leftThumb.Y; //throttle
            controlState[4] = leftTrigger - rightTrigger; //emerge

            int[] ret = new int[channels];
            for (int i = 0; i < channels; i++)
            {
                ret[i] = (int)Math.Round(controlState[i]);
                if (Math.Abs(ret[i]) > 1000) ret[i] = Math.Abs(ret[i]) / ret[i] * 1000;
            }

            return ret;
        }

    }
}