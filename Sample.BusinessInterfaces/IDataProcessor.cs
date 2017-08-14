using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coqueta.BusinessInterfaces
{
    using Types;

    public interface IDataProcessor
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> GetById(int id);

        Task AddUser(User model);

        Task RemoveUser(int id);

        Task UpdateUser(User model);

    }
}
