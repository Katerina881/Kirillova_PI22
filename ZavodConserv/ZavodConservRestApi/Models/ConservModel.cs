using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZavodConservRestApi.Models
{
    public class ConservModel
    {
        public int Id { get; set; }

        public string ConservName { get; set; }

        public decimal Price { get; set; }
    }
}
