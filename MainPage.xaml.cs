using Newtonsoft.Json; // Is used to handle the Json file

namespace Read_API
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        // Deserializing Json files means that you are
        // Making them into objects.
        // Therefore you need to save the values correctly

        // Change these according to your API's needs
        // One fast way to do that is using https://json2csharp.com/
        class JsonSave
        {
            public string abbreviation { get; set; }
            public string client_ip { get; set; }
            public DateTime datetime { get; set; }
            public int day_of_week { get; set; }
            public int day_of_year { get; set; }
            public bool dst { get; set; }
            public DateTime dst_from { get; set; }
            public int dst_offset { get; set; }
            public DateTime dst_until { get; set; }
            public int raw_offset { get; set; }
            public string timezone { get; set; }
            public int unixtime { get; set; }
            public DateTime utc_datetime { get; set; }
            public string utc_offset { get; set; }
            public int week_number { get; set; }
        }

        private async void GetApiBtn_Clicked(object sender, EventArgs e)
        {
            // Starts the HttpClient
            HttpClient client = new HttpClient();
            
            // Tries to connect to the given URL and deserialize the return Json text
            try
            {
                // Asks for the file and saves it into "response"
                var response = await client.GetStringAsync(ApiLocationEditor.Text);
                
                // Using Newsoft, converting the recieved Json string into object
                JsonSave call = JsonConvert.DeserializeObject<JsonSave>(response);

                // Displaying the recieved time using the just created object
                ApiRecivedLabel.Text = call.datetime.ToString();
            }

            // If the URL or message is not as expected
            catch
            {
                ApiRecivedLabel.Text = "Something went wrong, is the link written correctly?";
            }
        }

        private void CopyBtn_Clicked(object sender, EventArgs e)
        {
            // Simply copies the editors text to clipboard
            Clipboard.SetTextAsync(ApiLocationEditor.Text);
        }

        private async void PasteBtn_Clicked(object sender, EventArgs e)
        {
            // Simply pastes the text from clipboard
            // Checks if it's in text - format
            if (Clipboard.HasText)
            {
                ApiLocationEditor.Text = await Clipboard.Default.GetTextAsync();
            }
            else
            {
                ApiLocationEditor.Text = "Clipboard is empty or doesn't contain text!";
            }
        }
    }
}