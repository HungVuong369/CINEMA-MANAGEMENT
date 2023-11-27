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
    /// Interaction logic for ucOrderDetail.xaml
    /// </summary>
    public partial class ucOrderDetail : UserControl
    {
        public ucOrderDetail(XmlNodeList nodes)
        {
            InitializeComponent();

            foreach (XmlNode node in nodes)
            {
                dgOrderDetail.Items.Add(
                    new
                    {
                        SeatNumber = node.Attributes["SeatNumber"].Value,
                        Price = int.Parse(node.Attributes["Price"].Value).ToString("N0") + " VND",
                        Type = node.Attributes["CustomerType"].Value
                    });
            }
        }
    }
}
