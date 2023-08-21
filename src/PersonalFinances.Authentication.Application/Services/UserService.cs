using PersonalFinances.Authentication.Api.Interfaces.Services;
using PersonalFinances.Authentication.Application.Interfaces.Services;
using PersonalFinances.Authentication.Application.Models.InputModels;
using PersonalFinances.Authentication.Application.Models.OutputModel;
using PersonalFinances.Authentication.Domain.Entities;
using PersonalFinances.Authentication.Domain.Exceptions;
using PersonalFinances.Authentication.Domain.Interfaces.Repository;

namespace PersonalFinances.Authentication.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserOutputModel> SignUpAsync(SignUpInputModel inputModel, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(inputModel.Email, cancellationToken);

            if (user != null)
                throw new DomainException($"E-mail informado já possui cadastro.");

            inputModel.Password = _tokenService.ComputerSha256Hash(inputModel.Password);

            user = inputModel.ToEntity();

            await _userRepository.CreateUserAsync(user, cancellationToken);

            return new UserOutputModel().ToOutputModel(user);
        }

        public async Task<SignInOutputModel> SignInAsync(string email, string password, CancellationToken cancellationToken)
        {
            var passwordhash = _tokenService.ComputerSha256Hash(password);

            var user = await _userRepository.GetByEmailAndPasswordAsync(email, passwordhash, cancellationToken);

            if (user == null)
                throw new DomainException($"Usuário ou senha informado não coincidem");

            var token = _tokenService.GenerateJWtToken(user);

            var model = new SignInOutputModel().ToOutputModel(user);
            model.TokenJwt = token;

            return model;

        }

        public Task<User> SignOutAsync(string email, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserOutputModel> ChangePasswordAsync(string email, string oldPassword, string newPassword, CancellationToken cancellationToken)
        {

            var oldPasswordHash = _tokenService.ComputerSha256Hash(oldPassword);
            var user = await _userRepository.GetByEmailAndPasswordAsync(email, oldPasswordHash, cancellationToken);

            if (user == null)
                throw new DomainException($"Usuário ou senha informado não coincidem");

            user.ChangePassword(_tokenService.ComputerSha256Hash(newPassword));

            await _userRepository.ChangePasswordAsync(user, cancellationToken);

            return new UserOutputModel().ToOutputModel(user);

        }

        public Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
