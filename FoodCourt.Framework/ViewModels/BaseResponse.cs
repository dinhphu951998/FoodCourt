using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.ViewModels
{
    public partial class BaseResponse
    {
        public bool Success { get; set; }
        public dynamic Errors { get; set; }
        public dynamic Data { get; set; }

        public static BaseResponse GetSuccessResponse(dynamic data)
        {
            return new BaseResponse()
            {
                Success = true,
                Data = data
            };
        }

        public static BaseResponse GetErrorResponse(Dictionary<string, IEnumerable<string>> pairs)
        {
            return new BaseResponse()
            {
                Success = false,
                Errors = pairs
            };
        }

    }
}
