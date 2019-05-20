using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Transactions;

namespace Doitsu.Service.Core
{
    public interface IUnitOfWork : IUnitOfWork<DbContext>
    {
        #region Unit Of Work features

        #endregion
    }
    public interface IUnitOfWork<TContext> : IDisposable
        where TContext : DbContext
    {
        #region Unit Of Work features
        TContext DbContext { get; }
        void Commit();
        IDbContextTransaction CreateTransac();
        #endregion
    }
}