namespace Intern.ViewModels.ChangeAccount
{
    public class RepassResponse
    {
        public RepassResponse(int status, string detail)
        {
            this.status = status;
            this.detail = detail;
        }

        public int status { get; set; }
        public string detail { get; set; }
    }
}
