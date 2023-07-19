using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Models;
using WEB_API.Data;
using WEB_API.Models;
using WEB_API.Repository.Interface;

namespace WEB_API.Repository.ServiceClass
{
    public class UserDBService : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretKey;
        private readonly IMapper _mapper;

        public UserDBService(ApplicationDbContext db, IConfiguration configuration,
            UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _roleManager = roleManager;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public ApplicationUser GetUserInfo(string username)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName == username);
            if (user != null)
            {
                return user ;
            }
            return null;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel loginRequestDTO)
        {
            var user = _db.ApplicationUsers
                .FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);


            if (user == null || isValid == false)
            {
                return new LoginResponseModel()
                {
                    Token = "",
                    User = null
                };
            }

            //if user was found generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                    new Claim(ClaimTypes.Email,user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseModel loginResponseDTO = new LoginResponseModel()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserModel>(user),

            };
            return loginResponseDTO;
        }

        public async Task<UserModel> Register(RegisterationRequestModel registerationRequestDTO)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registerationRequestDTO.UserName,
                Email = registerationRequestDTO.Name,
                NormalizedEmail = registerationRequestDTO.Name.ToUpper(),
                NormalizedUserName = registerationRequestDTO.Name,
                Name = registerationRequestDTO.Customer_Id,
                Soc_Id = registerationRequestDTO.Soc_No,
                Soc_Name = registerationRequestDTO.Soc_Name

            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequestDTO.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.AdminRole));
                        await _roleManager.CreateAsync(new IdentityRole(SD.CustomerRole));
                    }
                    if (!_roleManager.RoleExistsAsync(SD.MasterAdminRole).GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.MasterAdminRole));
                    }

                    if (registerationRequestDTO.Role != null && registerationRequestDTO.Role == SD.AdminRole)
                    {
                        await _userManager.AddToRoleAsync(user, SD.AdminRole);
                    }
                    else if (registerationRequestDTO.Role != null && registerationRequestDTO.Role == SD.MasterAdminRole)
                    {
                        await _userManager.AddToRoleAsync(user, SD.MasterAdminRole);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.CustomerRole);
                    }

                    var userToReturn = _db.ApplicationUsers
                        .FirstOrDefault(u => u.UserName == registerationRequestDTO.UserName);
                    return _mapper.Map<UserModel>(userToReturn);

                }
            }
            catch (Exception e)
            {

            }

            return new UserModel();
        }

        public bool AnyUserPresent()
        {
            var user = _db.ApplicationUsers.FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public List<string> GetRoles()
        {
            var roles = _db.Roles.ToArray();
            List<string> userRoles = null;
            if (roles != null)
            {
                userRoles = new List<string>();
                foreach (var eachrole in roles)
                    userRoles.Add(eachrole.Name);

            }
            return userRoles;
        }

        public string GetMaxSocietyId()
        {
            var UserInfo = _db.Users.Max(t => t.Soc_Id);
            return UserInfo;


        }
    }
}

