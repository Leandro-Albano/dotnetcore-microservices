using IndeedIQ.Common.Domain.Entities;
using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Common.Infrastructure.Messaging.PubSub;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndeedIQ.Common.Infrastructure.Repositories
{
    public class ApplicationDataContext : DbContext, IDomainDataContext
    {
        private readonly IMediator mediator;

        public ApplicationDataContext(DbContextOptions options, IMediator mediator) : base(options)
            => this.mediator = mediator;

        public async Task PersistChangesAsync()
        {
            IEnumerable<Domain.Contracts.DomainEvent> domainEvents
                = this.ChangeTracker.Entries<IAggregateRoot>().SelectMany(e => e.Entity.Events).ToArray();
            using var scope = this.Database.BeginTransaction();
            try
            {
                // First we save so we get the generated ids.
                await this.SaveChangesAsync();

                var envelopes = new List<MessageEnvelope>();
                foreach (var item in domainEvents)
                    envelopes.Add(new MessageEnvelope { Topic = item.GetType().FullName, Message = item });

                await this.mediator.Publish(envelopes.ToArray());
                await scope.CommitAsync();
            }
            catch (System.Exception)
            {
                await scope.RollbackAsync();
                throw;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.LogTo(s => System.Console.WriteLine(s));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
