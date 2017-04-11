# Allwin.Sitecore.Modules.LiveList

## Compatible with the following versions

 - Sitecore 8.0 MVC
 - Sitecore 8.1 MVC
 - Sitecore 8.2 MVC

## Purpose of the module

We agreed upon a list module that can render almost anything, but we didn’t stop there and  added another functionality. To make this list update in real time for the viewer. 

We chose **SignalR** for the technology that we want to learn and we used  that to make a websocket connection between Sitecore and the browser.  We were trying to make a list that would be versitile enough and could be used in various situations. So we came to the conclusion that we should be able to render almost anything into this list. We built this module to be as general as possible from the ground up. 

We hope that the easy usage and the simple setup will help people set up this list on their main page and help them keep in thouch with customers, visitors or passangers.

## How to install to dev environment

 1. Clone the repository
 2. Install clean Sitecore instance here: `\sitecore\`
 3. Build and publish to solution
 4. Go to `/unicorn.aspx` and Sync all configs

## How to install to Content Management environment

 1. **Compatible only with Sitecore MVC**
 2. Install the Sitecore package with **Installation Wizard** (including items and files)
 3. Add the `/sitecore/layout/Renderings/LiveList/LiveList` rendering to your Placeholder Settings where you want to use to make usable in **Experience Editor**

## How to install to Content Delivery environment

 1. Do an item publish from **CM** environment and copy the required files to **CD**
	 1. `\App_Config\Include\Allwin.Sitecore.Modules.LiveList\LiveList.config` - **Remove the following  processor: type="Allwin.Sitecore.Modules.LiveList.Pipelines.RegisterPrivateRoutes"**
	 2.  `\bin`
		 1. `Microsoft.AspNet.SignalR.Core.dll`
		 2. `Microsoft.AspNet.SignalR.SystemWeb.dll`
		 3. `Microsoft.Owin.dll`
		 4. `Microsoft.Owin.Host.SystemWeb.dll`
		 5. `Microsoft.Owin.Security.dll`
		 6. `Owin.dll`
		 7. `Allwin.Sitecore.Modules.LiveList.dll`
	 3. `\scripts\live-list\` folder
	 4. `\styles\live-list\live-list.css`
	 5. `\Views`
		 1. `\LiveList\Layouts\LiveListLayout.cshtml`
		 2. `\LiveList\LiveList.cshtml`
		 3. `\LiveList\LiveListShort.cshtml`

## How to use the module

After you installed everything you need create your own **Live Lists**. To create a new one you just need create an item from `/sitecore/templates/Branches/LiveList/Live List Folder` branch. Here you can find the following 3 folders:

### Live Lists Folder

Here you can create your **Live List Container** items, these will be your root items for your lists

#### Live List Container

##### Fields

1. **Max Items On Page Load** - How many items you want to show after the page is loaded
2. **Title** - Title of the module
3. **Show more text** - Button text on the module

In this container you can store your **Live List Items**

#### Live List Item

##### Fields

1. **Rendering** - which rendering the editor wants to use from the **Live List Renderings** folder
2. **Datasource** - which datasource the editor wants to use from the **Datasources** folder depending on the selected **Rendering**

#### Live List Rendering Item

Need to be setup by developers or administrators. Here you need to setup the **Rendering Definitions** (which **Rendering** with which **Templates** is usable)

##### Fields

 1. **Rendering**
 2. **Allowed Templates**

#### Datasources
Here the editors can create their own **Datasources** for the **Live List items**. This folder contains items based on your custom templates.

### Global Settings
The global setting item is found here: `/sitecore/system/Modules/LiveList/Live List Settings`. You can setup here **Use Default CSS** or not not. Because by default the component has it's own style but you can overwrite it with your own.

### Sitecore 8.0 specialities

If you would like to install the module for Sitecore 8.0 then you need to do some additional changes to make it work. Go to your `web.config` and:

 1. Add `targetFramework="4.5" requestValidationMode="2.0"` to `system.web/httpRuntime`
 2. If you get the following exception:
 

> WebForms UnobtrusiveValidationMode requires a ScriptResourceMapping for 'jquery'. Please add a ScriptResourceMapping named jquery(case-sensitive).

Then you need to add `<add key="ValidationSettings:UnobtrusiveValidationMode" value="None" />` to your `<appSettings>`.
 

### Here is a tutorial about an older version with integrated in Habitat
[![YouTube Video](https://img.youtube.com/vi/AZje4ROX4dc/0.jpg)](https://www.youtube.com/watch?v=AZje4ROX4dc)