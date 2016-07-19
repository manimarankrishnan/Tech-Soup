using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceTests.Main.Other
{
   public class CreateResourceResponse
    {
        public class Rootobject
        {
            public string title { get; set; }
            public string body { get; set; }
            public int userId { get; set; }
            public int id { get; set; }
        }

    }
}
