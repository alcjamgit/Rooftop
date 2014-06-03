using RealEstateApp.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.UnitTest.DependencyInjections
{
  class MockGenericRepo<TEntity> : IGenericRepository<TEntity>
    where TEntity: class
  {
    private readonly List<TEntity> _dbSet;

    #region IGenericRepo Implementation
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
      List<TEntity> entity = new List<TEntity>() {};
      return entity;

    }

    public TEntity Single(Expression<Func<TEntity, bool>> predicate)
    {
      TEntity entity = new TEntity();
      return entity;
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
