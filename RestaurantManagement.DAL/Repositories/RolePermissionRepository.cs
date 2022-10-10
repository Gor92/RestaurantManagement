﻿using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Repositories.Contracts;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.DAL.Abstraction;
using RestaurantManagement.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.DAL.Repositories
{
    public class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(RestaurantManagementContext context, IAuthService authService) : base(context, authService)
        {

        }
    }
}