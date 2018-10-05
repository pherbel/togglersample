using System;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Data;

namespace TogglerService.Repositories
{
    public abstract class EFRepositoryBase : IDisposable
    {
        protected ApplicationDbContext Context { get; }

        public EFRepositoryBase(ApplicationDbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Context = context;
        }

        public Task Save(CancellationToken cancellationToken = default)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
