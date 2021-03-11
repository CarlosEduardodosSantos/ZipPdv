namespace Zip.Sat.Service
{

    public class OperationResult
    {
        public int Status { get; set; }
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
