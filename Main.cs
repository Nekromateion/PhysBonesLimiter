using Newtonsoft.Json;
using System.IO;

namespace PhysBonesLimiter
{
    internal class Main
    {
        internal const string FolderPath = "UserData/PhysBoneLimiter";
        internal const string ListsJsonPath = "UserData/PhysBoneLimiter/Lists.json";

        internal static void ApplicationStart()
        {
            Directory.CreateDirectory(FolderPath);
            if (File.Exists(ListsJsonPath))
            {
                try
                {
                    Lists.Instance = JsonConvert.DeserializeObject<Lists>(File.ReadAllText(ListsJsonPath));
                }
                catch
                {
                    Lists.Instance = new Lists();
                }
            }
            else Lists.Instance = new Lists();
            PhysBonesLimiter.MelonPrefrences.Init();
            UIExpansionKit.RegisterButtons();
        }
    }
}
