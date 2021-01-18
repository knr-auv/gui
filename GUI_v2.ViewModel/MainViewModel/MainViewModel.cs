
using GUI_v2.ViewModel;
using GUI_v2.ViewModel.Commands;
using GUI_v2.ViewModel.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace GUI_v2.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    { 
        public ModelContainer modelContainer;
        private IMUViewModel _IMUViewModel;
        private StatusViewModel _StatusViewModel;
        private ControlViewModel _ControlViewModel;
        private AutonomyViewModel _AutonomyViewModel;
        private BaseViewModel _selectedViewModel;

        public MainViewModel() {
            ModelContainer modelContainer = new ModelContainer();
            _ControlViewModel = new ControlViewModel(modelContainer);
            _IMUViewModel = new IMUViewModel(modelContainer);
            SelectedViewModel = _StatusViewModel;
        }
        public MainViewModel(ModelContainer modelContainer)
        {

            this.modelContainer = modelContainer;
            TopBarViewModel = new TopBarViewModel(modelContainer);
            _IMUViewModel = new IMUViewModel(modelContainer);
            _StatusViewModel = new StatusViewModel(modelContainer);
            _ControlViewModel = new ControlViewModel(modelContainer);
            _AutonomyViewModel = new AutonomyViewModel(modelContainer);
            SelectedViewModel = _ControlViewModel;

            ConnectedToJetson = modelContainer.modelStatus.networkStatus.ConnectedToJetson;
            modelContainer.modelStatus.networkStatus.ConnectedToJetsonCallback += (val) => { ConnectedToJetson = val; };

            modelContainer.dataContainer.Battery.newDataCallback += (float v1, float v2) => { Battery1Voltage = v1; Battery2Voltage = v2; };
            ChangeViewCommand = new RelayCommand(ChangeViewAction, AlwaysAllow);
            ExpandMenuCommand = new RelayCommand(ExpandMenuAction, AlwaysAllow);
 
            ConnectToJetsonCommand = new RelayCommand(ConnectToJetsonAction, AlwaysAllow);
            DisconnectFromJetsonCommand = new RelayCommand(DisconnectFromJetsonAction, AlwaysAllow);
            OpenSettingsCommand = new RelayCommand(OpenSettingsAction, AlwaysAllow);
            //SettingsDialogHideCommand = new RelayCommand(SettingsDialogHideAction, AlwaysAllow);
            Battery1Voltage = 12.9;
            Battery2Voltage = 12.2;


        }
        
    }
}

