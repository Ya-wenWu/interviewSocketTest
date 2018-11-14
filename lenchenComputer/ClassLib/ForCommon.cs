using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public enum FunctionName
    {
        FunctionOne,
        FunctionTwo,
    }

    public enum Status
    {
        Success,
        Fail,
        Exception,
        ConnectFail,
    }

    public class BaseRequest
    {
        public FunctionName Function { get; set; }
        public bool IsCallback { get; set; }
    }

    public class BaseResponse
    {
        public Status Status { get; set; }
        public string Message { get; set; }
    }
}
