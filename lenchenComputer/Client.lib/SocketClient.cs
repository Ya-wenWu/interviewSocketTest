using ClassLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Lib
{
    public class SocketClient : SendBase
    {
        public BaseResponse GetData(ForSocket dto)
        {
            var response = new BaseResponse();

            try
            {
                var result = base.Send<ForSocket>(dto, dto.IsCallback);

                response.Status = result.SendStatus;
                response.Message = result.ReceiveData;
            }
            catch (Exception ex)
            {
                response.Status = Status.Exception;
                response.Message = ex.Message;
            }

            return response;
        }

        public ForSocketResponse GetClassData(ForSocket dto)
        {
            var response = new ForSocketResponse();

            try
            {
                var result = base.Send<ForSocket>(dto, dto.IsCallback);

                switch (result.SendStatus)
                {
                    case Status.Success:
                        response = JsonConvert.DeserializeObject<ForSocketResponse>(result.ReceiveData);
                        break;
                    case Status.Fail:
                    case Status.Exception:
                        response.Status = result.SendStatus;
                        response.Message = result.ReceiveData;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                response.Status = Status.Exception;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
