using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
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
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

namespace HungVuong_WPF_C2_B1
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        AccountList accountList = new AccountList();
        TicketingStaffWindow ticketingStaffWindow;
        AdminWindow adminWindow;

        Notifier notifierForThis;

        private bool isLogin = false;

        public LoginWindow()
        {
            InitializeComponent();

            notifierForThis = new Notifier(cfg =>
            {
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(1));

                cfg.Dispatcher = Application.Current.Dispatcher;

                var positionProvider = new WindowPositionProvider(
                    parentWindow: this,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);
                cfg.PositionProvider = positionProvider;
            });
        }

        private bool openNewWindow(Account account)
        {
            if (account.priority == 0)
            {
                adminWindow = new AdminWindow(new AccountDTO(account.username));
                adminWindow.Show();
                return true;
            }
            else if (account.priority == 1)
            {
                ticketingStaffWindow = new TicketingStaffWindow(new AccountDTO(account.username));
                ticketingStaffWindow.Show();
                return true;
            }
            return false;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            foreach (var account in accountList.lstAccount)
            {
                SecureString securePassword = txtPassword.SecurePassword;
                IntPtr bstrPtr = Marshal.SecureStringToBSTR(securePassword);
                string password = Marshal.PtrToStringBSTR(bstrPtr);

                if (string.Compare(txtUsername.Text.ToLower(), account.username.ToLower()) == 0
                    && string.Compare(password.ToLower(), account.password.ToLower()) == 0)
                {
                    if (openNewWindow(account)) {
                        isLogin = true;
                        this.Close();
                        return;
                    }
                }
            }
            notifierForThis.ShowError("Tài khoản hoặc mật khẩu không chính xác!");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            txtUsername.Focus();
        }

        private void txtPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(e.Text == "\r")
            {
                btnLogin.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        public static void ShowLoginForm()
        {
            LoginWindow login = new LoginWindow();
            login.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(isLogin == false)
                Application.Current.Shutdown();
        }
    }
}
