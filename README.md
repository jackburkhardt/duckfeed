# duckfeed

This is a Unity project that serves as a kitchen sink where I design, build, and test various systems in Unity3D. While not a complete game, this project includes demo scenes and systems that can be easily taken and used in other projects. Some features of this project include:

* **An object interaction system**
  * Modes included for examining and picking up objects or displaying locked objects and warnings
  * Objects are outlined different colors based on the interaction type
  * There are multiple crosshairs that change based on the interaction type
  * Objects that are able to be picked up can be put in the player's hand (or dropped/swapped from the hand)
* **A basic carousel-style inventory system**
  * InventoryItem class allows for unique IDs, names, descriptions, inventory icons, and object prfab storage
  * The inventory can be rotated through, displaying the current capacity and info about the currently selected item
  * Items from the player's hand can be put into their inventory (and vice versa)
* **A branching dialogue system** -- dedicated documentation forthcoming
  * Allows for multiple player responses, with optional skill checks and perk checks available
  * Support for animation clips (player and NPCs)
  * Support for audio/voiceover clips (player and NPCs)
  * Dialog easily stored, edited, and modified in text (.npc) files
  * Potentially: seperate software tool for visualizing and editing conversation nodes
* **A skills and perks system**
  * Mainly created to supplement the dialog options above; implementation is not final
