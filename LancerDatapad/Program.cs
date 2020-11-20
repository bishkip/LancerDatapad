using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LancerDatapad
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    MainAsync().GetAwaiter().GetResult();
                }
                catch
                {
                }
            }
        }

        private static async Task MainAsync()
        {
            //Console setup
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetWindowSize(50, 35);
            Console.Clear();
            Console.WriteLine("Enter cloud id");
            string id = Console.ReadLine();
            Console.Clear();


            id = id != "" ? id : "ff290a8a27aec15e5a4641206cdc58ad";
            id = id != " " ? id : "6e44d8384a364290007670232016279e";
            LancerData ld = new LancerData();
            await ConsoleDisplay(ld, id);
        }

        private static async Task ConsoleDisplay(LancerData ld, string id)
        {
            //Pilot
            Content content = await ld.GetApiContent(id);
            if (content == null)
            {
                Debug.WriteLine("API CONTENT NULL");
                content = ld.GetLocalContent("LancerDatapad.JETGALACTIC.json");
            }
            if (content == null)
            {
                Debug.WriteLine("LOCAL CONTENT NULL");
                return;
            }

            Console.WriteLine($"\n---------------------------------------------\n");
            Console.WriteLine($"Callsign: \n\t{content.callsign} \n" +
                              $"Name: \n\t{content.name}");
            Console.WriteLine($"\n---------------------------------------------\n");

            //Mech
            Mech mech = content.mechs.Where(p => p.active == true).FirstOrDefault() ?? content.mechs.FirstOrDefault();
            if (mech == null) return;

            Console.WriteLine($"Name: \n\t{mech.name} \n" +
                              $"Frame: \n\t{mech.frame}");
            Console.WriteLine($"\n---------------------------------------------\n");

            //Mech Loadout
            Loadout2 loadout = mech.loadouts.FirstOrDefault();
            if (loadout == null) return;
            Console.WriteLine($"Systems:");
            foreach (var system in loadout.systems)
            {
                Console.WriteLine($"\t{system.id}");
            }
            Console.WriteLine($"\n---------------------------------------------\n");

            Console.WriteLine($"Weapons:");
            foreach (var mount in loadout.mounts)
            {
                Console.WriteLine($"\t{mount.mount_type}:");
                foreach (var slot in mount.slots)
                {
                    if (slot.weapon != null)
                        Console.WriteLine($"\t\t{slot.weapon.id}");
                }
                foreach (var extra in mount.extra)
                {
                    if (extra.weapon != null)
                        Console.WriteLine($"\t\t{extra.weapon.id}");
                }
            }
            Console.WriteLine($"\n---------------------------------------------\n");
            Console.ReadKey();
        }
    }
}