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
using System.Xml;

namespace HungVuong_WPF_C2_B1
{
    /// <summary>
    /// Interaction logic for ucStatistics.xaml
    /// </summary>
    public partial class ucStatistics : UserControl
    {
        private CinemaViewModel _cinemaVM;

        private XmlNodeList Nodes;

        public ucStatistics()
        {
            InitializeComponent();

            _cinemaVM = new CinemaViewModel();

            cbCinema.ItemsSource = _cinemaVM.cinemaRepo.Items;

            cbCinema.DisplayMemberPath = "Type";
            cbCinema.SelectedValuePath = "Type";

            cbCinema.SelectedIndex = 0;

            DatePicker.SelectedDate = DateTime.Now;
        }

        private void ShowDataGrid()
        {
            dgOrder.Items.Clear();

            XmlDocument doc = new XmlDocument();
            doc.Load(Path.OrdersXml);

            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.MinValue;

            XmlNodeList nodes = doc.SelectNodes(Path.getOrderByCinemaTypeAndDate(cbCinema.SelectedValue.ToString(), ConvertString.ConvertDateToStringTwo(selectedDate)));

            if (nodes.Count == 0)
                return;

            this.Nodes = nodes;
            int index = 0;
            foreach (XmlNode node in nodes)
            {
                dgOrder.Items.Add(
                    new {
                        Index = index++,
                        CustomerName = node.Attributes["CustomerName"].Value,
                        CustomerPhone = node.Attributes["CustomerPhone"].Value,
                        CinemaType = node.Attributes["CinemaType"].Value,
                        Date = node.Attributes["Date"].Value,
                        Showtime = node.Attributes["Showtime"].Value,
                        TotalPrice = int.Parse(node.Attributes["TotalPrice"].Value).ToString("N0") + " VND",
                        TotalSeat = node.Attributes["TotalSeat"].Value
                    });
            }

            doc.Save(Path.OrdersXml);
        }

        private void cbCinema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePicker.SelectedDate == null)
                return;
            ShowDataGrid();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowDataGrid();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dgOrder.SelectedItem;

            int index = (int)selectedRow.GetType().GetProperty("Index").GetValue(selectedRow, null);

            ucOrderDetail ucOrderdetail = new ucOrderDetail(Nodes[index].ChildNodes);

            Window window = new Window();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            window.Content = ucOrderdetail;

            window.ShowDialog();
        }
    }
}
