using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_GUI_v2.Model
{
    public class Logger
    {
        public Logger()
        {
        }
        public delegate void NewMsg();
        public NewMsg newMsgCb;
        private string _diary;
        public string Diary
        {
            get { return _diary; }
            set { if (_diary != null) _diary += "\n" + value; else _diary = value; }
        }
        public void ClearDiary()
        {
            _diary = null;
            newMsgCb?.Invoke();
        }
        public void HandleNewMsg(string msg)
        {
            Diary = msg;
            newMsgCb?.Invoke();
        }
    }
}
