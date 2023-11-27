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
using ToastNotifications;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;

namespace HungVuong_WPF_C2_B1
{
    /// <summary>
    /// Interaction logic for TicketingStaffWindow.xaml
    /// </summary>
    public partial class TicketingStaffWindow : Window
    {
        private Notifier notifier;

        private ucTicketPrice _ucTicketPrice = new ucTicketPrice();
        private ucBookingTicket _ucBookingTicket = new ucBookingTicket();

        public delegate void btnBookingEvent(object sender, RoutedEventArgs e);

        public static event btnBookingEvent btnBookingClicked;

        private bool isLogout = false;

        public TicketingStaffWindow(AccountDTO accountDTO)
        {
            InitializeComponent();

            tbHello.Text = "Xin chào " + accountDTO.Username;

            notifier = new Notifier(cfg =>
            {
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(3));

                cfg.Dispatcher = Application.Current.Dispatcher;

                var positionProvider = new WindowPositionProvider(
                    parentWindow: this,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);
                cfg.PositionProvider = positionProvider;
            });

            this.WindowState = WindowState.Maximized;
        }

        private void btnTicketPriceView_Click(object sender, RoutedEventArgs e)
        {
            grdFeature.Children.Clear();

            btnBookingView.Background = Brushes.LightGray;
            btnTicketPriceView.Background = Brushes.LightGray;

            btnTicketPriceView.Background = Brushes.Wheat;

            grdFeature.Children.Add(new ucTicketPrice());
        }

        private void btnBookingView_Click(object sender, RoutedEventArgs e)
        {
            grdFeature.Children.Clear();

            btnBookingView.Background = Brushes.LightGray;
            btnTicketPriceView.Background = Brushes.LightGray;

            btnBookingView.Background = Brushes.Wheat;

            grdFeature.Children.Add(_ucBookingTicket);
            btnBookingClicked?.Invoke(sender, e);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow.ShowLoginForm();
            isLogout = true;
            this.Close();
        }

        // Sự kiện khi người dùng ấn dấu X để tắt
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(isLogout == false)
                LoginWindow.ShowLoginForm();
        }
    }
}
