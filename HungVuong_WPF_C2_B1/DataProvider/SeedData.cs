using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Media;
using System.Windows.Controls;

namespace HungVuong_WPF_C2_B1
{
    public class SeedData
    {
        private static readonly XmlDocument doc = new XmlDocument();

        public static List<Cinema> getListCinema()
        {
            List<Cinema> list = new List<Cinema>();


            list.Add(new Cinema(
                    "STA01",      // Id
                    "Standard 1", // Name
                    "Standard",   // Type
                    60000,        // Price Center
                    0.5,          // Percent Off
                    10000         // Redution Amount
                    ));

            list.Add(new CinemaVip(
                "Vip01",
                "Vip 1",
                "Vip",
                100000,
                0.1,
                5000,
                40000 // Surcharge Value
                ));

            return list;
        }

        public static Seat[][] getListSeatByCinemaId(string Id)
        {
            Seat[][] seats = null;

            XmlDocument doc = new XmlDocument();
            doc.Load(Path.CinemasXml);

            XmlNode cinemaNode = doc.SelectSingleNode(Path.getCinemaById(Id));

            if (cinemaNode != null)
            {
                int maxRow = int.Parse(cinemaNode.Attributes["Row"].Value);
                int maxColumn = int.Parse(cinemaNode.Attributes["Column"].Value);

                int row = 0;
                int column = 0;
                seats = new Seat[maxRow][];

                seats[row] = new Seat[maxColumn];

                foreach (XmlNode seatNode in cinemaNode.SelectNodes("Seat"))
                {
                    if (column == maxRow)
                    {
                        row++;
                        column = 0;
                        seats[row] = new Seat[maxColumn];
                    }

                    seats[row][column++] = new Seat(
                        seatNode.Attributes["Id"].Value,
                        seatNode.Attributes["Name"].Value,
                        int.Parse(seatNode.Attributes["Status"].Value) != 0
                        );
                }
            }
            doc.Save(Path.CinemasXml);
            return seats;
        }

        public static List<Account> getListAccount(bool isHaveAdmin)
        {
            List<Account> accounts = new List<Account>();

            // 0 is admin
            // 1 is sell ticket
            doc.Load(Path.AccountsXml);

            XmlNodeList nodes = doc.SelectNodes(Path.AllAccount);

            foreach(XmlNode node in nodes)
            {
                if (!isHaveAdmin && node.Attributes["Priority"].Value == "0")
                    continue;
                    
                accounts.Add(new Account(
                        node.Attributes["Username"].Value,
                        node.Attributes["Password"].Value,
                        int.Parse(node.Attributes["Priority"].Value)
                        ));
            }

            doc.Save(Path.AccountsXml);

            return accounts;
        }

        public static List<TimeSpan> GetShowTimeList()
        {
            List<TimeSpan> showTimeList = new List<TimeSpan>();

            Random random = new Random();

            showTimeList.Add(new TimeSpan(12, 0, 0));
            showTimeList.Add(new TimeSpan(8, 0, 0));
            showTimeList.Add(new TimeSpan(22, 0, 0));
            showTimeList.Add(new TimeSpan(7, 30, 0));
            showTimeList.Add(new TimeSpan(20, 30, 0));

            return showTimeList;
        }

        public static Seat[][] getListSeatByPath(string id, string date, string showtime, MovieViewModel movieVM, int seatNumber, int indexCinema)
        {
            Seat[][] seats = null;

            string path = $"data/Movies/{id}/{movieVM.movieRepo.Items[seatNumber].CinemaLst[indexCinema].Type}/{date}/{showtime}.xml";

            doc.Load(path);

            XmlNode cinemaNode = doc.SelectSingleNode(Path.getCinemaById(movieVM.movieRepo.Items[seatNumber].CinemaLst[indexCinema].Id));

            if (cinemaNode != null)
            {
                int maxRow = int.Parse(cinemaNode.Attributes["Row"].Value);
                int maxColumn = int.Parse(cinemaNode.Attributes["Column"].Value);

                int row = 0;
                int column = 0;
                seats = new Seat[maxRow][];

                seats[row] = new Seat[maxColumn];

                foreach (XmlNode seatNode in cinemaNode.SelectNodes("Seat"))
                {
                    if (column == maxRow)
                    {
                        row++;
                        column = 0;
                        seats[row] = new Seat[maxColumn];
                    }

                    seats[row][column++] = new Seat(
                        seatNode.Attributes["Id"].Value,
                        seatNode.Attributes["Name"].Value,
                        int.Parse(seatNode.Attributes["Status"].Value) != 0
                        );
                }
            }
            doc.Save(path);
            return seats;
        }

