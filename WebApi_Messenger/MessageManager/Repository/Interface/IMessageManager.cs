using MessageManager.Models;
using MessageManager.Models.DTO;

namespace MessageManager.Repository.Interface
{
    public interface IMessageManager
    {
        int SendMessage(string text, string senderName, string receiverName);
        List<MessageDTO> GetAllMessages(string receiverName);
    }
}
