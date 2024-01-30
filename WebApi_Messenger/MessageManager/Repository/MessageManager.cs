using AutoMapper;
using MessageManager.Models.DTO;
using MessageManager.Repository.Interface;
using MessageManager.DataBase;

namespace MessageManager.Repository
{
    public class MessageManager : IMessageManager
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public MessageManager(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public List<MessageDTO> GetAllMessages(string receiverName)
        {
            using (var db = new ContextDataBase(_configuration.GetValue<string>("PathDataBase")))
            {
                var messages = db.Messages.Where(x => x.ReceiverMail == receiverName && x.IsRead == false).ToList();
                messages.ForEach(x => { x.IsRead = true; });
                db.SaveChanges();
                return messages.Select(x => _mapper.Map<MessageDTO>(x)).ToList();
            }
        }

        public int SendMessage(string text, string senderName, string receiverName)
        {
            using (var db = new ContextDataBase(_configuration.GetValue<string>("PathDataBase")))
            {
                bool isUserSenderExists = db.Users.FirstOrDefault(x => x.Name == senderName) != null;
                bool isUserReceiverExists = db.Users.FirstOrDefault(x => x.Name == receiverName) != null;
                if (!isUserSenderExists && isUserReceiverExists)
                    return -1;

                db.Messages.Add(new Models.Message() { IsRead = false, ReceiverMail = receiverName, SenderMail = senderName, Text = text });
                db.SaveChanges();
                return 1;
            }
        }
    }
}
