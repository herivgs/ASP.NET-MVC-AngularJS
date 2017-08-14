using Coqueta.Types;
using System.Threading.Tasks;

namespace Coqueta.DataInterfaces
{
    public interface IDataRepository
    {
        Task<string> Delete(int id);

        Task<string> Save(User model);

        Task<string> Update(User model);

        Task<bool> IsUserRegistered(string email);
        Task<User[]> RetrieveAll();
        Task<User> RetrieveById(int id);
    }
}
