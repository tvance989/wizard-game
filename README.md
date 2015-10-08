# wizard-game

Scripts/GameController.cs - Contains all of our spell combinations.
Scripts/SpellController.cs - Changes and casts spells and handles cooldown.
Scripts/Spells/ - Logic for each spell. Once SpellController calls the Cast method, these take over.
Resources/ - Prefabs for the spells.

To add a spell, first create an object.
Then create a script--with a name from the spell list in GameController--in the Scripts/Spells/ folder.
Attach the script to the object you created.
Save the object as a prefab in the Resources folder.
