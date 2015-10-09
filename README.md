# wizard-game


### Overview

##### Scripts/GameController.cs
Contains all of our spell combinations. This returns a list of spell combinations based on items in the inventory.
##### Scripts/Inventory.cs
When the player picks up an item, this tells the spell controller to update its available spells.
##### Scripts/Item.cs
These have public variables _wizardClass_ (pyr, hyd, etc) and _article_ (hat, staff, etc). Items must be given a wizardClass of the first 3 letters of the class name (e.g. druid -> dru; cleric -> cle).
##### Scripts/SpellController.cs
Changes and casts spells and handles cooldown.
##### Scripts/Spells/
Logic for each spell. Once SpellController calls the ISpell's Cast method, these take over.
##### Resources/
Prefabs and icons for each spell.


### Adding a Spell

1. Create a game object--the physical object of your spell.
2. Create a script (with a name from the spell list in GameController) in the Scripts/Spells/ folder.
3. Add the script to the object you created in step 1.
4. Save the object as a prefab in Resources/Spells/.
5. Add an image (with the same name as the spell's class) to Resources/Sprites/.
