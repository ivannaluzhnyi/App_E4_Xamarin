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
    class SpinnerSPE : ArrayAdapter<Specialite>
    {

        Activity context;
        List<Specialite> lesSpecialites;

        public SpinnerSPE(Activity unContext, List<Specialite> desSpesialites)
            : base(unContext, Resource.Layout.ItemSpinner, desSpesialites)
        {
            context = unContext;
            lesSpecialites = desSpesialites;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = context.LayoutInflater.Inflate(Resource.Layout.ItemSpinner, null);
            view.FindViewById<TextView>(Resource.Id.txtSpinerID).Text = lesSpecialites[position].codeSPE.ToString();
            view.FindViewById<TextView>(Resource.Id.txtSpinerPrenom).Text = lesSpecialites[position].libelleSPE.ToString();
            return view;
        }
    }
}