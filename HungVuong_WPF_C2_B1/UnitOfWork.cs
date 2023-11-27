using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1
{
    public class UnitOfWork
    {
        private RepositoryBase<Cinema> cinemaList;
        private RepositoryBase<Movie> movieList;

        public RepositoryBase<Cinema> GetRepositoryCinema
        {
            get
            {
                if (this.cinemaList == null)
                    this.cinemaList = new RepositoryBase<Cinema>();
                return this.cinemaList;
            }
        }

        public RepositoryBase<Movie> GetRepositoryMovie
        {
            get
            {
                if (this.movieList == null)
                    this.movieList = new RepositoryBase<Movie>();
                return this.movieList;
            }
        }

        public UnitOfWork() {
            cinemaList = new RepositoryBase<Cinema>();
            movieList = new RepositoryBase<Movie>();

            cinemaList.BulkInsert(SeedData.getListCinema());
            movieList.BulkInsert(SeedData.getListMovieXAML());
        }
    }
}
