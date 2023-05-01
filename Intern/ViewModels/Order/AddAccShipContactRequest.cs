namespace Intern.ViewModels.Order
{
    public class AddAccShipContactRequest
    {
        public int accountId { get; set; }
        public string accountDetailAddress { get; set; }
        public string accountPhoneNumber { get; set; }
        public string districtID { get; set; }
        public string provinceId { get; set; }
        public string receiverName { get; set; }
        public string wardCode { get; set; }
    }
}
