
using System.Text;
using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using static Android.Provider.SyncStateContract;
using System.Net;
using System.Json;
using Android.Media;
using System.Net.Sockets;
using SocketIO;
using Java.IO;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        TextView tv1;
        ProductViewModel pvm;
        EditText et1;
        SocketIO.Client.Socket socket;


        protected override void OnCreate(Bundle bundle)
        {
            pvm = new ProductViewModel();

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            


            Button translateButton = FindViewById<Button>(Resource.Id.button1);
            tv1 = FindViewById<TextView>(Resource.Id.textView1);
            et1 = FindViewById<EditText>(Resource.Id.messages);

            IPHostEntry hostEntry;
            hostEntry = Dns.GetHostEntry("10.0.2.2");

            IPAddress[] addresslist = Dns.GetHostAddresses("10.0.2.2");
                       
            IPEndPoint ipEndpoint = new IPEndPoint(addresslist[0], 1234);

            Socket clientSocket = new Socket(
              AddressFamily.InterNetwork,
              SocketType.Stream,
              ProtocolType.Tcp);

            IAsyncResult asyncConnect = clientSocket.BeginConnect(
              ipEndpoint,
              Client.connectCallback,
              clientSocket);

            
            //socket = SocketIO.Client.IO.Socket("http://10.0.2.2:1838");
            //socket.Connect();

            

           /* socket.On("new message", data => {

                // get the json data from the server message
                var jobject = data;

                // get the number of users
                //var numUsers = jobject. Value<int>("numUsers");

                // display the welcome message...
            });
            */
         
            // Add code to translate number
            string translatedNumber = string.Empty;

            translateButton.Click += (object sender, EventArgs e) =>
            {
                byte[] byteData = System.Text.Encoding.ASCII.GetBytes("new message");

                
                clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, Client.sendCallback, clientSocket);

                RefreshDataAsync();
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

        public async  void RefreshDataAsync()
        {

            string url = "http://10.0.2.2:3000/notes";
            
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            
            /*
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                System.IO.Stream st = response.GetResponseStream();

                JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(st));

                tv1.Text = jsonDoc.Count.ToString();
                // Get a stream representation of the HTTP web response:
               
            }
            */

            



            //socket.Emit("new message", "test de mensage");
            //socket.Emit("new message", "This is a message from Xamarin.Android...");
            

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

