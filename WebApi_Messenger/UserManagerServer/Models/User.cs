using System.ComponentModel.DataAnnotations.Schema;

namespace Messenger.Models
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }

        public UserRole Role { get; set; }
    }
}
