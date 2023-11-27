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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace HungVuong_WPF_C2_B1
{
    /// <summary>
    /// Interaction logic for ucBookingTicket.xaml
    /// </summary>
    public partial class ucBookingTicket : UserControl
    {
        private Window _DetailForm = new Window();

        private List<Button> _selectedButtons;

        private Button _selectedButton;

        public CinemaViewModel cinemaVM = new CinemaViewModel();

        private MovieViewModel movieVM = new MovieViewModel();

        public delegate void GetIndexCinema(int indexCinema, int indexDate, int indexShowtime);

        public static event GetIndexCinema CinemaChange;

        public delegate void GetIndexDate(int indexDate, int indexShowtime);

        public static event GetIndexDate DateChange;

        public delegate void GetIndexShowtime(int index);

        public static event GetIndexShowtime ShowtimeChange;

        private bool _isAdd = false;

        private int SeatNumber;

        private Notifier notifier;

        public ucBookingTicket()
        {
            InitializeComponent();

            ucListMovie.SeatClicked += UcListMovie_SeatClicked;

            cbCinemaBooking.DisplayMemberPath = "Name";
            cbCinemaBooking.SelectedValuePath = "Type";

            TicketingStaffWindow.btnBookingClicked += TicketingStaffWindow_btnBookingClicked;

            notifier = new Notifier(cfg =>
            {
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(3));

                cfg.Dispatcher = Application.Current.Dispatcher;

                var positionProvider = new WindowPositionProvider(
                    parentWindow: Window.GetWindow(this),
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);
                cfg.PositionProvider = positionProvider;
            });
        }

        private void ReloadCbDate()
        {
            cbDateBooking.Items.Clear();

            foreach (var itemDate in movieVM.movieRepo.Items[SeatNumber].ScheduleLst)
            {
                if (cbCinemaBooking.SelectedValue == null)
                    continue;
                if (itemDate.CinemaType == cbCinemaBooking.SelectedValue.ToString()
                    && itemDate.ShowtimeList.Count != 0
                    )
                    cbDateBooking.Items.Add(itemDate.ReleaseDate.Day + "/" + itemDate.ReleaseDate.Month + "/" + itemDate.ReleaseDate.Year);
            }
            cbDateBooking.SelectedIndex = 0;
        }

        private void ReloadCbShowtime()
        {
            cbShowtimeBooking.Items.Clear();
            if (cbDateBooking.SelectedIndex == -1)
                return;
            dynamic str = cbDateBooking.Items[cbDateBooking.SelectedIndex];
            string cinemaType = movieVM.movieRepo.Items[SeatNumber].CinemaLst[cbCinemaBooking.SelectedIndex].Type;

            Schedule schedule = movieVM.movieRepo.Items[SeatNumber].ScheduleLst.Find(
                item => ConvertString.ConvertDateToStringTwo(item.ReleaseDate) == str && item.CinemaType == cinemaType
                );
            if (schedule == null)
                return;

            foreach (var item in schedule.ShowtimeList)
                cbShowtimeBooking.Items.Add(ConvertString.ConvertHourAndMinuteToStringOne(item.Hours, item.Minutes));

            cbShowtimeBooking.SelectedIndex = 0;
        }

        private void TicketingStaffWindow_btnBookingClicked(object sender, RoutedEventArgs e)
        {
            spBooking.Visibility = Visibility.Collapsed;
            bdrDetail.Visibility = Visibility.Collapsed;
            ucLstSeat.Visibility = Visibility.Visible;
            lbHeader.Content = "Danh sách phim";
            resetBooking();
        }

        private bool isExistButton(Button button)
        {
            foreach (var item in _selectedButtons)
            {
                if (string.Compare(item.Content.ToString(), button.Content.ToString()) == 0)
                    return true;
            }
            return false;
        }

        private void UcListSeatNumber_buttonClicked(object sender, RoutedEventArgs e)
        {
            int row = int.Parse((sender as Button).Tag.ToString().Split(' ')[0]);
            int column = int.Parse((sender as Button).Tag.ToString().Split(' ')[1]);
            int indexSeat = int.Parse((sender as Button).Tag.ToString().Split(' ')[2]);

            if (movieVM.SeatLst != null && movieVM.SeatLst[row][column].Status == true)
            {
                notifier.ShowWarning("Ghế này hiện không còn trống!");
                return;
            }
            if (cbCinemaBooking.SelectedIndex != -1)
            {
                if (!isExistButton(sender as Button))
                {
                    BdrInfoCustomer.Visibility = Visibility.Visible;
                    spDetailBooking.Visibility = Visibility.Visible;
                    spBooking.Visibility = Visibility.Visible;

                    cinemaVM.quantitySeats++;
                    cinemaVM.cinema = movieVM.movieRepo.Items[SeatNumber].CinemaLst[cbCinemaBooking.SelectedIndex];

                    cinemaVM.totalPrice += cinemaVM.getPrice(row);

                    if (cinemaVM.cinema.Type == "Vip")
                        cinemaVM.totalPrice += (cinemaVM.cinema as CinemaVip).surchargeValue;

                    tbQuantitySeats.Text = "Số lượng ghế: " + cinemaVM.quantitySeats;

                    tbTotal.Text = "Tổng tiền: " + cinemaVM.totalPrice.ToString("N0") + " VND";

                    if (_selectedButton != null)
                        _selectedButton.Background = Brushes.Wheat;

                    (sender as Button).Background = Brushes.Violet;

                    _selectedButtons.Add(sender as Button);

                    cbTypeAge.SelectedIndex = 0;

                    cinemaVM.lstType.Add(indexSeat, cbTypeAge.SelectedIndex);

                    cinemaVM.types.Add(cbTypeAge.SelectedValue.ToString().Split(':')[1]);

                    cinemaVM.lstPrice.Add(cinemaVM.getPrice(row));

                    _selectedButton = sender as Button;

                    cinemaVM.selectedSeats.Add(movieVM.SeatLst[row][column]);
                }
                else
                {
                    if (_selectedButton != null)
                        _selectedButton.Background = Brushes.Wheat;
                    (sender as Button).Background = Brushes.Violet;
                    _selectedButton = sender as Button;
                    cbTypeAge.SelectedIndex = cinemaVM.lstType[indexSeat];
                }
            }
        }

        private int getDateIndex()
        {
            dynamic str = cbDateBooking.Items[cbDateBooking.SelectedIndex];
            string cinemaType = movieVM.movieRepo.Items[SeatNumber].CinemaLst[cbCinemaBooking.SelectedIndex].Type;

            int dateIndex = movieVM.movieRepo.Items[SeatNumber].ScheduleLst.FindIndex(
                item => ConvertString.ConvertDateToStringTwo(item.ReleaseDate) == str && item.CinemaType == cinemaType
                );
            return dateIndex;
        }

        private void UcListMovie_SeatClicked(int seatNumber)
        {
            ucLstSeat.Visibility = Visibility.Collapsed;
            bdrDetail.Visibility = Visibility.Visible;
            lbHeader.Content = "Chi tiết phim";
            this.SeatNumber = seatNumber;

            cbCinemaBooking.ItemsSource = movieVM.movieRepo.Items[SeatNumber].CinemaLst;

            cbCinemaBooking.SelectedIndex = 0;

            ReloadCbDate();

            ReloadCbShowtime();

            getListSeat();

            CinemaChange?.Invoke(cbCinemaBooking.SelectedIndex, getDateIndex(), cbShowtimeBooking.SelectedIndex);
        }

        private void getListSeat()
        {
            if (cbCinemaBooking.SelectedIndex == -1 || cbDateBooking.SelectedIndex == -1 || cbShowtimeBooking.SelectedIndex == -1)
                return;

            dynamic str = cbDateBooking.Items[cbDateBooking.SelectedIndex];
            string cinemaType = movieVM.movieRepo.Items[SeatNumber].CinemaLst[cbCinemaBooking.SelectedIndex].Type;

            Schedule schedule = movieVM.movieRepo.Items[SeatNumber].ScheduleLst.Find(
                item => ConvertString.ConvertDateToStringTwo(item.ReleaseDate) == str && item.CinemaType == cinemaType
                );

            string hour = schedule.ShowtimeList[cbShowtimeBooking.SelectedIndex].Hours.ToString();
            string minute = schedule.ShowtimeList[cbShowtimeBooking.SelectedIndex].Minutes.ToString();

            if (minute.Length == 1)
                minute = "0" + minute;
            if (hour.Length == 1)
                hour = "0" + hour;

            string convert = hour + "-" + minute;

            movieVM.SeatLst = SeedData.getListSeatByPath(movieVM.movieRepo.Items[SeatNumber].Id.Replace(':', ' '),
                ConvertString.ConvertDateToStringOne(schedule.ReleaseDate),
                convert, movieVM, SeatNumber, cbCinemaBooking.SelectedIndex);
        }

        private void btnBoonkingTicket_Click(object sender, RoutedEventArgs e)
        {
            lbHeader.Content = "Đặt vé";
            spBooking.Visibility = Visibility.Visible;
            bdrDetail.Visibility = Visibility.Collapsed;

            cbCinemaBooking.ItemsSource = movieVM.movieRepo.Items[SeatNumber].CinemaLst;
            cbCinemaBooking.SelectedIndex = 0;

            _selectedButtons = new List<Button>();
            cinemaVM.lstType = new Dictionary<int, int>();
            cinemaVM.types = new List<string>();
            cinemaVM.lstPrice = new List<double>();

            ucListSeatNumber.buttonClicked += UcListSeatNumber_buttonClicked;

            cinemaVM.totalPrice = 0;
            cinemaVM.quantitySeats = 0;
        }

        private void cbCinemaBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex != -1)
            {
                if (cbDateBooking.SelectedIndex == -1)
                    return;

                int dateIndex = getDateIndex();

                CinemaChange?.Invoke(cbCinemaBooking.SelectedIndex, dateIndex, cbShowtimeBooking.SelectedIndex);
                ReloadCbDate();
                ReloadCbShowtime();
                getListSeat();
                resetBooking();
            }
        }

        private void cbDateBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReloadCbShowtime();
            getListSeat();

            if (cbDateBooking.SelectedIndex == -1)
                return;

            int dateIndex = getDateIndex();

            DateChange?.Invoke(dateIndex, cbShowtimeBooking.SelectedIndex);

            resetBooking();
        }

        private void cbShowtimeBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getListSeat();

            ShowtimeChange?.Invoke(cbShowtimeBooking.SelectedIndex);

            resetBooking();
        }

        private void cbTypeAge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_selectedButton != null && _isAdd)
            {
                int indexSeat = int.Parse(_selectedButton.Tag.ToString().Split(' ')[2]);

                double price = 0;

                if (cinemaVM.lstType[indexSeat] == 1 && cbTypeAge.SelectedIndex == 0 && cinemaVM.cinema.Type != "Vip")
                {
                    price = cinemaVM.getPrice(int.Parse(_selectedButton.Tag.ToString().Split(' ')[0]));
                    cinemaVM.totalPrice -= cinemaVM.getPrice(int.Parse(_selectedButton.Tag.ToString().Split(' ')[0])) * cinemaVM.cinema.percentOff;
                    cinemaVM.totalPrice += cinemaVM.getPrice(int.Parse(_selectedButton.Tag.ToString().Split(' ')[0]));
                }
                else if (cinemaVM.lstType[indexSeat] == 1 && cbTypeAge.SelectedIndex == 1 && cinemaVM.cinema.Type != "Vip")
                {
                    cinemaVM.totalPrice -= cinemaVM.getPrice(int.Parse(_selectedButton.Tag.ToString().Split(' ')[0])) * cinemaVM.cinema.percentOff;
                }
                else if (cinemaVM.lstType[indexSeat] == 0 && cbTypeAge.SelectedIndex == 1 && cinemaVM.cinema.Type != "Vip")
                {
                    price = cinemaVM.getPrice(int.Parse(_selectedButton.Tag.ToString().Split(' ')[0])) * cinemaVM.cinema.percentOff;
                    cinemaVM.totalPrice -= cinemaVM.getPrice(int.Parse(_selectedButton.Tag.ToString().Split(' ')[0]));
                    cinemaVM.totalPrice += cinemaVM.getPrice(int.Parse(_selectedButton.Tag.ToString().Split(' ')[0])) * cinemaVM.cinema.percentOff;
                }

                tbTotal.Text = "Tổng tiền: " + cinemaVM.totalPrice.ToString("N0") + " VND";

                cinemaVM.lstType[indexSeat] = cbTypeAge.SelectedIndex;
                int index = this._selectedButtons.FindIndex(item => item == _selectedButton);
                cinemaVM.types[index] = cbTypeAge.SelectedValue.ToString().Split(':')[1];
                cinemaVM.lstPrice[index] = price;

                _isAdd = false;
            }
        }

        private void cbTypeAge_DropDownOpened(object sender, EventArgs e)
        {
            _isAdd = true;
        }

        private void cbTypeAge_DropDownClosed(object sender, EventArgs e)
        {
            _isAdd = false;
        }

        private void resetBooking()
        {
            cinemaVM.selectedSeats = new List<Seat>();
            cinemaVM.lstType = new Dictionary<int, int>();

            cinemaVM.types = new List<string>();
            cinemaVM.lstPrice = new List<double>();

            _selectedButton = null;
            _selectedButtons = new List<Button>();
            cinemaVM.quantitySeats = 0;
            cinemaVM.totalPrice = 0;

            txtCustomerName.Text = "";
            txtCustomerPhone.Text = "";

            BdrInfoCustomer.Visibility = Visibility.Collapsed;
            spDetailBooking.Visibility = Visibility.Collapsed;
        }

        private void btnDeselectionSeat_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedButton != null)
            {
                int row = int.Parse(_selectedButton.Tag.ToString().Split(' ')[0]);
                int column = int.Parse(_selectedButton.Tag.ToString().Split(' ')[1]);
                int indexSeat = int.Parse(_selectedButton.Tag.ToString().Split(' ')[2]);

                cinemaVM.selectedSeats.Remove(movieVM.SeatLst[row][column]);

                cinemaVM.quantitySeats--;

                if (cinemaVM.lstType.ContainsKey(indexSeat))
                {
                    if (cinemaVM.lstType[indexSeat] == 1 && cinemaVM.cinema.Type == "Standard")
                        cinemaVM.totalPrice -= cinemaVM.getPrice(row) * cinemaVM.cinema.percentOff;
                    else
                        cinemaVM.totalPrice -= cinemaVM.getPrice(row);
                    if (cinemaVM.cinema.Type == "Vip")
                        cinemaVM.totalPrice -= (cinemaVM.cinema as CinemaVip).surchargeValue;


                    tbTotal.Text = "Tổng tiền: " + cinemaVM.totalPrice.ToString("N0") + " VND";
                    tbQuantitySeats.Text = "Số lượng ghế: " + cinemaVM.quantitySeats;

                    _selectedButton.Background = Brushes.LightBlue;

                    cinemaVM.lstType.Remove(indexSeat);

                    int index = this._selectedButtons.FindIndex(item => item == _selectedButton);

                    cinemaVM.types.RemoveAt(index);
                    cinemaVM.lstPrice.RemoveAt(index);

                    _selectedButtons.Remove(_selectedButton);

                    _selectedButton = null;

                    if (cinemaVM.quantitySeats == 0)
                    {
                        BdrInfoCustomer.Visibility = Visibility.Collapsed;
                        spDetailBooking.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
                notifier.ShowWarning("Vui lòng chọn ghế trước khi bỏ chọn ghế!");
        }

        private void btnBookingTicket_Click(object sender, RoutedEventArgs e)
        {
            if (txtCustomerName.Text == string.Empty || txtCustomerPhone.Text == string.Empty)
            {
                notifier.ShowWarning("Vui lòng nhập đầy đủ thông tin khách hàng!");
                return;
            }
            else
            {
                _DetailForm = new Window();

                ucDetailBooking ucDetail = new ucDetailBooking(
                    new DetailBooking(
                        txtCustomerName.Text,
                        txtCustomerPhone.Text,
                        cbCinemaBooking.Text,
                        cbDateBooking.Text,
                        cbShowtimeBooking.Text,
                        cinemaVM.lstPrice,
                        cinemaVM.types,
                        this._selectedButtons
                        )
                    );

                ucDetail.CancelClicked += UcDetail_CancelClicked;
                ucDetail.ConfirmClicked += UcDetail_ConfirmClicked;

                _DetailForm.Content = ucDetail;

                _DetailForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                _DetailForm.Width = 1100;

                _DetailForm.ShowDialog();
            }
        }

        private void UcDetail_ConfirmClicked(object sender, RoutedEventArgs e)
        {
            DetailBooking detailBooking = sender as DetailBooking;

            Button button = null;
            double total = 0;

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            for (int index = 0; index < detailBooking.SelectedButton.Count; index++)
            {
                button = detailBooking.SelectedButton[index];
                int seatNumber = int.Parse(button.Tag.ToString().Split(' ')[2]);
                total += detailBooking.LstPrice[index];
                OrderDetail orderDetail = new OrderDetail(seatNumber + 1, detailBooking.LstPrice[index], detailBooking.Types[index]);
                orderDetails.Add(orderDetail);
            }

            Order order = new Order(
                new CustomerInfo(detailBooking.CustomerName, detailBooking.CustomerPhone),
                movieVM.movieRepo.Items[SeatNumber].CinemaLst[cbCinemaBooking.SelectedIndex].Type,
                ConvertString.ConvertDateToStringTwo(DateTime.Now),
                detailBooking.Showtime,
                total,
                detailBooking.SelectedButton.Count
            );

            XmlFileManager.WriteOrders(order, orderDetails);

            foreach (var item in _selectedButtons)
            {
                int row = int.Parse(item.Tag.ToString().Split(' ')[0]);
                int column = int.Parse(item.Tag.ToString().Split(' ')[1]);

                movieVM.SeatLst[row][column].Status = true;
                item.Background = Brushes.Red;
            }

            dynamic str = cbDateBooking.Items[cbDateBooking.SelectedIndex];
            string cinemaType = movieVM.movieRepo.Items[SeatNumber].CinemaLst[cbCinemaBooking.SelectedIndex].Type;

            Schedule schedule = movieVM.movieRepo.Items[SeatNumber].ScheduleLst.Find(
                item => ConvertString.ConvertDateToStringTwo(item.ReleaseDate) == str && item.CinemaType == cinemaType
                );

            SeedData.WriteFile(movieVM, movieVM.SeatLst, SeatNumber, cbCinemaBooking.SelectedIndex, schedule, cbShowtimeBooking.SelectedIndex);
            resetBooking();
            _DetailForm.Close();
            notifier.ShowSuccess("Đặt vé thành công!");
        }

        private void UcDetail_CancelClicked(object sender, RoutedEventArgs e)
        {
            _DetailForm.Close();
            notifier.ShowSuccess("Hủy đặt vé thành công!");
        }

        private void btnDeselectionSeats_Click(object sender, RoutedEventArgs e)
        {
            txtCustomerName.Text = "";
            txtCustomerPhone.Text = "";

            for (int index = _selectedButtons.Count - 1; index >= 0; index--)
            {
                this._selectedButton = _selectedButtons[index];
                btnDeselectionSeat_Click(sender, e);
            }
        }
    }
}