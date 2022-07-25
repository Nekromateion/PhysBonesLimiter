namespace PhysBonesLimiter
{
    internal class MelonPrefrences
    {
        #region PrivateFields

        private static readonly MelonLoader.MelonPreferences_Category _cat = MelonLoader.MelonPreferences.CreateCategory("PhysBonesLimiter", "PhysBones Limiter");

        #region MelonPreferences

        private static readonly MelonLoader.MelonPreferences_Entry<int> _maxPhysBoneComponents = _cat.CreateEntry("maxPhysBonesComponents", 24, "Max PhysBone Components");
        private static readonly MelonLoader.MelonPreferences_Entry<int> _maxPhysBoneColliderComponents = _cat.CreateEntry("maxPhysBonesColliderComponents", 8, "Max PhysBoneCollider Components");
        
        
        private static readonly MelonLoader.MelonPreferences_Entry<bool> _reloadAvatarsOnValueChange = _cat.CreateEntry("reloadAvatarsOnValueChange", true, "Reload all avatars when limits changed");
        private static readonly MelonLoader.MelonPreferences_Entry<bool> _excludeSelf = _cat.CreateEntry("excludeSelf", false, "Exclude self from checks");
        private static readonly MelonLoader.MelonPreferences_Entry<bool> _modEnabled = _cat.CreateEntry("modEnabled", true, "Enabled");

        #endregion MelonPreferences

        #endregion PrivateFields

        #region PublicFields

        internal static int MaxPhysBoneComponents = 24;
        internal static int MaxPhysBoneColliderComponents = 8;

        internal static bool ReloadAvatarsOnValueChange = true;
        internal static bool ExcludeSelf = false;
        internal static bool ModEnabled = false;

        #endregion PublicFields

        internal static void Init()
        {
            MaxPhysBoneComponents = _maxPhysBoneComponents.Value;
            _maxPhysBoneComponents.OnValueChanged += MaxPhysBoneComponents_OnValueChanged;
            MaxPhysBoneColliderComponents = _maxPhysBoneColliderComponents.Value;
            _maxPhysBoneColliderComponents.OnValueChanged += MaxPhysBoneColliderComponents_OnValueChanged;
            ReloadAvatarsOnValueChange = _reloadAvatarsOnValueChange.Value;
            _reloadAvatarsOnValueChange.OnValueChanged += ReloadAvatarsOnValueChange_OnValueChanged;
            ExcludeSelf = _excludeSelf.Value;
            _excludeSelf.OnValueChanged += ExcludeSelf_OnValueChanged;
            ModEnabled = _modEnabled.Value;
            _modEnabled.OnValueChanged += ModEnabled_OnValueChanged;
        }

        private static void ModEnabled_OnValueChanged(bool oldValue, bool newValue)
        {
            ModEnabled = newValue;
            _modEnabled.Save();
        }

        private static void ExcludeSelf_OnValueChanged(bool oldValue, bool newValue)
        {
            ExcludeSelf = newValue;
            _excludeSelf.Save();
        }

        private static void ReloadAvatarsOnValueChange_OnValueChanged(bool oldValue, bool newValue)
        {
            ReloadAvatarsOnValueChange = newValue;
            _reloadAvatarsOnValueChange.Save();
        }

        private static void MaxPhysBoneColliderComponents_OnValueChanged(int oldValue, int newValue)
        {
            MaxPhysBoneColliderComponents = newValue;
            _maxPhysBoneColliderComponents.Save();
            if(ReloadAvatarsOnValueChange) Extensions.ReloadAllAvatars();
        }

        private static void MaxPhysBoneComponents_OnValueChanged(int oldValue, int newValue)
        {
            MaxPhysBoneComponents = newValue;
            _maxPhysBoneComponents.Save();
            if (ReloadAvatarsOnValueChange) Extensions.ReloadAllAvatars();
        }
    }
}
