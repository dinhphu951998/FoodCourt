using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Doitsu.Service.Core
{
    public class UnitOfWork : UnitOfWork<DbContext>, IUnitOfWork, IDisposable
    {
        public UnitOfWork(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable
        where TContext : DbContext
    {
        private TContext _dbContext;
        public UnitOfWork(TContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region Unit Of Work features
        private bool _disposed = false;
        public TContext DbContext { get => _dbContext; }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        public IDbContextTransaction CreateTransac()
        {
            var transaction = _dbContext.Database.BeginTransaction();
            return transaction;
        }
        #endregion

    }
}
