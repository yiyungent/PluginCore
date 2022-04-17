namespace PluginCore.AspNetCore.ResponseModel
{
    public class BaseResponseModel
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
