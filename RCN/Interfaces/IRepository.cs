﻿using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RCN.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Find(int id);
        Task<T> FindAsync(int id);
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true
            );
        Task<IEnumerable<T>> GetAllAsync(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = null,
           bool isTracking = true
           );
        T FirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null,
            bool isTracking = true
            );
        Task<T> FirstOrDefaultAsync(
           Expression<Func<T, bool>> filter = null,
           string includeProperties = null,
           bool isTracking = true
           );
        void Add(T entity);
        Task AddAsync(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        void Save();
        Task SaveAsync();
    }
}