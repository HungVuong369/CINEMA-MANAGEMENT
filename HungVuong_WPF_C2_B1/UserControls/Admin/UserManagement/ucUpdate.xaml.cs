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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

namespace HungVuong_WPF_C2_B1
{
    /// <summary>
    /// Interaction logic for ucUpdate.xaml
    /// </summary>
    public partial class ucUpdate : UserControl
    {
        public static Notifier notifier;

        public delegate void CancelEvent(object sender, RoutedEventArgs e);

        public static event CancelEvent cancelEvent;

        public delegate void UpdateEvent(object sender, RoutedEventArgs e);

        public static event UpdateEvent updateEvent;

        public static readonly DependencyProperty lbMainContentProperty =
        DependencyProperty.Register("lbMainContent", typeof(string), typeof(ucUpdate), new UIPropertyMetadata(null));

        public string lbMainContent
        {
            get { return (string)GetValue(lbMainContentProperty); }
            set { SetValue(lbMainContentProperty, value); }
        }

        public static readonly DependencyProperty btnActionProperties =
        DependencyProperty.Register("btnAction", typeof(string), typeof(ucUpdate), new UIPropertyMetadata(null));

        public string btnActionContent
        {
            get { return (string)GetValue(btnActionProperties); }
            set { SetValue(btnActionProperties, value); }
        }

        public ucUpdate()
        {
            InitializeComponent();
            ucUserManagement.updateEvent += UcUserManagement_updateEvent;

            notifier = new Notifier(cfg =>
            {
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(1),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(1));

                cfg.Dispatcher = Application.Current.Dispatcher;

                var positionProvider = new WindowPositionProvider(
                    parentWindow: Window.GetWindow(this),
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);
                cfg.PositionProvider = positionProvider;
            });
        }

        private void UcUserManagement_updateEvent(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = (sender as Button).Tag.ToString().Split(' ')[0];
            txtPassword.Text = (sender as Button).Tag.ToString().Split(' ')[1];
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelEvent?.Invoke(sender, e);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == string.Empty || txtPassword.Text == string.Empty)
            {
                notifier.ShowError("Không được để trống dữ liệu!");
                return;
            }
            (sender as Button).Tag = txtUsername.Text + " " + txtPassword.Text;
            updateEvent?.Invoke(sender, e);
        }
    }
}
