# Game Gizmo
*Find the hottest games!*

## Description
An application where you can look for all (or most) games that have been released. Look for a game you want to try out or have forgoton the name of using filters. Filter by specific criteria like genre, release date, platform, etc. 

![Game Gizmo Start Menu](https://github.com/user-attachments/assets/fa07df9c-7a9a-446e-ae26-fc25485b893b)

Read game descriptions, look at screenshots, visit the game's webpage, find developers and see what other games they have released.

![Game Gizmo Top Rated Games Page](https://github.com/user-attachments/assets/a49720fe-58a2-46c5-b4b2-94aa76b7ca1d)

## Details
- WPF Framework
- C#
- Rawg API - used for game information

#### Used NuGet packages
- WPF-UI - for more ui elements (like <ui:LoadingScreen>), or improved ui elements
- CommunityToolkit.Mvvm - used for relaycommands and observableObject, to be able to update data on view and to communicate with the viewmodel
- Microsoft.AspNet.WebApi.Client - used to communicate with the api

## Instructions

Open, build and run using visual studios

- If there are unexpected issues turning on and off sometimes works.
- Sometimes the api stops sending data, (there will be an exception). To fix this wait 10-20 sec then restart or click on a menu or search button to trigger a new api call and fix the issue.
- If there is a long loading screen but no exception wait and it should load (up to 20 sec wait)

## Sources

- https://www.youtube.com/watch?v=PzP8mw7JUzI - Used to create the home page and the menu (you *might* see a resemblence :D ), but there is a lot of things built on top of it. (In the video most buttons are not linked, search bar doesn't work, etc.)
- https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf - makes you able to only type numbers in a text box. (used for filter, to enter metacritic score range)
- https://stackoverflow.com/a/5401483 - answer used to catch all defined and undefined exceptions and to show a message box with them.
- https://stackoverflow.com/a/17409054 - answered used kinda everywhere to bind view to viewmodel
