namespace Intern.ViewModels.ChangeAccount
{
    public class RepassRequest
    {
        public int accountId { get; set; }
        public string oldPass { get; set; }
        public string newPass { get; set; }
    }
}
