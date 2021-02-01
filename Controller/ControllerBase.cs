
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ControllerBase
    {
        public bool keyboard_disabled = false;


        protected ControlSettings controlSettings;

        protected void DisableKeyboard()
        {
            keyboard_disabled = true;
        }
        protected void EnableKeyboard()
        {
            keyboard_disabled = false;
        }

        public virtual void StartController(Action<int[]> callback)
        {
            throw new NotImplementedException();
        }

        public virtual void StopController()
        {
            throw new NotImplementedException();
        }
        public void SetUserSettings(ControlSettings controlSettings)
        {
            this.controlSettings = controlSettings;
        }
    }
}
