using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SDKTemplate.Models
{
    public class Month
    {
        public string Slug { get; set; }
        public string GroupName { get; set; }
        public string ProfName { get; set; }
        public string MonthName { get; set; }
        public Brush Color { get; set; }
        public long NumberSessions { get; set; }
        public long CurrentSessions { get; set; }
        public string SchoolYear { get; set; }
        public string Payement { get; set; }
        public string StudentName { get; set; }
        public Visibility Visibilty { get; set; }
        public Visibility Collapsed { get; set; }
        public float WantedMoney { get; set; }
        public float Montant { get; set; }
    }
}
