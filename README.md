# Kopano web application - Night theme
The build in "dark" theme, is actually a light theme with black text.

I started creating my own dark theme, but since the name "dark" is already used wrongfully, I named this theme "night".


## 2020-04-10
Replaced the "night-builder.sh" script and the "Fix-Svg" app with one new C# app (night-builder), that contains the theme colours, and the ability to update multiple "fill" colours in the SVG images.
The colour themes and SVG fill colour map is located in "night-builder - Themes.cs".

Now I am happy :-) , and so far I have tested with a few multi-colour images for the Notes icon view.

I also changed it, so there are two icon sets for each night theme, so it is possible to colour the icons.

Next is identifying remaining two colour icons and fixing the next level (as I call it), which is the "windows" and "dialogs", plus bug fixes.



## 2020-04-06
I was not entirely satisfied with the icon images. Using the "brightness" filter also brightened the background on some buttons.
So I tried to set the "fill" colour on the images in CSS, but as expected it did not work. You can read several places on the internet, that this do not work when the SVG is loaded/shown with "background-image". But I had to try.

During this process, I removed the "fill" colour style from the BASE64 encoded SVG images in the CSS, so now I have changed the small program I wrote for this, to set the "fill" colour style attribute, and changed the "night-builder.sh" script to re-colour the images.

The images are placed in tree files, "_icons.scss" and either "breeze-icons.css" or "classic-icons.css". The last two are the icon themes, and thus only one are used at a time. All the images in "breeze-icons.css" are SVG images, and almost all icons in "classic-icons.css" are PNG images.

The images in "_icons.scss" can be included in the "night-template.css" and thus overrides the original images, but the images from the icon theme can not. I suspect this is because the icon theme is loaded after the style theme. Insted I created two additional icon themes, that can be used to get the re-coloured icon images. In this first test they are red, so I can se the effect. There are some multi colour images, and my program only changes the first found "fill" colour - but luckyly it seems that it is the first "fill" colour that should be changed. Otherwise I have to look into multiple re-colouring.

The "Fix-Svg" program is written in C# and requires Microsoft .NET 5 (five is preview, you can also use Dot Net Core 3.1) to build. In the CS file you find the command used to build a single self contained executeable, that is too big to upload to GitHub. To build, install Microsoft .NET 5 from "https://dotnet.microsoft.com/download", open the console and "cd" into the directory containing the CS and CSPROJ files and issue "dotnet build" to build, and finally the command from the CS file to create the self contained executeable.


## 2020-04-01
Finished the first level, and setup the script to build a "Night blue" and a "Night green" theme, just to show the principle.
Added a blue and green screenshot below.


## 2020-03-27
Almost everything in level one is fixed. Only the "Meeting planner" and a small address popup is missing - to my knowledge.

I got this brilliant idea to darken the MCE editor with "filter: brightness(70%);" (70% for the editor itself, and 50% for the editor popup/dialogs. Then I used the opposite technique on the dark icons. The downside with the icons is that colours look "washed", so only icons with little colour are brightened.

I think the ideal solution with icons, which are SVG images, will be to clear the "fill" colour in the SVG XML on one colour images, which makes it possible to control their colour using CSS.

At the beginning of the CSS, you will find a "legend" which contains all colours used.
This makes it possible to generate different night themes, using a lot of search and replace operations. Another solution could be to use CSS variables, since everything modified is identified.

My initial plan is to create a script that can take the CSS file (a night template), and then replace the 250+ colours with what is appropiate. After this, many colours will be the same, all the whites, for example. Then I can create a script for several night themes like "Night blue", "Night red" and "Night yellow" - or perhaps "Sunset" and "Sunrise" for the last two  :-)

When something the original CSS is fixed or modified, the individual night themes can be re-generated wery easy with the scripts.


## 2020-03-24
The initial version was one of the original themes, with a search and replace, plus some tinkering.
I decided to start from scratch, with an empty CSS file and go from there.

Now I have almost fixed the first level, the next level (as I call it) are the "windows", "dialogs" and Settings.

I tok alot of screenshots, which can be seen from the "screenshots" folder.
A few are shown below.


## 2020-03-22
Initial version.
There are still some white borders that need to be fixed.
The large calendar selector is not fixed.
The CSS code need to be cleaned up, and unnecessary code removed.


## A few screenshots

![Today (red icons)](screenshots/2020-04-06%20Today%20%28red%20icons%29.png?raw=true "Today %28red%20icons%29")

![Mail (blue)](screenshots/2020-04-01%20Mail%20%28blue%29.png?raw=true "Mail %28blue%29")

![Mail (green)](screenshots/2020-04-01%20Mail%20%28green%29.png?raw=true "Mail %28green%29")

![Mail](screenshots/2020-03-24%20Mail.png?raw=true "Mail")

![Calendar](screenshots/2020-03-24%20Calendar.png?raw=true "Calendar")

![New mail](screenshots/2020-03-24%20New%20mail.png?raw=true "New mail")

![Notes](screenshots/2020-03-24%20Notes%20icons.png?raw=true "Notes")
