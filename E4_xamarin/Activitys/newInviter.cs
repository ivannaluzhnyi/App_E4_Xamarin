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
    [Activity(Label = "newInviter")]
    public class newInviter : Activity
    {
        Button btnValider;
        TextView txtSpecial;
        WebClient ws;
        List<Praticien> lesPraticiens;
        List<Activite> lesActivites;
        List<InviterNEW> lesInviters;
        ListView lstPraticien;
        ListView lstActivite;
        PraticienAdapter adapterPRA;
        ActiviteAdapter adapterACT;
        Praticien praSelect;
        Activite actSelect; 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.newInviter_layout);
            ws = new WebClient();
            lstPraticien = FindViewById<ListView>(Resource.Id.lstPraticien);
            lstActivite = FindViewById<ListView>(Resource.Id.lstActivite);
            txtSpecial = FindViewById<TextView>(Resource.Id.txtSpecialist);
            btnValider = FindViewById<Button>(Resource.Id.btnValider);

            this.DownloadDataActivites();
            this.DownloadDataInvite();
            this.DownloadDataPraticien();
            Toast.MakeText(this, "Attandez le chargement de données", ToastLength.Short).Show();


            lstActivite.ItemClick += LstActivite_ItemClick;
            lstPraticien.ItemClick += LstPraticien_ItemClickPra;
            btnValider.Click += BtnValider_Click;
        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (txtSpecial.Text != "")
            {
                var check = lesInviters.Find(x => x.idPRA == praSelect.idPRA);
                var t = 0;
                if(check != null)
                {
                    t = lesInviters.Find(x => x.idPRA == praSelect.idPRA).idActivite;
                }

                if (t == actSelect.idActivite)
                {
                    Toast.MakeText(this, "Praticien " + praSelect.prenomPRA + " " + praSelect.nomPRA + " est deja invité à catte activité", ToastLength.Short).Show();
                }
                else
                {
                    Uri url = new Uri("http://" + GetString(Resource.String.ip) + "insertInviter.php?numACT=" + actSelect.idActivite + "&codePRA=" + praSelect.idPRA + "&specialisteon=" + txtSpecial.Text);
                    ws.DownloadStringAsync(url);
                    Intent intent = new Intent(this, typeof(GestionInvit));
                    StartActivity(intent);
                    ws.DownloadStringCompleted += Ws_DownloadStringCompletedInsert;

                }

            }
            else
            {
                Toast.MakeText(this, "Veuillez saisir les données", ToastLength.Short).Show();
            }

        }

        private void Ws_DownloadStringCompletedInsert(object sender, DownloadStringCompletedEventArgs e)
        {
            Intent intent = new Intent(this, typeof(GestionInvit));
            StartActivity(intent);
        }

        private void LstPraticien_ItemClickPra(object sender, AdapterView.ItemClickEventArgs e)
        {
            praSelect = lesPraticiens.ElementAt(e.Position);
        }
        private void LstActivite_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            actSelect = lesActivites.ElementAt(e.Position);
        }

        public void DownloadDataInvite()
        {
            ws = new WebClient();
            Uri url = new Uri("http://" + GetString(Resource.String.ip) + "getAllInviter.php");
            ws.DownloadStringAsync(url);
            ws.DownloadStringCompleted += Ws_DownloadStringCompletedI;
        }

        public void DownloadDataActivites()
        {
            ws = new WebClient();
            Uri url = new Uri("http://" + GetString(Resource.String.ip) + "getAllActivites.php");
            ws.DownloadStringAsync(url);
            ws.DownloadStringCompleted += Ws_DownloadStringCompletedACT;
        }

        public void DownloadDataPraticien()
        {
            ws = new WebClient();
            Uri urlPRA = new Uri("http://" + GetString(Resource.String.ip) + "getAllPraticien.php");
            ws.DownloadStringAsync(urlPRA);
            ws.DownloadStringCompleted += Ws_DownloadStringCompleted45;
        }


        private void Ws_DownloadStringCompletedI(object sender, DownloadStringCompletedEventArgs e)
        {
            lesInviters = JsonConvert.DeserializeObject<List<InviterNEW>>(e.Result);
        }
        private void Ws_DownloadStringCompletedACT(object sender, DownloadStringCompletedEventArgs e)
        {
            lesActivites = JsonConvert.DeserializeObject<List<Activite>>(e.Result);
            this.ListActivites();
        }
        private void Ws_DownloadStringCompleted45(object sender, DownloadStringCompletedEventArgs e)
        {
            if(e.Result != "")
            {
                lesPraticiens = JsonConvert.DeserializeObject<List<Praticien>>(e.Result);
                this.ListPraticien();

            }
        }

        public void ListPraticien()
        {
            adapterPRA = new PraticienAdapter(this, lesPraticiens);
            lstPraticien.Adapter = adapterPRA;
        }
        public void ListActivites()
        {
            adapterACT = new ActiviteAdapter(this, lesActivites);
            lstActivite.Adapter = adapterACT;
        }
    }
}