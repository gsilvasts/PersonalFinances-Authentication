using PersonalFinances.Authentication.Domain.Entities;

namespace PersonalFinances.Authentication.Application.Models.OutputModel
{
    public class SignInOutputModel
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TokenJwt { get; set; }
        public List<string> Roles { get; set; } = new List<string>();

        public SignInOutputModel ToOutputModel(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;

            return this;
        }
    }
}
