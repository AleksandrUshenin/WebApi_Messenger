using Messenger.Models;
using Messenger.Models.DTO;

namespace Messenger.Repository.Interface
{
    public interface IUserManeger
    {
        public bool UserAdd(UserDTO userDTO, UserRole userRole);
        public UserRole UserCheck(LoginUser loginUser);
    }
}
