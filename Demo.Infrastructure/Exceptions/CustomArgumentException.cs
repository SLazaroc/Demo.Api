using System.Runtime.Serialization;

namespace Demo.Infrastructure.Exceptions
{
    [Serializable]
    public class CustomArgumentException : ArgumentException
    {
        public string Code { get; set; }
        public CustomArgumentException()
        {

        }
        public CustomArgumentException(string code, string message) : base(message)
        {
            Code = code;
        }
        public CustomArgumentException(string code, string message, Exception ex) : base(message, ex)
        {
            Code = code;
        }
        public CustomArgumentException(string code)
        {
            Code = code;
        }
        protected CustomArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
