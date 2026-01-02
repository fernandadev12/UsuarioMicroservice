using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserMicroservice.Domain.Entities;
using UserMicroservice.Domain.Repositories.Interface;

namespace UserMicroservice.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _context;

        public UserRepository(UsersDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllUserList()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            User user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Address != null && u.Email.Address == email) ?? throw new ArgumentException("Usuário não encontrado.");
            return user;
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByUsername(string username)
        {
            var userByUsername = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
            if (userByUsername == null) throw new InvalidOperationException("Usuário nao encontrado.");
            return userByUsername;
        }

        public async Task<User> Login(string username, string password, DateTime dataAcesso)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) throw new InvalidOperationException("Usuário não encontrado.");

            // ComparePassword do domínio
            if (!user.ComparePassword(password)) throw new UnauthorizedAccessException("Senha inválida.");

            // Registrar o último acesso
            user.LastAccess = dataAcesso;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> Register(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        public async Task<bool> SendEmailNewRegisterOrLogin(string email)
        {
            User usuario = await GetByEmail(email);

            string message = $@"Olá, {usuario.Username}, seja bem-vindo ao nosso sistema, sua conta foi criada com sucesso! 
                               e segue os dados de acesso: Usuário: {usuario.Username} e Senha: {usuario.Password}. ";
            string subject = "Bem-vindo ao nosso sistema";
            string recipient = email;

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://notificacoes-microservice.com/send");


                request.Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    recipient = recipient,
                    subject = subject,
                    body = message
                }));

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Email de cadastro/login enviado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Erro ao enviar email de cadastro/login .");
                    return false;
                }
            }
            return true;
        }

        public async Task<User> Update(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();

            return existingUser;
        }
    }
}