using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiMingAi.WebApi.Model
{
    public class CreateTaskRequest
    {
        public string TaksName { get; set; }
        public string UserName { get; set; }
        public long Phone { get; set; }
    }
}
