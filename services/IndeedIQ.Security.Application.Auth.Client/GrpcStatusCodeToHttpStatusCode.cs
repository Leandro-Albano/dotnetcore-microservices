using Grpc.Core;

using System.Collections.Generic;
using System.Net;

namespace IndeedIQ.Security.Application.Client
{
    public static class GrpcStatusCodeToHttpStatusCode
    {
        public static Dictionary<StatusCode, HttpStatusCode> Map = new Dictionary<StatusCode, HttpStatusCode>
        {
            { StatusCode.Aborted, HttpStatusCode.InternalServerError },
            { StatusCode.AlreadyExists, HttpStatusCode.Conflict },
            { StatusCode.Cancelled, HttpStatusCode.InternalServerError },
            { StatusCode.DataLoss, HttpStatusCode.InternalServerError },
            { StatusCode.DeadlineExceeded, HttpStatusCode.InternalServerError },
            { StatusCode.FailedPrecondition, HttpStatusCode.PreconditionFailed },
            { StatusCode.Internal, HttpStatusCode.InternalServerError },
            { StatusCode.InvalidArgument, HttpStatusCode.BadRequest },
            { StatusCode.NotFound, HttpStatusCode.NotFound },
            { StatusCode.OK, HttpStatusCode.OK },
            { StatusCode.OutOfRange, HttpStatusCode.RequestedRangeNotSatisfiable },
            { StatusCode.PermissionDenied, HttpStatusCode.Forbidden },
            { StatusCode.ResourceExhausted, HttpStatusCode.BadGateway },
            { StatusCode.Unauthenticated, HttpStatusCode.Unauthorized },
            { StatusCode.Unavailable, HttpStatusCode.BadGateway },
            { StatusCode.Unimplemented, HttpStatusCode.NotImplemented },
            { StatusCode.Unknown, HttpStatusCode.InternalServerError },
        };
    }
}
