using Controller;
using GUI_v2.Model;
using GUI_v2.Model.DataContainer;
using GUI_v2.Model.JetsonClient;
using GUI_v2.Model.Status;
using GUI_v2.Model.UserSettings;
using PC_GUI_v2.Model;

namespace GUI_v2.ViewModel
{
    public class ModelContainer
    {
        
        public CameraStreamClient cameraStreamClient;
        public JetsonClient jetsonClient;
        public UserSettings userSettings;
        public ModelStatus modelStatus;
        public DataContainer dataContainer;
        public KeyboardController keyboardController;
        public XBoxPadController xBoxPadController;
        public Logger logger;

        public ModelContainer()
        {
            logger = new Logger();
            dataContainer = new DataContainer();
            modelStatus = new ModelStatus();
            cameraStreamClient = new CameraStreamClient();
            jetsonClient = new JetsonClient();
            keyboardController = new KeyboardController();
            xBoxPadController = new XBoxPadController();
        }
        public void SetUserSettings()
        {
            keyboardController.SetUserSettings(userSettings.ControlSettings);
            xBoxPadController.SetUserSettings(userSettings.ControlSettings);
        }
    }
}
