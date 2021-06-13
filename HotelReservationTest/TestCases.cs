using HotelReservations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HotelReservationTest
{
    [TestClass]
    public class TestCases
    {
        [TestMethod]
        public void OutsidePlanningPeriodTest1()
        {
            Booking hotelReservetion = new Booking(1); // Set size=1

            string expectedResult = "Decline";
            string actualResult = hotelReservetion.CheckAvailability(-4, 2);
            hotelReservetion.ListBookingHistory();

            // Verify the result:
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OutsidePlanningPeriodTest2()
        {
            Booking hotelReservetion = new Booking(1); // Set size=1

            string expectedResult = "Decline";
            string actualResult = hotelReservetion.CheckAvailability(200, 400);
            hotelReservetion.ListBookingHistory();
            // Verify the result:
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void RequestsAreAccepted()
        {
            Booking hotelReservetion = new Booking(3); // Set size=3
            List<BookingInfo> inputData = new List<BookingInfo>
            {
                new BookingInfo { ArrivalDay = 0, DepartureDay = 5, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 7, DepartureDay = 13, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 3, DepartureDay = 9, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 5, DepartureDay = 7, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 6, DepartureDay = 6, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 0, DepartureDay = 4, Status = BookingStatus.Accept }
            };
            foreach (BookingInfo info in inputData)
            {
                string actualResult = hotelReservetion.CheckAvailability(info.ArrivalDay, info.DepartureDay);
                Assert.AreEqual(info.Status.ToString(), actualResult);
            }
            hotelReservetion.ListBookingHistory();
        }

        [TestMethod]
        public void RequestsAreDeclined()
        {
            Booking hotelReservetion = new Booking(3); // Set size=3
            List<BookingInfo> inputData = new List<BookingInfo>
            {
                new BookingInfo { ArrivalDay = 1, DepartureDay = 3, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 2, DepartureDay = 5, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 1, DepartureDay = 9, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 0, DepartureDay = 15, Status = BookingStatus.Decline }
            };
            foreach (BookingInfo info in inputData)
            {
                string actualResult = hotelReservetion.CheckAvailability(info.ArrivalDay, info.DepartureDay);
                Assert.AreEqual(info.Status.ToString(), actualResult);
            }
            hotelReservetion.ListBookingHistory();
        }

        [TestMethod]
        public void RequestsCanBeAcceptedAfter_a_Decline()
        {
            Booking hotelReservetion = new Booking(3); // Set size=3
            List<BookingInfo> inputData = new List<BookingInfo>
            {
                new BookingInfo { ArrivalDay = 1, DepartureDay = 3, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 0, DepartureDay = 15, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 1, DepartureDay = 9, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 2, DepartureDay = 5, Status = BookingStatus.Decline },
                new BookingInfo { ArrivalDay = 4, DepartureDay = 9, Status = BookingStatus.Accept }
            };
            foreach (BookingInfo info in inputData)
            {
                string actualResult = hotelReservetion.CheckAvailability(info.ArrivalDay, info.DepartureDay);
                Assert.AreEqual(info.Status.ToString(), actualResult);
            }
            hotelReservetion.ListBookingHistory();

        }

        [TestMethod]
        public void ComplexRequests()
        {
            Booking hotelReservetion = new Booking(2); // Set size=2
            List<BookingInfo> inputData = new List<BookingInfo>
            {
                new BookingInfo { ArrivalDay = 1 , DepartureDay = 3, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 0 , DepartureDay = 4, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 2 , DepartureDay = 3, Status = BookingStatus.Decline },
                new BookingInfo { ArrivalDay = 5 , DepartureDay = 5, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 4 , DepartureDay = 10, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 10, DepartureDay = 10, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 6 , DepartureDay = 7, Status = BookingStatus.Accept },
                new BookingInfo { ArrivalDay = 8 , DepartureDay = 10, Status = BookingStatus.Decline },
                new BookingInfo { ArrivalDay = 8 , DepartureDay = 9 , Status = BookingStatus.Accept }
            };
            foreach (BookingInfo info in inputData)
            {
                string actualResult = hotelReservetion.CheckAvailability(info.ArrivalDay, info.DepartureDay);
                Assert.AreEqual(info.Status.ToString(), actualResult);
            }
            hotelReservetion.ListBookingHistory();

        }
    }
}
