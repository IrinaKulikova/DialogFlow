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
    public interface IDataRepository : IRepository<Models.Data>
    {
        Task<Models.Data> Get(int id);

        Task Delete(Models.Data data);

        Task Delete(int id);
    }
}
