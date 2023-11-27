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
    /// Interaction logic for ucListMovie.xaml
    /// </summary>
    public partial class ucListMovie : UserControl
    {
        private MovieViewModel movieVM = new MovieViewModel();

        public delegate void SeatClickHandler(int seatNumber);

        public static event SeatClickHandler SeatClicked;

        public ucListMovie()
        {
            InitializeComponent();
            lvMovies.ItemsSource = movieVM.GetFilteredMoviesWithSchedule();
        }

        private void lvMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvMovies.SelectedItem != null)
            {
                var selectedItem = lvMovies.SelectedItem as Movie;
                int seatNumber = movieVM.movieRepo.Items.FindIndex(item => item.Id == selectedItem.Id);
                SeatClicked?.Invoke(seatNumber);
                lvMovies.SelectedIndex = -1;
            }
        }
    }
}
