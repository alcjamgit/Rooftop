using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace RealEstateApp.DataAccessLayer
{
  public interface IGenericRepository<TEntity>
    where TEntity: class
  {
    IQueryable<TEntity> AsQueryable();
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> Find(Expression<Func<TEntity,bool>> predicate);
    TEntity Single(Expression<Func<TEntity,bool>> predicate);
    TEntity SingleOrDefault(Expression<Func<TEntity,bool>> predicate);
    TEntity First(Expression<Func<TEntity,bool>> predicate);
    TEntity GetById(int Id);

    void Add(TEntity entity);
    void Delete(TEntity entity);
    void Attach(TEntity entity);
  }


}