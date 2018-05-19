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
    public class Specialite
    {
        [JsonProperty("SPE_CODE")]
        public string codeSPE { get; set; }
        [JsonProperty("SPE_LIBELLE")]
        public string libelleSPE { get; set; }
    }
}