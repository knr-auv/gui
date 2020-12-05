using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GUI_v2.Model.UserSettings;
using GUI_v2.Services;

namespace Services.DialogServices.SettingsDialog
{
    public class ControlSettingsViewModel:DialogBaseViewModel
    {
     
        public void AssignKeyAction( string key, Key value)
        {
            controlSettings.Assignment[key] = value;
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
