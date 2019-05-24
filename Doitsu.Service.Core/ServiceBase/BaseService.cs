using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doitsu.Service.Core.ModelBase;
using Doitsu.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Doitsu.Service.Core
{

    public abstract class BaseService<TEntity, TViewModel> : IBaseService<TEntity, TViewModel>
        where TEntity : class, new()
        where TViewModel : BaseViewModel<TEntity>, new()
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private DbContext dbContext;

        public IUnitOfWork UnitOfWork { get => unitOfWork; }
        public IMapper Mapper { get => mapper; }

        private DbSet<TEntity> selfDbSet;

        public DbSet<TEntity> SelfDbSet { get => selfDbSet; }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.dbContext = unitOfWork.DbContext;
            this.selfDbSet = this.dbContext.Set<TEntity>();
            this.mapper = mapper;
        }

        #region Find Services
        /// <summary>
        /// Finds the by identifier.
        /// But identitifier default is integer
        /// </summary>
        /// <returns>The by identifier.</returns>
        /// <param name="id">Identifier.</param>
        public TViewModel FindById(int id)
        {
            var entity = selfDbSet.Find(id);
            return CreateVM(entity);
        }
        /// <summary>
        /// Finds the by identifier async.
        /// </summary>
        /// <returns>The by identifier async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<TViewModel> FindByIdAsync(int id)
        {
            var entity = await selfDbSet.FindAsync(id);
            return CreateVM(entity);
        }
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <returns>The by identifier.</returns>
        /// <param name="id">Identifier.</param>
        /// <typeparam name="TKey">The 1st type parameter.</typeparam>
        public TViewModel FindById<TKey>(TKey id)
        {
            var entity = selfDbSet.Find(id);
            return CreateVM(entity);
        }
        /// <summary>
        /// Finds the by identifier async.
        /// </summary>
        /// <returns>The by identifier async.</returns>
        /// <param name="id">Identifier.</param>
        /// <typeparam name="TKey">The 1st type parameter.</typeparam>
        public async Task<TViewModel> FindByIdAsync<TKey>(TKey id)
        {
            var entity = await selfDbSet.FindAsync(id);
            return CreateVM(entity);
        }
        #endregion
        #region Get Services
        /// <summary>
        /// Gets all, get all no tracking data.
        /// </summary>
        /// <returns>The all.</returns>
        public IQueryable<TViewModel> GetAll()
        {
            return GetAllAsNoTracking().ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        /// <summary>
        /// Gets all, get all no tracking data and is active = true
        /// </summary>
        /// <returns>The all active.</returns>
        public IQueryable<TViewModel> GetAllActive()
        {
            return GetAllActiveAsNoTracking().ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        /// <summary>
        /// Get data with filter expression
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="predicate">Predicate.</param>
        public IQueryable<TViewModel> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAsNoTracking(predicate).ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        /// <summary>
        /// Get active data with filter expression
        /// </summary>
        /// <returns>The active.</returns>
        /// <param name="predicate">Predicate.</param>
        public IQueryable<TViewModel> GetActive(Expression<Func<TEntity, bool>> predicate)
        {
            return GetActiveAsNoTracking(predicate).ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        #endregion
        #region First or Default Services
        /// <summary>
        /// Firsts the or default. As no tracking
        /// </summary>
        /// <returns>The or default.</returns>
        public TViewModel FirstOrDefault()
        {
            var e = GetAllAsNoTracking().FirstOrDefault();
            return CreateVM(e);
        }

        public TViewModel FirstOrDefaultActive()
        {
            var e = GetAllActiveAsNoTracking().FirstOrDefault();
            return CreateVM(e);
        }

        public TViewModel FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var e = GetAsNoTracking(predicate).FirstOrDefault();
            return CreateVM(e);
        }

        public TViewModel FirstOrDefaultActive(Expression<Func<TEntity, bool>> predicate)
        {
            var e = GetActiveAsNoTracking(predicate).FirstOrDefault();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultAsync()
        {
            var e = await GetAllAsNoTracking().FirstOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultActiveAsync()
        {
            var e = await GetAllActiveAsNoTracking().FirstOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var e = await GetAsNoTracking(predicate).FirstOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultActiveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var e = await GetActiveAsNoTracking(predicate).FirstOrDefaultAsync();
            return CreateVM(e);
        }

        #endregion
        #region Create Services
        public TViewModel Create(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            normalizeToCreate(e);
            selfDbSet.Add(e);
            dbContext.SaveChanges();
            return CreateVM(e);
        }
        public async Task<TViewModel> CreateAsync(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            normalizeToCreate(e);
            await selfDbSet.AddAsync(e);
            await dbContext.SaveChangesAsync();
            return CreateVM(e);
        }

        #endregion
        #region Delete Services
        public void DeleteByObj(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            selfDbSet.Remove(e);
            dbContext.SaveChanges();
        }
        public async Task DeleteByObjAsync(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            selfDbSet.Remove(e);
            await dbContext.SaveChangesAsync();
        }
        public void DeleteByKey<TKey>(TKey id)
        {
            var entity = selfDbSet.Find(id);
            selfDbSet.Remove(entity);
            dbContext.SaveChanges();
        }
        public async Task DeleteByKeyAsync<TKey>(TKey id)
        {
            var entity = selfDbSet.Find(id);
            selfDbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
        #endregion
        #region Update Services
        public TViewModel Update(TViewModel viewModel)
        {
            var entity = VMToE(viewModel);
            selfDbSet.Update(entity);
            dbContext.SaveChanges();
            return CreateVM(entity);
        }

        public async Task<TViewModel> UpdateAsync(TViewModel viewModel)
        {
            var entity = VMToE(viewModel);
            selfDbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return CreateVM(entity);
        }
        /// <summary>
        /// Update the specified id and viewModel.
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="viewModel">View model.</param>
        /// <typeparam name="TKey">The 1st type parameter.</typeparam>
        public TViewModel Update<TKey>(TKey id, TViewModel viewModel)
        {
            var entity = VMToE(viewModel);
            selfDbSet.Update(entity);
            dbContext.SaveChanges();
            return CreateVM(entity);
        }
        public async Task<TViewModel> UpdateAsync<TKey>(TKey id, TViewModel viewModel)
        {
            var entity = await selfDbSet.FindAsync(id);
            VMToE(viewModel, entity);
            selfDbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return CreateVM(entity);
        }
        public async Task<TViewModel> DeactiveAsync<TKey>(TKey id)
        {
            var entity = await selfDbSet.FindAsync(id);
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                ((IActivable)entity).Activated = false;
            }
            selfDbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return CreateVM(entity);

        }
        #endregion
        #region Another Utils
        protected static DbSet<TRepoEntity> GetRepo<TRepoEntity>(UnitOfWork unitOfWork)
            where TRepoEntity : class
        {
            var result = unitOfWork.DbContext.Set<TRepoEntity>();
            return result;
        }
        #endregion
        #region As No Tracking Getter Utils
        protected IQueryable<TEntity> GetAllAsNoTracking()
        {
            return selfDbSet.AsNoTracking();
        }
        protected IQueryable<TEntity> GetAllActiveAsNoTracking()
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                var sourceQuerable = selfDbSet.AsNoTracking();
                Expression<Func<TEntity, bool>> checkActiveExpress = (TEntity t) => ((IActivable)t).Activated;
                var cleanCheckActiveExpress = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(checkActiveExpress);
                return sourceQuerable.Where(cleanCheckActiveExpress);
            }
            return this.GetAllAsNoTracking();
        }
        protected IQueryable<TEntity> GetAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return selfDbSet.AsNoTracking().Where(predicate);
        }
        protected IQueryable<TEntity> GetActiveAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                var sourceQuerable = selfDbSet.AsNoTracking().Where<TEntity>(predicate);
                Expression<Func<TEntity, bool>> checkActiveExpress = (TEntity t) => ((IActivable)t).Activated;
                var cleanCheckActiveExpress = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(checkActiveExpress);
                return sourceQuerable.Where(cleanCheckActiveExpress);

            }
            return this.GetAsNoTracking(predicate);
        }
        #endregion
        #region Mapping Utils
        // Area to solve View Model map to Entity
        protected TAlternativeEntity VMToE<TAlternativeViewModel, TAlternativeEntity>(TAlternativeViewModel model)
            where TAlternativeEntity : class, new()
        {
            var e = new TAlternativeEntity();
            mapper.Map(model, e);
            return e;
        }
        protected TEntity VMToE(TViewModel model)
        {
            var e = new TEntity();
            mapper.Map(model, e);
            return e;
        }
        protected void VMToE<TAlternativeViewModel, TAlternativeEntity>(TAlternativeViewModel model, TAlternativeEntity e)
        {
            mapper.Map(model, e);
        }
        protected void VMToE(TViewModel model, TEntity e)
        {
            mapper.Map(model, e);
        }

        // Area to solve Entity map to View Model
        protected TAlternativeViewModel EToVM<TAlternativeEntity, TAlternativeViewModel>(TAlternativeEntity model)
           where TAlternativeViewModel : class, new()
        {
            var vm = new TAlternativeViewModel();
            mapper.Map(model, vm);
            return vm;
        }
        protected TViewModel EToVM(TEntity model)
        {
            var vm = new TViewModel();
            mapper.Map(model, vm);
            return vm;
        }
        protected void EToVM<TAlternativeEntity, TAlternativeViewModel>(TAlternativeEntity e, TAlternativeViewModel model)
            where TAlternativeEntity : class, new()
        {
            mapper.Map(e, model);
        }
        protected void EToVM(TEntity e, TViewModel model)
        {
            mapper.Map(e, model);
        }
        protected TEntity CreateEntity(TViewModel viewModel)
        {
            if (viewModel == null) return null;
            viewModel.SetMapper(Mapper);
            return viewModel.ToEntity();
        }
        protected TViewModel CreateVM(TEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            TViewModel vModel = new TViewModel();
            vModel.SetMapper(Mapper);
            vModel.CopyFromEntity(entity);
            return vModel;
        }
        #endregion

        private void addCreateTime(TEntity model)
        {
            if (typeof(ICreateTimeEnable).IsAssignableFrom(typeof(TEntity)))
            {
                ((ICreateTimeEnable)model).CreateTime = DateTime.UtcNow;
            }
        }

        private void addActivated(TEntity model)
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                ((IActivable)model).Activated = true;
            }
        }

        private void normalizeToCreate(TEntity model)
        {
            addCreateTime(model);
            addActivated(model);
        }

    }

    public abstract class BaseService<TEntity, TViewModel, TDbContext> : IBaseService<TEntity, TViewModel>
        where TEntity : class, new()
        where TViewModel : BaseViewModel<TEntity>, new()
        where TDbContext : DbContext
    {
        private IUnitOfWork<TDbContext> unitOfWork;
        private IMapper mapper;
        private TDbContext dbContext;

        public IUnitOfWork<TDbContext> UnitOfWork { get => unitOfWork; }
        public IMapper Mapper { get => mapper; }

        private DbSet<TEntity> selfDbSet;
        public DbSet<TEntity> SelfDbSet { get => selfDbSet; }

        public BaseService(IUnitOfWork<TDbContext> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.dbContext = unitOfWork.DbContext;
            this.selfDbSet = this.dbContext.Set<TEntity>();
            this.mapper = mapper;
        }
        #region Find Services
        public TViewModel FindById(int id)
        {
            var entity = selfDbSet.Find(id);
            return CreateVM(entity);
        }
        public async Task<TViewModel> FindByIdAsync(int id)
        {
            var entity = await selfDbSet.FindAsync(id);
            return CreateVM(entity);
        }
        public TViewModel FindById<TKey>(TKey id)
        {
            var entity = selfDbSet.Find(id);
            return CreateVM(entity);
        }
        public async Task<TViewModel> FindByIdAsync<TKey>(TKey id)
        {
            var entity = await selfDbSet.FindAsync(id);
            return CreateVM(entity);
        }
        #endregion
        #region Get Services
        public IQueryable<TViewModel> GetAll()
        {
            return GetAllAsNoTracking().ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        public IQueryable<TViewModel> GetAllActive()
        {
            return GetAllActiveAsNoTracking().ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        public IQueryable<TViewModel> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAsNoTracking(predicate).ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        public IQueryable<TViewModel> GetActive(Expression<Func<TEntity, bool>> predicate)
        {
            return GetActiveAsNoTracking(predicate).ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        #endregion
        #region First or Default Services
        public TViewModel FirstOrDefault()
        {
            var e = GetAllAsNoTracking().FirstOrDefault();
            return CreateVM(e);
        }
        public TViewModel FirstOrDefaultActive()
        {
            var e = GetAllActiveAsNoTracking().FirstOrDefault();
            return CreateVM(e);
        }
        public TViewModel FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var e = GetAsNoTracking(predicate).FirstOrDefault();
            return CreateVM(e);
        }
        public TViewModel FirstOrDefaultActive(Expression<Func<TEntity, bool>> predicate)
        {
            var e = GetActiveAsNoTracking(predicate).FirstOrDefault();
            return CreateVM(e);
        }


        public async Task<TViewModel> FirstOrDefaultAsync()
        {
            var e = await GetAllAsNoTracking().FirstOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultActiveAsync()
        {
            var e = await GetAllActiveAsNoTracking().FirstOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var e = await GetAsNoTracking(predicate).FirstOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultActiveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var e = await GetActiveAsNoTracking(predicate).FirstOrDefaultAsync();
            return CreateVM(e);
        }

        #endregion
        #region Create Services
        public TViewModel Create(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            normalizeToCreate(e);
            selfDbSet.Add(e);
            dbContext.SaveChanges();
            return CreateVM(e);
        }
        public async Task<TViewModel> CreateAsync(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            normalizeToCreate(e);
            await selfDbSet.AddAsync(e);
            await dbContext.SaveChangesAsync();
            return CreateVM(e);
        }

        #endregion
        #region Delete Services
        public void DeleteByObj(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            selfDbSet.Remove(e);
            dbContext.SaveChanges();
        }
        public async Task DeleteByObjAsync(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            selfDbSet.Remove(e);
            await dbContext.SaveChangesAsync();
        }
        public void DeleteByKey<TKey>(TKey id)
        {
            var entity = selfDbSet.Find(id);
            selfDbSet.Remove(entity);
            dbContext.SaveChanges();
        }
        public async Task DeleteByKeyAsync<TKey>(TKey id)
        {
            var entity = selfDbSet.Find(id);
            selfDbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
        #endregion
        #region Update Services
        public TViewModel Update(TViewModel viewModel)
        {
            var entity = VMToE(viewModel);
            selfDbSet.Update(entity);
            dbContext.SaveChanges();
            return CreateVM(entity);
        }

        public async Task<TViewModel> UpdateAsync(TViewModel viewModel)
        {
            var entity = VMToE(viewModel);
            selfDbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return CreateVM(entity);
        }
        public TViewModel Update<TKey>(TKey id, TViewModel viewModel)
        {
            var entity = VMToE(viewModel);
            selfDbSet.Update(entity);
            dbContext.SaveChanges();
            return CreateVM(entity);
        }
        public async Task<TViewModel> UpdateAsync<TKey>(TKey id, TViewModel viewModel)
        {
            var entity = await selfDbSet.FindAsync(id);
            VMToE(entity, viewModel);
            selfDbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return CreateVM(entity);
        }
        public async Task<TViewModel> DeactiveAsync<TKey>(TKey id)
        {
            var entity = await selfDbSet.FindAsync(id);
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                ((IActivable)entity).Activated = false;
            }
            selfDbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return CreateVM(entity);

        }
        #endregion

        #region As No Tracking Getter Utils
        protected IQueryable<TEntity> GetAllAsNoTracking()
        {
            return selfDbSet.AsNoTracking();
        }
        protected IQueryable<TEntity> GetAllActiveAsNoTracking()
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                var sourceQuerable = selfDbSet.AsNoTracking();
                Expression<Func<TEntity, bool>> checkActiveExpress = (TEntity t) => ((IActivable)t).Activated;
                var cleanCheckActiveExpress = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(checkActiveExpress);
                return sourceQuerable.Where(cleanCheckActiveExpress);
            }
            return this.GetAllAsNoTracking();
        }
        protected IQueryable<TEntity> GetAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return selfDbSet.AsNoTracking().Where(predicate);
        }
        protected IQueryable<TEntity> GetActiveAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                var sourceQuerable = selfDbSet.AsNoTracking().Where<TEntity>(predicate);
                Expression<Func<TEntity, bool>> checkActiveExpress = (TEntity t) => ((IActivable)t).Activated;
                var cleanCheckActiveExpress = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(checkActiveExpress);
                return sourceQuerable.Where(cleanCheckActiveExpress);

            }
            return this.GetAsNoTracking(predicate);
        }
        #endregion
        #region Mapping Utils
        // Area to solve View Model map to Entity
        protected TAlternativeEntity VMToE<TAlternativeViewModel, TAlternativeEntity>(TAlternativeViewModel model)
            where TAlternativeEntity : class, new()
        {
            var e = new TAlternativeEntity();
            mapper.Map(model, e);
            return e;
        }
        protected TEntity VMToE(TViewModel model)
        {
            var e = new TEntity();
            mapper.Map(model, e);
            return e;
        }
        protected void VMToE<TAlternativeViewModel, TAlternativeEntity>(TAlternativeViewModel model, TAlternativeEntity e)
            where TAlternativeEntity : class, new()
        {
            mapper.Map(model, e);
        }
        protected void VMToE(TViewModel model, TEntity e)
        {
            mapper.Map(model, e);
        }

        // Area to solve Entity map to View Model
        protected TAlternativeViewModel EToVM<TAlternativeEntity, TAlternativeViewModel>(TAlternativeEntity model)
           where TAlternativeViewModel : class, new()
        {
            var vm = new TAlternativeViewModel();
            mapper.Map(model, vm);
            return vm;
        }
        protected TViewModel EToVM(TEntity model)
        {
            var vm = new TViewModel();
            mapper.Map(model, vm);
            return vm;
        }
        protected void EToVM<TAlternativeEntity, TAlternativeViewModel>(TAlternativeEntity e, TAlternativeViewModel model)
            where TAlternativeEntity : class, new()
        {
            mapper.Map(e, model);
        }
        protected void EToVM(TEntity e, TViewModel model)
        {
            mapper.Map(e, model);
        }

        protected TEntity CreateEntity(TViewModel viewModel)
        {
            if (viewModel == null) return null;
            viewModel.SetMapper(Mapper);
            return viewModel.ToEntity();
        }
        protected TViewModel CreateVM(TEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            TViewModel vModel = new TViewModel();
            vModel.SetMapper(Mapper);
            vModel.CopyFromEntity(entity);
            return vModel;
        }
        #endregion

        private void addCreateTime(TEntity model)
        {
            if (typeof(ICreateTimeEnable).IsAssignableFrom(typeof(TEntity)))
            {
                ((ICreateTimeEnable)model).CreateTime = DateTime.UtcNow;
            }
        }

        private void addActivated(TEntity model)
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                ((IActivable)model).Activated = true;
            }
        }

        private void normalizeToCreate(TEntity model)
        {
            addCreateTime(model);
            addActivated(model);
        }

    }
}
