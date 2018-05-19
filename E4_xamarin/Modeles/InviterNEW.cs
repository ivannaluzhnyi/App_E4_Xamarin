using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace E4_xamarin.Modeles
{
    public class InviterNEW
    {
        [JsonProperty("PRA_NUM")]
        public int idPRA { get; set; }
        [JsonProperty("SPECIALISTEON")]
        public int specialisteon { get; set; }
        [JsonProperty("AC_NUM")]
        public int idActivite { get; set; }
    }
}