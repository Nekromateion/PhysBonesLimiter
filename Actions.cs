using Newtonsoft.Json;
using System;
using System.IO;
using VRC.Core;

namespace PhysBonesLimiter
{
    internal class Actions
    {
        internal static void WhiteListOtherAvatar()
        {
            try
            {
                var user = Extensions.GetSelectedUser();
                if (user == null) return;

                WhitelistAvatar(user.GetUserId());
            }
            catch (Exception ex)
            {
                Logger.Err("Error while whitelisting an avatar:\n" + ex);
            }
        }

        internal static void BlackListOtherAvatar()
        {
            try
            {
                var user = Extensions.GetSelectedUser();
                if (user == null) return;

                BlacklistAvatar(user.GetUserId());
            }
            catch (Exception ex)
            {
                Logger.Err("Error while blacklisting an avatar:\n" + ex);
            }
        }

        internal static void WhiteListOtherUser()
        {
            try
            {
                var user = Extensions.GetSelectedUser();
                if (user == null) return;

                WhitelistUser(user.GetUserId());
            }
            catch (Exception ex)
            {
                Logger.Err("Error while whitelisting a user:\n" + ex);
            }
        }

        internal static void BlackListOtherUser()
        {
            try
            {
                var user = Extensions.GetSelectedUser();
                if (user == null) return;

                BlacklistUser(user.GetUserId());
            }
            catch (Exception ex)
            {
                Logger.Err("Error while blacklisting a user:\n" + ex);
            }
        }

        internal static void WhiteListSelfAvatar()
        {
            try
            {
                WhitelistAvatar(APIUser.CurrentUser.id);
            }
            catch (Exception ex)
            {
                Logger.Err("Error while whitelisting own(local player) avatar:\n" + ex);
            }
        }

        internal static void BlackListSelfAvatar()
        {
            try
            {
                BlacklistAvatar(APIUser.CurrentUser.id);
            }
            catch (Exception ex)
            {
                Logger.Err("Error while blacklisting own(local player) avatar:\n" + ex);
            }
        }

        internal static void WhiteListSelfUser()
        {
            try
            {
                WhitelistUser(APIUser.CurrentUser.id);
            }
            catch (Exception ex)
            {
                Logger.Err("Error while whitelisting self(local player):\n" + ex);
            }
        }

        internal static void BlackListSelfUser()
        {
            try
            {
                BlacklistUser(APIUser.CurrentUser.id);
            }
            catch (Exception ex)
            {
                Logger.Err("Error while blacklisting self(local player):\n" + ex);
            }
        }

        internal static void SaveLists()
        {
            File.WriteAllText(Main.ListsJsonPath, JsonConvert.SerializeObject(Lists.Instance));
        }

        private static void BlacklistAvatar(string userId)
        {
            var player = Extensions.GetPlayer(userId);
            if (player == null) return;

            var apiAvatar = player.GetApiAvatar();
            if (apiAvatar == null) return;

            var avatarId = apiAvatar.id;
            if (Lists.Instance.BlackList.Contains(avatarId))
            {
                Lists.Instance.BlackList.Remove(avatarId);
                Logger.Log($"Unblacklisted {avatarId}");
            }
            else
            {
                Lists.Instance.BlackList.Add(avatarId);
                Logger.Log($"Blacklisted {avatarId}");
            }

            SaveLists();
        }

        private static void WhitelistAvatar(string userId)
        {
            var player = Extensions.GetPlayer(userId);
            if (player == null) return;

            var apiAvatar = player.GetApiAvatar();
            if (apiAvatar == null) return;

            var avatarId = apiAvatar.id;
            if (Lists.Instance.WhiteList.Contains(avatarId))
            {
                Lists.Instance.WhiteList.Remove(avatarId);
                Logger.Log($"Unwhitelisted {avatarId}");
            }
            else
            {
                Lists.Instance.WhiteList.Add(avatarId);
                Logger.Log($"Whitelisted {avatarId}");
            }

            SaveLists();
        }

        private static void BlacklistUser(string userId)
        {
            if (Lists.Instance.UserBlackList.Contains(userId))
            {
                Lists.Instance.UserBlackList.Remove(userId);
                Logger.Log($"Unblacklisted {userId}");
            }
            else
            {
                Lists.Instance.UserBlackList.Add(userId);
                Logger.Log($"Blacklisted {userId}");
            }

            SaveLists();
        }

        private static void WhitelistUser(string userId)
        {
            if (Lists.Instance.UserWhiteList.Contains(userId))
            {
                Lists.Instance.UserWhiteList.Remove(userId);
                Logger.Log($"Unwhitelisted {userId}");
            }
            else
            {
                Lists.Instance.UserWhiteList.Add(userId);
                Logger.Log($"Whitelisted {userId}");
            }

            SaveLists();
        }
    }
}
