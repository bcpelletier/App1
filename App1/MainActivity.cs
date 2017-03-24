using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

                   
            Button translateButton = FindViewById<Button>(Resource.Id.button1);
            TextView t1 = FindViewById<TextView>(Resource.Id.textView1);
                        
            // Add code to translate number
            string translatedNumber = string.Empty;

            translateButton.Click += (object sender, EventArgs e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                t1.Text = "Hola";
                SetContentView(Resource.Layout.layout1);
            };

        }


    }
}

