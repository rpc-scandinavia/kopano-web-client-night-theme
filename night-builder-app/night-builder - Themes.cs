using System;
using System.Collections.Generic;

namespace RpcScandinavia.Tool {

	#region Themes
	//------------------------------------------------------------------------------------------------------------------
	// Themes
	//------------------------------------------------------------------------------------------------------------------
	public static class Themes {
		public static Theme NightBlue { get; internal set; }
		public static SvgMap MapBlue { get; internal set; }
		public static Theme NightGreen { get; internal set; }
		public static SvgMap MapGreen { get; internal set; }

		#region Constructors
		//--------------------------------------------------------------------------------------------------------------
		// Constructors
		//--------------------------------------------------------------------------------------------------------------
		static Themes() {
			// Initialize the themes.
			Themes.InitializeNightBlue();
			Themes.InitializeNightGreen();
		} // Themes
		#endregion

		#region NightBlue
		//--------------------------------------------------------------------------------------------------------------
		// NightBlue
		// Define the night blue theme colours.
		//--------------------------------------------------------------------------------------------------------------
		private static void InitializeNightBlue() {
			Themes.NightBlue = new Theme();
			Themes.NightBlue.BackPage = "#000d1a";
			Themes.NightBlue.BackDragged = "#506a94";
			Themes.NightBlue.BackMask = "#2f2f2f";
			Themes.NightBlue.BackMenubar = "#1f4b61";
			Themes.NightBlue.TextMenubar = "#e6f2ff";
			Themes.NightBlue.BackNavigationPanel = "#0d1a26";
			Themes.NightBlue.BackContentPanel = "#0d1a26";
			Themes.NightBlue.BackWidgetPanel = "#0d1a26";
			Themes.NightBlue.BackList = "#132639";
			Themes.NightBlue.BackNormal = "#132639";
			Themes.NightBlue.BackHover = "#204060";
			Themes.NightBlue.BackClick = "#6699cc";
			Themes.NightBlue.BackSelected = "#3b5264";
			Themes.NightBlue.BackHeader = "#29374b";
			Themes.NightBlue.BackExtra = "#664d00";
			Themes.NightBlue.BackInput = "#1c2d30";
			Themes.NightBlue.Back1 = "#1c2030";
			Themes.NightBlue.BackEditorBlue = "#00233b";
			Themes.NightBlue.BackEditorGreen = "#113a00";
			Themes.NightBlue.BackEditorYellow = "#4e4d00";
			Themes.NightBlue.BackEditorWhite = "#5c5c5c";
			Themes.NightBlue.BackEditorPink = "#684363";
			Themes.NightBlue.Line = "#004080";
			Themes.NightBlue.Border = "#004080";
			Themes.NightBlue.Icon = "#006699";
			Themes.NightBlue.Icon2 = "#b3b300";
			Themes.NightBlue.IconBright = "#99ddff";
			Themes.NightBlue.Text9 = "#f0f0f0";
			Themes.NightBlue.Text6 = "#b4b4e0";
			Themes.NightBlue.Text5 = "#808080";
			Themes.NightBlue.TextRed = "#ff0000";
			Themes.NightBlue.TextYellow = "#ffff00";
			Themes.NightBlue.TextGold = "#ffd700";
			Themes.NightBlue.Transparent = "unset";
			Themes.NightBlue.IconMdmSettings = 195;

			Themes.MapBlue = new SvgMap(Themes.NightBlue.Icon);
			Themes.MapBlue.AddEntry(".icon_settings", Themes.NightBlue.IconBright);
			Themes.MapBlue.AddEntry(".icon_logout", Themes.NightBlue.IconBright);
			Themes.MapBlue.AddEntry(".icon_cross", Themes.NightBlue.IconBright);
			Themes.MapBlue.AddEntry(".icon_folder_default_inbox", Themes.NightBlue.Icon2, Themes.NightBlue.Icon);
			Themes.MapBlue.AddEntry(".icon_folder_default_drafts", Themes.NightBlue.Icon2, Themes.NightBlue.Icon);
			Themes.MapBlue.AddEntry(".icon_folder_default_outbox", Themes.NightBlue.Icon2, Themes.NightBlue.Icon);
			Themes.MapBlue.AddEntry(".icon_folder_default_sent", Themes.NightBlue.Icon2, Themes.NightBlue.Icon);
			Themes.MapBlue.AddEntry(".icon_folder_contact", Themes.NightBlue.Icon2, Themes.NightBlue.Icon);
			Themes.MapBlue.AddEntry(".icon_folder_task", Themes.NightBlue.Icon2, Themes.NightBlue.Icon);
			Themes.MapBlue.AddEntry(".icon_folder_default_journal", Themes.NightBlue.Icon2, Themes.NightBlue.Icon);
			Themes.MapBlue.AddEntry(".icon_folder_rss", Themes.NightBlue.Icon2, Themes.NightBlue.Icon);
			Themes.MapBlue.AddEntry(".icon_contact_card", Themes.NightBlue.Icon, Themes.NightBlue.Icon2);
			Themes.MapBlue.AddEntry(".icon_checked", Themes.NightBlue.Icon2, Themes.NightBlue.Icon);
			Themes.MapBlue.AddEntry(".icon_task_complete", Themes.NightBlue.Icon2, Themes.NightBlue.Icon);
			Themes.MapBlue.AddEntry(".icon_indicator_calendar", Themes.NightBlue.TextGold);
			Themes.MapBlue.AddEntry(".icon_note_blue_large", Themes.NightBlue.Icon2, "#19b5f1");
			Themes.MapBlue.AddEntry(".icon_note_green_large", Themes.NightBlue.Icon2, "#81cc2b");
			Themes.MapBlue.AddEntry(".icon_note_pink_large", Themes.NightBlue.Icon2, "#9c1662");
			Themes.MapBlue.AddEntry(".icon_note_white_large", "#6d6d70", "#cac8c8");
			Themes.MapBlue.AddEntry(".icon_note_yellow_large", Themes.NightBlue.Icon2, "#fcc907");
		} // InitializeNightBlue
		#endregion

