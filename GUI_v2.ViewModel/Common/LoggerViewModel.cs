using GUI_v2.Services;
using PC_GUI_v2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_v2.ViewModel.Common
{
    public class LoggerViewModel:BaseViewModel
    {
        private Logger LoggerModel;

        public String LoggerText 
        {
            get { return LoggerModel.Diary; }
            set {;}
        }

        public ICommand ClearCommand { get; private set; }

        private bool Allow(object p)
        {
            return true;
        }

        private void ClearAction(object parameter)
        {
            LoggerModel.ClearDiary();
        }

        public LoggerViewModel(Logger logger)
        {
            logger.newMsgCb += () => RaisePropertyChanged("LoggerText");
            LoggerModel = logger;
            ClearCommand = new RelayCommand(ClearAction, Allow);
        }
    }
}
