using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_v2.View.IMUView.Plots
{
    /// <summary>
    /// Interaction logic for Plots.xaml
    /// </summary>
    public partial class Plots : UserControl
    {

        private RowDefinitionCollection LayoutRows;
        private RowDefinitionCollection BottomGridRows;
        private ColumnDefinitionCollection UpperBottomGridCol;
        private ColumnDefinitionCollection LowerBottomGridCol;


        public Plots()
        {
            InitializeComponent();
        }

    
        private bool CheckLayout()
        {
            if (LayoutRows == null)
            {
                if (PlotsLayout != null)
                    LayoutRows = PlotsLayout.RowDefinitions;
                else
                    return false;
            }

            if (BottomGridRows == null)
            {
                if (BottomGrid != null)
                    BottomGridRows = BottomGrid.RowDefinitions;
                else
                    return false;
            }
            if (UpperBottomGridCol == null)
            {
                if (UpperBottomGrid != null)
                    UpperBottomGridCol = UpperBottomGrid.ColumnDefinitions;
                else
                    return false;
            }
            if (LowerBottomGridCol == null)
            {
                if (BottomGrid != null)
                    LowerBottomGridCol = LowerBottomGrid.ColumnDefinitions;
                else
                    return false;
            }
            return true;
        }
        private bool OnlyAttitude(string except)
        {
            bool val1 = MagPlot.Visibility == Visibility.Collapsed;
            bool val2 = AccPlot.Visibility == Visibility.Collapsed;
            bool val3 = GyroPlot.Visibility == Visibility.Collapsed;
            bool val4 = DepthPlot.Visibility == Visibility.Collapsed;
            if (except == "mag")
                val1 = true;
            else if (except == "acc")
                val2 = true;
            else if (except == "gyro")
                val3 = true;
            else if (except == "depth")
                val4 = true;
            bool ret;
            if (val1 && val2 && val3 && val4)
                ret = true;
            else ret = false;
            return ret;
        }
        private void ManageBottomGrid(string ex)
        {
            if (OnlyAttitude(ex) == true)
                PlotsLayout.RowDefinitions[1].Height = new GridLength(1.0, GridUnitType.Auto);
            else
                PlotsLayout.RowDefinitions[1].Height = new GridLength(1.0, GridUnitType.Star);
        }
        private void ShowAttitudeChanged(object sender, RoutedEventArgs e)
        {
            if (CheckLayout() == false)
                return;
            var obj = (CheckBox)sender;
            if (obj.IsChecked == true)
            {

                PlotsLayout.RowDefinitions[0].Height = new GridLength(1.0, GridUnitType.Star);
                ManageBottomGrid("");
            }
            else
            {

                PlotsLayout.RowDefinitions[0].Height = new GridLength(1.0, GridUnitType.Auto);
            }
        }
        private void ShowDepthChanged(object sender, RoutedEventArgs e)
        {
            if (CheckLayout() == false)
                return;
            var obj = (CheckBox)sender;
            if (obj.IsChecked==true)
            {
                LowerBottomGridCol[1].Width = new GridLength(1.0, GridUnitType.Star);
                BottomGrid.RowDefinitions[1].Height = new GridLength(1.0, GridUnitType.Star);
                PlotsLayout.RowDefinitions[1].Height = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                LowerBottomGridCol[1].Width = new GridLength(1.0, GridUnitType.Auto);
                if (MagPlot.Visibility == Visibility.Collapsed)
                    BottomGrid.RowDefinitions[1].Height = new GridLength(1.0, GridUnitType.Auto);
                ManageBottomGrid("depth");
            }
        }
        private void ShowGyroChanged(object sender, RoutedEventArgs e)
        { 
        
             if (CheckLayout() == false)
                return;
            var obj = (CheckBox)sender;
            if (obj.IsChecked==true)
            {
                PlotsLayout.RowDefinitions[1].Height = new GridLength(1.0, GridUnitType.Star);
                BottomGrid.RowDefinitions[0].Height = new GridLength(1.0, GridUnitType.Star);
                UpperBottomGridCol[1].Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                UpperBottomGridCol[1].Width = new GridLength(1.0, GridUnitType.Auto);
                if (AccPlot.Visibility == Visibility.Collapsed)
                    BottomGrid.RowDefinitions[0].Height = new GridLength(1.0, GridUnitType.Auto);
                ManageBottomGrid("gyro");
            }
        }
        private void ShowAccChanged(object sender, RoutedEventArgs e)
        {
            if (CheckLayout() == false)
                return;
                var obj = (CheckBox)sender;
            if (obj.IsChecked == true)
            {
                BottomGrid.RowDefinitions[0].Height = new GridLength(1.0, GridUnitType.Star);
                PlotsLayout.RowDefinitions[1].Height = new GridLength(1.0, GridUnitType.Star);
                UpperBottomGridCol[0].Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                UpperBottomGridCol[0].Width = new GridLength(1.0, GridUnitType.Auto);
                if(GyroPlot.Visibility==Visibility.Collapsed)
                    BottomGrid.RowDefinitions[0].Height= new GridLength(1.0, GridUnitType.Auto);
                ManageBottomGrid("acc");
            }

        }

        private void ShowMagChanged(object sender, RoutedEventArgs e)
        {
        if (CheckLayout() == false)
            return;
        var obj = (CheckBox)sender;
            if (obj.IsChecked == true)
            {
                BottomGrid.RowDefinitions[1].Height = new GridLength(1.0, GridUnitType.Star);
                PlotsLayout.RowDefinitions[1].Height = new GridLength(1.0, GridUnitType.Star);
                LowerBottomGridCol[0].Width = new GridLength(1.0, GridUnitType.Star);
            }
            else
            {
                LowerBottomGridCol[0].Width = new GridLength(1.0, GridUnitType.Auto);
                if (DepthPlot.Visibility == Visibility.Collapsed)
                    BottomGrid.RowDefinitions[1].Height = new GridLength(1.0, GridUnitType.Auto);
                ManageBottomGrid("mag");
            }
        }


    }
}
