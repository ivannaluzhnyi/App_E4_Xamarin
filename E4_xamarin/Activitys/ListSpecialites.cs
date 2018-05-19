using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using E4_xamarin.Adapters;
using E4_xamarin.Modeles;
using Newtonsoft.Json;

namespace E4_xamarin.Activitys
{
    [Activity(Label = "ListSpecialites")]
    public class ListSpecialites : Activity
    {
        ListView lstSPEdePRA;
        List<Posseder> lesPosseders;
        PossederAdapter adapterPOS;
        WebClient ws;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Posseder_layout);
            lstSPEdePRA = FindViewById<ListView>(Resource.Id.lstSPEdePRA);

            ws = new WebClient();
            int idPRA = Intent.GetIntExtra("idPRA", 0);
            Uri url = new Uri("http://" + GetString(Resource.String.ip) + "getAllPossedersByPraticien.php?codePraticien=" + idPRA);
            ws.DownloadStringAsync(url);
            ws.DownloadStringCompleted += Ws_DownloadStringCompleted1;
            Toast.MakeText(this, "Attandez le chargement de données", ToastLength.Short).Show();

        }

        private void Ws_DownloadStringCompleted1(object sender, DownloadStringCompletedEventArgs e)
        {
            lesPosseders = JsonConvert.DeserializeObject<List<Posseder>>(e.Result);
            this.ListPosseders();
        }

        public void ListPosseders()
        {
            adapterPOS = new PossederAdapter(this, lesPosseders);
            lstSPEdePRA.Adapter = adapterPOS;
        }
    }
}