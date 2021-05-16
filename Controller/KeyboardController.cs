using SharpDX.DirectInput;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controller
{
    public class KeyboardController : ControllerBase
    {
        private Keyboard keyboard;
        private bool LoopActive;
        private Thread x;
        private double[] controlState;
        private int channels = 5;

        public KeyboardController()
        {
            keyboard_disabled = false;
            controlState = new double[channels];
        }

        public override void StartController(Action<int[]> callback)
        {
            DisableKeyboard();
            for (int i = 0; i < channels; i++)
                controlState[i] = 0;

            x = new Thread(() => ControlLoop(callback));
            var directInput = new DirectInput();
            keyboard = new Keyboard(directInput);
            keyboard.Acquire();
            x.Start();

        }
        public override void StopController()
        {
            if (LoopActive)
            {
                LoopActive = false;
                x.Join();
                keyboard.Unacquire();
                
            }
            EnableKeyboard();
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
            Dictionary<string, Key> keys = controlSettings.KeyboardAssignment;
            //roll
            var pressedKeys = keyboard.GetCurrentState().PressedKeys;
            Dictionary<Key, bool> memory = new Dictionary<Key, bool>();
            foreach (var key in controlSettings.KeyboardAssignment.Values)
            {
                if (!memory.ContainsKey(key))
                    memory.Add(key, false);
            }
            foreach(var key in pressedKeys)
            {
                if (memory.ContainsKey(key))
                {
                    memory[key] = true;
                }
                else
                {
                    memory.Add(key, true);
                }
            }


            if (memory[keys["rollLeft"]] && !memory[keys["rollRight"]])
            {
                controlState[0] -= 100;
            }
            else if (memory[keys["rollRight"]] && !memory[keys["rollLeft"]])
            {
                controlState[0] += 100;
            }
            else
            {
                controlState[0] *= 0.3;
            }
            //pitch
            if (memory[keys["pitchForward"]] && !memory[keys["pitchBackward"]])
            {
                controlState[1] += 100;
            }
            else if (memory[keys["pitchBackward"]] && !memory[keys["pitchForward"]])
            {
                controlState[1] -= 100;
            }
            else
            {
                controlState[1] *= 0.3;
            }
            //yaw
            if (memory[keys["yawLeft"]] && !memory[keys["yawRight"]])
            {
                controlState[2] -= 100;
            }
            else if (memory[keys["yawRight"]] && !memory[keys["yawLeft"]])
            {
                controlState[2] += 100;
            }
            else
            {
                controlState[2] *= 0.30;
            }
            //forward, backward
            if (memory[keys["backward"]] && !memory[keys["forward"]])
            {
                controlState[3] -= 100;
            }
            else if (memory[keys["forward"]] && !memory[keys["backward"]])
            {
                controlState[3] += 100;
            }
            else
            {
                controlState[3] *= 0.30;
            }

            //submerge, emerge
            if (memory[keys["submerge"]] && !memory[keys["emerge"]])
            {
                controlState[4] -= 100;
            }
            else if (memory[keys["emerge"]] && !memory[keys["submerge"]])
            {
                controlState[4] += 100;
            }
            else
            {
                controlState[4] *= 0.3;
            }



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
