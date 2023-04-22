using Intern.Entities;

namespace Intern.ViewModels.Authen
{
    public class SignInResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime? Born { get; set; }
        public List<AccountShipContact> ShipContacts { get; set; }
        public int RoleID { get; set; }
        public string Sdt { get; set; }
    }
}
