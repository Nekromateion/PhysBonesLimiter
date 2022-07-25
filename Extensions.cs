using MelonLoader;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.DataModel;
using VRC.UI.Elements.Menus;

namespace PhysBonesLimiter
{
    internal static class Extensions
    {
        private const string SelectedUserPath = "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local";
        private static Action<VRCPlayer, bool> _reloadAllAvatars;


        internal static void CreateUixButton(int uixExpansionMenu, string text, Action action) => MelonHandler.Mods.First(i => i.Info.Name == "UI Expansion Kit")?.Assembly?.GetType("UIExpansionKit.API.ExpansionKitApi")?.GetMethod("RegisterSimpleMenuButton", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)?.Invoke(null, new object[] { uixExpansionMenu, text, new Action(action), null });

        // yoinked from https://github.com/knah/VRCMods
        internal static void ReloadAllAvatars()
        {
            if (_reloadAllAvatars == null)
            {
                var targetMethod = typeof(VRCPlayer).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Single(it => it.Name.StartsWith("Method_Public_Void_Boolean_") && it.GetParameters().Length == 1 &&
                    it.GetParameters()[0].IsOptional && XrefScanner.UsedBy(it).Any(jt =>
                    {
                        if (jt.Type != XrefType.Method) return false;
                        var m = jt.TryResolve();
                        if (m == null) return false;
                        return m.DeclaringType == typeof(FeaturePermissionManager) &&
                               m.Name.StartsWith("Method_Public_Void_");
                    }));
                _reloadAllAvatars = (Action<VRCPlayer, bool>)Delegate.CreateDelegate(typeof(Action<VRCPlayer, bool>), targetMethod);
            }

            _reloadAllAvatars(VRCPlayer.field_Internal_Static_VRCPlayer_0, false);
        }


        internal static string GetUserId(this IUser user) => user.prop_String_0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PlayerManager PlayerManager()
        {
            return VRC.PlayerManager.field_Private_Static_PlayerManager_0;
        }

        internal static Il2CppSystem.Collections.Generic.List<Player> GetPlayers(this PlayerManager playerManager) => playerManager.field_Private_List_1_Player_0;
        internal static Il2CppSystem.Collections.Generic.List<Player> GetPlayers() => PlayerManager().field_Private_List_1_Player_0;
        internal static ApiAvatar GetApiAvatar(this VRCPlayer player) => player.field_Private_ApiAvatar_1;

        internal static Player GetPlayer(string userId)
        {
            foreach (var player in PlayerManager().GetPlayers())
            {
                if (player == null)
                    continue;

                var apiUser = player.GetAPIUser();
                if (apiUser == null)
                    continue;

                if (apiUser.id == userId)
                    return player;
            }

            return null;
        }

        internal static APIUser GetAPIUser(this Player player) => player.field_Private_APIUser_0;
        internal static APIUser GetAPIUser(this Player player, byte a) => player.field_Private_APIUser_0;
        internal static ApiAvatar GetApiAvatar(this Player player) => player.prop_ApiAvatar_0;
        internal static IUser GetSelectedUser()
        {
            var go = GameObject.Find(SelectedUserPath);
            if(go == null) return null;

            var selectedUserQm = go.GetComponent<SelectedUserMenuQM>();
            if (selectedUserQm == null) return null;

            var user = selectedUserQm.field_Private_IUser_0;

            return user;
        }
    }
}
