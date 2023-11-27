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
    /// Interaction logic for ucAddSchedule.xaml
    /// </summary>
    public partial class ucAddSchedule : UserControl
    {
        public delegate void AddHandle(object sender, RoutedEventArgs e);

        public event AddHandle AddEvent;

        public delegate void CancelHandle(object sender, RoutedEventArgs e);

        public event CancelHandle CancelEvent;

        public ucAddSchedule(List<Movie> movies)
        {
            InitializeComponent();
            cbScheduleMovie.ItemsSource = movies;
            cbScheduleMovie.DisplayMemberPath = "Name";

            cbScheduleMovie.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).Tag = cbScheduleMovie.SelectedIndex;
            AddEvent?.Invoke(sender, e);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CancelEvent?.Invoke(sender, e);
        }
    }
}
