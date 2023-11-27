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
    /// Interaction logic for ucDate.xaml
    /// </summary>
    public partial class ucDate : UserControl
    {
        private ucNotifier notifier;

        private Movie movie;

        private MovieViewModel movieVM;

        private Window window;

        private string cinemaType;

        public ucDate(MovieViewModel movieVM, Movie movie, string cinemaType)
        {
            InitializeComponent();

            this.movie = movie;

            this.movieVM = movieVM;

            this.cinemaType = cinemaType;

            ReloadDataGridDate();
            
            notifier = new ucNotifier(this);
        }

        private void ReloadDataGridDate()
        {
            dgDate.Items.Clear();

            int index = 0;
            foreach (var item in movie.ScheduleLst)
            {
                if (string.Compare(cinemaType, item.CinemaType) == 0)
                    dgDate.Items.Add(new { Index = index++, Date = ConvertString.ConvertDateToStringTwo(item.ReleaseDate), CinemaType = cinemaType, MovieName = movie.Name });
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            window = new Window();

            window.Width = 350;
            window.Height = 150;

            ucDatePicker datePicker = new ucDatePicker();

            datePicker.confirmEvent += DatePicker_confirmEventAdd;

            datePicker.cancelEvent += DatePicker_cancelEvent;

            window.Content = datePicker;

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            window.Show();
        }

        private void btnDeleteDate_Click(object sender, RoutedEventArgs e)
        {
            var isOk = new Modal().ShowDialog();

            if (isOk == true)
            {
                var selectedRow = dgDate.SelectedItem;

                if (selectedRow != null)
                {
                    string date = (string)selectedRow.GetType().GetProperty("Date").GetValue(selectedRow, null);

                    int scheduleIndex = movie.ScheduleLst.FindIndex(item => ConvertString.ConvertDateToStringTwo(item.ReleaseDate) == date && item.CinemaType == this.cinemaType);

                    Directory.Delete(Path.getFolderDate(cinemaType, movie.Id, ConvertString.ConvertDateToStringOne(movie.ScheduleLst[scheduleIndex].ReleaseDate)), true);

                    movie.ScheduleLst.RemoveAt(scheduleIndex);

                    XmlFileManager.WriteMoviesXMLSchedule(movieVM.movieRepo.Items);

                    ReloadDataGridDate();

                    notifier.ShowSuccess("Xóa thành công!");
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            window = new Window();

            window.Width = 350;
            window.Height = 150;

            ucDatePicker datePicker = new ucDatePicker();

            var selectedRow = dgDate.SelectedItem;

            string date = (string)selectedRow.GetType().GetProperty("Date").GetValue(selectedRow, null);

            Schedule schedule = movie.ScheduleLst.Find(item => ConvertString.ConvertDateToStringTwo(item.ReleaseDate.Date) == date && item.CinemaType == this.cinemaType);

            datePicker.dpMain.SelectedDate = schedule.ReleaseDate;

            datePicker.confirmEvent += DatePicker_confirmEvent;

            datePicker.cancelEvent += DatePicker_cancelEvent;

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            window.Content = datePicker;

            window.Show();
        }

        private void DatePicker_cancelEvent(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private bool isExistDate(string date)
        {
            date = date.Replace('-', '/');

            foreach(dynamic item in dgDate.Items)
            {
                if (item.Date == date)
                    return true;
            }
            return false;
        }

        private void DatePicker_confirmEvent(object sender, RoutedEventArgs e)
        {
            if(isExistDate((sender as Button).Tag.ToString()))
            {
                ucDatePicker.notifier.ShowWarning("Ngày bị trùng!");
                return;
            }
            else
            {
                window.Close();

                var selectedRow = dgDate.SelectedItem;

                string date = (string)selectedRow.GetType().GetProperty("Date").GetValue(selectedRow, null);

                Schedule schedule = movie.ScheduleLst.Find(item => ConvertString.ConvertDateToStringTwo(item.ReleaseDate.Date) == date && item.CinemaType == this.cinemaType);

                Button button = sender as Button;

                Directory.Move(
                  Path.getFolderDate(cinemaType, movie.Id, ConvertString.ConvertDateToStringOne(schedule.ReleaseDate)),
                  Path.getFolderDate(cinemaType, movie.Id, button.Tag.ToString())
                  );

                schedule.ReleaseDate = new DateTime(
                    int.Parse(button.Tag.ToString().Split('-')[2]),
                    int.Parse(button.Tag.ToString().Split('-')[1]),
                    int.Parse(button.Tag.ToString().Split('-')[0])
                    );

                XmlFileManager.WriteMoviesXMLSchedule(movieVM.movieRepo.Items);

                ReloadDataGridDate();

                notifier.ShowSuccess("Cập nhật thành công!");
            }
        }

        private void DatePicker_confirmEventAdd(object sender, RoutedEventArgs e)
        {
            if (isExistDate((sender as Button).Tag.ToString()))
            {
                ucDatePicker.notifier.ShowWarning("Ngày bị trùng!");
                return;
            }
            else
            {
                window.Close();

                Button button = sender as Button;

                movie.ScheduleLst.Add(new Schedule(
                    new DateTime(
                    int.Parse(button.Tag.ToString().Split('-')[2]),
                    int.Parse(button.Tag.ToString().Split('-')[1]),
                    int.Parse(button.Tag.ToString().Split('-')[0])
                    ),
                    new List<TimeSpan>(),
                    cinemaType
                    ));

                XmlFileManager.WriteMoviesXMLSchedule(movieVM.movieRepo.Items);

                Directory.CreateDirectory(
                    Path.getFolderDate(
                        cinemaType, 
                        movie.Id,
                        (sender as Button).Tag.ToString()
                        )
                    );

                ReloadDataGridDate();

                notifier.ShowSuccess("Thêm thành công!");
            }
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            window = new Window();

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            window.Width = 1100;
            window.Height = 500;

            var selectedRow = dgDate.SelectedItem;

            string date = (string)selectedRow.GetType().GetProperty("Date").GetValue(selectedRow, null);

            int scheduleIndex = movie.ScheduleLst.FindIndex(item => ConvertString.ConvertDateToStringTwo(item.ReleaseDate) == date && item.CinemaType == this.cinemaType);

            ucShowtime ucShowtime = new ucShowtime(movieVM, movie, cinemaType, scheduleIndex);

            window.Content = ucShowtime;

            window.ShowDialog();
        }
    }
}
