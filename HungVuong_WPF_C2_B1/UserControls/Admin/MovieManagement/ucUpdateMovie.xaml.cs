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
    /// Interaction logic for ucUpdateMovie.xaml
    /// </summary>
    public partial class ucUpdateMovie : UserControl
    {
        public static Notifier notifier;
        public delegate void clickCancel(object sender, RoutedEventArgs e);

        public static event clickCancel btnCancelEvent;

        public delegate void clickUpdate(object sender, RoutedEventArgs e);

        public static event clickUpdate updateEvent;

        public static readonly DependencyProperty lbMainContentProperty1 =
        DependencyProperty.Register("lbMainContent1", typeof(string), typeof(ucUpdate), new UIPropertyMetadata(null));

        public string lbMainContent1
        {
            get { return (string)GetValue(lbMainContentProperty1); }
            set { SetValue(lbMainContentProperty1, value); }
        }

        public static readonly DependencyProperty btnActionProperties1 =
        DependencyProperty.Register("btnAction1", typeof(string), typeof(ucUpdate), new UIPropertyMetadata(null));

        public string btnActionContent1
        {
            get { return (string)GetValue(btnActionProperties1); }
            set { SetValue(btnActionProperties1, value); }
        }

        public ucUpdateMovie()
        {
            InitializeComponent();

            ucMovieManagement.updateEvent += UcMovieManagement_updateEvent;
            ucMovieManagement.notifyEvent += UcMovieManagement_notifyEvent; ;


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

        private void UcMovieManagement_notifyEvent()
        {
            notifier.ShowError("Mã số bị trùng lặp!");
        }

        private void UcMovieManagement_updateEvent(object sender, RoutedEventArgs e)
        {
            txtId.IsEnabled = false;
            txtId.Text = (sender as Button).Tag.ToString().Split('`')[0];
            txtName.Text = (sender as Button).Tag.ToString().Split('`')[1];
            txtContent.Text = (sender as Button).Tag.ToString().Split('`')[2];
            txtRuntime.Text = (sender as Button).Tag.ToString().Split('`')[3];
            txtUrl.Text = (sender as Button).Tag.ToString().Split('`')[4];
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnCancelEvent?.Invoke(sender, e);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(txtId.Text == string.Empty || txtName.Text == string.Empty || txtContent.Text == string.Empty || txtRuntime.Text == string.Empty || txtUrl.Text == string.Empty)
            {
                notifier.ShowWarning("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            (sender as Button).Tag = $"{txtId.Text}`{txtName.Text}`{txtContent.Text}`{txtRuntime.Text}`{txtUrl.Text}";
            updateEvent?.Invoke(sender, e);
        }

        private void txtRuntime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
                e.Handled = true;
        }
    }
}
