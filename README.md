# unity-3d-starter

A basic unity turn-based combat game that includes two players, a number of different actions the players can take to defeat their opponent, and a HUD displaying the player's actions and HP. The game functions off of random number generation to decide who goes first and how succesful each of their actions is. It also includes a music track and a title and end credits that display at the beginning and end of the game. 

As of the updated version, it has an AI combatent that can be battled.

NOTE: Due to scripting errors, this current version of the game only allows battle against an AI opponent. This will be fixed in a future version, but for now if you select Player vs Player, Player1 will still run the autonamous action script. 

## Scenes

The project includes 4 scenes:
- TitleScene.unity is a landing screen for startup. Pressing space from the title screen starts the game and loads... 
- Main.unity contains a scene with a quad acting as the ground and a 1st person character with a simple controller. The game ends when the player falls from the quad, loading ...
- Credits.unity loads when the game finishes. Pressing any key exits the application.
- Arena.unity contains the bulk of the game itself. It is the arena in which the two player characters are loaded and also contains the UI required to play the game. The game ends when one of the players reduces their opponent to zero HP.
- ArenaAI.unity is a version of the above that loads the AI combatent

## Scripts

The game includes 7 C# scripts:
- StartGame.cs loads the Main scene when the spacebar is pressed. It is connected to the Main Camera in TitleScene.
- GameOver.cs detects when the player has fallen below a given Y coordinate and loads the Credits scene. It is attached to the Player Camera in the Main scene. It requires a reference to the player transform. This is currently set up as a public variable so that the script can be easily relocated if desired. When attached to the Player character, the transform can be obtained from gameObject.
- PlayerContgroller.cs controls player movement. Move the player forward, back, left and right using WASD or arrow keys, and rotate (look) using the mouse. It is attached to the Player GameObject.  NOTE: the Player object includes a Rigidbody componenent. For many games this will not be needed and can be removed.
- Quit.cs detects a keypress and quits the application. It is attached to the Credits scene Main camera.
- BattleSystem.cs creates the game states and most of the functions used in the game. It is attached to an empty object in the arena file during the game and requires the BattleHUD objects, player prefabs, and dialogue box text objects to be attached to it.
- BattleHUD.cs sets the player names and HP on the HUDs at the start of the game.
- Unit.cs contains the functions for the players taking damage and checking to see if a player has died and the game is over.
- ChooseMode.cs allows the player to select the mode they would like to play

## Building and Playing

The project includes two placeholder logos that will appear when the game is built and played (See Player Settings). The TitleScene should be loaded first (See Build Settings.) Be sure all three scenes (TitleScene, Arena, and Credits) have been added to the build.


External Asset packages
From the Unity Asset store:
Simple Urban Building Pack 1 (https://assetstore.unity.com/packages/3d/environments/urban/simple-urban-buildings-pack-1-33563#description) by Rune Studios
[Industrial Set 2.0](https://assetstore.unity.com/packages/3d/environments/industrial/rpg-fps-game-assets-for-pc-mobile-industrial-set-v2-0-86679) RPG/FPS Game assets for PC/Mobile by Dmitrii Kutsenko.
Free Low Poly Swords (https://assetstore.unity.com/packages/3d/props/weapons/free-low-poly-swords-189978) by Pure Poly
Modular Medieval Fighting Arena (https://assetstore.unity.com/packages/3d/environments/modular-medieval-fighting-arena-253040) by Nisu 
Modular Fantasy Character (https://assetstore.unity.com/packages/3d/characters/modular-fantasy-character-165896) by asoliddev
Low Poly Fantasy Warrior (https://assetstore.unity.com/packages/3d/characters/humanoids/low-poly-fantasy-warrior-127775) by asoliddev