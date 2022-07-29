using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Repository.Models;
using Web.ViewModels.User;

namespace BL.Users
{
    public interface IUserBL
    {
        bool CreateUser(RegisterUserFormModel userModel);

        Task<bool> UpdateUser(int userId, UpdateUserFormModel userModel);

        Task<bool> UpdateRole(int userId, UpdateRoleFormModel userModel);

        User ExistUser(LoginUserFormModel user);

        InformationUserFormModel InfoUser(int userId);

        InformationRoleFormModel InfoRole(int userId);

        Task<List<InformationUserFormModel>> IndexUsers();

        Task<List<InformationUserFormModel>> StudentsCourse(int courseId);

        Task<List<InformationUserFormModel>> TeachersCourse(int courseId);

        bool Vot(int userId,int categoruId);

        bool DeleteUser(int userId);
    }
}
