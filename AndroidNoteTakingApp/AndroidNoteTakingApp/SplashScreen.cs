
using Android.App;
using Android.Content;
using Android.OS;
using System.Timers;
using System.Threading.Tasks;

namespace AndroidNoteTakingApp
{
    [Activity(Label = "Android Note Taking App", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true)]
    public class SplashScreen : Activity
    {
        Timer timer = new Timer(3000);
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SplashScreen);
            
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            timer.Start();
           
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            timer.Dispose();

            var myIntent = new Intent(this, typeof(MainActivity));
            StartActivity(myIntent);

        }
    }
}