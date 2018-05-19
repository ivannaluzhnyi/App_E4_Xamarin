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
    public class InviterAdapter : ArrayAdapter<Inviter>
    {

        Activity context;
        List<Inviter> lesInviters;

        public InviterAdapter(Activity unContext, List<Inviter> desInviters)
            : base(unContext, Resource.Layout.ItemActivite, desInviters)
        {
            context = unContext;
            lesInviters = desInviters;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var mot = lesInviters[position].motif;
            var them = lesInviters[position].theme;
            var lieu = lesInviters[position].lieu;
            var special = lesInviters[position].specialisteon;
            if (mot == null)
            {
                mot = "sans motif";
            }
            if(them == null)
            {
                them = "sans theme";
            }
            if(lieu == null)
            {
                lieu = "sans lieu";
            }
            var view = context.LayoutInflater.Inflate(Resource.Layout.ItemActivite, null);
            view.FindViewById<TextView>(Resource.Id.txtThemeItem).Text = them.ToString();
            view.FindViewById<TextView>(Resource.Id.txtMotifItem).Text = mot.ToString();
            view.FindViewById<TextView>(Resource.Id.txtLieuItem).Text = lieu.ToString();
            view.FindViewById<TextView>(Resource.Id.txtDate).Text = lesInviters[position].date.ToShortDateString();
            view.FindViewById<TextView>(Resource.Id.txtSpecialist).Text = special.ToString();

            return view;
        }

    }
}