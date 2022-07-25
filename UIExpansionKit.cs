using PhysBoneLimiter;

namespace PhysBonesLimiter
{
    internal class UIExpansionKit
    {
        internal static void RegisterButtons()
        {
            #region OtherActions

            #region OtherAvatars
            Extensions.CreateUixButton(1, Data.ButtonNames.UnWhiteList, Actions.WhiteListOtherAvatar);

            Extensions.CreateUixButton(1, Data.ButtonNames.UnBlackList, Actions.BlackListOtherAvatar);
            #endregion OtherAvatars

            #region OtherUsers
            Extensions.CreateUixButton(1, Data.ButtonNames.UnWhiteListUser, Actions.WhiteListOtherUser);

            Extensions.CreateUixButton(1, Data.ButtonNames.UnBlackListUser, Actions.BlackListOtherUser);
            #endregion OtherUsers

            #endregion OtherActions

            #region SelfActions

            #region SelfAvatar
            Extensions.CreateUixButton(0, Data.ButtonNames.UnWhiteListSelf, Actions.WhiteListSelfAvatar);

            Extensions.CreateUixButton(0, Data.ButtonNames.UnBlackListSelf, Actions.BlackListSelfAvatar);
            #endregion SelfAvatar

            #region SelfUser
            Extensions.CreateUixButton(0, Data.ButtonNames.UnWhiteListSelfUser, Actions.WhiteListSelfUser);

            Extensions.CreateUixButton(0, Data.ButtonNames.UnBlackListSelfUser, Actions.BlackListSelfUser);
            #endregion SelfUser

            #endregion SelfActions
        }
    }
}
