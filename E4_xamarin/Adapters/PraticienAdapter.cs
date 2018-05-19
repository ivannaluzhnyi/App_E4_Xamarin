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
using E4_xamarin.Modeles;

namespace E4_xamarin.Adapters
{
    public class PraticienAdapter : ArrayAdapter<Praticien>
    {

        Activity context;
        List<Praticien> lesPraticiens;

        public PraticienAdapter(Activity unContext, List<Praticien> desPraticiens)
            : base(unContext, Resource.Layout.PraticienItem, desPraticiens)
        {
            context = unContext;
            lesPraticiens = desPraticiens;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = context.LayoutInflater.Inflate(Resource.Layout.PraticienItem, null);
            view.FindViewById<TextView>(Resource.Id.txtIdPRAIteme).Text = lesPraticiens[position].idPRA.ToString();
            view.FindViewById<TextView>(Resource.Id.txtPrenomPRAItem).Text = lesPraticiens[position].prenomPRA.ToString();
            view.FindViewById<TextView>(Resource.Id.txtNomPRAItem).Text = lesPraticiens[position].nomPRA.ToString();
            return view;
        }
    }
}