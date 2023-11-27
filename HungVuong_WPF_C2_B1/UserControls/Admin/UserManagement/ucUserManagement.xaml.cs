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
    /// Interaction logic for ucUserManagement.xaml
    /// </summary>
    public partial class ucUserManagement : UserControl
    {
        private List<Account> _accounts;

        Window updateDialog = new Window();

        private Notifier notifier;

        public delegate void UpdateEvent(object sender, RoutedEventArgs e);

        public static event UpdateEvent updateEvent;

        private bool _isAdd = false;

        public ucUserManagement()
        {
            InitializeComponent();

            _accounts = SeedData.getListAccount(false);

            this.reloadDataGrid();

            ucUpdate.cancelEvent += UcUpdate_cancelEvent;
            ucUpdate.updateEvent += UcUpdate_updateEvent;

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

        private bool isExistUsername(string username)
        {
            foreach(var item in _accounts)
            {
                if(!_isAdd)
                {
                    if (string.Compare(username.ToLower(), item.username.ToLower()) == 0)
                        continue;
                }
                if (string.Compare(username.ToLower(), item.username.ToLower()) == 0)
                    return true;
            }
            return false;
        }

        private void UcUpdate_updateEvent(object sender, RoutedEventArgs e)
        {
            if(isExistUsername((sender as Button).Tag.ToString().Split(' ')[0]))
            {
                ucUpdate.notifier.ShowWarning("Tên tài khoản đã tồn tại!");
                return;
            }
            updateDialog.Close();

            if (_isAdd)
            {
                btnAdd_Click(sender);
                return;
            }

            var selectedRow = dgUserManagement.SelectedItem;

            int index = (int)selectedRow.GetType().GetProperty("Index").GetValue(selectedRow, null);

            this._accounts[index - 1].username = (sender as Button).Tag.ToString().Split(' ')[0];
            this._accounts[index - 1].password = (sender as Button).Tag.ToString().Split(' ')[1];

            XmlFileManager.WriteAccountsXML(this._accounts);

            this.reloadDataGrid();

            notifier.ShowSuccess("Cập nhật dữ liệu thành công!");
        }

        private void UcUpdate_cancelEvent(object sender, RoutedEventArgs e)
        {
            updateDialog.Close();
        }

        private void reloadDataGrid()
        {
            dgUserManagement.Items.Clear();

            int index = 1;
            string role = "Nhân viên bán vé";

            foreach (var account in _accounts)
            {
                if (account.priority == 0)
                    continue;

                dgUserManagement.Items.Add(new { Index = index++, Username = account.username, Password = account.password, Priority = role });
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var isOk = new Modal().ShowDialog();

            if (isOk == true)
            {
                var selectedRow = dgUserManagement.SelectedItem;

                if (selectedRow != null)
                {
                    int index = (int)selectedRow.GetType().GetProperty("Index").GetValue(selectedRow, null);

                    this._accounts.RemoveAt(index - 1);

                    XmlFileManager.WriteAccountsXML(_accounts);

                    this.reloadDataGrid();

                    notifier.ShowSuccess("Xóa thành công!");
                }
            }
        }

        private void btnAdd_Click(object sender)
        {
            string username = (sender as Button).Tag.ToString().Split(' ')[0];
            string password = (sender as Button).Tag.ToString().Split(' ')[1];

            this._accounts.Add(
                new Account(username, password, 1)
                );
            this.reloadDataGrid();
            XmlFileManager.WriteAccountsXML(this._accounts);
            notifier.ShowSuccess("Thêm thành công!");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            updateDialog = new Window();

            updateDialog.Width = 420;
            updateDialog.Height = 250;
            ucUpdate ucUpdate = new ucUpdate();
            ucUpdate.lbMainContent = "SỬA THÔNG TIN";
            ucUpdate.btnActionContent = "LƯU";
            updateDialog.Content = ucUpdate;

            var selectedRow = dgUserManagement.SelectedItem;

            string username = (string)selectedRow.GetType().GetProperty("Username").GetValue(selectedRow, null);
            string password = (string)selectedRow.GetType().GetProperty("Password").GetValue(selectedRow, null);

            (sender as Button).Tag = username + " " + password;

            updateDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            updateEvent?.Invoke(sender, e);

            updateDialog.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _isAdd = true;
            updateDialog = new Window();

            updateDialog.Width = 420;
            updateDialog.Height = 250;
            ucUpdate ucUpdate = new ucUpdate();
            ucUpdate.lbMainContent = "THÊM MỚI TÀI KHOẢN";
            ucUpdate.btnActionContent = "THÊM";
            updateDialog.Content = ucUpdate;

            updateDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            updateDialog.ShowDialog();

            _isAdd = false;
        }
    }
}
