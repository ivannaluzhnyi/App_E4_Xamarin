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
    [Activity(Label = "Gestion de Specialite")]
    public class GestionSpecialite : Activity
    {
        ListView lstSPE;
        List<Specialite> lesSpecialites;
        Button btnInsertSPE;
        SpecialiteAdapter adaperSPE;
        WebClient ws;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GestionSpecialite_layout);

            btnInsertSPE = FindViewById<Button>(Resource.Id.btnInsertSPE);
            lstSPE = FindViewById<ListView>(Resource.Id.lstSPE);
            ws = new WebClient();
            Uri url = new Uri("http://" + GetString(Resource.String.ip) + "getAllSpecialites.php");

            Toast.MakeText(this, "Attandez le chargement de données", ToastLength.Short).Show();

            ws.DownloadStringAsync(url);
            ws.DownloadStringCompleted += Ws_DownloadStringCompleted;

            btnInsertSPE.Click += BtnInsertSPE_Click;
            lstSPE.ItemClick += LstSPE_ItemClick;
        }

        private void LstSPE_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            this.DialogModifierSPE(lesSpecialites.ElementAt(e.Position));
        }

        private void BtnInsertSPE_Click(object sender, EventArgs e)
        {
            this.DialogCreateSPE();
        }

        private void Ws_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            lesSpecialites = JsonConvert.DeserializeObject<List<Specialite>>(e.Result);
            this.ListSpecialites();
        }

        public void ListSpecialites()
        {
            adaperSPE = new SpecialiteAdapter(this, lesSpecialites);
            lstSPE.Adapter = adaperSPE;
        }

        public void DialogCreateSPE()
        {
            View view = LayoutInflater.Inflate(Resource.Layout.newSpecialiteDialogLayout, null);
            AlertDialog alertDialog = new AlertDialog.Builder(this).Create();

            alertDialog.SetView(view);
            alertDialog.SetCanceledOnTouchOutside(false);

            Button btnInsertSPE = view.FindViewById<Button>(Resource.Id.btnInsertSPE);
            Button btnExit = view.FindViewById<Button>(Resource.Id.btnExit);
            EditText txtCode = view.FindViewById<EditText>(Resource.Id.txtCodeDialogINS);
            EditText txtLibelle = view.FindViewById<EditText>(Resource.Id.txtLibelleDialogINS);
            txtCode.Hint = "Saisissez trois letres";

            btnExit.Click += delegate
            {
                alertDialog.Dismiss();
            };

            btnInsertSPE.Click += delegate
            {
                var codeSPE = txtCode.Text;
                var libelleSPE = txtLibelle.Text;
                var checkCode = lesSpecialites.Find(x => x.codeSPE == codeSPE);
                if (codeSPE != "" && libelleSPE != "")
                {
                    if (checkCode == null)
                    {
                        Uri url = new Uri("http://" + GetString(Resource.String.ip) + "insertSpecialite.php?codeSPE="+codeSPE + "&libelleSPE="+libelleSPE);
                        ws.DownloadDataAsync(url);
                        Specialite spe = new Specialite() { codeSPE = codeSPE, libelleSPE = libelleSPE };
                        lesSpecialites.Add(spe);
                        alertDialog.Dismiss();
                        this.ListSpecialites();
                        Toast.MakeText(this, "La spécialité a été créée", ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "La spécialité avec le code \"" + codeSPE+ "\" existe dèja!", ToastLength.Short).Show();
                    }

                }
                else
                {
                    Toast.MakeText(this, "Veuillez saisir le code ou le nom !", ToastLength.Short).Show();
                }

            };

            alertDialog.Show();
        }

        public void DialogModifierSPE(Specialite spaIntent)
        {
            View view = LayoutInflater.Inflate(Resource.Layout.newSpecialiteDialogLayout, null);
            AlertDialog alertDialog = new AlertDialog.Builder(this).Create();

            alertDialog.SetView(view);
            alertDialog.SetCanceledOnTouchOutside(false);

            Button btnInsertSPE = view.FindViewById<Button>(Resource.Id.btnInsertSPE);
            Button btnExit = view.FindViewById<Button>(Resource.Id.btnExit);
            EditText txtCode = view.FindViewById<EditText>(Resource.Id.txtCodeDialogINS);
            EditText txtLibelle = view.FindViewById<EditText>(Resource.Id.txtLibelleDialogINS);

            txtCode.Text = spaIntent.codeSPE;
            txtLibelle.Text = spaIntent.libelleSPE;

            btnExit.Click += delegate
            {
                alertDialog.Dismiss();
            };

            btnInsertSPE.Click += delegate
            {
                var codeSPE = txtCode.Text;
                var libelleSPE = txtLibelle.Text;
                if (codeSPE != "" && libelleSPE != "")
                {
                    Uri url = new Uri("http://" + GetString(Resource.String.ip) + "modifierSpecialite.php?codeSPE=" + codeSPE + "&libelleSPE=" + libelleSPE);
                    ws.DownloadDataAsync(url);
                    lesSpecialites.Where(s => s.codeSPE == codeSPE).First().libelleSPE = libelleSPE;
                    alertDialog.Dismiss();
                    this.ListSpecialites();
                    Toast.MakeText(this, "La spécialité a été modifée", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Veuillez saisir le code ou le nom !", ToastLength.Short).Show();
                }

            };

            alertDialog.Show();
        }
    }
}