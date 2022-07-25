using MelonLoader;

[assembly: MelonInfo(typeof(PhysBonesLimiter.MlMain), PhysBonesLimiter.BuildInfo.ModName, PhysBonesLimiter.BuildInfo.ModVersion, PhysBonesLimiter.BuildInfo.ModAuthor)]
[assembly: MelonGame(PhysBonesLimiter.BuildInfo.MelonGame, PhysBonesLimiter.BuildInfo.MelonGame)]

namespace PhysBonesLimiter
{
    public class MlMain : MelonMod
    {
        public override void OnApplicationStart()
        {
            Patches._harmonyInstance = this.HarmonyInstance;
            Main.ApplicationStart();
            MelonHandler.Mods.Remove(this);
        }
    }
}
