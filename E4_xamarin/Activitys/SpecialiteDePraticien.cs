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
using E4_xamarin.Modeles;
using E4_xamarin.Adapters;
using Newtonsoft.Json;
using System.Collections;

namespace E4_xamarin.Activitys
{
    [Activity(Label = "SpecialiteDePraticien")]
    public class SpecialiteDePraticien : Activity
    {
        Button btnInsereSPEdePRT;
        ListView lstPraticienPOS;
        WebClient ws;
        PraticienAdapter adapterPRA;
        List<Praticien> lesPraticiens;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SpecialiteDePraticien_layout);
            btnInsereSPEdePRT = FindViewById<Button>(Resource.Id.btnInsererSPEdePRT);
            lstPraticienPOS = FindViewById<ListView>(Resource.Id.lstPraticienPOS);
            lesPraticiens = new List<Praticien>();
            ws = new WebClient();
            Uri url = new Uri("http://" + GetString(Resource.String.ip) + "getAllPraticiensPosseder.php");

            Toast.MakeText(this, "Attandez le chargement de données", ToastLength.Short).Show();


            ws.DownloadStringAsync(url);
            ws.DownloadStringCompleted += Ws_DownloadStringCompleted;
            btnInsereSPEdePRT.Click += delegate
            {
                Intent intent = new Intent(this, typeof(newPosseder));
                StartActivity(intent);
            };
            lstPraticienPOS.ItemClick += LstPraticienPOS_ItemClick;
            
        }

        private void LstPraticienPOS_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(ListSpecialites));
            intent.PutExtra("idPRA", lesPraticiens.ElementAt(e.Position).idPRA);
            StartActivity(intent);
        }

        private void Ws_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
           // lesPraticiens = JsonConvert.DeserializeObject<List<Praticien>>(e.Result);
            List<Praticien> lsPrats = JsonConvert.DeserializeObject<List<Praticien>>(e.Result);
            foreach (Praticien p  in lsPrats)
            {
                    var check = lesPraticiens.Find(x => x.idPRA.Equals(p.idPRA));
                    if (check == null)
                    {
                        lesPraticiens.Add(p);
                    }

            }
            this.ListPraticiens();
        }

        public void ListPraticiens()
        {
            adapterPRA = new PraticienAdapter(this, lesPraticiens);
            lstPraticienPOS.Adapter = adapterPRA;
        }

       
    }
}