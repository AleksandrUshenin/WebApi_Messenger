using AutoMapper;
using MessageManager.Controllers;
using MessageManager.Models;
using MessageManager.Models.DTO;
using MessageManager.Repository.Interface;
using Messenger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjects
{
    [TestFixture]
    internal class TestsMessageManagerController
    {
        private MessageManagerController _controller;
        private Mock<IMessageManager> _mockMessageManager;
        [SetUp]
        public void Setup()
        {
            _mockMessageManager = new Mock<IMessageManager>();
            _controller = new MessageManagerController(_mockMessageManager.Object);
        }
        [Test]
        public void GetAllMessages_ReturnsOkResult()
        {
            var expectedMessages = new List<MessageDTO> {
            new MessageDTO {
                Text = "Hello, world!", SenderMail = "admin",
                ReceiverMail = "user1"
            },
            new MessageDTO {
                Text = "How are you?", SenderMail = "admin",
                ReceiverMail = "user1"
            }
        };
            _mockMessageManager.Setup(x => x.GetAllMessages(It.IsAny<string>())).Returns(expectedMessages);

            var result = _controller.GetAllMessages("John Doe");

            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public void SendMessage()
        {
            //var id = Guid.NewGuid();
            var id = 0;
            var message = new MessageDTO { Text = "Hello, world!", SenderMail = "admin", ReceiverMail = "user1" };
            _mockMessageManager.Setup(x => x.SendMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(id);

            // Act
            var result = _controller.SendMessage(message).ToString();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(id.ToString(), result);
        }
    }
}
