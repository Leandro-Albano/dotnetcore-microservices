namespace IndeedIQ.Security.Api.Routes
{
    /// <summary>
    /// Application resource management routes.
    /// </summary>
    public static class ApplicationResourceApiRoutes
    {
        /// <summary>
        /// Route to create a new <see cref="Domain.Entities.ResourceAggregate.ApplicationResource"/>.
        /// </summary>
        public const string CONTROLLER = "api/ApplicationResources";

        /// <summary>
        /// Route to get an existent <see cref="Domain.Entities.ResourceAggregate.ApplicationResource"/> by id.
        /// </summary>
        public const string CREATE_RESOURCE = "";

        /// <summary>
        /// Route to get an existent <see cref="Domain.Entities.ResourceAggregate.ApplicationResource"/> by id.
        /// </summary>
        /// <remarks>
        /// Has an {id} parameter.
        /// </remarks>
        public const string GET_RESOURCE = "{id}";

        /// <summary>
        /// Route to search <see cref="Domain.Entities.ResourceAggregate.ApplicationResource"/>.
        /// </summary>
        public const string SEARCH_RESOURCE = "";

        /// <summary>
        /// Route to add a new <see cref="Domain.Entities.ResourceAggregate.ResourceAction"/> to an existent <see cref="Domain.Entities.ResourceAggregate.ApplicationResource"/>. 
        /// </summary>
        /// <remarks>
        /// Has an {applicationResourceId} parameter.
        /// </remarks>
        public const string ADD_ACTION = "{applicationResourceId}/AvailableActions";

        /// <summary>
        /// Route to remove a <see cref="Domain.Entities.ResourceAggregate.ResourceAction"/> from an existent <see cref="Domain.Entities.ResourceAggregate.ApplicationResource"/>. 
        /// </summary>
        /// <remarks>
        /// **Parameters:**
        /// {applicationResourceId}
        /// {actionId}
        /// </remarks>
        public const string REMOVE_ACTION = "{applicationResourceId}/AvailableActions/{actionId}";

        /// <summary>
        /// Route to patch an existent <see cref="Domain.Entities.ResourceAggregate.ResourceAction"/> on an existent <see cref="Domain.Entities.ResourceAggregate.ApplicationResource"/>.
        /// </summary>
        /// <remarks>
        /// **Parameters:**
        /// {applicationResourceId}
        /// {actionId}
        /// </remarks>
        public const string PATCH_ACTION = "{applicationResourceId}/AvailableActions/{actionId}";

        /// <summary>
        /// Route to patch an existent <see cref="Domain.Entities.ResourceAggregate.ApplicationResource"/>.
        /// </summary>
        /// <remarks>
        /// **Parameters:**
        /// {applicationResourceId}
        /// </remarks>
        public const string PATCH_RESOURCE = "{applicationResourceId}";

        /// <summary>
        /// Route to delete an existent <see cref="Domain.Entities.ResourceAggregate.ApplicationResource"/>.
        /// </summary>
        /// <remarks>
        /// **Parameters:**
        /// {applicationResourceId}
        /// </remarks>
        public const string DELETE_RESOURCE = "{applicationResourceId}";
    }
}
