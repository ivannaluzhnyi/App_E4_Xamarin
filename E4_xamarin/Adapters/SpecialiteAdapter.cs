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
    public class SpecialiteAdapter : ArrayAdapter<Specialite>
    {

        Activity context;
        List<Specialite> lesSpecialites;

        public SpecialiteAdapter(Activity unContext, List<Specialite> desSpesialites)
            : base(unContext, Resource.Layout.ItemSpecialite, desSpesialites)
        {
            context = unContext;
            lesSpecialites = desSpesialites;
        }




        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var view = context.LayoutInflater.Inflate(Resource.Layout.ItemSpecialite, null);
            //view.FindViewById<TextView>(Resource.Id.txtCodeSPE).Text = lesSpecialites[position].codeSPE.ToString();
            view.FindViewById<TextView>(Resource.Id.txtLiballeSPE).Text = lesSpecialites[position].libelleSPE.ToString();
            return view;
        }
    }
}