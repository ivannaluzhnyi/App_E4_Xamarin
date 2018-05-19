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
    [Activity(Label = "ListActivites")]
    public class ListActivites : Activity
    {
        ListView lstActivite;
        WebClient ws;
        List<Inviter> lesInviters;
        InviterAdapter adapterINV;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Inviter_layout);
            lstActivite = FindViewById<ListView>(Resource.Id.lstActivites);

            ws = new WebClient();
            int idPRA = Intent.GetIntExtra("idPRA", 0);
            this.DownloadDataActivites(idPRA);
            Toast.MakeText(this, "Attandez le chargement de données", ToastLength.Short).Show();

        }

        public void DownloadDataActivites(int idPRA)
        {
            Uri url = new Uri("http://" + GetString(Resource.String.ip) + "getAllActivitesByPraticien.php?codePraticien=" + idPRA);
            ws.DownloadStringAsync(url);
            ws.DownloadStringCompleted += Ws_DownloadStringCompleted;
        }

        private void Ws_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            lesInviters = JsonConvert.DeserializeObject<List<Inviter>>(e.Result);
            this.ListActivite();
        }

        public void ListActivite()
        {
            adapterINV = new InviterAdapter(this, lesInviters);
            lstActivite.Adapter = adapterINV;
        }
    }
}