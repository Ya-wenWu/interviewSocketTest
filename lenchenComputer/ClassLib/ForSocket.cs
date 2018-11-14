using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public class ForSocket : BaseRequest
    {
        public string ThisInpuMsg { get; set; }
    }

    public class ForSocketResponse : BaseResponse
    {
        public string ThisInpuMsg { get; set; }
    }
}
