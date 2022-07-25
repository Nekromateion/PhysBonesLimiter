using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PhysBonesLimiter
{
    internal class Lists
    {
        public static Lists Instance;

        public HashSet<string> WhiteList = new HashSet<string>();
        public HashSet<string> BlackList = new HashSet<string>();

        public HashSet<string> UserWhiteList = new HashSet<string>();
        public HashSet<string> UserBlackList = new HashSet<string>();
    }

    internal class AvatarScanner
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ScanAvatar(GameObject go, string avatarId, string userId)
        {
            if (go == null) return;

            if (Lists.Instance.UserWhiteList.Contains(userId)) return;

            if (Lists.Instance.UserBlackList.Contains(userId))
            {
                var physBones = go.GetComponentsInChildren<VRC.SDK3.Dynamics.PhysBone.Components.VRCPhysBone>();
                var physBoneColliders = go.GetComponentsInChildren<VRC.SDK3.Dynamics.PhysBone.Components.VRCPhysBoneCollider>();

                CleanPhysBones(physBones);
                CleanPhysBoneColliders(physBoneColliders);
            }

            ScanAvatar(go, avatarId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ScanAvatar(GameObject go, string avatarId)
        {
            if (go == null) return;

            if (Lists.Instance.WhiteList.Contains(avatarId)) return;

            var physBones = go.GetComponentsInChildren<VRC.SDK3.Dynamics.PhysBone.Components.VRCPhysBone>();
            var physBoneColliders = go.GetComponentsInChildren<VRC.SDK3.Dynamics.PhysBone.Components.VRCPhysBoneCollider>();

            if (Lists.Instance.BlackList.Contains(avatarId))
            {
                CleanPhysBones(physBones);
                CleanPhysBoneColliders(physBoneColliders);
                return;
            }

            if (physBones != null && physBones.Count > MelonPrefrences.MaxPhysBoneComponents)
            {
                CleanPhysBones(physBones);
            }
            if (physBoneColliders != null && physBoneColliders.Count > MelonPrefrences.MaxPhysBoneColliderComponents)
            {
                CleanPhysBoneColliders(physBoneColliders);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CleanPhysBones(UnhollowerBaseLib.Il2CppArrayBase<VRC.SDK3.Dynamics.PhysBone.Components.VRCPhysBone> physBones)
        {
            if (physBones == null) return;
            foreach(var physBone in physBones)
            {
                GameObject.Destroy(physBone);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CleanPhysBoneColliders(UnhollowerBaseLib.Il2CppArrayBase<VRC.SDK3.Dynamics.PhysBone.Components.VRCPhysBoneCollider> physBoneColliders)
        {
            if (physBoneColliders == null) return;
            foreach (var physBoneCollider in physBoneColliders)
            {
                GameObject.Destroy(physBoneCollider);
            }
        }
    }
}
