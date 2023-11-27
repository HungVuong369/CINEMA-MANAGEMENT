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
    /// Interaction logic for ucTicketPrice.xaml
    /// </summary>
    public partial class ucTicketPrice : UserControl
    {
        private int SeatNumber;
        private MovieViewModel movieVM = new MovieViewModel();
        public delegate void TicketPriceViewHandler(object sender, RoutedEventArgs e);
        public static event TicketPriceViewHandler TicketPriceViewClicked;

        public ucTicketPrice()
        {
            InitializeComponent();
            ucListMovie.SeatClicked += UcListMovie_SeatClicked;
            ucDetailMovie.movieDetailClicked += UcDetailMovie_movieDetailClicked; ;
        }

        private void UcDetailMovie_movieDetailClicked(int seatNumber)
        {
            bdrDetail.Visibility = Visibility.Visible;
        }

        private void UcListMovie_SeatClicked(int seatNumber)
        {
            ucLstSeat.Visibility = Visibility.Collapsed;
            lbHeader.Content = "Chi tiết phim";
            this.SeatNumber = seatNumber;
        }

        private void btnTicketPriceView_Click(object sender, RoutedEventArgs e)
        {
            bdrDetail.Visibility = Visibility.Collapsed;
            lbHeader.Content = "Xem giá vé của phim " + movieVM.movieRepo.Items[SeatNumber].Name;
            TicketPriceViewClicked?.Invoke(sender, e);
        }
    }
}
