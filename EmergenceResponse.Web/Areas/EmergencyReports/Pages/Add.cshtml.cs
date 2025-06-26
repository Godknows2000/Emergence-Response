using EmergenceResponse.Data;
using EmergenceResponse.Lib;
using EmergenceResponse.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;


namespace EmergenceResponse.Web.Areas.EmergencyReports.Pages
{
    public class AddModel : SysPageModel
    {
        [BindProperty]
        public Emergency Emergency { get; set; }
        [BindProperty]
        public Location Location { get; set; }   
        public EmergencyType? EType { get;set; }
        public void OnGet(EmergencyType? etype)
        {
            if(etype != null)
                EType = (EmergencyType)etype;

        }
        public async Task<IActionResult> OnPost(string audioData)
        {
            if (!string.IsNullOrEmpty(audioData))
            {
                // Check if the data starts with the base64 data URL prefix
                if (audioData.StartsWith("data:audio/wav;base64,"))
                {
                    // Remove the prefix and get the base64 string
                    string base64Audio = audioData.Substring("data:audio/wav;base64,".Length);

                    // Convert the base64 string to a byte array
                    byte[] audioBytes = Convert.FromBase64String(base64Audio);

                    // Save the byte array and MIME type to the database
                    Emergency.AudioData = audioBytes;
                    Emergency.AudioMimeType = "audio/wav"; // Set the appropriate MIME type
                }
                else
                {
                    // Handle invalid or unsupported audio data format
                    // Log an error or throw an exception as needed
                }
            }
            Location.Id = Guid.NewGuid();
            Db.Locations.Add(Location);

            Emergency.Id = Guid.NewGuid();
            Emergency.CreationDate = DateTime.Now;
            Emergency.CreatorId = (Guid)CurrentUser.MemberId;
            Emergency.LocationId = Location.Id;
            Emergency.StatusId = (int)EmergencyStatus.UNASSIGNED;

            Db.Emergencies.Add(Emergency);
            await Db.SaveChangesAsync();

            await GetDistance(Location, Emergency);

            return Redirect("/Index");
        }


        public async Task<Data.ServiceProvider> GetDistance(Location origin, Emergency em)
        {
            var apiKey = "AIzaSyD8358F9bbWt_IDpOAXqbDfwwYago-RPPk";
            var dests = Db.ServiceProviders.Include(c => c.Location).Where(c => c.TypeId == em.TypeId).ToList();

            var distances = new List<double>();

            foreach (var dest in dests)
            {
                using (var client = new HttpClient())
                {
                    var distanceResponse = await client.GetAsync($"https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={origin.Latitude},{origin.Longitude}&destinations={dest.Location.Latitude},{dest.Location.Longitude}&key={apiKey}");
                    var distanceContent = await distanceResponse.Content.ReadAsStringAsync();
                    var jsonResponse = JObject.Parse(distanceContent);

                    var status = jsonResponse["status"].ToString();
                    if (status != "OK")
                    {
                        Console.WriteLine($"Error with Distance Matrix API: {status}");
                        continue;
                    }

                    var rows = jsonResponse["rows"];
                    if (rows == null || rows.Count() == 0 || rows[0]["elements"] == null || rows[0]["elements"].Count() == 0)
                    {
                        Console.WriteLine("No valid distance found for this destination.");
                        continue;
                    }

                    var distance = rows[0]["elements"][0]["distance"]["value"];
                    distances.Add((double)distance); // Add the distance value to the list
                }
            }

            if (distances.Count == 0)
            {
                Console.WriteLine("No distances available to compare.");
                return null;  // Handle the case where no distances were found
            }

            var sp = dests[distances.IndexOf(distances.Min())];
            em.ServiceProviderId = sp.Id;
            em.StatusId = (int)EmergencyStatus.ASSIGNED;

            await Db.SaveChangesAsync();

            return sp;
        }
    }
}
