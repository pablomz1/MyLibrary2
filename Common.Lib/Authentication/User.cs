using Common.Lib.Core;

namespace Common.Lib.Authentication
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
      

    }
}
