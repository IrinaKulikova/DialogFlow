using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDialog.Models;
using WebDialog.Data;

namespace WebDialog.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        public ApplicationDbContext Context { get; set; }
        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }
        public async Task<List<T>> GetAll() => await Context.Set<T>().ToListAsync();
        public async Task Create(T entity) => await Context.Set<T>().AddAsync(entity);
        public void Update(T entity) => Context.Set<T>().Update(entity);
    }
}
