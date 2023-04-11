using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace SaberScaler
{
    internal class Config
    {
        public static Config Instance { get; set; }
        public bool ModToggle { get; set; } = true;
        public bool Surprise {  get; set; } = true;

        public float SaberLength { get; set; } = 1;
        public float SaberWidth { get; set; } = 1;

    }
}
