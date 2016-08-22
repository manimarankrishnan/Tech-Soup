using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceCSharp.Core;
using Utils.Core;

namespace WebServiceTests.Main.Other
{
    public class CreateResourceRequest
    {
        Rootobject obj;
        public CreateResourceRequest(string title, string body, int userId)
        {
            obj = new Rootobject();
            obj.title = title;
            obj.body = body;
            obj.userId = userId;
        }
        public override string ToString()
        {
            return GeneralUtils.JSONSerializeObject(obj);
        }

        public class Rootobject
        {
            public string title { get; set; }
            public string body { get; set; }
            public int userId { get; set; }
        }

    }

}
