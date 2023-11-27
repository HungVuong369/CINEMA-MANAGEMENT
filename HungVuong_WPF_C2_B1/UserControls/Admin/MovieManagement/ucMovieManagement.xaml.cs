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
using System.IO;

namespace HungVuong_WPF_C2_B1
{
    /// <summary>
    /// Interaction logic for ucMovieManagement.xaml
    /// </summary>
    public partial class ucMovieManagement : UserControl
    {
        private MovieViewModel movieVM = new MovieViewModel();

        private Window updateDialog = new Window();

        private Notifier notifier;

        public delegate void UpdateEvent(object sender, RoutedEventArgs e);

        public static event UpdateEvent updateEvent;

        public delegate void NotifyEvent();

        public static event NotifyEvent notifyEvent;

        private CinemaViewModel cinemaVM = new CinemaViewModel();

        private bool _isAdd = false;

        public ucMovieManagement()
        {
            InitializeComponent();

            reloadDataGrid();

            ucUpdateMovie.btnCancelEvent += UcUpdateMovie_btnCancelEvent;
            ucUpdateMovie.updateEvent += UcUpdateMovie_updateEvent;

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

        private void AddMovie(object sender)
        {
            Button button = sender as Button;

            if (movieVM.isExistId(button.Tag.ToString().Split('`')[0]))
            {
                if(ucMovieManagement.notifyEvent != null)
                    ucMovieManagement.notifyEvent?.Invoke();
                return;
            }
               
            movieVM.movieRepo.Items.Add(new Movie(
                button.Tag.ToString().Split('`')[0],
                button.Tag.ToString().Split('`')[1],
                button.Tag.ToString().Split('`')[2],
                int.Parse(button.Tag.ToString().Split('`')[3]),
                button.Tag.ToString().Split('`')[4],
                new List<Schedule>(),
                new List<Cinema>()
                ));

            reloadDataGrid();

            XmlFileManager.WriteMoviesXMLSchedule(movieVM.movieRepo.Items);

            updateDialog.Close();

            Directory.CreateDirectory(Path.getFolderMovies(button.Tag.ToString().Split('`')[0].Replace(":", " ")));

            notifier.ShowSuccess("Thêm thành công!");
        }

        private void UcUpdateMovie_updateEvent(object sender, RoutedEventArgs e)
        {
            var selectedRow = dgMovieManagement.SelectedItem;
            if(_isAdd == true)
            {
                AddMovie(sender);
                return;
            }

            if(selectedRow != null)
            {
                int index = (int)selectedRow.GetType().GetProperty("Index").GetValue(selectedRow, null);
                Button button = sender as Button;

                movieVM.movieRepo.Items[index - 1].Id = button.Tag.ToString().Split('`')[0];
                movieVM.movieRepo.Items[index - 1].Name = button.Tag.ToString().Split('`')[1];
                movieVM.movieRepo.Items[index - 1].Content = button.Tag.ToString().Split('`')[2];
                movieVM.movieRepo.Items[index - 1].Runtime = int.Parse(button.Tag.ToString().Split('`')[3]);
                movieVM.movieRepo.Items[index - 1].UrlImage = button.Tag.ToString().Split('`')[4];

                XmlFileManager.WriteMoviesXMLSchedule(movieVM.movieRepo.Items);

                reloadDataGrid();

                updateDialog.Close();

                notifier.ShowSuccess("Cập nhật dữ liệu thành công!");
            }
        }

        private void UcUpdateMovie_btnCancelEvent(object sender, RoutedEventArgs e)
        {
            this.updateDialog.Close();
        }

        private void reloadDataGrid()
        {
            dgMovieManagement.Items.Clear();

            int index = 1;
            foreach (var movie in movieVM.movieRepo.Items)
                dgMovieManagement.Items.Add(new { Index = index++, Id = movie.Id, Name = movie.Name, Content = movie.Content, Runtime = movie.Runtime, Url = movie.UrlImage });
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            updateDialog = new Window();

            updateDialog.Width = 420;
            updateDialog.Height = 400;

            ucUpdateMovie ucUpdateM = new ucUpdateMovie();

            ucUpdateM.lbMainContent1 = "SỬA THÔNG TIN";
            ucUpdateM.btnActionContent1 = "LƯU";
            updateDialog.Content = ucUpdateM;

            var selectedRow = dgMovieManagement.SelectedItem;

            int index = (int)selectedRow.GetType().GetProperty("Index").GetValue(selectedRow, null);

            (sender as Button).Tag = $"{movieVM.movieRepo.Items[index - 1].Id}`{movieVM.movieRepo.Items[index - 1].Name}`{movieVM.movieRepo.Items[index - 1].Content}`{movieVM.movieRepo.Items[index - 1].Runtime}`{movieVM.movieRepo.Items[index - 1].UrlImage}";

            updateDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            updateEvent?.Invoke(sender, e);

            updateDialog.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var isOk = new Modal().ShowDialog();

            if (isOk == true)
            {
                var selectedRow = dgMovieManagement.SelectedItem;

                if (selectedRow != null)
                {
                    int index = (int)selectedRow.GetType().GetProperty("Index").GetValue(selectedRow, null);

                    movieVM.movieRepo.Items.RemoveAt(index - 1);

                    XmlFileManager.WriteMoviesXMLSchedule(movieVM.movieRepo.Items);

                    this.reloadDataGrid();

                    notifier.ShowSuccess("Xóa thành công!");
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this._isAdd = true;

            updateDialog = new Window();

            updateDialog.Width = 420;
            updateDialog.Height = 400;
            ucUpdateMovie ucUpdate = new ucUpdateMovie();
            ucUpdate.lbMainContent1 = "THÊM MỚI PHIM";
            ucUpdate.btnActionContent1 = "THÊM";
            updateDialog.Content = ucUpdate;

            updateDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            updateDialog.ShowDialog();

            this._isAdd = false;
        }
    }
}
