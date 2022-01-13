using System;
using System.Threading.Tasks;
using System.Web.Helpers;
using Windows.Web.Http;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace App3
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async Task Login_Click(object sender, RoutedEventArgs e)
        {
            /// Using worldclockapi for Example > Error Still Appears on other JSON APIs
            var uri = new System.Uri("http://worldclockapi.com/api/json/utc/now");
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string web_result = await httpClient.GetStringAsync(uri);
                    dynamic  result = Json.Decode(web_result);
                    
                    /// Example of how the data is Processed
                    ContentDialog server_response = new ContentDialog()
                    {
                        Title = "Server Response",
                        Content = "Current Date Time: " + result.currentDateTime,
                        CloseButtonText = "Ok"
                    };
                    await server_response.ShowAsync();
                }
                catch (Exception ex)
                {
                    if (ex.Message != "")
                    {
                        ContentDialog server_error = new ContentDialog()
                        {
                            Title = "Server Fehler",
                            Content = "Fehler bei der Verbindung mit dem Server.\x0AÜberprüfe deine Internet verbindung oder deine Eingaben und versuche es erneut!\x0AFehler: " + ex.Message,
                            CloseButtonText = "Ok"
                        };
                        await server_error.ShowAsync();
                    }
                }
            }
        }
    }
}
