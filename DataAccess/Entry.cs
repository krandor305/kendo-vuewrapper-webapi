using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess
{
    public class Entry
    {
        public static List<TEntity> GetList<TContext, TEntity>(Expression<Func<TEntity, bool>> filter = null, params string[] includes)
        where TContext : DbContext,new()
        where TEntity : class
        {
        DbContext db = new TContext();
        IQueryable<TEntity> query = db.Set<TEntity>(); 
        if(filter!=null)
            query = query.Where(filter);
        foreach (var inc in includes)
        {
             query = query.Include(inc);
        }
        return query.ToList();
        }

        public static TEntity Get<TContext, TEntity>(Expression<Func<TEntity, bool>> filter = null, params string[] includes)
        where TContext : DbContext, new()
        where TEntity : class
        {
            DbContext db = new TContext();
            IQueryable<TEntity> query = db.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var inc in includes)
            {
                query = query.Include(inc);
            }
            return query.FirstOrDefault();
        }

        public static int add<TContext,TEntity>(ref TEntity entity)
        where TContext : DbContext, new()
        where TEntity : class
        {
            DbContext db = new TContext();
            db.Entry(entity).State = EntityState.Added;
            return db.SaveChanges();
        }

        public static int update<TContext, TEntity>(ref TEntity entity)
        where TContext : DbContext, new()
        where TEntity : class
        {
            DbContext db = new TContext();
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

       public static int delete<TContext, TEntity>(ref TEntity entity)
       where TContext : DbContext, new()
       where TEntity : class
        {
            DbContext db = new TContext();
            db.Entry(entity).State = EntityState.Deleted;
            return db.SaveChanges();
        }

        public static int delete<TContext, TEntity>(ref IEnumerable<TEntity> entities)
       where TContext : DbContext, new()
       where TEntity : class
        {
            DbContext db = new TContext();
            foreach(var entity in entities)
            {
                db.Entry(entity).State = EntityState.Deleted;
            }
            return db.SaveChanges();
        }

    }
}
