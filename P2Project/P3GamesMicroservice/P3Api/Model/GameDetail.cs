using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Api.Model
{
    public class GameDetail
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Route { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImageSource { get; set; }
    }
}
