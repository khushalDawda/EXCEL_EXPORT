namespace WEB_APP.Controllers
{
    public class DataTableResponse
    {
        public int RecordsTotal { get; internal set; }
        public int RecordsFiltered { get; internal set; }
        public object Data { get; internal set; }
    }
}