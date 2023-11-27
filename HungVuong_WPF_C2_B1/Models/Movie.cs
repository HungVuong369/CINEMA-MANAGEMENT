using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1
{
    public class Movie
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int Runtime { get; set; }
        public string UrlImage { get; set; }
        public List<Schedule> ScheduleLst { get; set; }
        public List<Cinema> CinemaLst { get; set; }

        public Movie(string id, string name, string content, int runtime, string urlImage, List<Schedule> scheduleLst, List<Cinema> cinemaLst)
        {
            this.Id = id;
            this.Name = name;
            this.Content = content;
            this.Runtime = runtime;
            this.UrlImage = urlImage;
            this.ScheduleLst = scheduleLst;
            this.CinemaLst = cinemaLst;
        }
    }
}