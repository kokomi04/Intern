using Intern.Entities;

namespace Intern.ViewModels
{
    public class AccountCustom
    {
        public int Id;
        public string name;
        public string address;
        public DateTime? born;
        public List<AccountShipContact> shipContacts;
        public int roleID;
        public string sdt;
    }
}
