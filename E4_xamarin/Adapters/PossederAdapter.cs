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
    class PossederAdapter : ArrayAdapter<Posseder>
    {

        Activity context;
        List<Posseder> lesPosseders;

        public PossederAdapter(Activity unContext, List<Posseder> desPosseders)
            : base(unContext, Resource.Layout.ItemPosseder, desPosseders)
        {
            context = unContext;
            lesPosseders = desPosseders;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = context.LayoutInflater.Inflate(Resource.Layout.ItemPosseder, null);
            view.FindViewById<TextView>(Resource.Id.txtSpecialiteItemPOS).Text = lesPosseders[position].libelleSPE.ToString();
            view.FindViewById<TextView>(Resource.Id.txtDiplomeItemPOS).Text = lesPosseders[position].diplomePOS.ToString();
            view.FindViewById<TextView>(Resource.Id.txtCoefItemPOS).Text = lesPosseders[position].coefPOS.ToString();
            return view;
        }
    }
}