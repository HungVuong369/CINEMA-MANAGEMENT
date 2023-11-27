using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HungVuong_WPF_C2_B1
{
    public class XmlFileManager
    {
        private static XmlDocument doc = new XmlDocument();

        public static void UpdateSeatById(string Id, int status)
        {
            doc.Load(Path.CinemasXml);

            XmlNode seatNode = doc.SelectSingleNode(Path.getSeatById(Id));

            if (seatNode != null)
            {
                XmlAttribute statusAttr = seatNode.Attributes["Status"];
                if (statusAttr != null)
                    statusAttr.Value = status.ToString();
            }
            doc.Save(Path.CinemasXml);
        }

        public static void WriteAccountsXML(List<Account> accounts)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlElement xml = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, xml);

            XmlElement root = doc.CreateElement("Accounts");
            doc.AppendChild(root);

            XmlElement admin = doc.CreateElement("Account");
            admin.SetAttribute("Username", "admin");
            admin.SetAttribute("Password", "admin");
            admin.SetAttribute("Priority", "0");
            root.AppendChild(admin);

            foreach (var item in accounts)
            {
                XmlElement itemNode = doc.CreateElement("Account");
                itemNode.SetAttribute("Username", item.username);
                itemNode.SetAttribute("Password", item.password);
                itemNode.SetAttribute("Priority", item.priority.ToString());
                root.AppendChild(itemNode);
            }
            doc.Save(Path.AccountsXml);
        }

        private static List<string> getCinemaTypeInCinemaLst(List<Cinema> cinemas)
        {
            List<string> strings = new List<string>();
            foreach (var cinema in cinemas)
            {
                strings.Add(cinema.Type);
            }
            return strings;
        }

        private static List<Schedule> GetSchedule(List<Schedule> schedules, string cinemaType)
        {
            List<Schedule> lstSchedule = new List<Schedule>();
            List<string> strings = new List<string>();

            foreach (Schedule schedule in schedules)
            {
                if(string.Compare(cinemaType, schedule.CinemaType) == 0)
                {
                    lstSchedule.Add(schedule);
                    strings.Add(schedule.CinemaType);
                }
            }

            return lstSchedule;
        }

        public static void WriteMoviesXMLSchedule(List<Movie> movies)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlElement xml = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, xml);

            XmlElement root = doc.CreateElement("Movies");
            doc.AppendChild(root);

            foreach (var item in movies)
            {
                XmlElement movieNode = doc.CreateElement("Movie");
                movieNode.SetAttribute("Id", item.Id);
                movieNode.SetAttribute("Name", item.Name);
                movieNode.SetAttribute("Content", item.Content);
                movieNode.SetAttribute("runtime", item.Runtime.ToString());
                movieNode.SetAttribute("Url", item.UrlImage);

                foreach(string cinemaType in getCinemaTypeInCinemaLst(item.CinemaLst))
                {
                    XmlElement cinemaNode = doc.CreateElement("Cinema");
                    cinemaNode.SetAttribute("Type", cinemaType);

                    foreach (var schedule in GetSchedule(item.ScheduleLst, cinemaType))
                    {
                        XmlElement dateNode = doc.CreateElement("Date");
                        dateNode.SetAttribute("Date", schedule.ReleaseDate.Day + "/" + schedule.ReleaseDate.Month + "/" + schedule.ReleaseDate.Year);

                        foreach (var showtime in schedule.ShowtimeList)
                        {
                            XmlElement showtimeNode = doc.CreateElement("Showtime");

                            string hour = showtime.Hours.ToString();
                            string minute = showtime.Minutes.ToString();

                            if (hour.Length == 1)
                                hour = "0" + hour;
                            if (minute.Length == 1)
                                minute = "0" + minute;

                            showtimeNode.SetAttribute("Hour", hour);
                            showtimeNode.SetAttribute("Minute", minute);

                            dateNode.AppendChild(showtimeNode);
                        }
                        cinemaNode.AppendChild(dateNode);
                    }
                    movieNode.AppendChild(cinemaNode);
                }
                root.AppendChild(movieNode);
            }
            doc.Save(Path.MoviesXml);
        }

        public static void WriteOrders(Order order, List<OrderDetail> orderDetails)
        {
            doc = new XmlDocument();
            doc.Load(Path.OrdersXml);

            XmlElement orderNode = doc.CreateElement("Order");
            orderNode.SetAttribute("CustomerName", order.customerInfo.Name);
            orderNode.SetAttribute("CustomerPhone", order.customerInfo.Phone);
            orderNode.SetAttribute("CinemaType", order.CinemaType);
            orderNode.SetAttribute("Date", order.Date);
            orderNode.SetAttribute("Showtime", order.Showtime);
            orderNode.SetAttribute("TotalPrice", order.TotalPrice.ToString());
            orderNode.SetAttribute("TotalSeat", order.TotalSeat.ToString());

            foreach (var detail in orderDetails)
            {
                XmlElement detailNode = doc.CreateElement("OrderDetail");
                detailNode.SetAttribute("SeatNumber", detail.SeatNumber.ToString());
                detailNode.SetAttribute("Price", detail.Price.ToString());
                detailNode.SetAttribute("CustomerType", detail.CustomerType.Substring(1));

                orderNode.AppendChild(detailNode);
            }

            doc.SelectSingleNode(Path.Orders).AppendChild(orderNode);
            doc.Save(Path.OrdersXml);
        }
    }
}
