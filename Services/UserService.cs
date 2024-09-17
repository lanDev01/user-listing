using AutoMapper;
using Dapper;
using System.Data.SqlClient;
using user_listing.Dto;
using user_listing.Models;

namespace user_listing.Services
{
    public class UserService : IUserInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<UserListDto>>> CreateUser(CreateUserDto user)
        {
            ResponseModel<List<UserListDto>> response = new ResponseModel<List<UserListDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var rowsAffected = await connection.ExecuteAsync(
                    "INSERT INTO Usuarios (Nome, Email, Cargo, Salario, CPF, Senha, Situacao) " +
                    "VALUES (@Nome, @Email, @Cargo, @Salario, @CPF, @Senha, @Situacao)", user);

                if (rowsAffected == 0)
                {
                    response.Message = "Ocorreu um erro ao realizar o registro!";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListUser(connection);

                var usuariosMapeados = _mapper.Map<List<UserListDto>>(usuarios);

                response.Data = usuariosMapeados;
                response.Status = true;
                response.Message = "Usuário criado com sucesso!";
            }

            return response;
        }

        private static async Task<IEnumerable<User>> ListUser(SqlConnection connection)
        {
            return await connection.QueryAsync<User>("SELECT * FROM Usuarios");
        }

        public async Task<ResponseModel<UserListDto>> GetUserById(int userId)
        {
            ResponseModel<UserListDto> response = new ResponseModel<UserListDto>();

            using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var userList = await connection.QueryFirstOrDefaultAsync<User>("select * from Usuarios where id = @Id", new {Id = userId});

                if (userList == null)
                {
                    response.Message = "Nenhum usuário localizado!";
                    response.Status = false;
                    return response;
                }

                var mappedUser = _mapper.Map<UserListDto>(userList);

                response.Data = mappedUser;
                response.Message = "Usuário localizado com sucesso!";
            }
            
            return response;
        }

        public async Task<ResponseModel<List<UserListDto>>> GetUsers()
        {
            ResponseModel<List<UserListDto>> response = new ResponseModel<List<UserListDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var userList = await connection.QueryAsync<User>("select * from Usuarios");

                if (userList.Count() == 0)
                {
                    response.Message = "Nenhum usuário localizado!";
                    response.Status = false;
                    return response;
                }

                var mappedUser = _mapper.Map<List<UserListDto>>(userList);

                response.Data = mappedUser;
                response.Message = "Usuários localizados com sucesso!";
            }

            return response;
        }

        public async Task<ResponseModel<List<UserListDto>>> UpdateUser(UpdateUserDto updateUser)
        {
            ResponseModel<List<UserListDto>> response = new ResponseModel<List<UserListDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var rowsAffected = await connection.ExecuteAsync(
                    "UPDATE Usuarios SET Nome = @Nome, Email = @Email, Cargo = @Cargo, Salario = @Salario, Situacao = @Situacao, CPF = @CPF WHERE Id = @Id",
                    updateUser);

                if (rowsAffected == 0)
                {
                    response.Message = "Ocorreu um erro ao realizar a edição!";
                    response.Status = false;
                    return response;
                }

                var users = await ListUser(connection);
                var mappedUsers = _mapper.Map<List<UserListDto>>(users);

                response.Data = mappedUsers;
                response.Message = "Usuários atualizados com sucesso!";
            }

            return response;
        }

        public async Task<ResponseModel<List<UserListDto>>> RemoveUser(int userId)
        {
            ResponseModel<List<UserListDto>> response = new ResponseModel<List<UserListDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var rowsAffected = await connection.ExecuteAsync("delete from Usuarios where id = @Id", new {Id = userId});

                if (rowsAffected == 0)
                {
                    response.Message = "Ocorreu um erro ao realizar a remoção!";
                    response.Status = false;
                    return response;
                }

                var users = await ListUser(connection);

                var mappedUsers = _mapper.Map<List<UserListDto>>(users);

                response.Data = mappedUsers;
                response.Message = "Usuário listado com sucesso!";
            }

            return response;
        }
    }
}
