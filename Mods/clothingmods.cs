using MelonLoader;
using UnityEngine;
using System.Collections;
using Il2CppInterop.Runtime;
using Il2CppScheduleOne.UI.Shop;
using Il2CppScheduleOne.AvatarFramework;
using HarmonyLib;
using Il2CppScheduleOne.AvatarFramework.Animation;
using static MelonLoader.MelonLogger;
using Il2CppScheduleOne.Networking;
using Il2CppScheduleOne.PlayerScripts;

[assembly: MelonInfo(typeof(HatMod), "Clothing", "1.0.0", "ResiK")]
[assembly: MelonGame("TVGS", "Schedule I")]

public class HatMod : MelonMod
{
    private bool isInitialized = false;
    private GameObject hat;
    private Quaternion desiredRotation = Quaternion.Euler(0, 90, 0);
    
    public override void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        if (sceneName == "Main")
        {
            MelonLogger.Msg($"Scene Loaded: {sceneName}");
            isInitialized = true;

            MelonLogger.Msg($"{Player.Local.PlayerName} ({Lobby.Instance.LocalPlayerID})");
            
            MelonCoroutines.Start(FindListings());
        }

    }

    private IEnumerator FindListings()
    {
        yield return new WaitForSeconds(2f);

        var accessories = UnityEngine.Object.FindObjectsOfType(Il2CppType.Of<Accessory>());
        foreach (var accessoryObj in accessories)
        {
            var accessory = accessoryObj.TryCast<Accessory>();
            if (accessory != null)
            {
                MelonLogger.Msg($"Accessory found: {accessory.Name}");
            }
        }

    }

    public override void OnLateUpdate()
    {
        if (!isInitialized) return;
        
        if (hat == null)
        {
            var player = GameObject.Find($"{Player.Local.PlayerName} ({Lobby.Instance.LocalPlayerID})");
            if (player == null) return;

            var bodyContainer = player.transform.Find("Avatar/BodyContainer");
            if (bodyContainer == null) return;

            var hatObj = bodyContainer.Find("Hat(Clone)");
            if (hatObj == null) return;

            hat = hatObj.gameObject;
            MelonLogger.Msg("🎩 Hat found.");
        }

        if (hat != null)
        {
            hat.transform.localRotation = desiredRotation;
        }
    }
}




