using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungVuong_WPF_C2_B1
{
    public class CinemaViewModel
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public RepositoryBase<Cinema> cinemaRepo { get; set; }
        public Cinema cinema;

        public int quantitySeats = 0;
        public double totalPrice = 0;
        public List<Seat> selectedSeats;
        public Dictionary<int, int> lstType;
        public List<string> types;
        public List<double> lstPrice;

        public CinemaViewModel()
        {
            var myArray = new object[2, 3];
            cinemaRepo = unitOfWork.GetRepositoryCinema;
            cinema = cinemaRepo.Items[0];
        }

        public List<string> getListCinema()
        {
            List<string> nameCinemas = new List<string>();

            foreach (var cinema in cinemaRepo.Items)
            {
                nameCinemas.Add(cinema.Name);
            }
            return nameCinemas;
        }

        public double getPrice(int rowOfSeat)
        {
            rowOfSeat++;

            int rowCenter = (cinema.seats.GetLength(0) / 2) + 1;

            double price = 0;

            if (rowOfSeat == rowCenter)
                price = cinema.priceCenter;

            else if (rowOfSeat > rowCenter)
                price = cinema.priceCenter - ((rowOfSeat % rowCenter) * cinema.reductionAmount);

            else if (rowOfSeat < rowCenter)
                price = cinema.priceCenter - ((rowCenter - rowOfSeat) * cinema.reductionAmount);

            return price;
        }

        public bool isExistInSelectedSeats(Seat seat) {
            foreach(var item in selectedSeats)
            {
                if (item == seat)
                    return true;
            }
            return false;
        }

        public int getIndexSeatExistInSelectedSeats(Seat seat)
        {
            int index = 0;
            foreach (var item in selectedSeats)
            {
                if (item == seat)
                    return index;
                index++;
            }
            return -1;
        }

        //public int getIndexInCinemaLst(Seat seat)
        //{
        //    for(int indexRow = 0; indexRow < cinema.seats.listSeat.GetLength(0); indexRow++)
        //    {
        //        for(int indexColumn = 0; indexColumn < cinema.seats.listSeat.GetLength(0); indexColumn++)
        //        {
        //            if (seat == cinema.seats.listSeat[indexRow][indexColumn])
        //                return indexRow * cinema.seats.listSeat.GetLength(0) + indexColumn;
        //        }
        //    }
        //    return -1;
        //}
    }
}
