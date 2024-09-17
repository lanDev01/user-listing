using user_listing.Dto;
using user_listing.Models;

namespace user_listing.Services
{
    public interface IUserInterface
    {
        Task<ResponseModel<List<UserListDto>>> GetUsers();
        Task<ResponseModel<UserListDto>> GetUserById(int userId);
        Task<ResponseModel<List<UserListDto>>> CreateUser(CreateUserDto usuarioCriarDto);
        Task<ResponseModel<List<UserListDto>>> UpdateUser(UpdateUserDto updateUser);
        Task<ResponseModel<List<UserListDto>>> RemoveUser(int userId);
    }
}
