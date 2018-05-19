using System;
using System.Collections.Generic;
using System.Drawing;
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
    [Activity(Label = "newPosseder")]
    public class newPosseder : Activity
    {
        List<Specialite> allSpecialites;
        List<Praticien> allPraticiens;
        List<Posseder> allPosseders;
        WebClient ws;
        ListView lvSPE;
        ListView lvPRE;
        SpecialiteAdapter adapterSPE;
        PraticienAdapter adapterPRA;
        Specialite speSelectione;
        Praticien praSelectione;
        EditText txtCoef;
        EditText txtDiplome;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.newSpecialiteDePraticienDialog_layout);
            // Create your application here
            ws = new WebClient();
            this.DownloadDataPraticien();
            this.DownloadDataSpecialite();
            this.DownloadDataPosseder();
            Button btnInserePosseder = FindViewById<Button>(Resource.Id.btnInserePosseder);

            lvSPE = FindViewById<ListView>(Resource.Id.lstSpecialiteNEWPOSS);
            lvPRE = FindViewById<ListView>(Resource.Id.lstPratisienNEWPOS);
            txtCoef = FindViewById<EditText>(Resource.Id.txtCoef);
            txtDiplome = FindViewById<EditText>(Resource.Id.txtDiplome);

            Toast.MakeText(this, "Attandez le chargement de données", ToastLength.Short).Show();

            lvPRE.ItemClick += LvPRE_ItemClick;
            lvSPE.ItemClick += LvSPE_ItemClick;

            btnInserePosseder.Click += BtnInserePosseder_Click;

        }

        private void BtnInserePosseder_Click(object sender, EventArgs e)
        {
            if (txtCoef.Text != "" && txtDiplome.Text != "")
            {
                var coefConvert = Convert.ToDouble(txtCoef.Text);
                Posseder checkP = new Posseder() {codeSPE = speSelectione.codeSPE, idPRT = praSelectione.idPRA, coefPOS = coefConvert, diplomePOS = txtDiplome.Text, libelleSPE = speSelectione.libelleSPE };


                var t = allPosseders.Find(x => x.idPRT == praSelectione.idPRA);
                var check = "";
                if (t != null)
                {
                    check = allPosseders.Find(x => x.idPRT == praSelectione.idPRA).codeSPE;
                }

                if (check == speSelectione.codeSPE)
                {
                    Toast.MakeText(this, "Praticien " + praSelectione.prenomPRA + " " + praSelectione.nomPRA + " a deja cette specialite", ToastLength.Short).Show();
                }
                else
                {
                    Uri url = new Uri("http://" + GetString(Resource.String.ip) + "insertPosseder.php?codeSPE=" + speSelectione.codeSPE + "&codePRA=" + praSelectione.idPRA + "&coef=" + txtCoef.Text + "&diplome=" + txtDiplome.Text);
                    ws.DownloadStringAsync(url);
                    Intent intent = new Intent(this, typeof(SpecialiteDePraticien));
                    StartActivity(intent);
                    ws.DownloadDataCompleted += Ws_DownloadDataCompleted;
                }

            }
            else
            {
                Toast.MakeText(this, "Veuillez saisir les données", ToastLength.Short).Show();
            }


        }

        private void Ws_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            Intent intent = new Intent(this, typeof(SpecialiteDePraticien));
            StartActivity(intent);
        }

        private void LvSPE_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            speSelectione = allSpecialites.ElementAt(e.Position);
           // lvSPE.GetChildAt(e.Position).SetBackgroundColor(Android.Graphics.Color.ParseColor("#e8de2e"));
        }
        private void LvPRE_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            praSelectione = allPraticiens.ElementAt(e.Position);
          //  lvPRE.GetChildAt(e.Position).SetBackgroundColor(Android.Graphics.Color.ParseColor("#e8de2e"))

        }

        public void ListSpecialite()
        {
            adapterSPE = new SpecialiteAdapter(this, allSpecialites);
            lvSPE.Adapter = adapterSPE;
        }
        public void ListPraticien()
        {
            adapterPRA = new PraticienAdapter(this, allPraticiens);
            lvPRE.Adapter = adapterPRA;
        }

        public void DownloadDataPosseder()
        {
            ws = new WebClient();
            Uri urlPOS = new Uri("http://" + GetString(Resource.String.ip) + "getAllPosseder.php");
            ws.DownloadStringAsync(urlPOS);
            ws.DownloadStringCompleted += Ws_DownloadStringCompleted4;
        }

        private void Ws_DownloadStringCompleted4(object sender, DownloadStringCompletedEventArgs e)
        {
            allPosseders = JsonConvert.DeserializeObject<List<Posseder>>(e.Result);
        }

        public void DownloadDataSpecialite()
        {
            ws = new WebClient();
            Uri urlSPE = new Uri("http://" + GetString(Resource.String.ip) + "getAllSpecialites.php");
            ws.DownloadStringAsync(urlSPE);
            ws.DownloadStringCompleted += Ws_DownloadStringCompleted1;
        }

        private void Ws_DownloadStringCompleted1(object sender, DownloadStringCompletedEventArgs e)
        {
            allSpecialites = JsonConvert.DeserializeObject<List<Specialite>>(e.Result);
            this.ListSpecialite();
        }

        public void DownloadDataPraticien()
        {
            ws = new WebClient();
            Uri urlPRA = new Uri("http://" + GetString(Resource.String.ip) + "getAllPraticien.php");
            ws.DownloadStringAsync(urlPRA);
            ws.DownloadStringCompleted += Ws_DownloadStringCompleted2;
        }

        private void Ws_DownloadStringCompleted2(object sender, DownloadStringCompletedEventArgs e)
        {
            allPraticiens = JsonConvert.DeserializeObject<List<Praticien>>(e.Result);
            this.ListPraticien();
        }
    }
}