using AutoMapper;
using Messenger.DataBase;
using Messenger.Models;
using Messenger.Models.DTO;
using Messenger.Repository.Interface;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace UserManagerServer.Repository
{
    public class UserManeger : IUserManeger
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserManeger(IConfiguration configuration, IMapper mapper) 
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public bool UserAdd(UserDTO userDTO, UserRole userRole)
        {
            using (var db = new ContextDataBase(_configuration.GetValue<string>("PathDataBase")))
            {
                var users = db.Users.ToList();
                var admin = users.FindAll(x => x.Role == UserRole.Administrator).Count > 0 && userRole == UserRole.Administrator;
                if (admin)
                {
                    throw new Exception("There can only be one administrator");
                    return false;
                }

                var res = users.FirstOrDefault(x => x.Name.ToLower() == userDTO.Name.ToLower());
                if (res is null)
                {
                    var salt = new byte[16];
                    new Random().NextBytes(salt);

                    var data = Encoding.ASCII.GetBytes(userDTO.Password).Concat(salt).ToArray();

                    SHA512 shaM = new SHA512Managed();

                    res = new User { Name = userDTO.Name, Email = userDTO.Email, Password = shaM.ComputeHash(data), Role = userRole, Salt = salt };

                    db.Users.Add(res);
                    db.SaveChanges();
                    return true;
                }
                else 
                    return false;
            }
        }

        public UserRole UserCheck(LoginUser loginUser)
        {
            using (var db = new ContextDataBase(_configuration.GetValue<string>("PathDataBase")))
            {
                var user = db.Users.FirstOrDefault(x => x.Name == loginUser.Name);
                if (user is null)
                    throw new Exception("User not found");

                var data = Encoding.ASCII.GetBytes(loginUser.Password).Concat(user.Salt).ToArray();
                SHA512 shaM = new SHA512Managed();
                var bpassword = shaM.ComputeHash(data);

                if (user.Password.SequenceEqual(bpassword))
                {
                    return user.Role;
                }
                else
                    throw new Exception("Wrong password");
            }
        }
        public List<UserDTO> GetUsers()
        {
            using (var db = new ContextDataBase(_configuration.GetValue<string>("PathDataBase")))
            {
                return db.Users.ToList().Select(x => _mapper.Map<UserDTO>(x)).ToList();
            }
        }
        public void DeleteUser(string name)
        {
            using (var db = new ContextDataBase(_configuration.GetValue<string>("PathDataBase")))
            {
                var users = db.Users.ToList();
                var user = users.FirstOrDefault(x => x.Name == name);

                if (user is null)
                    throw new Exception("User not found");
                else
                {
                    if (user.Role == UserRole.Administrator)
                        throw new Exception("Unable to remove administrator");
                    try
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }
    }
}
