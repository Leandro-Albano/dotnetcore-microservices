namespace IndeedIQ.Security.Api.Routes
{
    /// <summary>
    /// Roles management routes.
    /// </summary>
    public static class RoleApiRoutes
    {
        /// <summary>
        /// Root endpoing of account management api.
        /// </summary>
        public const string CONTROLLER = "api/roles";

        /// <summary>
        /// Route to create a new <see cref="Domain.Entities.RoleAggregate.Role"/>.
        /// </summary>
        public const string CREATE_ROLE = "";

        /// <summary>
        /// Route to get an existent <see cref="Domain.Entities.RoleAggregate.Role"/> by id.
        /// </summary>
        /// <remarks>
        /// Has an {id} parameter.
        /// </remarks>
        public const string GET_ROLE = "{id}";

        /// <summary>
        /// Route to search <see cref="Domain.Entities.RoleAggregate.Role"/>.
        /// </summary>
        public const string SEARCH_ROLE = "";

        /// <summary>
        /// Route to patch an existent <see cref="Domain.Entities.RoleAggregate.Role"/>.
        /// </summary>
        /// <remarks>
        /// **Parameters:**
        /// {roleId}
        /// </remarks>
        public const string PATCH_ROLE = "{roleId}";

        /// <summary>
        /// Route to delete an existent <see cref="Domain.Entities.RoleAggregate.Role"/>.
        /// </summary>
        /// <remarks>
        /// **Parameters:**
        /// {roleId}
        /// </remarks>
        public const string DELETE_ROLE = "{roleId}";

        /// <summary>
        /// Route to update the role's permissions. 
        /// </summary>
        /// <remarks>
        /// Has an {roleId} parameter.
        /// </remarks>
        public const string UPDATE_PERMISSIONS = "{roleId}/permissions";

    }
}
