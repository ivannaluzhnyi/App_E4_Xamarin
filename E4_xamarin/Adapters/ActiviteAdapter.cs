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
    public class ActiviteAdapter : ArrayAdapter<Activite>
    {

        Activity context;
        List<Activite> lesActivites;

        public ActiviteAdapter(Activity unContext, List<Activite> desActivites)
            : base(unContext, Resource.Layout.ItemActiviteInv, desActivites)
        {
            context = unContext;
            lesActivites = desActivites;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var mot = lesActivites[position].motif;
            var them = lesActivites[position].theme;
            var lieu = lesActivites[position].lieu;
            if (mot == null)
            {
                mot = "sans motif";
            }
            if (them == null)
            {
                them = "sans theme";
            }
            if (lieu == null)
            {
                lieu = "sans lieu";
            }
            var view = context.LayoutInflater.Inflate(Resource.Layout.ItemActiviteInv, null);
            view.FindViewById<TextView>(Resource.Id.txtTheme).Text = them.ToString();
            view.FindViewById<TextView>(Resource.Id.txtMotif).Text = mot.ToString();
            view.FindViewById<TextView>(Resource.Id.txtLieu).Text = lieu.ToString();
            view.FindViewById<TextView>(Resource.Id.txtDate).Text = lesActivites[position].date.ToShortDateString();

            return view;
        }

    }
}