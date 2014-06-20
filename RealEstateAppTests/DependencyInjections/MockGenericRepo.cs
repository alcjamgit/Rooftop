using RealEstateApp.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAppTests.DependencyInjections
{
  public class MockGenericRepo<TEntity> : IGenericRepository<TEntity>
    where TEntity: class
  {
    private List<TEntity> _data;

    public MockGenericRepo()
    {
      _data = new List<TEntity>();
    }
    public MockGenericRepo(List<TEntity> list)
    {
      _data = list;
    }

    #region IGenericRepo Implementation
    public virtual IQueryable<TEntity> AsQueryable()
    {
      return _data.AsQueryable();
    }

    public IEnumerable<TEntity> GetAll()
    {
      return _data;
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
      //TODO:add the predicate
      return _data;

    }

    public TEntity Single(Expression<Func<TEntity, bool>> predicate)
    {
      return _data.AsQueryable().Single(predicate);
    }

    public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
      return _data.AsQueryable().SingleOrDefault(predicate);
    }

    public TEntity First(Expression<Func<TEntity, bool>> predicate)
    {
      return _data.AsQueryable().FirstOrDefault(predicate);
    }

    public TEntity GetById(int id)
    {
      return _data.AsQueryable().FirstOrDefault();
    }

    public void Add(TEntity entity)
    {
      _data.Add(entity);
    }

    public void Delete(TEntity entity)
    {
      _data.Remove(entity);
    }

    //Update
    public void Attach(TEntity entity)
    {
      //TODO
    } 
    #endregion
  }
}
