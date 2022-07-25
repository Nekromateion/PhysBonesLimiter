using HarmonyLib;
using System;
using VRC;
using VRC.Core;

namespace PhysBonesLimiter
{
    public class Patches
    {
        internal static HarmonyLib.Harmony _harmonyInstance;
        private static HarmonyMethod GetPatch(string name) => new HarmonyMethod(typeof(Patches).GetMethod(name, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic));
    
        public static void Patch()
        {
            try
            {
                _harmonyInstance.Patch(typeof(PipelineManager).GetMethod("Start", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance), GetPatch(nameof(AvatarLoad)));
            }
            catch (Exception e)
            {
                Console.Beep();
                Logger.Err("Failed to patch avatar load: " + e);
            }
        }

        public static void Unpatch()
        {
            try
            {
                _harmonyInstance.UnpatchSelf();
            } 
            catch (Exception e)
            {
                Console.Beep();
                Logger.Err("Failed to unpatch: " + e);
            }
        }

        private static void AvatarLoad(PipelineManager __instance)
        {
            try
            {
                if(__instance != null && __instance.contentType == PipelineManager.ContentType.avatar)
                {
                    bool anyUserList = Lists.Instance.UserBlackList.Count > 0 || Lists.Instance.UserWhiteList.Count > 0;
                    if (anyUserList || MelonPrefrences.ExcludeSelf)
                    {
                        var player = __instance.GetComponentInParent<Player>();
                        if (player != null)
                        {
                            var apiUser = player.GetAPIUser();
                            if (MelonPrefrences.ExcludeSelf && apiUser.id == APIUser.CurrentUser.id) return;
                            if (anyUserList)
                            {
                                AvatarScanner.ScanAvatar(__instance.gameObject, __instance.blueprintId, apiUser.id);
                                return;
                            }
                        }
                    }
                    AvatarScanner.ScanAvatar(__instance.gameObject, __instance.blueprintId);
                }
            }
            catch{}
        }
    }
}
