
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XInput;
namespace Controller
{
    public class ControllerBase
    {
        public static List<string> GetAvailableControllers()
        {
            List<string> availableControllers = new List<string>();
            availableControllers.Add("Keyboard");
            var controllers = new[] { new SharpDX.XInput.Controller(UserIndex.One), new SharpDX.XInput.Controller(UserIndex.Two), new SharpDX.XInput.Controller(UserIndex.Three), new SharpDX.XInput.Controller(UserIndex.Four) };
            foreach(var ctr in controllers)
            {
                if (ctr.IsConnected)
                    availableControllers.Add("XBox controller " + ctr.UserIndex.ToString());
            }
            return availableControllers;

        }
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
