
using GUI_v2.ViewModel;
using System.Windows;

namespace GUI_v2.View
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
            this.Topmost = true;
        }
        public string LoadingState
        {
            get { return this.LoadingLabel.Text; }
            set { LoadingLabel.Text += "\n"+value; }
        }
    }
}
