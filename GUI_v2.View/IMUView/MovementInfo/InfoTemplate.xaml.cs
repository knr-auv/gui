
using System.Windows;
using System.Windows.Controls;
using GUI_v2.Tools;


namespace GUI_v2.View.IMUView.MovementInfo
{
    /// <summary>
    /// Interaction logic for InfoTemplate.xaml
    /// </summary>
    public partial class InfoTemplate : UserControl
    {
        public InfoTemplate()
        {
            InitializeComponent();
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public static readonly DependencyProperty HeaderProperty =
      DependencyProperty.Register("Header",
          typeof(string), typeof(InfoTemplate));

        public MovementInfoClass InfoBank
        {
            get { return (MovementInfoClass)GetValue(InfoBankProperty); }
            set { SetValue(InfoBankProperty, value); }
        }

        public static readonly DependencyProperty InfoBankProperty =
       DependencyProperty.Register("InfoBank",
           typeof(MovementInfoClass), typeof(InfoTemplate));
    }
}
