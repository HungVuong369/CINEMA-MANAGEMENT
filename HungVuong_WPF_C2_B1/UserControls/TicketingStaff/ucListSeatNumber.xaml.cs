using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace HungVuong_WPF_C2_B1
{
    /// <summary>
    /// Interaction logic for ucListSeatNumber.xaml
    /// </summary>
    public partial class ucListSeatNumber : UserControl
    {
        private int SeatNumber = -1;
        private int indexCinema = -1;
        private int indexDate = -1;
        private int indexShowtime = -1;
        private Movie Movie;
        private MovieViewModel movieVM = new MovieViewModel();

        public delegate void ButtonClicked(object sender, RoutedEventArgs e);

        public static event ButtonClicked buttonClicked;

        public ucListSeatNumber()
        {
            InitializeComponent();
            ucListMovie.SeatClicked += UcListMovie_SeatClicked;

            ucBookingTicket.CinemaChange += UcBookingTicket_CinemaChange;
            ucBookingTicket.DateChange += UcBookingTicket_DateChange;
            ucBookingTicket.ShowtimeChange += UcBookingTicket_ShowtimeChange;
        }

        private void UcBookingTicket_ShowtimeChange(int index)
        {
            this.indexShowtime = index;

            if (indexCinema != -1 && indexDate != -1)
            {
                CreateSeats();
            }
        }

        private void UcBookingTicket_DateChange(int indexDate, int indexShowtime)
        {
            this.indexShowtime = indexShowtime;
            this.indexDate = indexDate;

            if (indexCinema != -1)
            {
                CreateSeats();
            }
        }

        private void UcBookingTicket_CinemaChange(int indexCinema, int indexDate, int indexShowtime)
        {
            this.indexCinema = indexCinema;
            this.indexDate = indexDate;
            this.indexShowtime = indexShowtime;

            CreateSeats();
        }

        private void UcListMovie_SeatClicked(int seatNumber)
        {
            this.SeatNumber = seatNumber;

            Movie = movieVM.movieRepo.Items[this.SeatNumber];
        }

        private void CreateSeats()
        {
            grdCinemaSeatsNumber.Children.Clear();

            Seat[][] seatList;
            Cinema cinema = null;
            if (SeatNumber == -1 || indexShowtime == -1 || indexDate == -1)
                return;
            cinema = movieVM.movieRepo.Items[SeatNumber].CinemaLst.Find(item => item.Name == movieVM.movieRepo.Items[SeatNumber].CinemaLst[indexCinema].Name);

            string hour = movieVM.movieRepo.Items[SeatNumber].ScheduleLst[indexDate].ShowtimeList[indexShowtime].Hours.ToString();
            string minute = movieVM.movieRepo.Items[SeatNumber].ScheduleLst[indexDate].ShowtimeList[indexShowtime].Minutes.ToString();

            if (minute.Length == 1)
                minute = "0" + minute;
            if (hour.Length == 1)
                hour = "0" + hour;

            string convert = hour + "-" + minute;

            movieVM.SeatLst = seatList = SeedData.getListSeatByPath(movieVM.movieRepo.Items[SeatNumber].Id.Replace(':', ' '),
                movieVM.movieRepo.Items[SeatNumber].ScheduleLst[indexDate].ReleaseDate.Date.ToString("dd-M-yyyy"),
                convert, movieVM, SeatNumber, indexCinema);
            int index = 0;
            for (int rowIndex = 0; rowIndex < seatList.GetLength(0); rowIndex++)
            {
                RowDefinition rowDef = new RowDefinition();

                rowDef.Height = new GridLength();

                grdCinemaSeatsNumber.RowDefinitions.Add(rowDef);

                for (int columnIndex = 0; columnIndex < seatList.GetLength(0); columnIndex++)
                {
                    ColumnDefinition colDef = new ColumnDefinition();
                    colDef.Width = new GridLength();

                    grdCinemaSeatsNumber.ColumnDefinitions.Add(colDef);

                    Button button1 = new Button();

                    if (seatList[rowIndex][columnIndex].Status == false)
                        button1.Background = Brushes.LightBlue;
                    else
                        button1.Background = Brushes.Red;

                    button1.Width = 60;
                    button1.Height = 50;
                    button1.Tag = (rowIndex) + " " + (columnIndex) + " " + (rowIndex * seatList.GetLength(0) + columnIndex);

                    button1.Content = index + 1;
                    index++;

                    button1.Margin = new Thickness(5);

                    button1.Click += new RoutedEventHandler(Button_Click);

                    Grid.SetRow(button1, rowIndex);
                    Grid.SetColumn(button1, columnIndex);

                    grdCinemaSeatsNumber.Children.Add(button1);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            buttonClicked?.Invoke(sender, e);
        }
    }
}
