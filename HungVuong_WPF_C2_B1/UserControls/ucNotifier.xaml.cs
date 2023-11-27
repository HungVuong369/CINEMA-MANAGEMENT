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
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace HungVuong_WPF_C2_B1
{
    /// <summary>
    /// Interaction logic for ucNotifier.xaml
    /// </summary>
    public partial class ucNotifier : UserControl
    {
        private Notifier notifier;

        public ucNotifier(UserControl userControl)
        {
            InitializeComponent();

            notifier = new Notifier(cfg =>
            {
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(1),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(1));

                cfg.Dispatcher = Application.Current.Dispatcher;

                var positionProvider = new WindowPositionProvider(
                    parentWindow: Window.GetWindow(userControl),
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);
                cfg.PositionProvider = positionProvider;
            });
        }

        public void ShowSuccess(string text)
        {
            notifier.ShowSuccess(text);
        }

        public void ShowError(string text)
        {
            notifier.ShowError(text);
        }

        public void ShowWarning(string text)
        {
            notifier.ShowWarning(text);
        }

        public void ShowInfomation(string text)
        {
            notifier.ShowInformation(text);
        }
    }
}
