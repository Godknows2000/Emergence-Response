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
                // Convert Base64 to Byte Array
                byte[] audioBytes = Convert.FromBase64String(audioData.Split(',')[1]);

                // Save audio to the database
                Emergency.AudioData = audioBytes;
                Emergency.AudioMimeType = "audio/wav"; // Set MIME type
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
            var apiKey = "";
            var dests = Db.ServiceProviders.Include(c => c.Location).Where(c => c.TypeId == em.TypeId).ToList();

            var distances = new List<double>();

            foreach (var dest in dests)
            {
                using (var client = new HttpClient())
                {

                    var distanceResponse = await client.GetAsync($"https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={origin.Latitude},{origin.Longitude}&destinations={dest.Location.Latitude},{dest.Location.Longitude}&key={apiKey}");
                    var distanceContent = await distanceResponse.Content.ReadAsStringAsync();
                    var distance = JObject.Parse(distanceContent)["rows"][0]["elements"][0]["distance"]["value"];

                    distances.Add((double)distance); // Convert meters to miles
                }
            }
            var sp = dests[distances.IndexOf(distances.Min())];
            em.ServiceProviderId = sp.Id;
            em.StatusId = (int)EmergencyStatus.ASSIGNED;

            if (await TryUpdateModelAsync(em, nameof(Emergency), c => c.ServiceProviderId, c => c.StatusId))
            {
                await Db.SaveChangesAsync();

            }
            return sp;           
        }
    }
}
