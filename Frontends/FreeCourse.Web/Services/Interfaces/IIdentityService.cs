﻿using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models.InputModels;
using IdentityModel.Client;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<ResponseDto<bool>> SingIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeResfreshToken();
    }
}
