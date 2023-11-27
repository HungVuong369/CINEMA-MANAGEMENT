using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1 {
    public class Path {
        public static string CinemasXml = "data/cinemas.xml";
        public static string OrdersXml = "data/orders.xml";
        public static string AccountsXml = "data/Accounts.xml";
        public static string MoviesXml = "data/Movies/Movies.xml";

        public static string CinemaVipXml = "data/Cinemas/CinemaVip.xml";
        public static string CinemaStandardXml = "data/Cinemas/CinemaStandard.xml";

        public static string AllMovie = "//Movie";
        public static string AllAccount = "//Account";
        public static string Orders = "//Orders";
        public static string AllOrder = "//Order";

        public static string getCinemaById(string Id) {
            return $"//Cinema[@IdCinema='{Id}']";
        }

        public static string getSeatById(string Id) {
            return $"//Seat[@Id='{Id}']";
        }

        public static string getFolderMovies(string text)
        {
            return $"Data/Movies/{text}";
        }

        public static string getFolderCinema(string cinemaType, string movieID)
        {
            return $"Data/Movies/{movieID}/{cinemaType}";
        }

        public static string getFolderDate(string cinemaType, string movieID, string date)
        {
            return $"Data/Movies/{movieID}/{cinemaType}/{date}";
        }

        public static string getFileShowtime(string cinemaType, string movieID, string date, string showtime)
        {
            return $"Data/Movies/{movieID}/{cinemaType}/{date}/{showtime}.xml";
        }

        public static string getOrderByCinemaTypeAndDate(string cinemaType, string date)
        {
            return $"//Order[@CinemaType='{cinemaType}' and @Date='{date}']";
        }
    }
}
