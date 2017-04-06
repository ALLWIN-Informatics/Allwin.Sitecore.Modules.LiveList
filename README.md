# Allwin.Sitecore.Modules.LiveList

##Purpose of the module

We agreed upon a list  module that can render almost anything, but we didn’t stop there and  added another functionality. To make this list update in real time for the  viewer. 

We chose **SignalR** for the technologyt hat we want to learn and we used  that to make a websocket connection between Sitecore and the browser.  We were trying to make a list that would be versitile enough and could be  used in various situations. So we came to the conclusion that we should be  able to render almost anything into this list. We built this module to be as  general as possible from the ground up. 

We hope that the easy usage and the simple setup will help people set  up this list on their main page and help them keep in thouch with  customers, visitors or passangers.

##How to install to dev environment

 1. Clone the repository
 2. Install clean Sitecore instance here: `c:\Works\Allwin\Sitecore_Hackathon_2017\sitecore\`
 3. Build and publish to solution
 4. Go to `/unicorn.aspx` and Sync all configs

##How to install to Content Management environment

 1. Install the Sitecore package with **Installation Wizard** (including items and files)
 2. Add the `/sitecore/layout/Renderings/LiveList/LiveList` rendering to your Placeholder Settings where you want to use to make usable in **Experience Editor**

##How to install to Content Delivery environment

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

 1. Live Lists Folder
	 1. Here you can create your **Live List Container** items, these will be your root items for your lists
	 2. Live List Container
		 1. Fields
			 1. Max Items On Page Load - How many items you want to show after the page is loaded
			 2. Title - Title of the module
			 3. Show more text - Button text on the module
		 2. Under this container you can create your **Live List Items**
		 3. Live List Item fields
				 1. Rendering - which rendering the editor wants to use from the **Live List Renderings** folder
				 2. Datasource - which datasource the editor wants to use from the **Datasources** folder depending on the selected **Rendering**
 2. Live List Renderings - for developers
	 1. Here you need to setup the **Rendering Definitions** (which **Rendering** with which **Templates** is usable)
 3. Datasources
	 1. Here the editors can create their own **Datasources** for the Live List items
	 2. This folder contains items based on your custom templates

###Here is a tutorial about an older version with integrated in Habitat
[![YouTube Video](https://img.youtube.com/vi/AZje4ROX4dc/0.jpg)](https://www.youtube.com/watch?v=AZje4ROX4dc)