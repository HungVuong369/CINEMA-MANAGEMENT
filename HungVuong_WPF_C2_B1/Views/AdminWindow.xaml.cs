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
using System.Windows.Shapes;

namespace HungVuong_WPF_C2_B1
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private ucUserManagement ucUserManagement = new ucUserManagement();
        private ucMovieManagement ucMovieManagement = new ucMovieManagement();
        private ucScheduleManagement ucScheduleManagement = new ucScheduleManagement();
        private bool isLogout = false;

        public AdminWindow(AccountDTO accountDTO)
        {
            InitializeComponent();

            tbHello.Text = "Xin chào " + accountDTO.Username;

            this.WindowState = WindowState.Maximized;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow.ShowLoginForm();
            isLogout = true;
            this.Close();
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var clickedItem = e.OriginalSource as TreeViewItem;

            if (clickedItem != null && clickedItem.Tag != null)
            {
                grdFeature.Children.Clear();

                string tagValue = clickedItem.Tag.ToString();

                if (tagValue == "UserManagement")
                {
                    grdFeature.Children.Add(ucUserManagement);
                }
                else if (tagValue == "MovieManagement")
                {
                    grdFeature.Children.Add(ucMovieManagement);
                }
                else if (tagValue == "MovieScheduleManagement")
                {
                    grdFeature.Children.Add(ucScheduleManagement);
                }
                else if(tagValue == "Statistics")
                {
                    grdFeature.Children.Add(new ucStatistics());
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(isLogout == false)
                LoginWindow.ShowLoginForm();
        }
    }
}
