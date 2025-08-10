using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Mini_Project.User_The_Admin;

namespace Mini_Project_Testing
{
    [TestFixture]
    class Admin_Side
    {
        [Test]
        [TestCase(1)]
        public void TestDeleteBooking_ValidId_ReturnsTrue(int bookingId)
        {
            bool result = Manifest.TestDeleteBooking(bookingId);
            ClassicAssert.IsTrue(result);
        }


        [Test]
        [TestCase(10010)]
        public void TestDeletetrain_ValidId_ReturnsTrue(int trainId)
        {
            bool result = Railworks.TestDeleteTrain(trainId);
            ClassicAssert.IsTrue(result);
        }


        [Test]
        [TestCase(1)]
        public void TestDeleteCancellation_ValidId_ReturnsTrue(int bookingId)
        {
            bool result = Manifest.TestDeleteCancellation(bookingId);
            ClassicAssert.IsTrue(result);
        }



        [Test]
        [TestCase(3)]
        public void TestUpdateUserBlockStatus_ValidUser_ReturnsTrue(int userId)
        {
            bool result = Security.TestUpdateUserBlockStatus(userId, true);
            ClassicAssert.IsTrue(result);
        }



        [Test]
        [TestCase(100)]
        public void TestDeleteBooking_InValidId_ReturnsTrue(int bookingId)
        {
            bool result = Manifest.TestDeleteBooking(bookingId);
            ClassicAssert.IsFalse(result);
        }




        [Test]
        [TestCase(23456)]
        public void TestDeletetrain_InValidId_ReturnsTrue(int trainId)
        {
            bool result = Railworks.TestDeleteTrain(trainId);
            ClassicAssert.IsFalse(result);
        }



        [Test]
        [TestCase(100)]
        public void TestDeleteCancellation_InValidId_ReturnsFalse(int bookingId)
        {
            bool result = Manifest.TestDeleteCancellation(bookingId);
            ClassicAssert.IsFalse(result);
        }




        [Test]
        [TestCase(16)]
        public void TestUpdateUserBlockStatus_InValidUser_ReturnsTrue(int userId)
        {
            bool result = Security.TestUpdateUserBlockStatus(userId, false);
            ClassicAssert.IsFalse(result);
        }

    }
}
