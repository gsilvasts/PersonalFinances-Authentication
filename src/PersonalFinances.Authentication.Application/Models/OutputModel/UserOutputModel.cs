using PersonalFinances.Authentication.Domain.Entities;

namespace PersonalFinances.Authentication.Application.Models.OutputModel
{
    public class UserOutputModel
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public UserOutputModel ToOutputModel(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;

            return this;
        }
    }
}
