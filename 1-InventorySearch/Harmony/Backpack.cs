using HarmonyLib;
using System.Reflection;
using UnityEngine;
using DMT;

[HarmonyPatch(typeof(XUiC_ItemStack))]
[HarmonyPatch("Update")]
public class mirashii_BackpackWindow : IHarmony
{

   public void Start()
   {
       Debug.Log("Loading Patch: " + GetType().ToString());
       var harmony = new Harmony(GetType().ToString());
       harmony.PatchAll(Assembly.GetExecutingAssembly());
   }

   static bool Prefix()
   {
       

        return true;
   }
}