using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using quanlybenh.DataModels.Repositories;
using quanlybenh.Filters;
using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.User;
using quanlybenh.Services.Interfaces;
using quanlybenh.Utilities.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using static quanlybenh.Utilities.Configurations.Constants;
using Constants = quanlybenh.Utilities.Configurations.Constants;

namespace quanlybenh.Controllers
{
    public class LoginController : ApiController
    {
        private readonly ApplicationUserManager _userManager;
        private IUserService _userService;
        public LoginController(ApplicationUserManager userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        // Tao token khi nguoi dung login thanh cong
        private async Task<string> GenerateJwtToken(string UserName, UserDTO userDto)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // get options
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(AppSettings.JwtExpireDays));

            var token = new JwtSecurityToken(
                AppSettings.JwtIssuer,
                AppSettings.JwtIssuer,
                claims,
                expires: expires,
               signingCredentials: creds
               );
            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token)).ConfigureAwait(false);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseResponse<string>> Login([FromBody] LoginDTO model)
        {
            var response = new BaseResponse<string>
            {
                Status = false,
                Error = new Error()
            };
            try
            {
                var user = _userService.GetByUserName(model.UserName);
                if (user == null || user.Status == StatusObject.Deleted || user.Status == StatusObject.Locked)
                {
                    return await Task.FromResult(new BaseResponse<string>(Message.UserNameNotExists, true)).ConfigureAwait(false);
                }

                var result = _userService.CheckPassword(model.UserName, model.Password);
                if (await result.ConfigureAwait(false))
                {
                    response.Data = await GenerateJwtToken(model.UserName, user).ConfigureAwait(false);
                    response.Status = true;
                    return await Task.FromResult(response).ConfigureAwait(false);
                }
                else
                {
                    return await Task.FromResult(new BaseResponse<string>(Message.LoginFaile, true)).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new BaseResponse<string>(Message.LoginFaile, true)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/Account/GetUser")]
        [FeatureAuthentication((int)Constants.Action.CanRead)]
        public async Task<BaseResponse<UserDTO>> GetUser()
        {
            try
            {
                string userId = User.Identity.GetUserId();

                var user = _userService.GetById(userId);

                if (user == null)
                {
                    return await Task.FromResult(new BaseResponse<UserDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
                }
                return await Task.FromResult(new BaseResponse<UserDTO>(user, true)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new BaseResponse<UserDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }
    }
}
