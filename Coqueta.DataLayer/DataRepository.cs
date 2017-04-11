using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Coqueta.DataLayer
{
    using DataInterfaces;
    using Types;

    public class DataRepository : IDataRepository
    {
        private static readonly string _connectionString;

        static DataRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Coqueta.Database"].ConnectionString;
        }
        async Task<User[]> IDataRepository.RetrieveAll()
        {
            using (var dbcontext = new CoquetaDbContext(_connectionString))
            {
                var result = dbcontext.Users.ToArray();
                return await Task.Run(() => result);
            }
        }

        async Task<string> IDataRepository.Delete(int id)
        {
            using (var dbContext = new CoquetaDbContext(_connectionString))
            {
                var entity = dbContext.Users.SingleOrDefault(e => e.Id == id);
                dbContext.Users.Remove(entity);
                await dbContext.SaveChangesAsync();
                return entity.Username;
            }
        }

        Task<bool> IDataRepository.IsUserRegistered(string email)
        {
            using (var dbContext = new CoquetaDbContext(_connectionString))
            {
                var entity = dbContext.Users.SingleOrDefault(e => e.Email == email);

                if (entity == null)
                {
                    return Task.FromResult(false);
                }
                else
                {
                    return Task.FromResult(true);
                }
            }
        }


        async Task<string> IDataRepository.Save(User model)
        {
            using (var dbContext = new CoquetaDbContext(_connectionString))
            {
                dbContext.Users.Add(model);
                await dbContext.SaveChangesAsync();
                return model.Username;
            }
        }

        async Task<string> IDataRepository.Update(User model)
        {
            using (var dbContext = new CoquetaDbContext(_connectionString))
            {
                var entity = await dbContext.Users.FindAsync(model.Id);
                dbContext.Entry(entity).CurrentValues.SetValues(model);
                await dbContext.SaveChangesAsync();
                return model.Username;
            }
        }

        Task<User> IDataRepository.RetrieveById(int id)
        {
            using (var dbContext = new CoquetaDbContext(_connectionString))
            {
                var entity = dbContext.Users.SingleOrDefault(e => e.Id == id);
                var result = new User
                {
                    Id = entity.Id,
                    Email = entity.Email.Trim(),
                    Username = entity.Username.Trim(),
                    Password = entity.Password.Trim(),
                    ConfirmationPassword = entity.ConfirmationPassword.Trim()
                };
                return Task.FromResult(result);
            }
        }
    }
}
