using System;

namespace MiniBankApi.models
{
    public class Response
    {
        public string RequestId => $"{Guid.NewGuid().ToString()}";
        public string ReponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public Object data { get; set; }

    }
}