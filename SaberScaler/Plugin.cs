using IPA;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;
using HarmonyLib;
using IPA.Config.Stores;
using SaberScaler.UI;

namespace SaberScaler
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static Harmony harmony;

        [Init]
        public void Init(IPA.Config.Config conf, IPALogger logger)
        {
            Instance = this;
            Log = logger;
            Config.Instance = conf.Generated<Config>();
            harmony = new Harmony("Nuggo.BeatSaber.SaberScaler");
        }

        [OnEnable]
        public void OnApplicationStart()
        {
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            BsmlWrapper.EnableUI();
        }

        [OnExit]
        public void OnApplicationQuit()
        {

            BsmlWrapper.DisableUI();
        }
    }
}
