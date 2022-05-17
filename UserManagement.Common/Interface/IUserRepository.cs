using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Common.Dto;

namespace UserManagement.Common.Interface
{
    public interface IUserRepository
    {
        Task AddAsync(UserDataDTO userDataDTO);

        Task<IReadOnlyCollection<UserDataDTO>> GetAllUser();

        Task DeleteUser(int id);

        Task UpdateAsync(UserDataDTO userDataDTO);

    }
}
