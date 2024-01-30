using Messenger.Models;
using Messenger.Models.DTO;

namespace Messenger.Repository.Interface
{
    public interface IUserManeger
    {
        bool UserAdd(UserDTO userDTO, UserRole userRole);
        UserRole UserCheck(LoginUser loginUser);
        List<UserDTO> GetUsers();
        void DeleteUser(string name);
    }
}
