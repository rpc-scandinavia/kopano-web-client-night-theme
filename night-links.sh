#!/bin/bash
#┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┐
#│    FILE: /data/setup/Linux/bincs.files/kopano/webapp-themes/night-links.sh                                          │
#│ VERSION: 2020-03-28 Initial version.                                                                                │
#│          2020-04-09 Added symbolic links for the icon sets.                                                         │
#│  SYSTEM: Kopano web application / MS1.                                                                              │
#│FUNCTION: Create symbolic links to the Kopano web application night themes and associated icon sets.                 │
#│          This is used when developping the night themes, to awoid copying the files to the web-server all the time. │
#│                                                                                                                     │
#└─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┘

KOPANO_WEB_APP_PLUGINS_DIRECTORY="/usr/share/kopano-webapp/plugins"
KOPANO_WEB_APP_ICONS_DIRECTORY="/usr/share/kopano-webapp/client/resources/iconsets"

SETUP_NIGHT_THEMES_DIRECTORY="/data/setup/Linux/bincs.files/kopano/webapp-themes/plugins"
SETUP_NIGHT_ICONS_DIRECTORY="/data/setup/Linux/bincs.files/kopano/webapp-themes/iconsets"

SETUP_NIGHT_THEME_NAMES=("night-blue" "night-green")


ln  -s  "$SETUP_NIGHT_THEMES_DIRECTORY/night-template"  "$KOPANO_WEB_APP_PLUGINS_DIRECTORY"				# The template has no associated icon sets.
for themeName in "${SETUP_NIGHT_THEME_NAMES[@]}"
do
	echo "Creating symbolic link for '$themeName'"
	ln  -s  "$SETUP_NIGHT_THEMES_DIRECTORY/$themeName"  "$KOPANO_WEB_APP_PLUGINS_DIRECTORY"
	ln  -s  "$SETUP_NIGHT_ICONS_DIRECTORY/$themeName-breeze"  "$KOPANO_WEB_APP_ICONS_DIRECTORY"
	ln  -s  "$SETUP_NIGHT_ICONS_DIRECTORY/$themeName-classic"  "$KOPANO_WEB_APP_ICONS_DIRECTORY"
done


echo "End of script"
exit 0
