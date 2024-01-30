﻿using MessageManager.Models.DTO;
using MessageManager.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MessageManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageManagerController : ControllerBase
    {
        private readonly IMessageManager _messageManager;
        public MessageManagerController(IMessageManager messageManager)
        {
            _messageManager = messageManager;
        }

        [HttpGet]
        [Route("GetMessages")]
        public IActionResult GetAllMessages(string userName)
        {
            var messages = _messageManager.GetAllMessages(userName);
            return Ok(messages);
        }

        [HttpPost]
        [Route("SendMessage")]
        public IActionResult SendMessage(MessageDTO message)
        {
            if (message.SenderMail != null && message.ReceiverMail != null && message.Text != null)
            {
                var messageResult = _messageManager.SendMessage(
                    message.Text,
                    message.SenderMail,
                    message.ReceiverMail
                );
                if (messageResult > 0)
                    return Ok("Successfully!");
            }

            return NotFound("Sender or Receiver not fount");
        }
    }
}
