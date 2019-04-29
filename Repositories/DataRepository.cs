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
    public class DataRepository : BaseRepository<Models.Data>, IDataRepository
    {
        public DataRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<Models.Data> Get(int id) => await Context.Datas.FirstOrDefaultAsync(d => d.Id == id);

        public async Task Delete(Models.Data data) 
        {
             Context.Datas.Remove(data);
             await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var data = await Get(id);
            await Delete(data);
        }
    }
}
