using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDialog.Models;
using WebDialog.Repositories;
using Microsoft.AspNetCore.Authorization;
using WebDialog.DTO;
using Microsoft.Extensions.Configuration;

namespace WebDialog.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        IDataRepository _repository { get; set; }
        IConfiguration _configuration { get; set; }
        public DataController(IDataRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(new DataDTO
         {
             Datas = await _repository.GetAll(),
             SRC = _configuration["DialogFlowSRC"]
         });
    }
}
