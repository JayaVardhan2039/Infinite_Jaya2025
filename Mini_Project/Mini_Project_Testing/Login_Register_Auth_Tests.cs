using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Mini_Project.Authentication;

namespace Mini_Project_Testing
{
    [TestFixture]
    public class Login_Register_Auth_Tests
    {


        [Test]
        [TestCase("admin", "bindu", "bindu123", true)]//will pass
        [TestCase("admin", "wrongadmin", "wrongpass", false)]//will fail
        public static void Admin_Login_Valid_or_Not(string role, string username, string password, bool expectedSuccess)
        {
            var result = LoginFlow.Login(role, username, password);
            if (expectedSuccess)
            {
                ClassicAssert.IsNotNull(result);
                ClassicAssert.AreEqual(username, result?.username);
            }
            else
            {
                ClassicAssert.IsNull(result);
            }
        }

        [Test]
        [TestCase("user", "vyshnavi", "vyshu@567", true)]//will pass
        [TestCase("user", "wronguser", "wrongpass", false)]//will fail
        public static void User_Login_Valid_or_Not(string role, string username, string password, bool expectedSuccess)
        {
            var result = LoginFlow.Login(role, username, password);
            if (expectedSuccess)
            {
                ClassicAssert.IsNotNull(result);
                ClassicAssert.AreEqual(username, result?.username);
            }
            else
            {
                ClassicAssert.IsNull(result);
            }
        }

        [Test]
        public void RegisterUser_ValidData_ShouldSucceed()//will pass
        {
            string firstName = "Test";
            string lastName = "User";
            string phone = "9876543210";
            string email = "testuser@example.com";
            string password = "test@123";
            string username = "testuser";

            bool result = UserGenesis.RegisterUserWithData(firstName, lastName, phone, email, password, username);
            ClassicAssert.IsTrue(result);
        }


        [Test]
        [TestCase("vyshnavi", "vyshu@567", 3)]//will pass
        [TestCase("wronguser", "wrongpass", null)]//will faill
        public void Authenticate_User_Valid_or_Not(string username, string password, int? expectedUserId)
        {
            var result = LoginFlow.Authenticate(username, password);
            if (expectedUserId.HasValue)
            {
                ClassicAssert.IsNotNull(result);
                ClassicAssert.AreEqual(expectedUserId.Value, result?.userid);
            }
            else
            {
                ClassicAssert.IsNull(result);
            }
        }





    }
}


