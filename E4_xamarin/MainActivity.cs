using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using E4_xamarin.Activitys;

namespace E4_xamarin
{
    [Activity(Label = "Gestion de Praticien", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button btnSPE;
        Button btnSPEdePET;
        Button btnInvite;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            btnSPE = FindViewById<Button>(Resource.Id.btnSPE);
            btnSPEdePET = FindViewById<Button>(Resource.Id.btnSPAdePRT);
            btnInvite = FindViewById<Button>(Resource.Id.btnInvite);

            btnSPE.Click += BtnSPE_Click;
            btnSPEdePET.Click += BtnSPEdePET_Click;
            btnInvite.Click += BtnInvite_Click;
        }

        private void BtnInvite_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GestionInvit));
            StartActivity(intent);
        }

        private void BtnSPEdePET_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SpecialiteDePraticien));
            StartActivity(intent);
        }

        private void BtnSPE_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GestionSpecialite));
            StartActivity(intent);
        }
    }
}

