﻿using HarmonyLib;
using StardewValley;
using StardewModdingAPI;
using StardewValley.Objects;
using StardewValley.Locations;
using Microsoft.Xna.Framework;
using System;
using StardewValley.Menus;

namespace AnythingAnywhere.Framework.Patches.StandardObjects    
{
    internal class FurniturePatch : PatchTemplate
    {
        private readonly Type _object = typeof(Furniture);

        internal FurniturePatch(IMonitor modMonitor, IModHelper modHelper) : base(modMonitor, modHelper)
        {

        }

        internal void Apply(Harmony harmony)
        {
            harmony.Patch(AccessTools.Method(_object, nameof(Furniture.GetAdditionalFurniturePlacementStatus), new[] { typeof(GameLocation), typeof(int), typeof(int), typeof(Farmer) }), postfix: new HarmonyMethod(GetType(), nameof(GetAdditionalFurniturePlacementStatusPostfix)));
            harmony.Patch(AccessTools.Method(_object, nameof(Furniture.canBePlacedHere), new[] { typeof(GameLocation), typeof(Vector2), typeof(CollisionMask), typeof(bool) }), postfix: new HarmonyMethod(GetType(), nameof(CanBePlacedHerePostfix)));
        }

        // Enables disabling wall furniture in all places in decortable locations. It can be annoying indoors.
        private static void GetAdditionalFurniturePlacementStatusPostfix(Furniture __instance, GameLocation location, int x, int y, Farmer who, ref int __result)
        {
            // Check if the furniture is wall furniture
            bool isWallFurniture =
                (__instance.furniture_type.Value == 6 ||
                __instance.furniture_type.Value == 17 ||
                __instance.furniture_type.Value == 13 ||
                __instance.QualifiedItemId == "(F)1293");   

            if (!ModEntry.modConfig.EnableWallFurnitureIndoors && location is DecoratableLocation decoratableLocation && !ModEntry.modConfig.EnableBuildAnywhere)
            {
                if (!isWallFurniture)
                {
                    __result = 0;
                }
                return;
            }
            else
            {
                __result = 0;
            }
        }

        //Enable placing furniture in walls
        private static void CanBePlacedHerePostfix(Furniture __instance, GameLocation l, Vector2 tile, ref bool __result, CollisionMask collisionMask = CollisionMask.All, bool showError = false)
        {
            if (ModEntry.modConfig.EnableFreePlace)
                __result = true;
        }

    }
}
