namespace PersonalFinances.Authentication.Api.Models
{
    public class User
    {
        public User(string userName, string firstName, string lastName, string email, string password, string role)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Active = true;
            Role = role;
        }

        public Guid Id { get; set; }
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public bool Active { get; set; }
    }
}
