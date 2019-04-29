using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebDialog.Models;

namespace WebDialog.Repositories
{
    public interface IRepository<T>  where T : class
    { 
        Task<List<T>> GetAll();
        void Update(T entity);
        Task Create(T entity);
    }
}