using System.Threading.Tasks;
using UserManagement.Common.Dto;
using UserManagement.Common.Interface;
using UserManagement.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data;
using UserManagement.Common.DbModel;

namespace UserManagement.Business.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userDbContext;

        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task AddAsync(UserDataDTO userDataDTO)
        {
            long? mobileNumber = null;
            if (!string.IsNullOrEmpty(userDataDTO.MobileNumber))
            {
                mobileNumber = Convert.ToInt64(userDataDTO.MobileNumber);
            }
            var parameters = new[] {
                 new SqlParameter("@Name", SqlDbType.VarChar)
                 {
                    Direction = ParameterDirection.Input,
                    Value = userDataDTO.Name,
                    Size = 200
                 },
                 new SqlParameter("@Age", SqlDbType.Int)
                 {
                    Direction = ParameterDirection.Input,
                    Value = userDataDTO.Age,
                    IsNullable = true
                 },
                 new SqlParameter("@Gender", SqlDbType.VarChar)
                 {
                    Direction = ParameterDirection.Input,
                    Value = userDataDTO.Gender,
                    Size = 7
                 },
                 new SqlParameter("@Email", SqlDbType.VarChar)
                 {
                    Direction = ParameterDirection.Input,
                    Value = userDataDTO.Email,
                    Size = 100
                 },
                 new SqlParameter("@Address", SqlDbType.VarChar)
                 {
                    Direction = ParameterDirection.Input,
                    Value = userDataDTO.Address,
                    Size = 250,
                    IsNullable = true
                 },
                 new SqlParameter("@MobileNumber", SqlDbType.BigInt)
                 {
                    Direction = ParameterDirection.Input,
                    Value = mobileNumber,
                    IsNullable = true
                 },
                 new SqlParameter("@ProfilePictureBase64Data", SqlDbType.VarChar)
                 {
                    Direction = ParameterDirection.Input,
                    Value = userDataDTO.ProfilePictureBase64Data,
                    IsNullable = true
                 },
                 new SqlParameter("@FileName", SqlDbType.VarChar)
                 {
                    Direction = ParameterDirection.Input,
                    Value = userDataDTO.FileName,
                    Size = 150,
                    IsNullable = true
                 }
            };

            using (var cmd = _userDbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "dbo.CreateUser";
                cmd.CommandType = CommandType.StoredProcedure;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.Parameters.AddRange(parameters);
                try
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message);
                }
            }
        }

        public async Task DeleteUser(int id)
        {
            var toDelete = new UserData { UserDataId = id };

            _userDbContext.UserData.Attach(toDelete);
            _userDbContext.UserData.Remove(toDelete);
            await _userDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<UserDataDTO>> GetAllUser()
        {
            var users = await _userDbContext.UserData.FromSqlRaw("SELECT * FROM GetUser").ToListAsync();
            return users.Select(item =>
                new UserDataDTO()
                {
                    Name = item.Name,
                    Address = item.Address,
                    Age = item.Age,
                    Email = item.Email,
                    FileName = item.FileName,
                    Gender = item.Gender,
                    MobileNumber = item.MobileNumber?.ToString(),
                    ProfilePictureBase64Data = item.ProfilePictureBase64Data,
                    UserDataId = item.UserDataId
                }).ToArray();
        }

        public async Task UpdateAsync(UserDataDTO userDataDTO)
        {
            long? mobileNumber = null;
            if (!string.IsNullOrEmpty(userDataDTO.MobileNumber))
            {
                mobileNumber = Convert.ToInt64(userDataDTO.MobileNumber);
            }
            await _userDbContext.Database.ExecuteSqlInterpolatedAsync($"UPDATE dbo.UserData SET Name={userDataDTO.Name}, Age = {userDataDTO.Age}, Gender = {userDataDTO.Gender}, Email = {userDataDTO.Email}, Address = {userDataDTO.Address}, MobileNumber = {mobileNumber} WHERE UserDataId = {userDataDTO.UserDataId}");
        }
    }
}
