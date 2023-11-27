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

namespace HungVuong_WPF_C2_B1
{
    /// <summary>
    /// Interaction logic for ucShowtime.xaml
    /// </summary>
    public partial class ucShowtime : UserControl
    {
        private Window window;

        private MovieViewModel movieVM;

        private Movie movie;

        private string cinemaType;

        private int scheduleIndex;

        private ucNotifier notifier;

        public ucShowtime(MovieViewModel movieVM, Movie movie, string cinemaType, int scheduleIndex)
        {
            InitializeComponent();

            this.movieVM = movieVM;
            this.movie = movie;
            this.cinemaType = cinemaType;
            this.scheduleIndex = scheduleIndex;

            notifier = new ucNotifier(this);

            ReloadDataGridShowtime();
        }

        private void ReloadDataGridShowtime()
        {
            dgShowtime.Items.Clear();

            int index = 0;
            foreach (var item in movie.ScheduleLst[scheduleIndex].ShowtimeList)
            {
                string showtimePlusRuntime = item.Add(new TimeSpan(0, movie.Runtime, 0)).ToString();
                string showtimePlusRuntimeAndCleanupTime = item.Add(new TimeSpan(0, movie.Runtime + movieVM.CleanupTime, 0)).ToString();

                dgShowtime.Items.Add(new
                {
                    Index = index++,
                    MovieName = movie.Name,
                    Runtime = movie.Runtime + " Phút",
                    CinemaType = cinemaType,
                    Date = ConvertString.ConvertDateToStringTwo(movie.ScheduleLst[scheduleIndex].ReleaseDate),
                    Showtime = $"{ConvertString.ConvertHourAndMinuteToStringOne(item.Hours, item.Minutes)} - {showtimePlusRuntime.Substring(0, showtimePlusRuntime.LastIndexOf(":"))} - {showtimePlusRuntimeAndCleanupTime.Substring(0, showtimePlusRuntimeAndCleanupTime.LastIndexOf(":"))}"
                });
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            window = new Window();

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ucUpdateShowtime ucUpdateShowtime = new ucUpdateShowtime();
            ucUpdateShowtime.lbHeader.Content = "Thêm giờ chiếu";

            ucUpdateShowtime.CancelEvent += UcUpdateShowtime_CancelEvent;

            ucUpdateShowtime.ConfirmEvent += UcUpdateShowtime_ConfirmEventAdd;

            window.Content = ucUpdateShowtime;

            window.Show();
        }

        private void UcUpdateShowtime_ConfirmEventAdd(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            int hours = int.Parse(button.Tag.ToString().Split(':')[0]);
            int minutes = int.Parse(button.Tag.ToString().Split(':')[1]);

            string date = ConvertString.ConvertDateToStringOne(movie.ScheduleLst[scheduleIndex].ReleaseDate);

            if(movieVM.isExistSchedule(new TimeSpan(hours, minutes, 0), cinemaType, date))
            {
                ucUpdateShowtime.notifier.ShowWarning("Thời điểm này đã có phim chiếu!");
                return;
            }

            movie.ScheduleLst[scheduleIndex].ShowtimeList.Add(new TimeSpan(hours, minutes, 0));

            XmlFileManager.WriteMoviesXMLSchedule(movieVM.movieRepo.Items);

            string path = null;

            if (cinemaType == "Vip")
                path = Path.CinemaVipXml;   
            else
                path = Path.CinemaStandardXml;

            File.Copy(path, Path.getFileShowtime(
                        cinemaType,
                        movie.Id,
                        ConvertString.ConvertDateToStringOne(movie.ScheduleLst[scheduleIndex].ReleaseDate),
                        ConvertString.ConvertHourAndMinuteToStringTwo(
                            hours,
                            minutes
                            )
                        ));

            ReloadDataGridShowtime();
            window.Close();
            notifier.ShowSuccess("Thêm thành công!");
        }

        private void btnDeleteDate_Click(object sender, RoutedEventArgs e)
        {
            var isOk = new Modal().ShowDialog();

            if (isOk == true)
            {
                var selectedRow = dgShowtime.SelectedItem;

                if (selectedRow != null)
                {
                    int index = (int)selectedRow.GetType().GetProperty("Index").GetValue(selectedRow, null);

                    File.Delete(Path.getFileShowtime(
                        cinemaType,
                        movie.Id,
                        ConvertString.ConvertDateToStringOne(movie.ScheduleLst[scheduleIndex].ReleaseDate),
                        ConvertString.ConvertHourAndMinuteToStringTwo(
                            movie.ScheduleLst[scheduleIndex].ShowtimeList[index].Hours,
                            movie.ScheduleLst[scheduleIndex].ShowtimeList[index].Minutes
                            )
                        ));

                    movie.ScheduleLst[scheduleIndex].ShowtimeList.Remove(movie.ScheduleLst[scheduleIndex].ShowtimeList[index]);

                    XmlFileManager.WriteMoviesXMLSchedule(movieVM.movieRepo.Items);

                    ReloadDataGridShowtime();

                    notifier.ShowSuccess("Xóa thành công!");
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            window = new Window();

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ucUpdateShowtime ucUpdateShowtime = new ucUpdateShowtime();

            var selectedRow = dgShowtime.SelectedItem;

            int index = (int)selectedRow.GetType().GetProperty("Index").GetValue(selectedRow, null);

            ucUpdateShowtime.txtHours.Text = movie.ScheduleLst[scheduleIndex].ShowtimeList[index].Hours.ToString();
            ucUpdateShowtime.txtMinutes.Text = movie.ScheduleLst[scheduleIndex].ShowtimeList[index].Minutes.ToString();

            ucUpdateShowtime.CancelEvent += UcUpdateShowtime_CancelEvent;

            ucUpdateShowtime.ConfirmEvent += UcUpdateShowtime_ConfirmEventUpdate;

            window.Content = ucUpdateShowtime;

            window.Show();
        }

        private void UcUpdateShowtime_ConfirmEventUpdate(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int hours = int.Parse(button.Tag.ToString().Split(':')[0]);
            int minutes = int.Parse(button.Tag.ToString().Split(':')[1]);

            string date = ConvertString.ConvertDateToStringOne(movie.ScheduleLst[scheduleIndex].ReleaseDate);

            if (movieVM.isExistSchedule(new TimeSpan(hours, minutes, 0), cinemaType, date))
            {
                ucUpdateShowtime.notifier.ShowWarning("Thời điểm này đã có phim chiếu!");
                return;
            }

            var selectedRow = dgShowtime.SelectedItem;

            int index = (int)selectedRow.GetType().GetProperty("Index").GetValue(selectedRow, null);

            File.Move(
                Path.getFileShowtime(
                    cinemaType,
                    movie.Id,
                    ConvertString.ConvertDateToStringOne(movie.ScheduleLst[scheduleIndex].ReleaseDate),
                    ConvertString.ConvertHourAndMinuteToStringTwo(movie.ScheduleLst[scheduleIndex].ShowtimeList[index].Hours, movie.ScheduleLst[scheduleIndex].ShowtimeList[index].Minutes)),

                Path.getFileShowtime(
                    cinemaType,
                    movie.Id,
                    ConvertString.ConvertDateToStringOne(movie.ScheduleLst[scheduleIndex].ReleaseDate),
                    ConvertString.ConvertHourAndMinuteToStringTwo(hours, minutes))
                );

            movie.ScheduleLst[scheduleIndex].ShowtimeList[index] = new TimeSpan(hours, minutes, 0);

            XmlFileManager.WriteMoviesXMLSchedule(movieVM.movieRepo.Items);

            ReloadDataGridShowtime();

            this.window.Close();

            notifier.ShowSuccess("Cập nhật thành công!");
        }

        private void UcUpdateShowtime_CancelEvent(object sender, RoutedEventArgs e)
        {
            this.window.Close();
        }
    }
}
