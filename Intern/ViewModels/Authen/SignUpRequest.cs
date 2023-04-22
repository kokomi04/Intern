using System.ComponentModel.DataAnnotations;

namespace Intern.ViewModels.Authen
{
    public class SignUpRequest
    {
        public string userName { get; set; }
        public string userPass { get; set; }
        public string sdt { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int status { get; set; }
    }
}
