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
    /// Interaction logic for ucDetailMovie.xaml
    /// </summary>
    public partial class ucDetailMovie : UserControl
    {
        private MovieViewModel movieVM = new MovieViewModel();

        public delegate void movieDetailHandle(int seatNumber);

        public static event movieDetailHandle movieDetailClicked;

        public ucDetailMovie()
        {
            InitializeComponent();
            ucListMovie.SeatClicked += UcListMovie_SeatClicked;
        }

        private string getContent(int seatNumber)
        {
            StringBuilder sb = new StringBuilder(movieVM.movieRepo.Items[seatNumber].Content);

            for (int i = 101; i < sb.Length; i += 102)
            {
                sb.Insert(i, "\n");
            }
            return sb.ToString();
        }

        private void UcListMovie_SeatClicked(int seatNumber)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(movieVM.movieRepo.Items[seatNumber].UrlImage, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttps;
            if (result)
                imgDetail.Source = new BitmapImage(new Uri(movieVM.movieRepo.Items[seatNumber].UrlImage));
            else
                imgDetail.Source = new BitmapImage(new Uri("https://cdn0.iconfinder.com/data/icons/ui-fill-glyphs/20/null-256.png"));
            tbNameDetail.Text = "Tên phim: " + movieVM.movieRepo.Items[seatNumber].Name;
            tbContentDetail.Text = "Nội dung: " + movieVM.movieRepo.Items[seatNumber].Content;
            tbRuntimeDetail.Text = "Thời lượng: " + movieVM.movieRepo.Items[seatNumber].Runtime + " Phút";
            movieDetailClicked?.Invoke(seatNumber);
        }
    }
}
