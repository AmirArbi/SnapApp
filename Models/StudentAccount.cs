using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace SDKTemplate.Models
{
    public class StudentAccount
    {

        public string Username { get; set; }
        public string Page { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string School { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FeedBack { get; set; }
        public string Avrage { get; set; }
        public string PhoneNumber { get; set; }
        public string remember_token { get; set; }
        public ImageSource Image { get; set; }
        public string Absence { get; set; }
        public string Note { get; set; }
        public string Gender { get; set; }
        public float Payment { get; set; }
        public bool IsPresent { get; set; }
    }
}
