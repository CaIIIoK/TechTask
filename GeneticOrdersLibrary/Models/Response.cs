namespace CustomLibrary.Models
{
    public class Response
    {
        public ResponseType ResponseType { get; set; }
        public string Description { get; set; }
    }

    public enum ResponseType
    {       
        Failed,
        Success
    }
}
