namespace PluginCore.AspNetCore.ResponseModel
{
    public class CommonResponseModel 
    {
        public int code { get; set; }

        public string message { get; set; }

        public object data { get; set; }
    }
}
