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
    /// Interaction logic for ucDetailBooking.xaml
    /// </summary>
    public partial class ucDetailBooking : UserControl
    {
        public delegate void ConfirmHandler(object sender, RoutedEventArgs e);

        public event ConfirmHandler ConfirmClicked;

        public delegate void CancelHandler(object sender, RoutedEventArgs e);

        public event CancelHandler CancelClicked;

        private DetailBooking _detailBooking;

        private double totalPrice = 0;

        private int totalSeat = 0;

        public ucDetailBooking(DetailBooking detailBooking)
        {
            InitializeComponent();

            this._detailBooking = detailBooking;

            tbCinemaType.Text = detailBooking.CinemaName;
            tbCustomerName.Text = detailBooking.CustomerName;
            tbCustomerPhone.Text = detailBooking.CustomerPhone;
            tbDate.Text = detailBooking.Date;
            tbShowtime.Text = detailBooking.Showtime;

            CinemaViewModel cinemaVM = new CinemaViewModel();

            Button button = null;
            double total = 0;

            for(int index = 0; index < detailBooking.SelectedButton.Count; index++)
            {
                button = detailBooking.SelectedButton[index];
                int seatNumber = int.Parse(button.Tag.ToString().Split(' ')[2]);
                total += detailBooking.LstPrice[index];
                dgListTicket.Items.Add(new { SeatNumber = seatNumber + 1, Price = detailBooking.LstPrice[index].ToString("N0") + " VND", Type = detailBooking.Types[index] });
            }

            this.totalPrice = total;
            this.totalSeat = detailBooking.SelectedButton.Count;

            tbTotalSeat.Text = detailBooking.SelectedButton.Count + " Ghế";
            tbTotalPrice.Text = total.ToString("N0") + " VND";
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            object obj = _detailBooking;
            ConfirmClicked?.Invoke(obj, e);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CancelClicked?.Invoke(sender, e);
        }
    }
}
