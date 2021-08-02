namespace FTS.Precatorio.Application.ViewModels
{
    public class ReturnContentJson<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public dynamic Errors { get; set; }

        public ReturnContentJson() { }

        public ReturnContentJson(bool success, T data, dynamic errors = null)
        {
            this.Success = success;
            this.Data = data;
            this.Errors = errors;
        }
    }
}