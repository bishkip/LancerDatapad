using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace LancerDatapad
{
    internal class LancerData
    {
        private readonly string pathPrefix = "https://api.github.com/gists/";

        private readonly HttpClient _client;

        public LancerData()
        {
            _client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            _client.DefaultRequestHeaders.Referrer = new Uri("https://compcon.app/");
            _client.DefaultRequestHeaders.UserAgent.TryParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", "e9822ae1436214d469a9de3d02114f85d52a2002");
        }

        public async Task<Content> GetContent(string id)
        {
            string path = pathPrefix + id;
            var json = await GetJSONAsync(path);
            return ReadContent(json);
        }

        //Get JSON from API
        public async Task<string> GetJSONAsync(string url)
        {
            string content;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                Debug.WriteLine(url);
                Debug.WriteLine($"API Response: {response.StatusCode}");
                //Debug.WriteLine(response.RequestMessage);
                //Debug.WriteLine(response.Content.Headers);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Debug.WriteLine("API FAILED");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tFETCH ERROR", ex.Message);
                return null;
            }

            return content;
        }

        //Get Rates from JSON
        public Content ReadContent(string rawJson)
        {
            Content contentData = null;
            try
            {
                var parsedJson = (string)JObject.Parse(rawJson)["files"]["pilot.txt"]["content"];
                contentData = JsonConvert.DeserializeObject<Content>(parsedJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                Debug.WriteLine("DATA READ OKAY");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tREAD ERROR", ex.Message);
                return null;
            }
            return contentData;
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class License
    {
        public string id { get; set; }
        public int rank { get; set; }
    }

    public class Skill
    {
        public string id { get; set; }
        public int rank { get; set; }
        public bool? custom { get; set; }
        public string custom_desc { get; set; }
        public string custom_detail { get; set; }
    }

    public class Talent
    {
        public string id { get; set; }
        public int rank { get; set; }
    }

    public class Armor
    {
        public string id { get; set; }
        public bool destroyed { get; set; }
        public int uses { get; set; }
        public bool cascading { get; set; }
        public object customDamageType { get; set; }
    }

    public class Weapon
    {
        public string id { get; set; }
        public bool destroyed { get; set; }
        public int uses { get; set; }
        public bool cascading { get; set; }
        public object customDamageType { get; set; }
    }

    public class Gear
    {
        public string id { get; set; }
        public bool destroyed { get; set; }
        public int uses { get; set; }
        public bool cascading { get; set; }
        public object customDamageType { get; set; }
    }

    public class Loadout
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Armor> armor { get; set; }
        public List<Weapon> weapons { get; set; }
        public List<Gear> gear { get; set; }
        public List<object> extendedWeapons { get; set; }
        public List<object> extendedGear { get; set; }
    }

    public class System
    {
        public string id { get; set; }
        public int uses { get; set; }
        public bool destroyed { get; set; }
        public bool cascading { get; set; }
    }

    public class Weapon2
    {
        public string id { get; set; }
        public int uses { get; set; }
        public bool destroyed { get; set; }
        public bool cascading { get; set; }
        public bool loaded { get; set; }
        public object mod { get; set; }
        public object customDamageType { get; set; }
        public int maxUseOverride { get; set; }
    }

    public class Slot
    {
        public string size { get; set; }
        public Weapon2 weapon { get; set; }
    }

    public class Weapon3
    {
        public string id { get; set; }
        public int uses { get; set; }
        public bool destroyed { get; set; }
        public bool cascading { get; set; }
        public bool loaded { get; set; }
        public object mod { get; set; }
        public object customDamageType { get; set; }
        public int maxUseOverride { get; set; }
    }

    public class Extra
    {
        public string size { get; set; }
        public Weapon3 weapon { get; set; }
    }

    public class Mount
    {
        public string mount_type { get; set; }
        public bool @lock { get; set; }
        public List<Slot> slots { get; set; }
        public List<Extra> extra { get; set; }
        public List<object> bonus_effects { get; set; }
    }

    public class Slot2
    {
        public string size { get; set; }
        public object weapon { get; set; }
    }

    public class Extra2
    {
        public string size { get; set; }
        public object weapon { get; set; }
    }

    public class ImprovedArmament
    {
        public string mount_type { get; set; }
        public bool @lock { get; set; }
        public List<Slot2> slots { get; set; }
        public List<Extra2> extra { get; set; }
        public List<object> bonus_effects { get; set; }
    }

    public class Slot3
    {
        public string size { get; set; }
        public object weapon { get; set; }
    }

    public class IntegratedWeapon
    {
        public string mount_type { get; set; }
        public bool @lock { get; set; }
        public List<Slot3> slots { get; set; }
        public List<object> extra { get; set; }
        public List<object> bonus_effects { get; set; }
    }

    public class Loadout2
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<System> systems { get; set; }
        public List<object> integratedSystems { get; set; }
        public List<Mount> mounts { get; set; }
        public List<object> integratedMounts { get; set; }
        public ImprovedArmament improved_armament { get; set; }
        public IntegratedWeapon integratedWeapon { get; set; }
    }

    public class State
    {
        public string stage { get; set; }
        public int turn { get; set; }
        public int move { get; set; }
        public int actions { get; set; }
        public bool overwatch { get; set; }
        public bool braced { get; set; }
        public bool overcharged { get; set; }
        public bool prepare { get; set; }
        public bool bracedCooldown { get; set; }
        public bool redundant { get; set; }
        public List<object> history { get; set; }
    }

    public class Mech
    {
        public string id { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public string gm_note { get; set; }
        public string portrait { get; set; }
        public string cloud_portrait { get; set; }
        public string frame { get; set; }
        public bool active { get; set; }
        public int current_structure { get; set; }
        public int current_hp { get; set; }
        public int overshield { get; set; }
        public int current_stress { get; set; }
        public int current_heat { get; set; }
        public int current_repairs { get; set; }
        public int current_overcharge { get; set; }
        public int current_core_energy { get; set; }
        public List<Loadout2> loadouts { get; set; }
        public int active_loadout_index { get; set; }
        public List<object> statuses { get; set; }
        public List<object> conditions { get; set; }
        public List<object> resistances { get; set; }
        public List<object> reactions { get; set; }
        public int burn { get; set; }
        public bool ejected { get; set; }
        public bool destroyed { get; set; }
        public string defeat { get; set; }
        public int activations { get; set; }
        public bool meltdown_imminent { get; set; }
        public bool reactor_destroyed { get; set; }
        public string cc_ver { get; set; }
        public State state { get; set; }
    }

    public class CounterData
    {
        public string id { get; set; }
        public int val { get; set; }
    }

    public class Content
    {
        public string id { get; set; }
        public string campaign { get; set; }
        public string group { get; set; }
        public int sort_index { get; set; }
        public string cloudID { get; set; }
        public string cloudOwnerID { get; set; }
        public string lastCloudUpdate { get; set; }
        public int level { get; set; }
        public string callsign { get; set; }
        public string name { get; set; }
        public string player_name { get; set; }
        public string status { get; set; }
        public bool mounted { get; set; }
        public string text_appearance { get; set; }
        public string notes { get; set; }
        public string history { get; set; }
        public string portrait { get; set; }
        public string cloud_portrait { get; set; }
        public string quirk { get; set; }
        public int current_hp { get; set; }
        public List<object> reserves { get; set; }
        public List<object> orgs { get; set; }
        public string background { get; set; }
        public List<int> mechSkills { get; set; }
        public List<License> licenses { get; set; }
        public List<Skill> skills { get; set; }
        public List<Talent> talents { get; set; }
        public List<string> core_bonuses { get; set; }
        public Loadout loadout { get; set; }
        public List<Mech> mechs { get; set; }
        public string active_mech { get; set; }
        public string cc_ver { get; set; }
        public List<CounterData> counter_data { get; set; }
        public List<object> custom_counters { get; set; }
        public List<object> brews { get; set; }
    }
}