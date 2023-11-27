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

namespace HungVuong_WPF_C2_B1
{
    /// <summary>
    /// Interaction logic for ucListSeat.xaml
    /// </summary>
    public partial class ucListSeat : UserControl
    {
        private int SeatNumber = -1;
        private Movie Movie;
        private MovieViewModel movieVM = new MovieViewModel();
        private CinemaViewModel cinemaVM = new CinemaViewModel();

        public ucListSeat()
        {
            InitializeComponent();
            ucTicketPrice.TicketPriceViewClicked += UcTicketPrice_TicketPriceViewClicked;
            ucListMovie.SeatClicked += UcListMovie_SeatClicked;

            cbCinema.DisplayMemberPath = "Name";
            cbCinema.SelectedValuePath = "Name";
        }

        private void UcListMovie_SeatClicked(int seatNumber)
        {
            this.SeatNumber = seatNumber;

            cbCinema.ItemsSource = movieVM.movieRepo.Items[this.SeatNumber].CinemaLst;

            Movie = movieVM.movieRepo.Items[this.SeatNumber];

            cbCinema.SelectedIndex = 0;
        }

        private void UcTicketPrice_TicketPriceViewClicked(object sender, RoutedEventArgs e)
        {
            spPriceView.Visibility = Visibility.Visible;
        }

        private void CreateSeats(string value)
        {
            if(string.Compare(value, "Id") == 0)
                grdCinemaSeatsId.Children.Clear();
            else
                grdCinemaSeatsNumber.Children.Clear();

            Seat[][] seatList;
            Cinema cinema = null;

            cinema = movieVM.movieRepo.Items[SeatNumber].CinemaLst.Find(item => item.Name == cbCinema.SelectedValue.ToString());

            seatList = cinema.seats;

            int index = 0;
            for (int rowIndex = 0; rowIndex < seatList.GetLength(0); rowIndex++)
            {
                RowDefinition rowDef = new RowDefinition();

                rowDef.Height = new GridLength();

                if (string.Compare(value, "Id") == 0)
                    grdCinemaSeatsId.RowDefinitions.Add(rowDef);
                else
                    grdCinemaSeatsNumber.RowDefinitions.Add(rowDef);

                for (int columnIndex = 0; columnIndex < seatList.GetLength(0); columnIndex++)
                {
                    ColumnDefinition colDef = new ColumnDefinition();
                    colDef.Width = new GridLength();

                    if (string.Compare(value, "Id") == 0)
                        grdCinemaSeatsId.ColumnDefinitions.Add(colDef);
                    else
                        grdCinemaSeatsNumber.ColumnDefinitions.Add(colDef);

                    Button button1 = new Button();

                    if (seatList[rowIndex][columnIndex].Status == false)
                        button1.Background = Brushes.LightBlue;
                    else
                        button1.Background = Brushes.Red;

                    button1.Width = 60;
                    button1.Height = 50;
                    button1.Tag = (rowIndex) + " " + (columnIndex) + " " + (rowIndex * seatList.GetLength(0) + columnIndex);

                    if (string.Compare(value, "Id") == 0)
                        button1.Content = seatList[rowIndex][columnIndex].Id;
                    else
                    {
                        button1.Content = index + 1;
                        index++;
                    }

                    button1.Margin = new Thickness(5);
                    button1.Padding = new Thickness(0);

                    button1.Click += new RoutedEventHandler(Button_Click);

                    Grid.SetRow(button1, rowIndex);
                    Grid.SetColumn(button1, columnIndex);

                    if (string.Compare(value, "Id") == 0)
                        grdCinemaSeatsId.Children.Add(button1);
                    else
                    {
                        grdCinemaSeatsNumber.Children.Add(button1);
                    }
                }
            }
        }

        private string getStatus(bool status)
        {
            if (status)
                return "Không còn trống";
            return "Còn trống";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int row = int.Parse((sender as Button).Tag.ToString().Split(' ')[0]);
            int column = int.Parse((sender as Button).Tag.ToString().Split(' ')[1]);
            int indexSeat = int.Parse((sender as Button).Tag.ToString().Split(' ')[2]);

            double price = cinemaVM.getPrice(row);

            var button1 = grdCinemaSeatsId.Children[indexSeat] as Button;
            var button2 = grdCinemaSeatsNumber.Children[indexSeat] as Button;

            Brush savedBackground1 = button1.Background;
            Brush savedBackground2 = button2.Background;

            button1.Background = Brushes.Wheat;
            button2.Background = Brushes.Wheat;

            if (cinemaVM.cinema.Type == "Vip")
            {
                dynamic cinemaVip = cinemaVM.cinema;
                MessageBox.Show($"Tên: {cinemaVM.cinema.seats[row][column].Name}\nMã: {cinemaVM.cinema.seats[row][column].Id}\nTrạng thái: {getStatus(cinemaVM.cinema.seats[row][column].Status)}\nGiá vé: {(price + cinemaVip.surchargeValue).ToString("N0")} VND", "Chi tiết");
            }
            else
                MessageBox.Show($"Tên: {cinemaVM.cinema.seats[row][column].Name}\nMã: {cinemaVM.cinema.seats[row][column].Id}\nTrạng thái: {getStatus(cinemaVM.cinema.seats[row][column].Status)}\nGiá vé: {price.ToString("N0")} VND", "Chi tiết");
            
            button1.Background = savedBackground1;
            button2.Background = savedBackground2;
        }

        private void cbCinema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCinema.SelectedIndex == -1)
                return;
            cinemaVM.cinema = movieVM.movieRepo.Items[SeatNumber].CinemaLst[cbCinema.SelectedIndex];
            CreateSeats("Id");
            CreateSeats("Number");
        }
    }
}
