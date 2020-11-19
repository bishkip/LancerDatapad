using System;
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
            Console.SetWindowSize(50, 35);
            Console.Clear();
            Console.WriteLine("Enter cloud id");
            string id = Console.ReadLine();
            id = id != "" ? id : "ff290a8a27aec15e5a4641206cdc58ad";
            id = id != " " ? id : "6e44d8384a364290007670232016279e";
            Console.Clear();
            LancerData ld = new LancerData();
            Content content = await ld.GetContent(id);
            if (content == null) return;

            Console.WriteLine($"Callsign: \n\t{content.callsign} \n" +
                              $"Name: \n\t{content.name}");
            Console.WriteLine($"\n---------------------------------------------\n");

            Mech mech = content.mechs.Where(p => p.active == true).FirstOrDefault() ?? content.mechs.FirstOrDefault();
            if (mech == null) return;
            Console.WriteLine($"Name: \n\t{mech.name} \n" +
                              $"Frame: \n\t{mech.frame}");
            Console.WriteLine($"\n---------------------------------------------\n");

            Loadout2 loadout = mech.loadouts.FirstOrDefault();
            if (loadout == null) return;
            Console.WriteLine($"Systems:");
            foreach (var system in loadout.systems)
            {
                Console.WriteLine($"\t{system.id}");
            }
            Console.WriteLine ($"\n---------------------------------------------\n");

            Console.WriteLine($"Weapons:");
            foreach (var mount in loadout.mounts)
            {
                Console.WriteLine($"\t{mount.mount_type}:");
                foreach(var slot in mount.slots)
                {
                    if (slot.weapon != null)
                        Console.WriteLine($"\t\t{slot.weapon.id}");
                }
                foreach (var extra in mount.extra)
                {
                    if(extra.weapon != null)
                        Console.WriteLine($"\t\t{extra.weapon.id}");
                }
            }
            Console.WriteLine($"\n---------------------------------------------\n");
            Console.ReadKey();
        }
    }
}