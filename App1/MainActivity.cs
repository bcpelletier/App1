
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
using Newtonsoft.Json;
using Android.Views.InputMethods;


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

            string mensage;

            Button translateButton = FindViewById<Button>(Resource.Id.button1);
            Button benviar = FindViewById<Button>(Resource.Id.benviar);
            RelativeLayout rl = FindViewById<RelativeLayout>(Resource.Id.relativeLayout1);

            //ScrollView scroll = FindViewById<ScrollView>(Resource.Id.scrollView1);
            LinearLayout llChatMessages = FindViewById<LinearLayout>(Resource.Id.linear);
            //ListView lv = FindViewById<ListView>(Resource.Id.listView1);

            //ArrayAdapter<string> adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, list);
            //llChatMessages. = adapter;

            tv1 = FindViewById<TextView>(Resource.Id.textView1);
            et1 = FindViewById<EditText>(Resource.Id.messages);


                        /*
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

            */
            socket = SocketIO.Client.IO.Socket("http://10.0.2.2:8080");
            socket.Connect();


            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                socket.On("chat message", data =>
                {

                    // get the json data from the server message

                    string t = data[0].ToString();

                    var jobject = Newtonsoft.Json.Linq.JObject.Parse(t);

                    // get the message data values
                    var username = jobject.Value<string>("username");

                    var message = jobject.Value<string>("message");

                RunOnUiThread(() =>
                {
                    tv1.Text = tv1.Text.ToString() + "\n" + message;
                    
                    TextView tvtemp = new TextView(this.BaseContext);
                    tvtemp.Gravity = Android.Views.GravityFlags.Right;
                    tvtemp.Text = message;
                    llChatMessages.AddView(tvtemp);
                });
                    

                    mensage = username + " " + message;
                    // get the number of users
                    //var numUsers = jobject. Value<int>("numUsers");

                    // display the welcome message...
                });
            })).Start();
            
         
            // Add code to translate number
            string translatedNumber = string.Empty;

            translateButton.Click += (object sender, EventArgs e) =>
            {
                byte[] byteData = System.Text.Encoding.ASCII.GetBytes("new message");

                
                //clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, Client.sendCallback, clientSocket);

                RefreshDataAsync();
                tv1.Text = "Cargando....";
                SearchAllProducts();
                
            };

            benviar.Click += (object sender, EventArgs e) =>
            {
                socket.Emit("chat message", et1.Text);
                et1.Text = "";
                


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

