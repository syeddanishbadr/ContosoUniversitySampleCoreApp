using Framework.Logic;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class UnitOfWork : IDisposable
    {
        private DataBaseContext _context;
        private ILogger _logger;
        public IStudentRepository StudentRepository;

        public UnitOfWork
            (
                DataBaseContext context,
                ILogger<UnitOfWork> logger,
                IStudentRepository studentRepository
            )
        {
            _context = context;
            _logger = logger;
            StudentRepository = studentRepository;
        }

        public async Task<bool> Save()
        {
            var isSaved = false;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    isSaved = await _context.SaveChangesAsync() > 0;
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    _logger.LogError("Error while saving the changes in database", exception);
                    transaction.Rollback();
                    throw;
                }
            }

            return isSaved;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
