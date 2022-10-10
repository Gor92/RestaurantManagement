using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Attributes;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;

namespace RestaurantManagement.Core.Services.Implementation
{
    public class AccessControlService : IAccessControlService
    {
        private readonly IUserRolePermissionRepository _userRolePermissionRepository;
        private readonly IUserRepository _userRepository;

        public AccessControlService(IUserRolePermissionRepository userRolePermissionRepository,
                                    IUserRepository userRepository)
        {
            _userRolePermissionRepository = userRolePermissionRepository;
            _userRepository = userRepository;
        }

        public async Task ValidateAccessByUserAsync(int userId, IEnumerable<Permission> resourceAccesses, CancellationToken cancellationToken)
        {
            var userPermissions = await _userRolePermissionRepository.GetAsync(x => x.UserId == userId, cancellationToken);
            ValidateAccessAsync(userPermissions.Select(x => new Permission() { AccessLevel = x.AccessLevel, Resource = x.Resource }).ToList(), resourceAccesses);
        }

        public void ValidateAccessAsync(ICollection<Permission> userRolePermissions, IEnumerable<Permission> resourceAccesses)
        {
            ValidateAccess(userRolePermissions, resourceAccesses);
        }

        public async Task ValidateAccessAsync(int userId, Type classType, string methodName, CancellationToken cancellationToken)
        {
            var permissions = (await _userRolePermissionRepository.GetAsync(x => x.UserId == userId, cancellationToken))
                              .Select(x => new Permission() { AccessLevel = x.AccessLevel, Resource = x.Resource })
                              .ToList();
            var neededPermissions = GetAccess(classType.GetMethod(methodName)!, classType);

            ValidateAccess(permissions, neededPermissions);
        }

        private ICollection<Permission> GetAccess(MethodInfo methodInfo, Type classType)
        {
            var list = new List<Permission>();
            var classResourceType = classType.GetCustomAttribute<AccessControlAttribute>();
            var methodAccessAttributes = methodInfo.GetCustomAttributes<AccessControlAttribute>();

            foreach (var attr in methodAccessAttributes)
            {
                var access = new Permission()
                {
                    AccessLevel = attr.AccessLevel,
                    Resource = new Resource()
                    {
                        Type = attr.ResourceType == ResourceType.Undefined
                                                     ? classResourceType!.ResourceType
                                                     : attr.ResourceType
                    },
                };

                if (access.Resource.Type == ResourceType.Undefined)
                    throw new ArgumentException($"invalid attribute in {methodInfo.Name}");

                list.Add(access);
            }

            return list;
        }

        private static void ValidateAccess(IEnumerable<Permission> userPesmissions, IEnumerable<Permission> neededPemrissions)
        {
            if (neededPemrissions == null || !neededPemrissions.Any())
                return;

            var dict = GetDict(userPesmissions);

            foreach (var neededPermission in neededPemrissions)
            {
                bool isResourceAvailable = dict.ContainsKey(neededPermission.Resource.Type);
                bool isAccessLevelValid = isResourceAvailable && dict[neededPermission.Resource.Type].HasFlag(neededPermission.AccessLevel);
                if (!isResourceAvailable || !isAccessLevelValid)
                    throw new Exception($"access to {neededPermission.Resource.Type} resource needed with {neededPermission.AccessLevel} permission");

            }
        }

        private static IDictionary<ResourceType, AccessLevel> GetDict(IEnumerable<Permission> permissions)
        {
            var dict = new Dictionary<ResourceType, AccessLevel>();

            foreach (var permission in permissions)
            {
                if (dict.ContainsKey(permission.Resource.Type))
                    dict[permission.Resource.Type] |= permission.AccessLevel;
                else
                    dict.Add(permission.Resource.Type, permission.AccessLevel);
            }

            return dict;
        }
    }
}

