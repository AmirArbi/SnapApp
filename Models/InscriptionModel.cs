using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace SDKTemplate.Models
{
    class InscriptionModel
    {
        public DateTime Date { get; set; }
        public float AmountRecette { get; set; }
        public float AmountExpense { get; set; }
        public float AmountGain { get; set; }
        public Brush ColorBrush { get; set; }
    }
}
