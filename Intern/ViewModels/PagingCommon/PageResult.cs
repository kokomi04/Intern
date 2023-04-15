namespace Intern.ViewModels.PagingCommon
{
    public class PageResult<T>
    {
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }
        public List<T> Data {get; set;}
    }
}
