using System;
using System.Collections.Generic;
using HarmonyLib;
using BepInEx;
using UnityEngine;
using System.Reflection;
using UnityEngine.XR;
using Photon.Pun;
using System.IO;
using System.Net;
using Photon.Realtime;
using UnityEngine.Rendering;

namespace MonkeMod
{
    [BepInPlugin("org.J-JOE.monkeytag.Untag", "Untag", "0.0.0.1")]
    public class MyPatcher : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = new Harmony("com.J-JOE.monkeytag.RightGripNoGrav");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Update", MethodType.Normal)]
    public class Class1
    {
        static bool untag = false;
        static void Postfix(GorillaLocomotion.Player __instance)
        {
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller, list);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out untag);
            if (untag)
            {
                __instance.bodyCollider.attachedRigidbody.detectCollisions = false;

            }
            else
            {
                __instance.bodyCollider.attachedRigidbody.detectCollisions = true;
            }
        }
    }
}
