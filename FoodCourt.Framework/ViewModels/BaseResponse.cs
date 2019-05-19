using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.ViewModels
{
    public partial class BaseResponse
    {
        public string Result { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public static BaseResponse GetSuccessResponse(dynamic data)
        {
            return new BaseResponse()
            {
                Success = true,
                Data = data
            };
        }

        public static BaseResponse GetErrorResponse(string message)
        {
            return new BaseResponse()
            {
                Success = false,
                Message = message
            };
        }

    }
}
