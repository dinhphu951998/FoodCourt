using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Doitsu.Service.Core
{

    public interface IBaseService<TEntity, TViewModel>
        where TEntity : class, new()
        where TViewModel : BaseViewModel<TEntity>
    {
        TViewModel FindById(int id);
        Task<TViewModel> FindByIdAsync(int id);
        TViewModel FindById<TKey>(TKey id);
        Task<TViewModel> FindByIdAsync<TKey>(TKey id);

        IQueryable<TViewModel> GetAll();
        IQueryable<TViewModel> GetAllActive();
        IQueryable<TViewModel> Get(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TViewModel> GetActive(Expression<Func<TEntity, bool>> predicate);

        TViewModel FirstOrDefault();
        TViewModel FirstOrDefaultActive();
        TViewModel FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TViewModel FirstOrDefaultActive(Expression<Func<TEntity, bool>> predicate);
        Task<TViewModel> FirstOrDefaultAsync();
        Task<TViewModel> FirstOrDefaultActiveAsync();
        /// <summary>
        /// Firsts the or default async. No tracking and filter expression
        /// </summary>
        /// <returns>The or default async.</returns>
        /// <param name="predicate">Predicate.</param>
        Task<TViewModel> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Firsts the or default active. No tracking and filter expression
        /// </summary>
        /// <returns>The or default active.</returns>
        /// <param name="predicate">Predicate.</param>
        Task<TViewModel> FirstOrDefaultActiveAsync(Expression<Func<TEntity, bool>> predicate);

        TViewModel Create(TViewModel viewModel);
        Task<TViewModel> CreateAsync(TViewModel viewModel);
        TViewModel Update(TViewModel viewModel);
        Task<TViewModel> UpdateAsync(TViewModel viewModel);
        TViewModel Update<TKey>(TKey id, TViewModel viewModel);
        Task<TViewModel> UpdateAsync<TKey>(TKey id, TViewModel viewModel);
        Task<TViewModel> DeactiveAsync<TKey>(TKey id);

        void DeleteByObj(TViewModel id);
        Task DeleteByObjAsync(TViewModel id);

        void DeleteByKey<TKey>(TKey id);
        Task DeleteByKeyAsync<TKey>(TKey id);
    }
}