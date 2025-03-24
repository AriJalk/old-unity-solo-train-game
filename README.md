# ⚠️ Project Abandoned – Moved to New Repo
This project is no longer maintained.
Development has moved to a New Repository implementing the game in Godot.

# Solo Train Game in Unity Engine

Developed in Unity with C#, this is my current game project, still early in prototype but most critical parts of the framework are working. It is aimed for Desktop-PC and hopefully Android once everything is mechanically sound.</br></br>
A solitaire train game, inspired by Cube-Rails board games like Chicago-Express and Steam, designed with physical implementation in mind. It is played through light Deck-Building and card play similar to the game Concordia, aiming to provide a short, puzzly card-based experience.</br></br>

It uses a Hex-System I ported from my implementation of [godot-hex-grid-module](https://github.com/AriJalk/godot-hex-grid-module), which in turn is based on [Red Blob Games Hexagonal Grids](https://www.redblobgames.com/grids/hexagons).

Starting with a small scope, it grew quickly, so I try to limit the amount of content both for shorter development time and to keep each Game-Run relatively fast without overwhelming choices.

## Game Idea

With a starting hand of three distinct cards — one that builds tracks, towns, and cities; one that transports goods along those tracks; and one that ends the round and reclaims all played cards to hand — and one origin station on a map with various terrain types, the player needs to expand their network within a set number of rounds to connect all required stations on the map to one unified network (a round ends when the player chooses to play the end-of-round card).</br>
In order to achieve this objective, the player, in addition to expanding the network, needs to obtain newer, more powerful cards from a dynamic market of cards that is sorted and appearing by era, through transportation of goods from towns that produce them to cities that receive those goods and provide those cards.

## What's Implemented

* Modular and easily Balancable Game Elements through ScriptableObjects for Terrain, Cards, Goods, and Buildings.
* Behavior abstraction interface makes creating new cards and behaviors reusable and relatively easy to implement.
* State and Event-Driven GameLoop, ensuring that the context of UI elements will behave according to the current State Context.
* Simple implementation of the hand of cards (made with Horizontal Layout for the meantime, might change to a fanned one once all critical parts of the game are implemented).
* Build logic is implemented for all building types.
* GameWorld Network-Expansion mechanic through rails (depicted by the Train Meeples) is working properly along with terrain cost and neighbor calculations.
* 3D Spherical camera with zoom that with borders defined by the map size.

## Yet to be implemented

* The level editor is still not done and is crucial before proper game balancing could be made, which is my current focus.
* The transport from town to city logic is still not ready; I need a proper map editor before I could make better gameplay decisions about the best way to handle it.
* The Build-Action needs a choice interface to select the building to be built.
* The card-market and End-Round mechanics are not yet implemented; all cards are already defined in the spreadsheet but are yet to be implemented beyond the starting ones. However, most cards derive their behavior from the two basic actions, Build/Transport. So, once the core actions are fully implemented and refined, I will implement the rest.

## Video Demonstration of the Network Expansion Mechanic

In this demonstration, we'll use a test environment map and the regular starting hand to initiate a build action through a card. We'll build rails to test if track building limitations are working properly, and also increment money through the discard of another card to observe its implication on possible build locations. Note that the card used for money incrementation is not discarded in the demo, as it's testing its contribution. The money doesn't decrement in this demo since it's testing a scenario of expanding over several turns. This demonstration also showcases the Spherical rotation camera.


[![Network Expansion](https://img.youtube.com/vi/4NCJbKw9o4U/0.jpg)](https://www.youtube.com/watch?v=4NCJbKw9o4U)

## Earlier development images from the project
*Note: The original version was color-blind friendly but not colorful enough.*

**Original Godot proof-of-concept before moving to Unity**
![Godot Version](ReadmeImages/SoloTrainGame_Godot.PNG)

**In-Game Card-Viewer**
This feature can be used in any menu to show a list of cards in any context, including discards.
![Card Viewer](ReadmeImages/SoloTrainGame_CardViewer.png)

**Towns and Cities**
Towns and Cities All tiles with double rails, towns, and cities built on them (A single square cube slot is a Town and the rectangle double slot is a City). This version used an older color palette, which was changed to the current one in the video demonstration for better color visibility while still being color-blind friendly. Also, Towns are meant to be upgraded to cities, so a tile won't hold both in an actual game.

![Towns and Cities](ReadmeImages/SoloTrainGame_TownsCities.png)
