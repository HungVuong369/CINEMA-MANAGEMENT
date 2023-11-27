using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HungVuong_WPF_C2_B1
{
    /// <summary>
    /// Interaction logic for ucDatePicker.xaml
    /// </summary>
    public partial class ucDatePicker : UserControl
    {
        public delegate void ConfirmHandle(object sender, RoutedEventArgs e);
        public event ConfirmHandle confirmEvent;

        public delegate void CancelHandle(object sender, RoutedEventArgs e);
        public event CancelHandle cancelEvent;

        public static ucNotifier notifier;

        public ucDatePicker()
        {
            InitializeComponent();
            notifier = new ucNotifier(this);
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (dpMain.SelectedDate != null)
            {
                DateTime selectedDate = (DateTime)dpMain.SelectedDate;

                (sender as Button).Tag = ConvertString.ConvertDateToStringOne(selectedDate);

                if (confirmEvent != null)
                    confirmEvent?.Invoke(sender, e);
            }
            else
                notifier.ShowWarning("Vui lòng chọn ngày!");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (cancelEvent != null)
                cancelEvent?.Invoke(sender, e);
        }
    }
}
