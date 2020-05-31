using System;

namespace XSoluciones
{
    public class OperationResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public OperationResult(bool result)
        {
            this.Result = result;
        }
        public OperationResult(bool result, string message)
        {
            this.Result = result;
            this.Message = message;
        }
        public OperationResult(bool result, string message, object data)
        {
            this.Result = result;
            this.Message = message;
            this.Data = data;
        }
        public OperationResult()
        {
        }
    }
}
