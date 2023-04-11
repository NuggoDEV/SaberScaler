using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.MenuButtons;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Components.Settings;
using HMUI;

namespace SaberScaler.UI
{
    class SSFlowCoordinator : FlowCoordinator
    {
        SSViewController view = null;

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (!firstActivation) return;

            SetTitle("Saber Scaler");
            showBackButton = true;

            if (view == null)
                view = BeatSaberUI.CreateViewController<SSViewController>();

            ProvideInitialViewControllers(view);
        }
        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Horizontal);
        }

        public void ShowFlow()
        {
            var _parentFlow = BeatSaberUI.MainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf();
            BeatSaberUI.PresentFlowCoordinator(_parentFlow, this);
        }

        static SSFlowCoordinator flow = null;
        static MenuButton menuButton;

        public static void Initialise()
        {
            MenuButtons.instance.RegisterButton(menuButton ??= new MenuButton("Saber Scaler", "", () =>
            {
                if (flow == null)
                    flow = BeatSaberUI.CreateFlowCoordinator<SSFlowCoordinator>();
                flow.ShowFlow();
            }, true));
        }

        public static void Deinit()
        {
            if (menuButton != null)
                MenuButtons.instance.UnregisterButton(menuButton);
        }
    }

    [HotReload(RelativePathToLayout = @"./settings.bsml")]
    [ViewDefinition("ThinSaber.UI.settings.bsml")]
    class SSViewController : BSMLAutomaticViewController
    {
        Config config = Config.Instance;

        [UIComponent("SaberLengthID")] private SliderSetting lengthSlider;

        [UIComponent("SaberWidthID")] private SliderSetting widthSlider;

        [UIValue("ModToggle")]
        private bool ModToggle
        {
            get => config.ModToggle;
            set => config.ModToggle = value;
        }

        [UIValue("SaberLengthValue")]
        private float SaberLength
        {
            get => config.SaberLength;
            set => config.SaberLength = value;
        }

        [UIValue("SaberWidthValue")]
        private float SaberWidth
        {
            get => config.SaberWidth;
            set => config.SaberWidth = value;
        }

        [UIAction("ResetLength")]
        private void ResetLength()
        {
            Plugin.Log.Info("Resetting length");
            config.SaberLength = 1;
            lengthSlider.Value = config.SaberLength;
        }

        [UIAction("ResetWidth")]
        private void ResetWidth()
        {
            Plugin.Log.Info("Resetting width");
            config.SaberWidth = 1;
            widthSlider.Value = config.SaberWidth;
        }
    }

    public static class BsmlWrapper
    {
        public static void EnableUI()
        {
            SSFlowCoordinator.Initialise();
        }
        public static void DisableUI()
        {
            SSFlowCoordinator.Deinit();
        }
    }
}
