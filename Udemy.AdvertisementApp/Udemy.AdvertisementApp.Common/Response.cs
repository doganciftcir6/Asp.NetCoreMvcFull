using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.AdvertisementApp.Common
{
    //base class , herhangi bir data taşımayan Response yapısı, validation durumu söz konusu değil
    public class Response : IResponse
    {
        public Response(ResponseType responseType)
        {
            ResponseType = responseType;
        }
        public Response(ResponseType responseType, string message)
        {
            ResponseType = responseType;
            Message = message;
        }
        public string Message { get; set; }
        //enum
        public ResponseType ResponseType { get; set; }
    }
    //enum
    public enum ResponseType
    {
        Success,
        ValidationError,
        NotFound
    }
}
