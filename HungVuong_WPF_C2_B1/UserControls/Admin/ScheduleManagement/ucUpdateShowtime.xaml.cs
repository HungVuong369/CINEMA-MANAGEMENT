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
    /// Interaction logic for ucUpdateShowtime.xaml
    /// </summary>
    public partial class ucUpdateShowtime : UserControl
    {
        public delegate void CancelHandle(object sender, RoutedEventArgs e);
        public event CancelHandle CancelEvent;

        public delegate void ConfirmHandle(object sender, RoutedEventArgs e);
        public event ConfirmHandle ConfirmEvent;

        public static ucNotifier notifier;

        public ucUpdateShowtime()
        {
            InitializeComponent();
            notifier = new ucNotifier(this);
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmEvent != null)
            {
                (sender as Button).Tag = $"{txtHours.Text}:{txtMinutes.Text}";
                ConfirmEvent?.Invoke(sender, e);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (CancelEvent != null)
                CancelEvent?.Invoke(sender, e);
        }

        private bool isInvalid()
        {
            if (txtHours.Text == string.Empty || txtMinutes.Text == string.Empty)
                return true;

            int hours = int.Parse(txtHours.Text);
            int minutes = int.Parse(txtMinutes.Text);

            if (hours >= 24 || minutes >= 60 || hours < 0 || minutes < 0)
                return true;
            return false;
        }

        private void txtHours_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnConfirm.IsEnabled = !isInvalid();
        }

        private void txtMinutes_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnConfirm.IsEnabled = !isInvalid();
        }

        private void txtMinutes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!char.IsDigit(e.Text[0]))
                e.Handled = true;
        }

        private void txtHours_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
                e.Handled = true;
        }
    }
}
