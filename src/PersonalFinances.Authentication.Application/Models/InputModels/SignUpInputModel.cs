using PersonalFinances.Authentication.Domain.Entities;

namespace PersonalFinances.Authentication.Application.Models.InputModels
{
    public class SignUpInputModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User ToEntity()
        {
            return new User(FirstName, LastName, Email, Password);
        }
    }
}
