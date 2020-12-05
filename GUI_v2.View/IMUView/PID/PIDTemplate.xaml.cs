using GUI_v2.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;



namespace GUI_v2.View.IMUView.PID
{
    /// <summary>
    /// Interaction logic for PIDTemplate.xaml
    /// </summary>
    public partial class PIDTemplate : UserControl
    {
        public PIDTemplate()
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
                typeof(string), typeof(PIDTemplate));



        public PIDClass PIDBank
        {
            get { return  (PIDClass)GetValue(PIDBankProperty); }
            set { SetValue(PIDBankProperty, value); }
        }

             public static readonly DependencyProperty PIDBankProperty =
            DependencyProperty.Register("PIDBank",
                typeof(PIDClass), typeof(PIDTemplate));



    }
}
