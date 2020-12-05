using Services.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Services.DialogServices
{
    public static class DialogService
    {
        public static DialogResult OpenDialog(DialogBaseViewModel parameter)
        {
            DialogWindow win = new DialogWindow();
            win.DataContext = parameter;
            win.Owner = Application.Current.MainWindow;
            win.ShowDialog();
            DialogResult result =
            (win.DataContext as DialogBaseViewModel).UserDialogResult;
            return result;
        }
    }
}
