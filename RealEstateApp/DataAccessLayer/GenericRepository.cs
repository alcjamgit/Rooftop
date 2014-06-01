using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace RealEstateApp.DataAccessLayer
{
  public class GenericRepository<TEntity>: 
    IGenericRepository<TEntity> where TEntity: class
  {
    private readonly DbSet<TEntity> _dbSet;
    public GenericRepository(DbSet<TEntity> dbSet)
    {
      _dbSet = dbSet;
    }

    #region IGenericRepository Implementation
    public virtual IQueryable<TEntity> AsQueryable()
    {
      return _dbSet.AsQueryable();
    }

    public IEnumerable<TEntity> GetAll()
    {
      return _dbSet;
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
      return _dbSet.Where(predicate);
    }

    public TEntity Single(Expression<Func<TEntity, bool>> predicate)
    {
      return _dbSet.Single(predicate);
    }

    public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
      return _dbSet.SingleOrDefault(predicate);
    }

    public TEntity First(Expression<Func<TEntity, bool>> predicate)
    {
      return _dbSet.First(predicate);
    }

    public TEntity GetById(int id)
    {
      return _dbSet.Find(id);
    }

    public void Add(TEntity entity)
    {
      _dbSet.Add(entity);
    }

    public void Delete(TEntity entity)
    {
      _dbSet.Remove(entity);
    }

    //Update
    public void Attach(TEntity entity)
    {
      _dbSet.Attach(entity);
    } 
    #endregion
  }
}