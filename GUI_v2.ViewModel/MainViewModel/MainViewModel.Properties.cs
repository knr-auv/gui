using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_v2.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        private bool _ShouldMenuExpand = false;
        private bool _ConnectedToJetson;
        private double _battery1Voltage;
        private double _battery2Voltage;
        private double _battery1Percent;
        private double _battery2Percent;
        private double _hummidity = 10;
   
        private Cursor _cursor;

        public double Battery1Percent
        {
            get { return _battery1Percent; }
            set { SetProperty(ref _battery1Percent, value); }
        }
        public double Battery2Percent
        {
            get { return _battery2Percent; }
            set { SetProperty(ref _battery2Percent, value); }
        }
        public double Battery1Voltage
        {
            get { return _battery1Voltage; }
            set { SetProperty(ref _battery1Voltage, value); Battery1Percent = value / modelContainer.userSettings.Battery1MaxVoltage*100; }
        }
        public double Battery2Voltage
        {
            get { return _battery2Voltage; }
            set { SetProperty(ref _battery2Voltage, value); Battery2Percent = value / modelContainer.userSettings.Battery2MaxVoltage*100; }
        }


        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                if (_selectedViewModel != null)
                    _selectedViewModel.Hide();
                SetProperty(ref _selectedViewModel, value);
                SelectedViewModel.Show();
            }
        }

        public TopBarViewModel TopBarViewModel { get; set; }
        public bool ShouldMenuExpand
        {
            get { return _ShouldMenuExpand; }
            set { SetProperty(ref _ShouldMenuExpand, value); }
        }


        public bool ConnectedToJetson
        {
            get { return _ConnectedToJetson; }
            set { SetProperty(ref _ConnectedToJetson, value); }
        }


        public Cursor Cursor
        {
            get { return _cursor; }
            set { SetProperty(ref _cursor, value); }
        }
    }
}
