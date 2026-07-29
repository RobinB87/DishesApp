using Microsoft.AspNetCore.Authorization;

namespace MyDishesApp.WebApi.Authorization
{
    /// <summary>
    /// Class with policies for authorization
    /// </summary>
    public class Policies
    {
        public const string Admin = "Admin";
        public const string User = "User";

        /// <summary>
        /// Get the admin authorization policy
        /// </summary>
        /// <returns>A new AuthorizationPolicyBuilder with the admin role</returns>
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        /// <summary>
        /// Get the user authorization policy
        /// </summary>
        /// <returns>A new AuthorizationPolicyBuilder with the admin role</returns>
        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }
    }
}
