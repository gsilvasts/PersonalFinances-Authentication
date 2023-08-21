using PersonalFinances.Authentication.Domain.Entities;

namespace PersonalFinances.Authentication.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retorna um usuário através do e-mail
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Retornar o usuário através do e-mail e do Hash da senha
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User?> GetByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken);

        /// <summary>
        /// Insere um novo usuário
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CreateUserAsync(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Atualiza a senha de um usuário
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ChangePasswordAsync(User user, CancellationToken cancellationToken);
    }
}
