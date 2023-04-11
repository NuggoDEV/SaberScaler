using HarmonyLib;
using System;
using UnityEngine;
using Random = System.Random;

namespace SaberScaler.HarmonyPatches
{
    [HarmonyPatch(typeof(SaberModelController), nameof(SaberModelController.Init))]
    public class SaberModelControllerPatch
    {
        public static void Postfix(SaberModelController __instance)
        {
            if (!Config.Instance.ModToggle) return;

            SaberStuff saberStuff = new SaberStuff();
            Vector3 scale = saberStuff.SaberScale();

            __instance.transform.localScale = scale;
        }
    }

    class SaberStuff
    {
        Config config = Config.Instance;
        public Vector3 SaberScale()
        {
            float thickness;
            float length;

            if (DateTime.Now.Month == 4 && DateTime.Now.Day == 11 && config.Surprise)
            {
                Random random = new Random();
                float minValue = 0.1f, maxValue = 10.0f;
                float randomValue1 = (float)(random.NextDouble() * (maxValue - minValue) + minValue);
                float roundedValue1 = (float)Math.Round(randomValue1 / 0.1f) * 0.1f;
                float randomValue2 = (float)(random.NextDouble() * (maxValue - minValue) + minValue);
                float roundedValue2 = (float)Math.Round(randomValue2 / 0.1f) * 0.1f;

                length = roundedValue1;
                thickness = roundedValue2;
            }
            else
            {
                thickness = config.SaberWidth;
                length = config.SaberLength;
            }

            Vector3 vector3 = new Vector3(thickness, thickness, length);
            Plugin.Log.Info($"Vector3: {vector3}");
            return vector3;
        }
    }
}
