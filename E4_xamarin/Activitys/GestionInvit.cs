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
    [Activity(Label = "GestionInvit")]
    public class GestionInvit : Activity
    {
        ListView lstPraticien;
        Button btnInviter;
        PraticienAdapter adapterPRA;
        WebClient ws;
        List<Praticien> lesPraticiens;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GestionInvit_layout);
            // Create your application here
            ws = new WebClient();
            lesPraticiens = new List<Praticien>();
            lstPraticien = FindViewById<ListView>(Resource.Id.lstPraticientInviter);
            btnInviter = FindViewById<Button>(Resource.Id.btnInvite);
            this.DownloadPraticientInviter();
            Toast.MakeText(this, "Attandez le chargement de données", ToastLength.Short).Show();
            lstPraticien.ItemClick += LstPraticien_ItemClick;
            btnInviter.Click += BtnInviter_Click;
            
        }

        private void BtnInviter_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(newInviter));
            StartActivity(intent);
        }

        private void LstPraticien_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(ListActivites));
            intent.PutExtra("idPRA", lesPraticiens.ElementAt(e.Position).idPRA);
            StartActivity(intent);
        }

        public void DownloadPraticientInviter()
        {
            Uri url = new Uri("http://" + GetString(Resource.String.ip) + "getAllPraticienInviter.php");
            ws.DownloadStringAsync(url);
            ws.DownloadStringCompleted += Ws_DownloadStringCompleted;
        }

        private void Ws_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //lesPraticiens = JsonConvert.DeserializeObject<List<Praticien>>(e.Result);
            List<Praticien> lsPrats = JsonConvert.DeserializeObject<List<Praticien>>(e.Result);
            foreach (Praticien p in lsPrats)
            {
                var check = lesPraticiens.Find(x => x.idPRA.Equals(p.idPRA));
                if (check == null)
                {
                    lesPraticiens.Add(p);
                }

            }
            this.ListPraticien();
        }

        public void ListPraticien()
        {
            adapterPRA = new PraticienAdapter(this, lesPraticiens);
            lstPraticien.Adapter = adapterPRA;
        }
    }
}