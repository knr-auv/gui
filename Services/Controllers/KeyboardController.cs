using GUI_v2.Model.UserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_v2.ViewModel.Tools.Controllers
{

    public class KeyboardController
    {
        public Dictionary<Key, bool> memory;
        public bool locked;
        private bool LoopActive;
        private Thread x;
        private UserSettings settings;
        private double[] controlState;
        private int channels =5;
        private void LockKeyboard()
        {
            locked = true;
        }
        private void UnlockKeyboard()
        {
            locked = false;
        }
        
        public void SetUserSettings(UserSettings settings)
        {
            this.settings = settings;
        }
        public KeyboardController()
        {

            locked = false;
            memory = new Dictionary<Key, bool>();
            controlState = new double[channels];
        }
        public void StopController()
        {
            UnlockKeyboard();
            if (x != null&& x.IsAlive)
            {
                LoopActive = false;
                x.Join(100);
                x.Abort();
            }
        }
        public void StartController(Action<int[]> callback)
        {
            for (int i = 0; i < channels; i++)
                controlState[i] = 0;
            foreach(var key in settings.ControlSettings.Assignment.Values)
            {
                if (!memory.ContainsKey(key))
                    memory.Add(key, false);
            }
            LockKeyboard();
            x = new Thread(() => ControlLoop(callback));
            x.Start();

        }

        private void ControlLoop(Action<int[]> callback)
        {
            LoopActive = true;
            DateTime last = DateTime.Now;
            int freq = settings.ControllerInterval;
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
            Dictionary<string, Key> keys= settings.ControlSettings.Assignment;
            //roll
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
        public void HandleKeyDown (KeyEventArgs e)
        {
            if (memory.ContainsKey(e.Key))
            {
                memory[e.Key] = true;
            }
            else
            {
                memory.Add(e.Key, true);
            }
            
            if (locked)
                e.Handled = true;
        }
        public void HandleKeyUp(KeyEventArgs e)
        {
            if (memory.ContainsKey(e.Key))
            {
                memory[e.Key] = false;
            }
            else
            {
                memory.Add(e.Key, false);
            }
            if(locked)
                e.Handled = true;

        }
    }
}
