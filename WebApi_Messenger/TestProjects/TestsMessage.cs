using MessageManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjects
{
    internal class TestsMessage
    {
        [Test]
        public void Message_Properties_SetAndGet()
        {
            // Arrange
            var message = new Message();

            // Act
            message.SenderMail = "";
            message.ReceiverMail = "new User()";
            message.Text = "Hello, World!";
            message.IsRead = true;

            // Assert
            Assert.IsNotNull(message.SenderMail);
            Assert.IsNotNull(message.ReceiverMail);
            Assert.AreEqual(message.Text, "Hello, World!");
            Assert.IsTrue(message.IsRead);
        }
    }
}
