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
    public class Praticien
    {   
        [JsonProperty("PRA_NUM")]
        public int idPRA { get; set; }
        [JsonProperty("PRA_PRENOM")]
        public string prenomPRA { get; set; }
        [JsonProperty("PRA_NOM")]
        public string nomPRA { get; set; }
    }
}