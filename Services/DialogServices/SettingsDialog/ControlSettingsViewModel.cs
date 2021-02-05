using Controller;
using System;
using System.Collections.Generic;
using SharpDX.DirectInput;
namespace Services.DialogServices.SettingsDialog
{
    public class ControlSettingsViewModel:DialogBaseViewModel
    {
     
        public void AssignKeyAction( string key, System.Windows.Input.Key value)
        {
            Key  keys;
            Enum.TryParse(value.ToString(), out keys);
            controlSettings.Assignment[key] = keys;
        }
        private bool Allow(object param) => true;

        public void AssignKeyAction(object param)
        {
            Console.WriteLine("done");
        }
        private ControlSettings controlSettings;
        public Dictionary<string, Key> KeyboardAssignment
        {
            get { return controlSettings.Assignment; }
            set { controlSettings.Assignment = value; }
        }
        public ControlSettingsViewModel(ControlSettings controlSettings)
        {
            this.controlSettings = controlSettings;

        }
    }
}
