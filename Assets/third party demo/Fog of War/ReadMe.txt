--------------------------------------------------
             Tasharen Fog of War
 Copyright © 2012 Tasharen Entertainment
                Version 1.0
            support@tasharen.com
http://www.tasharen.com/forum/index.php?topic=1291.0
--------------------------------------------------

Thank you for buying Tasharen Fog of War!

----------------------------------------------
 Overview
----------------------------------------------

Fog of War system needs 3 components in order to work:
* Fog of War system that will create a height map of your scene and perform all the updates.
* Fog of War Image Effect on the camera that will be displaying the fog of war.
* Fog of War Revealer on one or more game objects in the world.

----------------------------------------------
 Package Structure
----------------------------------------------

Fog of War
|
+-FoW		-- Contains the actual Fog of War system in its entirety.
|
+-Examples	-- Contains example scene, assets, and scripts. Safe to delete.

----------------------------------------------
 Usage
----------------------------------------------

1. Drag & drop the Fog of War prefab into your scene. You can find this prefab in Assets/Fog of War/FoW folder.
2. Add FOWEffect script to your Main Camera. Make sure it references the "Image Effects/Fog of War" shader.
3. Add FOWRevealer script to one or more game objects in your scene. It's best to add it to a game object
   located at around the unit's "eye level" in order for the fog of war to be accurate.

----------------------------------------------

If you have any questions, suggestions, comments or feature requests, please
drop by the forums, found here: http://www.tasharen.com/forum/index.php?topic=1291.0