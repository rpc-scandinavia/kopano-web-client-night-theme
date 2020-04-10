// Build and run this program:
// 		dotnet  build
// 		dotnet  run  SETUP_NIGHT_THEMES_DIRECTORY="<plugins folder>"  SETUP_NIGHT_ICONS_DIRECTORY="<iconsets folder>"
//
// Build this program as one self contained file (this includes the .NET runtime):
// 		dotnet  publish  /p:PublishSingleFile=true  /p:DebugType=None  --self-contained true  --configuration release  --runtime "linux-x64"  --output "../"
//
// Run the self contained file:
// 		night-builder  SETUP_NIGHT_THEMES_DIRECTORY="<plugins folder>"  SETUP_NIGHT_ICONS_DIRECTORY="<iconsets folder>"
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace RpcScandinavia.Tool {

	#region Program
	//------------------------------------------------------------------------------------------------------------------
	// Program
	//------------------------------------------------------------------------------------------------------------------
	public class Program {
		private String SETUP_NIGHT_THEMES_DIRECTORY = "/data/setup/Linux/bincs.files/kopano/webapp-themes/plugins";
		private String SETUP_NIGHT_ICONS_DIRECTORY = "/data/setup/Linux/bincs.files/kopano/webapp-themes/iconsets";

		#region Constructors
		//--------------------------------------------------------------------------------------------------------------
		// Constructors
		//--------------------------------------------------------------------------------------------------------------
		public Program(String[] args) {
			// Get parameters.
			foreach (String argument in args) {
				if (argument.ToUpper().StartsWith("SETUP_NIGHT_THEMES_DIRECTORY=") == true) {
					this.SETUP_NIGHT_THEMES_DIRECTORY = argument.Substring(29);
				}
				if (argument.ToUpper().StartsWith("SETUP_NIGHT_ICONS_DIRECTORY=") == true) {
					this.SETUP_NIGHT_ICONS_DIRECTORY = argument.Substring(28);
				}
			}

			// Convert relative paths to rooted paths.
			this.SETUP_NIGHT_THEMES_DIRECTORY = Path.GetFullPath(this.SETUP_NIGHT_THEMES_DIRECTORY);
			this.SETUP_NIGHT_ICONS_DIRECTORY = Path.GetFullPath(this.SETUP_NIGHT_ICONS_DIRECTORY);

			// Validate.
			if (Directory.Exists(this.SETUP_NIGHT_THEMES_DIRECTORY) == false) {
				throw new Exception($"The SETUP_NIGHT_THEMES_DIRECTORY directory '{this.SETUP_NIGHT_THEMES_DIRECTORY}' does not exist.");
			}
			if (Directory.Exists(this.SETUP_NIGHT_ICONS_DIRECTORY) == false) {
				throw new Exception($"The SETUP_NIGHT_ICONS_DIRECTORY directory '{this.SETUP_NIGHT_ICONS_DIRECTORY}' does not exist.");
			}

			// Build the themes.
			Console.WriteLine($"Building the 'night-blue' theme and associated 'Breeze' and 'Classic' icon sets.");
			this.BuildTheme("night-blue", Themes.NightBlue, Themes.MapBlue);

			Console.WriteLine($"Building the 'night-green' theme and associated 'Breeze' and 'Classic' icon sets.");
			this.BuildTheme("night-green", Themes.NightGreen, Themes.MapGreen);
		} // Program
		#endregion

		#region BuildTheme methods
		//--------------------------------------------------------------------------------------------------------------
		// BuildTheme methods
		//--------------------------------------------------------------------------------------------------------------
		private void BuildTheme(String themeName, Theme theme, SvgMap map) {
			// Get the file names.
			String templateThemeFileName = Path.Combine(this.SETUP_NIGHT_THEMES_DIRECTORY, $"night-template/css/night-template.css");
			String resultThemeFileName = Path.Combine(this.SETUP_NIGHT_THEMES_DIRECTORY, $"{themeName}/css/theme-{themeName}.css");

			String templateBreezeFileName = Path.Combine(this.SETUP_NIGHT_ICONS_DIRECTORY, $"night-breeze-template/breeze-icons.css");
			String resultBreezeFileName = Path.Combine(this.SETUP_NIGHT_ICONS_DIRECTORY, $"{themeName}-breeze/breeze-icons.css");

			String templateClassicFileName = Path.Combine(this.SETUP_NIGHT_ICONS_DIRECTORY, $"night-classic-template/classic-icons.css");
			String resultClassicFileName = Path.Combine(this.SETUP_NIGHT_ICONS_DIRECTORY, $"{themeName}-classic/classic-icons.css");

			// Validate that the file and directory exists.
			if (File.Exists(templateThemeFileName) == false) {
				throw new FileNotFoundException($"The template CSS file '{templateThemeFileName}' does not exist.");
			}
			if (Directory.Exists(Path.GetDirectoryName(resultThemeFileName)) == false) {
				throw new DirectoryNotFoundException($"The theme CSS directory '{Path.GetDirectoryName(resultThemeFileName)}' does not exist.");
			}

			if (File.Exists(templateBreezeFileName) == false) {
				throw new FileNotFoundException($"The Breeze icon CSS file '{templateBreezeFileName}' does not exist.");
			}
			if (Directory.Exists(Path.GetDirectoryName(resultBreezeFileName)) == false) {
				throw new DirectoryNotFoundException($"The Breeze icon CSS directory '{Path.GetDirectoryName(resultBreezeFileName)}' does not exist.");
			}

			if (File.Exists(templateClassicFileName) == false) {
				throw new FileNotFoundException($"The Classic icon CSS file '{templateClassicFileName}' does not exist.");
			}
			if (Directory.Exists(Path.GetDirectoryName(resultClassicFileName)) == false) {
				throw new DirectoryNotFoundException($"The Classic icon CSS directory '{Path.GetDirectoryName(resultClassicFileName)}' does not exist.");
			}

			// Theme: Load the template CSS.
			String css = File.ReadAllText(templateThemeFileName);

			// Theme: Perform the search and replace on all the colours in the CSS.
			css = this.SearchAndReplace(css, theme);

			// Theme: Trim the beginning lines from the CSS.
			css = this.TrimStart(css);

			// Template: Replace the fill colours in the SVG images contained in the CSS.
			css = this.ReplaceFillColors(css, map);

			// Theme: Save the theme CSS.
			File.WriteAllText(resultThemeFileName, css);

			// Breeze: Load the templatte CSS.
			css = File.ReadAllText(templateBreezeFileName);

			// Breeze: Replace the fill colours in the SVG images contained in the CSS.
			css = this.ReplaceFillColors(css, map);

			// Breeze: Save the theme CSS.
			File.WriteAllText(resultBreezeFileName, css);

			// Classic: Load the templatte CSS.
			css = File.ReadAllText(templateClassicFileName);

			// Classic: Replace the fill colours in the SVG images contained in the CSS.
			css = this.ReplaceFillColors(css, map);

			// Classic: Save the theme CSS.
			File.WriteAllText(resultClassicFileName, css);
		} // BuildTheme
		#endregion

		#region CSS manipulation methods
		//--------------------------------------------------------------------------------------------------------------
		// CSS manipulation methods
		//--------------------------------------------------------------------------------------------------------------
		public String SearchAndReplace(String css, Theme theme) {
			// Replace the colours in the CSS file
			// Page
			css = css.Replace("#111120", theme.BackPage);								// Default background colour
			css = css.Replace("#2f2f2f", theme.BackMask);								// Mask colour when showing windows/dialogs

			// Top menubar
			css = css.Replace("#1c2d30", theme.BackMenubar);							// Menubar and normal button background colour
			css = css.Replace("#f0f0f0", theme.TextMenubar);							// Menubar and normal button text colour
			css = css.Replace("#2587c0", theme.BackHover);								// Hovered button background colour
			css = css.Replace("#f0f0f1", theme.Text9);									// Hovered button text colour
			css = css.Replace("#2587c1", theme.BackHover);								// Clicked button background colour
			css = css.Replace("#f0f0f2", theme.Text9);									// Clicked button text colour
			css = css.Replace("#506a70", theme.BackSelected);							// Selected button background colour
			css = css.Replace("#f0f0f3", theme.Text9);									// Selected button text colour

			// Top toolbar
			css = css.Replace("#1c2d31", theme.Back1);									// Toolbar background colour
			css = css.Replace("#506a71", theme.BackSelected);							// Normal button Separator colour
			css = css.Replace("#f0f0f4", theme.BackSelected);							// Selected button Separator colour
			css = css.Replace("#364460", theme.BackNormal);								// Normal button background colour
			css = css.Replace("#f0f0f5", theme.Text9);									// Normal button text colour
			css = css.Replace("#2587c2", theme.BackHover);								// Hovered button background colour
			css = css.Replace("#2587c3", theme.BackHover);								// Clicked button background colour
			css = css.Replace("#506a72", theme.BackSelected);							// Selected button background colour

			// Navigation panel
			css = css.Replace("#111121", theme.BackPage);								// Navigation panel background colour
			css = css.Replace("#364476", theme.BackNavigationPanel);					// Header background colour
			css = css.Replace("#f0f0f6", theme.Text9);									// Header text colour
			css = css.Replace("#102132", theme.BackNavigationPanel);					// Toolbar background colour
			css = css.Replace("#f0f0f7", theme.Text9);									// Toolbar text colour
			css = css.Replace("#102133", theme.BackNavigationPanel);					// Tree background colour
			css = css.Replace("#f0f0f8", theme.Text9);									// Tree node text colour
			css = css.Replace("#ffd700", theme.TextGold);								// Tree node counter text colour
			css = css.Replace("#084e80", theme.Line);									// Line between tree and toolbar/bottombar

			// Main panel
			css = css.Replace("#102137", theme.BackContentPanel);						// Empty background colour
			css = css.Replace("#808080", theme.Text5);									// Empty text colour
			css = css.Replace("#084e93", theme.Line);									// Border colour
			css = css.Replace("#102139", theme.BackContentPanel);						// Toolbar background colour
			css = css.Replace("#f0f0fd", theme.Text9);									// Toolbar text colour
			css = css.Replace("#084e84", theme.Line);									// Toolbar bottom border colour
			css = css.Replace("#102140", theme.BackContentPanel);						// Header background colour
			css = css.Replace("#084e85", theme.Line);									// Header bottom border colour
			css = css.Replace("#506a81", theme.BackSelected);							// Bottom status slider background colour
			css = css.Replace("#f0f4ff", theme.Text9);									// Bottom status slider text colour
			css = css.Replace("#111123", theme.BackPage);								// Space between message list and message preview

			// Main panel, tabs
			css = css.Replace("#111122", theme.BackPage);								// Tab panel background colour
			css = css.Replace("#084e76", theme.BackPage);								// Tab panel bottom border colour
			css = css.Replace("#364459", theme.BackNormal);								// Normal tab background colour
			css = css.Replace("#f0f0f9", theme.Text9);									// Normal tab text colour
			css = css.Replace("#506a75", theme.BackSelected);							// Selected tab background colour
			css = css.Replace("#f0f0f9", theme.Text9);									// Selected tab text colour
			css = css.Replace("#2587c7", theme.BackHover);								// Hovered tab background colour
			css = css.Replace("#f0f0fa", theme.Text9);									// Hovered tab text colour

			// Main panel, search
			css = css.Replace("#282c37", theme.Back1);									// Search panel background colour
			css = css.Replace("#f0f5ff", theme.Text9);									// Search panel text colour
			css = css.Replace("#808092", theme.Text5);									// Search panel group text colour

			// Main panel, content
			css = css.Replace("#364461", theme.BackNormal);								// Group header background colour
			css = css.Replace("#808081", theme.Text5);									// Group header text colour
			css = css.Replace("#102138", theme.BackList);								// Normal list item background colour
			css = css.Replace("#084e81", theme.Line);									// Normal list item top/bottom border colour
			css = css.Replace("#f0f0fb", theme.Text9);									// Normal list items first line text colour
			css = css.Replace("#f0f0fc", theme.Text9);									// Normal list items second line text colour
			css = css.Replace("#2587c8", theme.BackHover);								// Hovered list item background colour
			css = css.Replace("#084e82", theme.Line);									// Hovered list item top/bottom border colour
			css = css.Replace("#506a76", theme.BackSelected);							// Selected list item background colour
			css = css.Replace("#084e83", theme.Line);									// Selected list item top/bottom border colour

			// Main panel, message preview
			css = css.Replace("#102141", theme.BackHeader);								// Preview background colour
			css = css.Replace("#808082", theme.Text5);									// Preview content (iframe) background colour
			css = css.Replace("#506a77", theme.BackExtra);								// Extra information background colour
			css = css.Replace("#f0f0fd", theme.Text9);									// Extra information text colour
			css = css.Replace("#f0f0fe", theme.Text9);									// Normal subject text colour
			css = css.Replace("#f0f0ff", theme.Text9);									// Normal sender text colour
			css = css.Replace("#ffd701", theme.TextGold);								// Hovered sender text colour
			css = css.Replace("#808083", theme.Text5);									// Normal timestamp label text colour
			css = css.Replace("#f0f1ff", theme.Text9);									// Normal timestamp text colour
			css = css.Replace("#808084", theme.Text5);									// Normal recipient label text colour
			css = css.Replace("#f0f2ff", theme.Text9);									// Normal recipient text colour
			css = css.Replace("#ffd702", theme.TextGold);								// Hovered recipient text colour
			css = css.Replace("#808085", theme.Text5);									// Normal attachment label text colour
			css = css.Replace("#102142", theme.Back1);									// Attachment area background colour
			css = css.Replace("#102143", theme.Back1);									// Normal attachment background colour
			css = css.Replace("#f0f3ff", theme.Text9);									// Normal attachment text colour
			css = css.Replace("#102144", theme.Back1);									// Hovered attachment background colour
			css = css.Replace("#ffd703", theme.TextGold);								// Hovered attachment text colour

			// Message editor
			css = css.Replace("#506a78", theme.BackSelected);							// Normal recipient item border colour
			css = css.Replace("#506a79", theme.BackSelected);							// Normal recipient item background colour
			css = css.Replace("#f0f6ff", theme.Text9);									// Normal recipient item text colour
			css = css.Replace("#506a80", theme.BackSelected);							// Hovered recipient item background colour
			css = css.Replace("#f0f7ff", theme.Text9);									// Hovered recipient item text colour

			// Calendar tabs
			css = css.Replace("#102145", theme.BackContentPanel);						// Tab panel background colour
			css = css.Replace("#364462", theme.BackNormal);								// Normal tab background colour
			css = css.Replace("#f0f8ff", theme.Text9);									// Normal tab text colour
			css = css.Replace("#2587c9", theme.BackHover);								// Hovered tab background colour
			css = css.Replace("#f0f9ff", theme.Text9);									// Hovered tab text colour
			css = css.Replace("#506a82", theme.BackSelected);							// Selected tab background colour
			css = css.Replace("#f0faff", theme.Text9);									// Selected tab text colour

			// Calendar date picker
			css = css.Replace("#282c36", theme.BackList);								// Background colour
			css = css.Replace("#084e79", theme.Line);									// Separator line colour
			css = css.Replace("#1c2d32", theme.BackList);								// Weekdays background colour
			css = css.Replace("#ffffeb", theme.Text9);									// Weekdays text colour
			css = css.Replace("#1c2d33", theme.BackList);								// Week numbers background colour
			css = css.Replace("#ff0001", theme.TextRed);								// Week numbers text colour
			css = css.Replace("#1c2d34", theme.BackList);								// Normal days from previous month background colour
			css = css.Replace("#808090", theme.Text5);									// Normal days from previous month text colour
			css = css.Replace("#1c2d35", theme.BackList);								// Normal days from this month background colour
			css = css.Replace("#ffffec", theme.Text9);									// Normal days from this month text colour
			css = css.Replace("#1c2d36", theme.BackList);								// Normal days from next month background colour
			css = css.Replace("#808091", theme.Text5);									// Normal days from next month text colour
			css = css.Replace("#282c40", theme.BackList);								// Disabled days background colour
			css = css.Replace("#ff0002", theme.TextRed);								// Disabled days text colour
			css = css.Replace("#2587d3", theme.BackHover);								// Hovered day background colour
			css = css.Replace("#ffffed", theme.Text9);									// Hovered day text colour
			css = css.Replace("#506a93", theme.BackSelected);							// Selected day background colour
			css = css.Replace("#ffffee", theme.Text9);									// Selected day text colour
			css = css.Replace("#506a92", theme.BackList);								// Normal today background colour
			css = css.Replace("#ffd704", theme.TextGold);								// Normal today text colour
			css = css.Replace("#2587d4", theme.BackHover);								// Hovered today background colour
			css = css.Replace("#ffd705", theme.TextGold);								// Hovered today text colour

			// Calendar month and year picker
			css = css.Replace("#364463", theme.BackNormal);								// Border colour when opened from dropdown button
			css = css.Replace("#084e86", theme.Line);									// Top and bottom border for the bottom panel
			css = css.Replace("#282c38", theme.Back1);									// Background colour
			css = css.Replace("#f0fbff", theme.Text9);									// Text colour
			css = css.Replace("#282d38", theme.Back1);									// Normal month background colour
			css = css.Replace("#f0fcff", theme.Text9);									// Normal month text colour
			css = css.Replace("#2587ca", theme.BackHover);								// Hovered month background colour
			css = css.Replace("#f0fdff", theme.Text9);									// Hovered month text colour
			css = css.Replace("#506a83", theme.BackSelected);							// Selected month background colour
			css = css.Replace("#f0feff", theme.Text9);									// Selected month text colour
			css = css.Replace("#506a84", theme.Line);									// Devider colour between months and years
			css = css.Replace("#282d39", theme.Back1);									// Normal year background colour
			css = css.Replace("#f0ffff", theme.Text9);									// Normal year text colour
			css = css.Replace("#2587cc", theme.BackHover);								// Hovered year background colour
			css = css.Replace("#f1ffff", theme.Text9);									// Hovered year text colour
			css = css.Replace("#506a85", theme.BackSelected);							// Selected year background colour
			css = css.Replace("#f2ffff", theme.Text9);									// Selected year text colour

			// Calendar content
			css = css.Replace("#282c39", theme.Back1);									// Calendar drag selection colour
			css = css.Replace("#808093", theme.Text5);									// Calendar drag selection text colour
			css = css.Replace("#364464", theme.BackNormal);								// Month view header background colour
			css = css.Replace("#f3ffff", theme.Text9);									// Month view header text colour
			css = css.Replace("#102146", theme.Back1);									// Month view content background colour
			css = css.Replace("#084e87", theme.Line);									// Day/week view border colour
			css = css.Replace("#102147", theme.Back1);									// Day/week view content background colour
			css = css.Replace("#f6ffff", theme.Text9);									// Day/Week view time devider line colour
			css = css.Replace("#f4ffff", theme.Text9);									// Day/week view time text colour
			css = css.Replace("#ff0000", theme.TextRed);								// Day/week view current time line
			css = css.Replace("#506a86", theme.BackSelected);							// Current day background and border colour
			css = css.Replace("#084e94", theme.Text5);									// Calendar appointment border colour */
			css = css.Replace("#fffdff", theme.Text9);									// Calendar appointment text colour */
			css = css.Replace("#506a87", theme.BackExtra);								// Editor extra information background colour
			css = css.Replace("#f5ffff", theme.Text9);									// Editor extra information text colour

			// Calendar, free/busy panel
			css = css.Replace("#364477", theme.BackNormal);								// Normal date header background colour
			css = css.Replace("#fffefd", theme.Text9);									// Normal date header text colour
			css = css.Replace("#506a73", theme.BackSelected);							// Today date header background colour
			css = css.Replace("#fffeff", theme.Text9);									// Today date header text colour
			css = css.Replace("#102136", theme.Back1);									// Time header background colour
			css = css.Replace("#fffefe", theme.Text9);									// Time header text colour
			css = css.Replace("#102156", theme.Back1);									// Background colour

			// Contacts
			css = css.Replace("#364465", theme.BackNormal);								// List category box background colour
			css = css.Replace("#f7ffff", theme.Text9);									// List category box text colour
			css = css.Replace("#364466", theme.BackHeader);								// Normal contact card header background colour
			css = css.Replace("#f8ffff", theme.Text9);									// Normal contact card header text colour
			css = css.Replace("#102148", theme.BackList);								// Normal contact card body background colour
			css = css.Replace("#fbffff", theme.Text9);									// Normal contact card body text colour
			css = css.Replace("#808086", theme.Text5);									// Normal contact card body label text colour
			css = css.Replace("#2587cd", theme.BackContentPanel);						// Hovered contact card border colour
			css = css.Replace("#364467", theme.BackHover);								// Hovered contact card header background colour
			css = css.Replace("#f9ffff", theme.Text9);									// Hovered contact card header text colour
			css = css.Replace("#2587ce", theme.BackHover);								// Hovered contact card body background colour
			css = css.Replace("#fcffff", theme.Text9);									// Hovered contact card body text colour
			css = css.Replace("#808087", theme.Text5);									// Hovered contact card body label text colour
			css = css.Replace("#506a88", theme.BackContentPanel);						// Selected contact card border colour
			css = css.Replace("#364468", theme.BackHeader);								// Selected contact card header background colour
			css = css.Replace("#faffff", theme.Text9);									// Selected contact card header text colour
			css = css.Replace("#102149", theme.BackSelected);							// Selected contact card body background colour
			css = css.Replace("#fdffff", theme.Text9);									// Selected contact card body text colour
			css = css.Replace("#808088", theme.Text5);									// Selected contact card body label text colour

			// Sticky notes
			css = css.Replace("#102150", theme.Border);									// Normal note icon border colour
			css = css.Replace("#102151", theme.BackList);								// Normal note icon background colour
			css = css.Replace("#feffff", theme.Text9);									// Normal note icon text colour
			css = css.Replace("#2587cf", theme.BackHover);								// Hovered note icon border colour
			css = css.Replace("#2587d0", theme.BackHover);								// Hovered note icon background colour
			css = css.Replace("#ffffe0", theme.Text9);									// Hovered note icon text colour
			css = css.Replace("#506a89", theme.BackSelected);							// Selected note icon border colour
			css = css.Replace("#506a90", theme.BackSelected);							// Selected note icon background colour
			css = css.Replace("#ffffe1", theme.Text9);									// Selected note icon text colour
			css = css.Replace("#00233b", theme.BackEditorBlue);							// Editor blue background colour
			css = css.Replace("#ffffe2", theme.Text9);									// Editor blue text colour
			css = css.Replace("#113a00", theme.BackEditorGreen);						// Editor green background colour
			css = css.Replace("#ffffe3", theme.Text9);									// Editor green text colour
			css = css.Replace("#684363", theme.BackEditorPink);							// Editor pink background colour
			css = css.Replace("#ffffe4", theme.Text9);									// Editor pink text colour
			css = css.Replace("#4e4d00", theme.BackEditorYellow);						// Editor yellow background colour
			css = css.Replace("#ffffe5", theme.Text9);									// Editor yellow text colour
			css = css.Replace("#5c5c5c", theme.BackEditorWhite);						// Editor white background colour
			css = css.Replace("#ffffe6", theme.Text9);									// Editor white text colour

			// Settings
			css = css.Replace("#506a96", theme.BackExtra);								// Extra information background colour
			css = css.Replace("#fffef4", theme.Text9);									// Extra information text colour
			css = css.Replace("#102155", theme.Back1);									// Normal about link background colour
			css = css.Replace("#fffef5", theme.Text9);									// Normal about link text colour
			css = css.Replace("#2587d7", theme.BackHover);								// Hovered about link background colour
			css = css.Replace("#fffef6", theme.Text9);									// Hovered about link text colour
			css = css.Replace("#084e92", theme.BackNormal);								// Keyboard shortcut row border colour
			css = css.Replace("#102131", theme.BackNormal);								// Keyboard shortcut row background colour
			css = css.Replace("#fffef8", theme.Text9);									// Keyboard shortcut row text colour
			css = css.Replace("#364474", theme.BackNormal);								// Keyboard shortcut type box border colour
			css = css.Replace("#364475", theme.BackNormal);								// Keyboard shortcut type box background colour
			css = css.Replace("#fffef9", theme.Text9);									// Keyboard shortcut type box text colour

			// Menus and buttons
			css = css.Replace("#084e88", theme.Border);									// Dark box border colour
			css = css.Replace("#102152", theme.Back1);									// Dark box background colour
			css = css.Replace("#084e90", theme.Border);									// Light box border colour
			css = css.Replace("#364469", theme.BackNormal);								// Light box background colour
			css = css.Replace("#084e89", theme.Line);									// Separator colour
			css = css.Replace("#102153", theme.BackNormal);								// Normal item background colour
			css = css.Replace("#ffffe7", theme.Text9);									// Normal item text colour
			css = css.Replace("#2587d1", theme.BackHover);								// Hovered item background colour
			css = css.Replace("#ffffe8", theme.Text9);									// Hovered item text colour
			css = css.Replace("#2587d2", theme.BackClick);								// Clicked item background colour
			css = css.Replace("#ffffe9", theme.Text9);									// Clicked item text colour
			css = css.Replace("#506a91", theme.BackSelected);							// Selected item background colour
			css = css.Replace("#ffffea", theme.Text9);									// Selected item text colour
			css = css.Replace("#102154", theme.Back1);									// Disabled item background colour
			css = css.Replace("#808089", theme.Text5);									// Disabled item text colour

			// Text input and text input area
			css = css.Replace("#b4b4e0", theme.Text6);									// Normal label text colour
			css = css.Replace("#2587d5", theme.BackHover);								// Hovered label text colour
			css = css.Replace("#364470", theme.Border);									// Normal text input/area border colour
			css = css.Replace("#1c2d37", theme.BackInput);								// Normal text input/area background colour
			css = css.Replace("#fffef0", theme.Text9);									// Normal text input/area text colour
			css = css.Replace("#2587d6", theme.BackHover);								// Hovered text input/area border colour
			css = css.Replace("#1c2d38", theme.BackInput);								// Hovered text input/area background colour
			css = css.Replace("#fffef1", theme.Text9);									// Hovered text input/area text colour
			css = css.Replace("#364471", theme.BackNormal);								// Focused text input/area border colour
			css = css.Replace("#1c2d39", theme.BackInput);								// Focused text input/area background colour
			css = css.Replace("#ffff00", theme.TextYellow);								// Focused text input/area text colour

			css = css.Replace("#364472", theme.BackNormal);								// Normal checkbox border colour
			css = css.Replace("#1c2d40", theme.BackInput);								// Normal checkbox background colour
			css = css.Replace("#fffef2", theme.Text9);									// Normal checkbox text colour

			// Panels, dialogs and windows
			css = css.Replace("#084e78", theme.Border);									// Panel border colour
			css = css.Replace("#364473", theme.BackHeader);								// Panel header background colour
			css = css.Replace("#fffef3", theme.Text9);									// Panel header text colour
			css = css.Replace("#102135", theme.Back1);									// Panel body background colour
			css = css.Replace("#506a95", theme.BackHeader);								// Panel footer background colour
			css = css.Replace("#506a94", theme.BackDragged);							// Dragged panel background colour

			// Widget panel and widgets
			css = css.Replace("#111124", theme.BackPage);								// Widget panel background colour
			css = css.Replace("#084e77", theme.Border);									// Widget border colour
			css = css.Replace("#102130", theme.BackWidgetPanel);						// Widget background colour

			// Tree
			css = css.Replace("#102134", theme.BackList);								// Normal tree node background colour
			css = css.Replace("#fffef7", theme.Text9);									// Normal tree node text colour
			css = css.Replace("#2587c4", theme.BackHover);								// Hovered tree node background colour
			css = css.Replace("#fffefa", theme.Text9);									// Hovered tree node text colour
			css = css.Replace("#fffefb", theme.BackClick);								// Clicked tree node background colour
			css = css.Replace("#2587c5", theme.BackHover);								// Clicked tree node text colour
			css = css.Replace("#506a74", theme.BackSelected);							// Selected leaf tree node background colour
			css = css.Replace("#fffefc", theme.Text9);									// Selected tree node background colour

			// Return the CSS.
			return css;
		} // SearchAndReplace

		public String TrimStart(String css) {
			// Trim the CSS from the beginning to the "/* end of legend */" comment.
			Int32 index = css.ToLower().IndexOf("/* end of legend */");
			if (index > -1) {
				css = css.Substring(index + 19);
			}

			// Return the CSS.
			return css;
		} // TrimStart

		public String ReplaceFillColors(String css, SvgMap map) {
			// IMPORTANT
			//
			// This asumes that each SVG image is embedded as BASE64 encoded data, identified in the example with "data:image/svg+xml;base64,".
			// And that only one SVG image is contained in one line.
			// And that the SVG image is identified by the first id/class, which is ".plus" in the example.
			//
			// Example CSS line:
			//	.plus, .x-tab-add {background-image: url(data:image/svg+xml;base64,PHN2ZyB4bWxuc.......AnIC8+PC9zdmc==) !important; background-repeat: no-repeat !important; background-position: center center !important;}

			// Split the CSS into lines.
			String[] cssLines = css.Split(new String[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
			for (Int32 cssIndex = 0; cssIndex < cssLines.Length; cssIndex++) {
				// Get the current line of CSS.
				Boolean cssSave = false;
				String cssLine = cssLines[cssIndex];

				// Find the BASE64 encoded SVG data.
				Int32 base64Start = cssLine.ToLower().IndexOf($"(data:image/svg+xml;base64,");
				if (base64Start > -1) {
					Int32 base64End = cssLine.ToLower().IndexOf($")", base64Start);
					if (base64End > base64Start) {
						// Get the id, which is the first "id" or "class" identifier in the CSS.
						// In the example abowe, this is ".plus".
						String cssId = String.Empty;
						String[] cssIds = cssLine.Split(new String[] { ",", "{" }, StringSplitOptions.None);
						if (cssIds.Length > 0) {
							cssId = cssIds[0].Trim().ToLower();
						}
//Console.WriteLine($"--------------------------------------------------------------------------------");
//Console.WriteLine($"{cssId}");
//Console.WriteLine($"{cssLine}");
//Console.WriteLine($"--------------------------------------------------------------------------------");
						// Decode the BASE64 data.
						String base64 = cssLine.Substring(base64Start + 27, base64End - base64Start - 27);
						String svg = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
//Console.WriteLine($"{svg}");
//Console.WriteLine($"--------------------------------------------------------------------------------");

						// Get the XML.
						//XmlNameTable xmlNameTable = new NameTable();
						//XmlNamespaceManager xmlNameSpaceManager = new XmlNamespaceManager(xmlNameTable);
						//xmlNameSpaceManager.AddNamespace("xmlns", "http://www.w3.org/2000/svg");
						//XmlDocument xml = new XmlDocument(xmlNameSpaceManager.NameTable);
						XmlDocument xml = new XmlDocument();
						xml.LoadXml(svg);

						// Iterate through all "path" elements.
						XmlElement xmlElement = xml.DocumentElement;
						Int32 xmlIndex = 0;
						//foreach (XmlNode xmlNode in xml.SelectNodes("//path")) {					this does not work
						foreach (XmlNode xmlNode in xmlElement.ChildNodes) {
							if (xmlNode.Name.ToLower() == "path") {
//Console.WriteLine($"{xmlNode.OuterXml}");
//Console.WriteLine($"--------------------------------------------------------------------------------");
								// Get the "style" attribute, or create a new if it does not exist.
								XmlNode styleNode = xmlNode.Attributes.GetNamedItem("style");
								if (styleNode == null) {
									styleNode = xml.CreateAttribute("style");
								}

								if (styleNode != null) {
									// Split the styles, and remove the "fill".
									List<String> styles = new List<String>(styleNode.Value.Split(';'));
									for (Int32 styleIndex = 0; styleIndex < styles.Count; ) {
										if (styles[styleIndex].ToLower().Contains("fill:") == true) {
											styles.RemoveAt(styleIndex);
										} else {
											styleIndex++;
										}
									}

									// Add the mapped "fill" colour.
									if ((map.Entries.ContainsKey(cssId) == true) && (map.Entries[cssId].Count > xmlIndex)) {
										// Use the mapped colour.
//Console.WriteLine($"{cssId}: {map.Entries[cssId][xmlIndex]}");
										styles.Add($"fill:{map.Entries[cssId][xmlIndex]}");
									} else {
										// Use the default colour.
										styles.Add($"fill:{map.DefaultColor}");
									}

									// Join the styles.
									styleNode.Value = String.Join(";", styles);
									xmlNode.Attributes.SetNamedItem(styleNode);
								}

								// Iterate the index.
								xmlIndex++;
							}
						}

						// Save the XML.
						svg = xml.OuterXml;

						// Encode the BASE64 data.
						base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(svg));
						cssLine = cssLine.Remove(base64Start + 27, base64End - base64Start - 27);
						cssLine = cssLine.Insert(base64Start + 27, base64);

						// Save the changed CSS.
						cssSave = true;
					}
				}

				// Save the changed CSS.
				if (cssSave == true) {
					cssLines[cssIndex] = cssLine;
				}
			}

			// Returen the css
			return String.Join(Environment.NewLine, cssLines);
		} // ReplaceFillColors
		#endregion

		#region Static main method
		//--------------------------------------------------------------------------------------------------------------
		// Static main method
		//--------------------------------------------------------------------------------------------------------------
		public static void Main(String[] args) {
			try {
				// Create a new instance of the program, end run it.
				Program program = new Program(args);
				//program.Run();
			} catch (Exception e) {
				Console.WriteLine($"Error: {e.Message}");
				Console.WriteLine($"{e.StackTrace}");
			}
			Console.WriteLine($"End of program");
		} // Main
		#endregion

	} // Program
	#endregion

	#region Theme
	//------------------------------------------------------------------------------------------------------------------
	// Theme
	// This class defines the colours you need to find (design) for each colour theme.
	//------------------------------------------------------------------------------------------------------------------
	public class Theme {
		public String BackPage { get; set; }
		public String BackDragged { get; set; }
		public String BackMask { get; set; }
		public String BackMenubar { get; set; }
		public String TextMenubar { get; set; }
		public String BackNavigationPanel { get; set; }
		public String BackContentPanel { get; set; }
		public String BackWidgetPanel { get; set; }
		public String BackList { get; set; }
		public String BackNormal { get; set; }
		public String BackHover { get; set; }
		public String BackClick { get; set; }
		public String BackSelected { get; set; }
		public String BackHeader { get; set; }
		public String BackExtra { get; set; }
		public String BackInput { get; set; }
		public String Back1 { get; set; }
		public String BackEditorBlue { get; set; }
		public String BackEditorGreen { get; set; }
		public String BackEditorYellow { get; set; }
		public String BackEditorWhite { get; set; }
		public String BackEditorPink { get; set; }
		public String Line { get; set; }
		public String Border { get; set; }
		public String Icon { get; set; }
		public String Text9 { get; set; }
		public String Text6 { get; set; }
		public String Text5 { get; set; }
		public String TextRed { get; set; }
		public String TextYellow { get; set; }
		public String TextGold { get; set; }
		public String Transparent { get; set; }
	} // Theme
	#endregion

	#region SvgMap
	//------------------------------------------------------------------------------------------------------------------
	// SvgMap
	// This class defines the fill colours set in a SVG.
	// The first colour is set in the first "path" element, the second colour in the second "path" element and so on.
	//------------------------------------------------------------------------------------------------------------------
	public class SvgMap {
		public String DefaultColor { get; }
		public Dictionary<String, List<String>> Entries { get; } = new Dictionary<String, List<String>>();

		public SvgMap(String defaultColor) {
			this.DefaultColor = defaultColor;
		} // SvgMap

		public void AddEntry(String id, params String[] colors) {
			this.Entries.Add(id.ToLower(), new List<String>(colors));
		} // AddEntry
	} // SvgMap
	#endregion

} // RpcScandinavia.Tool
