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
            Console.SetWindowSize(70, 50);
            Console.Clear();
            Console.WriteLine("Enter cloud id");
            string id = Console.ReadLine();
            Console.Clear();


            id = id != "" ? id : "76d9eb5305d76911d26563668a8af3ae";
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

            Console.WriteLine($"\n-------------------------------------------------------------\n");
            Console.WriteLine($"\tName: \n\t\t\t{content.name}");
            Console.WriteLine($"\tCallsign: \n\t\t\t{content.callsign}");
            Console.WriteLine($"\tSkills:");
            foreach (var skill in content.skills)
            {
                Console.WriteLine($"\t\t\t{skill.id}: {skill.rank}");
            }
            Console.WriteLine($"\tTalents:");
            foreach (var talent in content.talents)
            {
                Console.WriteLine($"\t\t\t{talent.id}: {talent.rank}");
            }
            Console.WriteLine($"\n-------------------------------------------------------------\n");

            //Mech
            Mech mech = content.mechs.Where(p => p.active == true).FirstOrDefault() ?? content.mechs.FirstOrDefault();
            if (mech == null) return;

            Console.WriteLine($"\tName: \n\t\t\t{mech.name}");
            Console.WriteLine($"\tFrame: \n\t\t\t{mech.frame}");
            Console.WriteLine($"\tCore Bonuses:");
            foreach (var core_bonus in content.core_bonuses)
            {
                Console.WriteLine($"\t\t\t{core_bonus}");
            }
            //Mech Loadout
            Loadout2 loadout = mech.loadouts.FirstOrDefault();
            if (loadout == null) return;
            Console.WriteLine($"\tSystems:");
            foreach (var system in loadout.systems)
            {
                Console.WriteLine($"\t\t\t{system.id}");
            }
            Console.WriteLine($"\tWeapons:");
            foreach (var mount in loadout.mounts)
            {
                Console.WriteLine($"\t\t{mount.mount_type}:");
                foreach (var slot in mount.slots)
                {
                    if (slot.weapon != null)
                        Console.WriteLine($"\t\t\t{slot.weapon.id}");
                }
                foreach (var extra in mount.extra)
                {
                    if (extra.weapon != null)
                        Console.WriteLine($"\t\t\t{extra.weapon.id}");
                }
            }
            Console.WriteLine($"\n-------------------------------------------------------------\n");
            Console.ReadKey();
        }
    }
}