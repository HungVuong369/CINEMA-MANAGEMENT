using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1
{
    public class MovieViewModel
    {
        public int CleanupTime = 45; // Phút

        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public RepositoryBase<Movie> movieRepo { get; set; }
        public Seat[][] SeatLst;

        public MovieViewModel()
        {
            movieRepo = unitOfWork.GetRepositoryMovie;
        }

        public List<Movie> GetFilteredMoviesWithCinemaName(string cinemaName)
        {
            List<Movie> movies = new List<Movie>();

            foreach(var movie in movieRepo.Items)
            {
                Cinema cinema = movie.CinemaLst.Find(item => item.Name == cinemaName);

                if (cinema != null)
                    movies.Add(movie);
            }
            return movies;
        }

        public List<Movie> GetFilteredMoviesWithCinema(string cinemaName)
        {
            List<Movie> movies = new List<Movie>();

            foreach (var movie in movieRepo.Items)
            {
                Cinema cinema1 = movie.CinemaLst.Find(item => item.Name == cinemaName);

                Cinema cinema2 = movie.CinemaLst.Find(item => item.Name != cinemaName);

                if(movie.CinemaLst.Count == 0)
                {
                    movies.Add(movie);
                    continue;
                }

                if (cinema1 != null && cinema2 != null)
                    continue;

                if (cinema2 != null)
                    movies.Add(movie);
            }
            return movies;
        }

        public List<Movie> GetFilteredMoviesWithSchedule()
        {
            List<Movie> movies = new List<Movie>();
            foreach(Movie movie in movieRepo.Items)
            {
                if (!isEmptySchedule(movie))
                    movies.Add(movie);
            }
            return movies;
        }

        public bool isEmptySchedule(Movie movie)
        {
            if (movie.CinemaLst.Count == 0)
                return true;
            if (movie.ScheduleLst.Count == 0)
                return true;

            foreach (Schedule schedule in movie.ScheduleLst)
            {
                if (schedule.ReleaseDate == null)
                    return true;
            }
            return false;
        }

        public bool isExistId(string id)
        {
            foreach(Movie movie in movieRepo.Items)
            {
                if (string.Compare(movie.Id.ToLower(), id.ToLower()) == 0)
                    return true;
            }
            return false;
        }

        public bool isExistSchedule(TimeSpan item, string cinemaType, string date)
        {
            foreach(var movie in movieRepo.Items)
            {
                foreach(var schedule in movie.ScheduleLst)
                {
                    if (schedule.CinemaType != cinemaType || ConvertString.ConvertDateToStringOne(schedule.ReleaseDate) != date)
                        continue;
                    
                    foreach (var showtime in schedule.ShowtimeList)
                    {
                        if(item >= showtime && item <= showtime.Add(new TimeSpan(0, movie.Runtime + CleanupTime, 0)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
