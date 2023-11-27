using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ucScheduleManagement.xaml
    /// </summary>
    public partial class ucScheduleManagement : UserControl
    {
        private CinemaViewModel cinemaVM = new CinemaViewModel();
        private MovieViewModel movieVM = new MovieViewModel();
        private Window window = new Window();
        private Notifier notifier;

        public ucScheduleManagement()
        {
            InitializeComponent();
            cbCinema.ItemsSource = cinemaVM.cinemaRepo.Items;
            cbCinema.DisplayMemberPath = "Name";
            cbCinema.SelectedValuePath = "Name";
            cbCinema.SelectedIndex = 0;
            dgScheduleManagement.ItemsSource = movieVM.GetFilteredMoviesWithCinemaName(cbCinema.SelectedValue.ToString());

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

        private void cbCinema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCinema.SelectedValue != null)
                dgScheduleManagement.ItemsSource = movieVM.GetFilteredMoviesWithCinemaName(cbCinema.SelectedValue.ToString());
        }

        private void btnDeleteSchedule_Click(object sender, RoutedEventArgs e)
        {
            var isOk = new Modal().ShowDialog();

            if (isOk == true)
            {
                var selectedRow = dgScheduleManagement.SelectedItem;

                if (selectedRow != null)
                {
                    string id = (string)selectedRow.GetType().GetProperty("Id").GetValue(selectedRow, null);
                    Movie movie = movieVM.GetFilteredMoviesWithCinemaName(cbCinema.SelectedValue.ToString()).Find(item => item.Id == id);

                    Schedule schedule = null;

                    for (int index = movie.ScheduleLst.Count - 1; index >= 0; index--)
                    {
                        schedule = movie.ScheduleLst[index];
                        if (string.Compare(schedule.CinemaType, cinemaVM.cinemaRepo.Items[cbCinema.SelectedIndex].Type) == 0)
                            movie.ScheduleLst.Remove(schedule);
                    }

                    movie.CinemaLst.Remove(movie.CinemaLst.Find(item => item.Name == cbCinema.SelectedValue.ToString()));

                    dgScheduleManagement.ItemsSource = movieVM.GetFilteredMoviesWithCinemaName(cbCinema.SelectedValue.ToString());

                    if (Directory.Exists(Path.getFolderCinema((cbCinema.SelectedItem as Cinema).Type, movie.Id)))
                        Directory.Delete(Path.getFolderCinema((cbCinema.SelectedItem as Cinema).Type, movie.Id), true);

                    XmlFileManager.WriteMoviesXMLSchedule(movieVM.movieRepo.Items);
                    notifier.ShowSuccess("Xóa thành công!");
                }
            }
            else
                notifier.ShowSuccess("Hủy thành công!");
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (movieVM.GetFilteredMoviesWithCinema(cbCinema.SelectedValue.ToString()).Count == 0)
            {
                notifier.ShowWarning("Không tìm thấy lịch trình!");
                return;
            }

            window = new Window();

            window.Width = 350;
            window.Height = 180;

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ucAddSchedule ucAddSchedule = new ucAddSchedule(movieVM.GetFilteredMoviesWithCinema(cbCinema.SelectedValue.ToString()));
            ucAddSchedule.AddEvent += UcAddSchedule_AddEvent;
            ucAddSchedule.CancelEvent += UcAddSchedule_CancelEvent;
            window.Content = ucAddSchedule;
            window.ShowDialog();
        }

        private void UcAddSchedule_CancelEvent(object sender, RoutedEventArgs e)
        {
            this.window.Close();
        }

        private void UcAddSchedule_AddEvent(object sender, RoutedEventArgs e)
        {
            List<Movie> movies = movieVM.GetFilteredMoviesWithCinema(cbCinema.SelectedValue.ToString());

            int index = (int)(sender as Button).Tag;

            movies[index].CinemaLst.Add(cinemaVM.cinemaRepo.Items[cbCinema.SelectedIndex]);

            XmlFileManager.WriteMoviesXMLSchedule(movieVM.movieRepo.Items);

            window.Close();

            dgScheduleManagement.ItemsSource = movieVM.GetFilteredMoviesWithCinemaName(cbCinema.SelectedValue.ToString());

            Directory.CreateDirectory(Path.getFolderCinema(cinemaVM.cinemaRepo.Items[cbCinema.SelectedIndex].Type, movies[index].Id));

            notifier.ShowSuccess("Thêm lịch trình thành công!");
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dgScheduleManagement.SelectedItem;
            Movie movie = null;

            if (selectedRow != null)
            {
                string id = (string)selectedRow.GetType().GetProperty("Id").GetValue(selectedRow, null);
                movie = movieVM.GetFilteredMoviesWithCinemaName(cbCinema.SelectedValue.ToString()).Find(item => item.Id == id);
            }

            window = new Window();

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            window.Width = 1100;
            window.Height = 500;

            ucDate ucDate = new ucDate(movieVM, movie, (cbCinema.SelectedItem as Cinema).Type);
            window.Content = ucDate;
            window.ShowDialog();
        }
    }
}
