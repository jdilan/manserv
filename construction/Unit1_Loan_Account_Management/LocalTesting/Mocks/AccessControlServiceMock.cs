/*
================================================================================
MANSERV Loan Account Management System - Local Testing
Access Control Service Mock
================================================================================
Purpose: Mock implementation of IAccessControlService for local testing
Author: System Generated
Date: December 6, 2025
================================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using ManservLoanSystem.Models.Common;
using ManservLoanSystem.ExternalServices;

namespace ManservLoanSystem.LocalTesting.Mocks
{
    /// <summary>
    /// Mock implementation of access control service
    /// Supports role-based access control and center restrictions
    /// </summary>
    public class AccessControlServiceMock : IAccessControlService
    {
        private Dictionary<string, UserPermissions> _users;

        public AccessControlServiceMock()
        {
            InitializeUsers();
        }

        #region IAccessControlService Implementation

        public ServiceResponse<bool> CheckUserPermission(string userId, string action, int resourceId)
        {
            if (!_users.ContainsKey(userId))
            {
                return ServiceResponse<bool>.Failure("USER_NOT_FOUND", $"User '{userId}' not found");
            }

            var user = _users[userId];

            // Check role-based permissions
            bool hasPermission = action.ToUpper() switch
            {
                "CREATE" => user.Role == "User" || user.Role == "Authorizer" || user.Role == "Administrator",
                "VIEW" => user.Role == "User" || user.Role == "Authorizer" || user.Role == "Administrator",
                "UPDATE" => user.Role == "Authorizer" || user.Role == "Administrator",
                "DELETE" => user.Role == "Administrator",
                "CLOSE" => user.Role == "Administrator",
                "ARCHIVE" => user.Role == "Administrator",
                "REOPEN" => user.Role == "Administrator",
                _ => false
            };

            return ServiceResponse<bool>.Success(hasPermission);
        }

        public ServiceResponse<string> GetUserRole(string userId)
        {
            if (!_users.ContainsKey(userId))
            {
                return ServiceResponse<string>.Failure("USER_NOT_FOUND", $"User '{userId}' not found");
            }

            return ServiceResponse<string>.Success(_users[userId].Role);
        }

        public ServiceResponse<List<string>> GetUserCenters(string userId)
        {
            if (!_users.ContainsKey(userId))
            {
                return ServiceResponse<List<string>>.Failure("USER_NOT_FOUND", $"User '{userId}' not found");
            }

            return ServiceResponse<List<string>>.Success(_users[userId].Centers);
        }

        public ServiceResponse<bool> CheckCenterAccess(string userId, string centerCode)
        {
            if (!_users.ContainsKey(userId))
            {
                return ServiceResponse<bool>.Failure("USER_NOT_FOUND", $"User '{userId}' not found");
            }

            var user = _users[userId];
            var hasAccess = user.Centers.Contains(centerCode);

            return ServiceResponse<bool>.Success(hasAccess);
        }

        #endregion

        #region User Management

        private void InitializeUsers()
        {
            _users = new Dictionary<string, UserPermissions>
            {
                // Regular User - Limited permissions, single center
                ["user1"] = new UserPermissions
                {
                    UserId = "user1",
                    Username = "John User",
                    Role = "User",
                    Centers = new List<string> { "01" }
                },

                // Authorizer - Can update, multiple centers
                ["auth1"] = new UserPermissions
                {
                    UserId = "auth1",
                    Username = "Jane Authorizer",
                    Role = "Authorizer",
                    Centers = new List<string> { "01", "02" }
                },

                // Administrator - Full permissions, all centers
                ["admin1"] = new UserPermissions
                {
                    UserId = "admin1",
                    Username = "Admin User",
                    Role = "Administrator",
                    Centers = new List<string> { "01", "02", "03" }
                },

                // Test user with no centers (for testing restrictions)
                ["restricted1"] = new UserPermissions
                {
                    UserId = "restricted1",
                    Username = "Restricted User",
                    Role = "User",
                    Centers = new List<string>()
                },

                // Default system user
                ["SYSTEM"] = new UserPermissions
                {
                    UserId = "SYSTEM",
                    Username = "System",
                    Role = "Administrator",
                    Centers = new List<string> { "01", "02", "03" }
                }
            };
        }

        /// <summary>
        /// Add a test user dynamically
        /// </summary>
        public void AddTestUser(string userId, string username, string role, List<string> centers)
        {
            _users[userId] = new UserPermissions
            {
                UserId = userId,
                Username = username,
                Role = role,
                Centers = centers
            };
        }

        /// <summary>
        /// Get all configured users (for testing/debugging)
        /// </summary>
        public List<UserPermissions> GetAllUsers()
        {
            return _users.Values.ToList();
        }

        #endregion
    }

    #region Supporting Classes

    public class UserPermissions
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public List<string> Centers { get; set; } = new List<string>();
    }

    #endregion
}
