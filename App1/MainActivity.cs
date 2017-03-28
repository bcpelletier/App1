using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Threading.Tasks;


namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        TextView tv1;
        ProductViewModel pvm;
        

        protected override void OnCreate(Bundle bundle)
        {
            pvm = new ProductViewModel();

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            


            Button translateButton = FindViewById<Button>(Resource.Id.button1);
            tv1 = FindViewById<TextView>(Resource.Id.textView1);
           



            // Add code to translate number
            string translatedNumber = string.Empty;

            translateButton.Click += (object sender, EventArgs e) =>
            {
                tv1.Text = "Cargando....";
                SearchAllProducts();
            };

            

        }

        protected async void SearchAllProducts()
        {
            pvm.SearchTerm = "s";

            int status = await pvm.Search();

            if (pvm.Products.Length > 0) tv1.Text = pvm.Products[0].Name;

            /*Task t2 = t1.ContinueWith( (antecedent) =>

            tv1.Text = "Connected"
            );

            t2.Wait();
            */

        }

        

        /*
        Task.Run(async () =>
    {
        RunOnUiThread(() => txt.Text = "Connecting...");
        await Task.Delay(2500); // Simulate SQL Connection time

        if (sql.testConnection())
        {
            RunOnUiThread(() => txt.Text = "Connected...");
        await Task.Delay(2500); // Simulate SQL Load time
                                //load();
    }
        else
            RunOnUiThread(() => txt.Text = "SQL Connection error");
            
);*/

    }
}

