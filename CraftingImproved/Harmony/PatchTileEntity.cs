using HarmonyLib;
using System.Reflection;
using UnityEngine;
using DMT;

[HarmonyPatch(typeof(TileEntity))]
[HarmonyPatch("Instantiate")]
public class mirashii_BackpackWindow : IHarmony {

    public void Start() {
       Debug.Log("Loading Patch: " + GetType().ToString());
       var harmony = new Harmony(GetType().ToString());
       harmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    static bool Prefix(ref TileEntity __result, TileEntityType type, Chunk _chunk) {
        if ((int)type == 0x17) {
            __result = new ImprovedTileEntitySecureLootContainer(_chunk);
            return false;
        }
        return true;
    }
}