		#region NightGreen
		//--------------------------------------------------------------------------------------------------------------
		// NightGreen
		// Define the night green theme colours.
		//--------------------------------------------------------------------------------------------------------------
		private static void InitializeNightGreen() {
			Themes.NightGreen = new Theme();
			Themes.NightGreen.BackPage ="#06130d";
			Themes.NightGreen.BackDragged ="#3d3d29";
			Themes.NightGreen.BackMask ="#2f2f2f";
			Themes.NightGreen.BackMenubar ="#223300";
			Themes.NightGreen.TextMenubar ="#e6f2ff";
			Themes.NightGreen.BackNavigationPanel ="#111a00";
			Themes.NightGreen.BackContentPanel ="#111a00";
			Themes.NightGreen.BackWidgetPanel ="#111a00";
			Themes.NightGreen.BackList ="#0d1a00";
			Themes.NightGreen.BackNormal ="#0d1a00";
			Themes.NightGreen.BackHover ="#133926";
			Themes.NightGreen.BackClick ="#26734d";
			Themes.NightGreen.BackSelected ="#0d261a";
			Themes.NightGreen.BackHeader ="#133926";
			Themes.NightGreen.BackExtra ="#664d00";
			Themes.NightGreen.BackInput ="#223300";
			Themes.NightGreen.Back1 ="#223300";
			Themes.NightGreen.BackEditorBlue ="#00233b";
			Themes.NightGreen.BackEditorGreen ="#113a00";
			Themes.NightGreen.BackEditorYellow ="#4e4d00";
			Themes.NightGreen.BackEditorWhite ="#5c5c5c";
			Themes.NightGreen.BackEditorPink ="#684363";
			Themes.NightGreen.Line ="#145214";
			Themes.NightGreen.Border ="#145214";
			Themes.NightGreen.Icon ="#1f7a1f";
			Themes.NightGreen.Icon2 = "white";
			Themes.NightGreen.IconBright ="#adebad";
			Themes.NightGreen.Text9 ="#f0f0f0";
			Themes.NightGreen.Text6 ="#b4b4e0";
			Themes.NightGreen.Text5 ="#808080";
			Themes.NightGreen.TextRed ="#ff0000";
			Themes.NightGreen.TextYellow ="#ffff00";
			Themes.NightGreen.TextGold ="#ffd700";
			Themes.NightGreen.Transparent ="transparent";
			Themes.NightGreen.IconMdmSettings = 140;

			Themes.MapGreen = new SvgMap(Themes.NightGreen.Icon);
			Themes.MapGreen.AddEntry(".icon_settings", Themes.NightGreen.IconBright);
			Themes.MapGreen.AddEntry(".icon_logout", Themes.NightGreen.IconBright);
			Themes.MapGreen.AddEntry(".icon_cross", Themes.NightGreen.IconBright);
			Themes.MapGreen.AddEntry(".icon_folder_default_inbox", Themes.NightGreen.Icon2, Themes.NightGreen.Icon);
			Themes.MapGreen.AddEntry(".icon_folder_default_drafts", Themes.NightGreen.Icon2, Themes.NightGreen.Icon);
			Themes.MapGreen.AddEntry(".icon_folder_default_outbox", Themes.NightGreen.Icon2, Themes.NightGreen.Icon);
			Themes.MapGreen.AddEntry(".icon_folder_default_sent", Themes.NightGreen.Icon2, Themes.NightGreen.Icon);
			Themes.MapGreen.AddEntry(".icon_folder_contact", Themes.NightGreen.Icon2, Themes.NightGreen.Icon);
			Themes.MapGreen.AddEntry(".icon_folder_task", Themes.NightGreen.Icon2, Themes.NightGreen.Icon);
			Themes.MapGreen.AddEntry(".icon_folder_default_journal", Themes.NightGreen.Icon2, Themes.NightGreen.Icon);
			Themes.MapGreen.AddEntry(".icon_folder_rss", Themes.NightGreen.Icon2, Themes.NightGreen.Icon);
			Themes.MapGreen.AddEntry(".icon_contact_card", Themes.NightGreen.Icon2, Themes.NightGreen.Icon);
			Themes.MapGreen.AddEntry(".icon_checked", Themes.NightGreen.Icon2, Themes.NightGreen.Icon);
			Themes.MapGreen.AddEntry(".icon_task_complete", Themes.NightGreen.Icon2, Themes.NightGreen.Icon);
			Themes.MapGreen.AddEntry(".icon_indicator_calendar", Themes.NightGreen.TextGold);
			Themes.MapGreen.AddEntry(".icon_note_blue_large", Themes.NightGreen.Icon2, "#19b5f1");
			Themes.MapGreen.AddEntry(".icon_note_green_large", Themes.NightGreen.Icon2, "#81cc2b");
			Themes.MapGreen.AddEntry(".icon_note_pink_large", Themes.NightGreen.Icon2, "#9c1662");
			Themes.MapGreen.AddEntry(".icon_note_white_large", "#6d6d70", "#cac8c8");
			Themes.MapGreen.AddEntry(".icon_note_yellow_large", Themes.NightGreen.Icon2, "#fcc907");
		} // InitializeNightGreen
		#endregion

	} // Themes
	#endregion

} // RpcScandinavia.Tool
