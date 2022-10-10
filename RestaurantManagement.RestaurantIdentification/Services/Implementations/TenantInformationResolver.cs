﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.RestaurantIdentification.Services.Contracts;

namespace RestaurantManagement.RestaurantIdentification.Services.Implementations
{
    public class TenantInformationResolver : ITenantInformationResolver
    {
        private readonly IJWTTokenService _jWTTokenService;

        public TenantInformationResolver( IJWTTokenService jWTTokenService)
        {
            _jWTTokenService = jWTTokenService;
        }

        public Expression<Func<RestaurantSettings, bool>> GetTenantSelector(HttpContext httpContext)
        {
            StringValues token = "";
            httpContext.Request.Headers.TryGetValue("Authentication", out token);

            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException();

            var userModel = _jWTTokenService.DecodeToken(token);

            if (userModel == null)
                throw new ArgumentNullException(nameof(userModel));

            return x => x.RestaurantId == userModel.RestaurantId;
        }
    }
}