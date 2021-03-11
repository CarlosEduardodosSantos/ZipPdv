using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileAppServerClient
{

    public class OperationResult
    {
        public int Status { get; set; }
        public string StatusDescricao => Status == 600 ? "Operacional" : "Não operacional";
        public string Message { get; set; }
        public object Entity { get; set; }

        public OperationResult(object entity, int status = 600, string message = "")
        {
            Entity = entity;
            Status = status;
            Message = message;
        }
    }
}
