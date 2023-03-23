using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.TodoAppNTier.Common.ResponseObjects
{
    public class Response : IResponse
    {
        //ctor
        public Response(ResponseType responseType)
        {
            ResponseType = responseType;
        }
        //ctor
        public Response(ResponseType responseType, string message)
        {
            ResponseType = responseType;
            Meessage = message;

        }
        public string Meessage { get; set; }
        public ResponseType ResponseType { get; set; }
    }
    //  public bool IsSuccess { get; set; } başarıszlığın sebebini bulmak için
    public enum ResponseType
    {
        Success,
        ValidationError,
        NotFound
    }
}
