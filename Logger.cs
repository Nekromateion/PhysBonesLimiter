using System;

namespace PhysBonesLimiter
{
    internal class Logger
    {
        private static MelonLoader.MelonLogger.Instance _instance = new MelonLoader.MelonLogger.Instance(BuildInfo.ModName, ConsoleColor.Magenta);

        internal static void Log(string message) => _instance.Msg(message);
        internal static void Warn(string message) => _instance.Warning(message);
        internal static void Err(string message) => _instance.Error(message);
    }
}
