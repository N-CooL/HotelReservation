using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations
{

    public class Booking
    {
        private readonly int roomCount;
        private List<BookingInfo> BookingHistory { get; set; } = new List<BookingInfo>();

        public Booking() { this.roomCount = 1000; }
        public Booking(int roomCount)
        {
            this.roomCount = roomCount > 1000 ? 1000 : roomCount;
        }

        /// <summary>
        /// Check room availability.
        /// </summary>
        /// <param name="arrivalDate"></param>
        /// <param name="departureDate"></param>
        /// <returns>return booking status</returns>
        public string CheckAvailability(int arrivalDate, int departureDate)
        {
            BookingStatus bookingStatus;
            BookingInfo newBooking = new BookingInfo
            {
                ArrivalDay = arrivalDate,
                DepartureDay = departureDate
            };

            // Check for invalid days input.
            if (arrivalDate < 0 || departureDate > 365)
            {
                bookingStatus = BookingStatus.Decline;
            }
            else
            {
                List<BookingInfo> bookedHistory = BookingHistory.FindAll(n => n.Status == BookingStatus.Accept); // Get all previous booking.
                bookedHistory.Add(newBooking);
                int[] arrival = (from n in bookedHistory select n.ArrivalDay).ToArray();
                int[] departure = (from n in bookedHistory select n.DepartureDay).ToArray();
                if (IsReservationPossible(arrival, departure, roomCount))
                {
                    bookingStatus = BookingStatus.Accept;
                }
                else
                {
                    bookingStatus = BookingStatus.Decline;
                }
            }
            newBooking.Status = bookingStatus;
            BookingHistory.Add(newBooking);
            return bookingStatus.ToString();
        }

        /// <summary>
        /// Check for reservation possibility.
        /// </summary>
        /// <param name="arrival"></param>
        /// <param name="departure"></param>
        /// <param name="roomCount"></param>
        /// <returns></returns>
        private bool IsReservationPossible(int[] arrival, int[] departure, int roomCount)
        {
            Array.Sort(arrival);
            Array.Sort(departure);
            for (int index = 0; index < arrival.Length; index++)
            {
                if (index + roomCount < arrival.Length && arrival[index + roomCount] <= departure[index])
                {
                    return false;
                }
            }
            return true;
        }

        public void ListBookingHistory()
        {
            System.Console.WriteLine($"Room Count: {roomCount}.\n");
            System.Console.WriteLine("\t StartDate \tEndDate \tResult");
            int counter = 0;
            foreach (BookingInfo info in BookingHistory)
            {
                counter++;
                System.Console.WriteLine("_________________________________________________________________________");
                System.Console.WriteLine($"Booking{counter} \t{info.ArrivalDay} \t{info.DepartureDay} \t{info.Status}");
            }
        }
    }

    public class BookingInfo
    {
        public int ArrivalDay { get; set; }
        public int DepartureDay { get; set; }
        public BookingStatus Status { get; set; }
    }
    public enum BookingStatus
    {
        Accept = 1,
        Decline = 2
    }
}
