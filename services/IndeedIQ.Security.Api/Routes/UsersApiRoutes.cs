namespace IndeedIQ.Security.Api.Routes
{
    /// <summary>
    /// Roles management routes.
    /// </summary>
    public static class UsersApiRoutes
    {
        /// <summary>
        /// Route to create a new <see cref="Domain.Entities.UserAggregate.User"/>.
        /// </summary>
        public const string CONTROLLER = "api/users";

        /// <summary>
        /// Route to get an existent <see cref="Domain.Entities.UserAggregate.User"/> by id.
        /// </summary>
        public const string CREATE_USER = "";

        /// <summary>
        /// Route to get an existent <see cref="Domain.Entities.UserAggregate.User"/> by id.
        /// </summary>
        /// <remarks>
        /// Has an {id} parameter.
        /// </remarks>
        public const string GET_USER = "{id}";

        /// <summary>
        /// Route to search <see cref="Domain.Entities.UserAggregate.User"/>.
        /// </summary>
        public const string SEARCH_USER = "";

        /// <summary>
        /// Route to patch an existent <see cref="Domain.Entities.UserAggregate.User"/>.
        /// </summary>
        /// <remarks>
        /// **Parameters:**
        /// {userId}
        /// </remarks>
        public const string PATCH_USER = "{userId}";

        /// <summary>
        /// Route to delete an existent <see cref="Domain.Entities.UserAggregate.User"/>.
        /// </summary>
        /// <remarks>
        /// **Parameters:**
        /// {userId}
        /// </remarks>
        public const string DELETE_USER = "{userId}";

    }
}
