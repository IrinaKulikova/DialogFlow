using System.Collections.Generic;
using WebDialog.Models;

namespace WebDialog.DTO
{

    public class DataDTO
    {
       public IList<Models.Data> Datas { get; set; }
       public string SRC { get; set; }
    }
}