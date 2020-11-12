using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.Base
{
    public class BaseResponse<TData> : BaseResponse
    {
        public BaseResponse()
        {
        }

        public BaseResponse(bool status) : base(status)
        {
        }

        public BaseResponse(string message, bool status = false) : base(message, status)
        {
        }

        public BaseResponse(Error error, bool status = false) : base(error, status)
        {
        }

        public BaseResponse(TData data, bool status = true)
        {
            this.Data = data;
            this.Status = status;
        }

        public TData Data { get; set; }
    }

    public class BaseResponse
    {
        public BaseResponse()
        {
        }

        public BaseResponse(bool status)
        {
            this.Status = status;
        }

        // not nesscesary
        public BaseResponse(Error error, bool status = false)
        {
            this.Error = error;
            this.Status = status;
        }

        public BaseResponse(string message, bool status = false)
        {
            this.Error = new Error(message);
            this.Status = status;
        }

        public bool Status { get; set; }

        public Error Error { get; set; }
    }

    public class Error
    {
        public Error()
        {
        }

        public Error(string message)
        {
            this.Message = message;
        }

        public string Code { get; set; }

        public string Message { get; set; }
    }

}
