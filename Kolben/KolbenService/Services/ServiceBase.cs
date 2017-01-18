using KolbenService.Database.IEntities;
using KolbenService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KolbenService.Utils;
using System.Threading;

namespace KolbenService.Services
{
    public abstract class ServiceBase<T> : IServiceBase<T> where T : class, IEntityBase, new()
    {
        protected KolbenContext _context;

        private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(initialCount: 1);

        #region Properties
        public ServiceBase(KolbenContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                var @where = PredicateBuilder.True<T>();

                @where = @where.And(o => o.SuppressionDate == null);

                var tmpEntities = await _context.Table<T>().Where(@where).ToListAsync();

                var entities = new List<T>();
                if (includes.Any())
                {
                    foreach (var tmpEntity in tmpEntities)
                    {
                        var entity = await Includes(tmpEntity, includes);
                        entities.Add(entity);
                    }
                }
                else
                {
                    entities = tmpEntities;
                }

                return entities;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }       
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                var @where = PredicateBuilder.True<T>();

                @where = @where.And(predicate);
                @where = @where.And(o => o.SuppressionDate == null);

                var tmpEntities = await _context.Table<T>().Where(@where).ToListAsync();

                var entities = new List<T>();
                if (includes.Any())
                {
                    foreach (var tmpEntity in tmpEntities)
                    {
                        var entity = await Includes(tmpEntity, includes);
                        entities.Add(entity);
                    }
                }
                else
                {
                    entities = tmpEntities;
                }

                return entities;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public async Task<int> Count()
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                return await _context.Table<T>().CountAsync();    
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }            
        }

        public async Task<T> GetSingle(int id, params Expression<Func<T, object>>[] includes)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                var @where = PredicateBuilder.True<T>();

                @where = @where.And(o => o.SuppressionDate == null);
                @where = @where.And(o => o.Id == id);

                var entity = await _context.Table<T>().Where(@where).FirstOrDefaultAsync();

                if (includes.Any() && entity != null)
                {
                    entity = await Includes(entity, includes);
                }

                return entity;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                var @where = PredicateBuilder.True<T>();

                @where = @where.And(predicate);
                @where = @where.And(o => o.SuppressionDate == null);

                var entity = await _context.Table<T>().Where(@where).FirstOrDefaultAsync();

                if (includes.Any() && entity != null)
                {
                    entity = await Includes(entity, includes);
                }

                return entity;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public async Task<List<T>> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                var @where = PredicateBuilder.True<T>();

                @where = @where.And(predicate);
                @where = @where.And(o => o.SuppressionDate == null);

                var tmpEntities = await _context.Table<T>().Where(@where).ToListAsync();

                var entities = new List<T>();
                if (includes.Any())
                {
                    foreach (var tmpEntity in tmpEntities)
                    {
                        var entity = await Includes(tmpEntity, includes);
                        entities.Add(entity);
                    }
                }
                else
                {
                    entities = tmpEntities;
                }

                return entities;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public virtual async Task<int> Add(T entity)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                entity.CreationDate = DateTime.Now;
                await _context.InsertAsync(entity);

                return entity.Id;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public async Task Update(T entity)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                entity.UpdateDate = DateTime.Now;
                await _context.UpdateAsync(entity);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }


        public virtual async Task Delete(T entity)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                entity.SuppressionDate = DateTime.Now;
                await _context.UpdateAsync(entity);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public virtual async Task Delete(int id)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                var entity = await _context.FindAsync<T>(id);
                entity.SuppressionDate = DateTime.Now;
                await _context.UpdateAsync(entity);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        protected abstract Task<T> Includes(T entity, params Expression<Func<T, object>>[] includes);
    }
}