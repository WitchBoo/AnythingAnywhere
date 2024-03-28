﻿using StardewModdingAPI;
using System;

namespace AnythingAnywhere.ModInteractions
{
    internal class GenericModConfigMenu(IModRegistry modRegistry, IManifest manifest, IMonitor monitor, Func<ModConfig> config, Action reset, Action save)
    {
        public void Register()
        {
            IGenericModConfigMenuApi configMenu = modRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");

            if (configMenu is null)
                return;

            configMenu.Register(mod: manifest, reset: reset, save: save);


            configMenu.AddSectionTitle(
                mod: manifest,
                text: I18n.Config_AnythingAnywhere_Title
            );

            configMenu.AddBoolOption(
                mod: manifest,
                name: I18n.Config_AnythingAnywhere_GroundEnabled_Name,
                tooltip: I18n.Config_AnythingAnywhere_GroundEnabled_Description,
                getValue: () => config().AllowAllGroundFurniture,
                setValue: value => config().AllowAllGroundFurniture = value
            );

            configMenu.AddBoolOption(
                mod: manifest,
                name: I18n.Config_AnythingAnywhere_WallEnabled_Name,
                tooltip: I18n.Config_AnythingAnywhere_WallEnabled_Description,
                getValue: () => config().AllowAllWallFurniture,
                setValue: value => config().AllowAllWallFurniture = value
            );

            configMenu.AddBoolOption(
                mod: manifest,
                name: I18n.Config_AnythingAnywhere_MiniObiliskAnywhere_Name,
                tooltip: I18n.Config_AnythingAnywhere_MiniObiliskAnywhere_Description,
                getValue: () => config().AllowMiniObelisksAnywhere,
                setValue: value => config().AllowMiniObelisksAnywhere = value
            );

            configMenu.AddBoolOption(
                mod: manifest,
                name: I18n.Config_AnythingAnywhere_UseJukeboxAnywhere_Name,
                tooltip: I18n.Config_AnythingAnywhere_UseJukeboxAnywhere_Description,
                getValue: () => config().EnableJukeboxFunctionality,
                setValue: value => config().EnableJukeboxFunctionality = value
            );

            configMenu.AddBoolOption(
                mod: manifest,
                name: I18n.Config_AnythingAnywhere_FarmWallEnabled_Name,
                tooltip: I18n.Config_AnythingAnywhere_FarmWallEnabled_Description,
                getValue: () => config().AllowAllWallFurnitureFarmHouse,
                setValue: value => config().AllowAllWallFurnitureFarmHouse = value
            );

        }
    }
}
