using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PC_GUI_v2.Model.Status
{
    public class TaskManagerStatus
    {
        public Dictionary<string, string> Status;
        public delegate void Notifier();
        public Notifier NewData;
        public TaskManagerStatus()
        {
            Status = new Dictionary<string, string>();

        }
        public void HandleNewData(string data)
        {
            Status = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            NewData?.Invoke();
        }

    }
}
