﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.DAL.Database;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.DAL.Abstraction;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;

namespace RestaurantManagement.DAL.Repositories
{
    public class RestaurantSettingsRepository : GenericRepository<RestaurantSettings>, IRestaurantSettingsRepository
    {
        public RestaurantSettingsRepository(RestaurantManagementContext context, IAuthService authService) : base(context, authService)
        {

        }
    }
}