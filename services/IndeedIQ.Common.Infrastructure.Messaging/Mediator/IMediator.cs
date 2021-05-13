using IndeedIQ.Common.Infrastructure.Messaging.PubSub;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace IndeedIQ.Common.Infrastructure.Messaging.Mediator
{
    /// <summary>
    /// Encapsulate and mediates the communication between objects.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Sends a command to registered handlers.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message to be sent, it must be a reference type.</typeparam>
        /// <param name="command">The message to be sent.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        Task Send<TMessage>([NotNull] TMessage command) where TMessage : class;

        /// <summary>
        /// Sends a command to registered handlers.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <typeparam name="TResponse">The type of the expected response.</typeparam>
        /// <param name="command">The message to be sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object that represents the asynchronous send operation. 
        /// The task result contains the response object emitted by the handler.
        /// </returns>
        Task<TResponse> Send<TMessage, TResponse>(TMessage command) where TMessage : IMessage<TResponse>;

        /// <summary>
        /// Publishes the message to the streaming but does not await for any response.
        /// </summary>
        /// <param name="message">The message to be published.</param>
        /// <returns>A <see cref="Task"/> object that represents the asynchronous publish operation.</returns>
        Task Publish(params MessageEnvelope[] message);
    }
}