        public static void WriteFile(MovieViewModel movieVM, Seat[][] seatList, int seatNumber, int indexCinema, Schedule schedule, int indexShowtime)
        {
            string hour = schedule.ShowtimeList[indexShowtime].Hours.ToString();
            string minute = schedule.ShowtimeList[indexShowtime].Minutes.ToString();

            if (minute.Length == 1)
                minute = minute + "0";
            if (hour.Length == 1)
                hour = "0" + hour;

            string convert = hour + "-" + minute;

            string id = movieVM.movieRepo.Items[seatNumber].Id.Replace(':', ' ');
            string date = schedule.ReleaseDate.Date.ToString("dd-M-yyyy");

            string path = $"data/Movies/{id}/{movieVM.movieRepo.Items[seatNumber].CinemaLst[indexCinema].Type}/{date}/{convert}.xml";

            doc.Load(path);

            XmlNode cinemaNode = doc.SelectSingleNode(Path.getCinemaById(movieVM.movieRepo.Items[seatNumber].CinemaLst[indexCinema].Id));

            if (cinemaNode != null)
            {
                int maxRow = int.Parse(cinemaNode.Attributes["Row"].Value);
                int maxColumn = int.Parse(cinemaNode.Attributes["Column"].Value);

                int row = 0;
                int column = 0;

                foreach (XmlNode seatNode in cinemaNode.SelectNodes("Seat"))
                {
                    if (column == maxRow)
                    {
                        row++;
                        column = 0;
                    }
                    int status = seatList[row][column].Status ? 1 : 0;
                    seatNode.Attributes["Status"].Value = status.ToString();
                    column++;
                }
            }
            doc.Save(path);
        }

        public static List<Movie> getListMovieXAML()
        {
            List<Movie> movies = new List<Movie>();

            doc.Load(Path.MoviesXml);

            XmlNodeList movieNodes = doc.SelectNodes(Path.AllMovie);

            foreach (XmlNode movieNode in movieNodes)
            {
                List<Schedule> schedules = new List<Schedule>();
                string movieName = movieNode.Attributes["Name"].Value;

                string movieId = movieNode.Attributes["Id"].Value;

                string movieContent = movieNode.Attributes["Content"].Value;

                int runtime = int.Parse(movieNode.Attributes["runtime"].Value);

                string url = movieNode.Attributes["Url"].Value;

                XmlNodeList CinemaNodes = movieNode.SelectNodes("Cinema");
                foreach(XmlNode cinemaNode in CinemaNodes)
                {
                    XmlNodeList dateNodes = cinemaNode.SelectNodes("Date");

                    foreach (XmlNode dateNode in dateNodes)
                    {
                        int day = int.Parse(dateNode.Attributes["Date"].Value.Split('/')[0]);
                        int month = int.Parse(dateNode.Attributes["Date"].Value.Split('/')[1]);
                        int year = int.Parse(dateNode.Attributes["Date"].Value.Split('/')[2]);
                        DateTime date = new DateTime(year, month, day);

                        XmlNodeList showtimeNodes = dateNode.SelectNodes("Showtime");
                        List<TimeSpan> showTimes = new List<TimeSpan>();
                        foreach (XmlNode showtimeNode in showtimeNodes)
                        {
                            string hour = showtimeNode.Attributes["Hour"].Value;
                            string minute = showtimeNode.Attributes["Minute"].Value;
                            showTimes.Add(new TimeSpan(int.Parse(hour), int.Parse(minute), 0));
                        }

                        schedules.Add(new Schedule(date, showTimes, cinemaNode.Attributes["Type"].Value));
                    }
                }

                if(CinemaNodes.Count >= 2)
                    movies.Add(new Movie(movieId, movieName, movieContent, runtime, url, schedules, getListCinema()));
                else
                {
                    if (CinemaNodes.Count == 0)
                    {
                        movies.Add(new Movie(movieId, movieName, movieContent, runtime, url, schedules, new List<Cinema>()));
                        continue;
                    }
                    if (string.Compare(CinemaNodes[0].Attributes["Type"].Value, "Standard") == 0)
                        movies.Add(new Movie(movieId, movieName, movieContent, runtime, url, schedules, new List<Cinema>() { getListCinema()[0] }));
                    else
                        movies.Add(new Movie(movieId, movieName, movieContent, runtime, url, schedules, new List<Cinema>() { getListCinema()[1] }));

                }
            }
            doc.Save(Path.MoviesXml);
            return movies;
        }
    }
}