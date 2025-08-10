using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Mini_Project.User_The_Passenger;

namespace Mini_Project_Testing
{
    [TestFixture]
    public class User_Side
    {
        [Test]
        [TestCase(6)]
        public void IsUserBlocked_UserIsBlocked_ReturnsTrue(int testUserId)
        {
            bool result = User.IsUserBlocked(testUserId);
            ClassicAssert.IsTrue(result);
        }

        [Test]
        [TestCase(2)]
        public void IsUserBlocked_UserIsNotBlocked_ReturnsFalse(int testUserId)
        {
            bool result = User.IsUserBlocked(testUserId);
            ClassicAssert.IsFalse(result);
        }

        [Test]
        [TestCase(3, 1)]
        public void TestCancelTickets_ValidBooking_ReturnsTrue(int bookingId, int seatsToCancel)
        {
            decimal refundAmount;
            string refundReason;

            bool result = Ledger.TestCancelTickets(bookingId, seatsToCancel, out refundAmount, out refundReason);

            ClassicAssert.IsTrue(result);
            ClassicAssert.IsTrue(refundAmount >= 0);
            ClassicAssert.IsNotEmpty(refundReason);
        }

        [Test]
        [TestCase(-1, 1)]
        public void TestCancelTickets_InvalidBooking_ReturnsFalse(int bookingId, int seatsToCancel)
        {
            decimal refundAmount;
            string refundReason;

            bool result = Ledger.TestCancelTickets(bookingId, seatsToCancel, out refundAmount, out refundReason);

            ClassicAssert.IsFalse(result);
        }

        [Test]
        [TestCase(2, "user", 1, "Hello Admins")]
        public void TestSendMail_UserRole_Valid_ReturnsTrue(int senderId, string senderRole, int receiverId, string message)
        {
            bool result = Mailing.TestSendMail(senderId, senderRole, receiverId, message);
            ClassicAssert.IsTrue(result);
        }

        [Test]
        [TestCase(1, "admin", 3, "Hello User")]
        public void TestSendMail_AdminRoleWithReceiver_Valid_ReturnsTrue(int senderId, string senderRole, int receiverId, string message)
        {
            bool result = Mailing.TestSendMail(senderId, senderRole, receiverId, message);
            ClassicAssert.IsTrue(result);
        }

        [Test]
        [TestCase(2, "admin", null, "No receiver")]
        public void TestSendMail_AdminRoleWithoutReceiver_ReturnsFalse(int senderId, string senderRole, int? receiverId, string message)
        {
            bool result = Mailing.TestSendMail(senderId, senderRole, receiverId, message);
            ClassicAssert.IsFalse(result);
        }

        [Test]
        [TestCase(3, "guest", 1, "Invalid role")]
        public void TestSendMail_InvalidRole_ReturnsFalse(int senderId, string senderRole, int receiverId, string message)
        {
            bool result = Mailing.TestSendMail(senderId, senderRole, receiverId, message);
            ClassicAssert.IsFalse(result);
        }
    }
}
