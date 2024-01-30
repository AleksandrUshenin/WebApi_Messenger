using MessageManager.Models;
using Messenger.Models;
using Messenger.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagerServer.Models.DTO;

namespace TestProjects
{
    [TestFixture]
    public class UserModelTests
    {
        private UserManagerServer.Models.DTO.UserDTO _user;

        [SetUp]
        public void Setup()
        {
            _user = new UserManagerServer.Models.DTO.UserDTO();
        }

        [Test]
        public void TestUsernameProperty()
        {
            string expectedUsername = "TestUser";
            _user.Name = expectedUsername;

            Assert.AreEqual(expectedUsername, _user.Name);
        }

        [Test]
        public void TestPasswordProperty()
        {
            string expectedPassword = "TestPassword";
            _user.Password = expectedPassword;

            Assert.AreEqual(expectedPassword, _user.Password);
        }

        [Test]
        public void TestRoleProperty()
        {
            UserRole expectedRole = UserRole.Administrator;
            _user.Role = (UserManagerServer.Models.UserRole)expectedRole;

            Assert.AreEqual((UserManagerServer.Models.UserRole)expectedRole, _user.Role);
        }
    }
}
