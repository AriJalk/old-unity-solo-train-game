# Turn-Based Hex Strategy Framework for Unity

A modular foundation for building **3D turn-based hex strategy games** in **Unity**.  
The project focuses on separating logic and visuals, defining game flow via state machines and commands, and structuring systems for reuse across multiple rulesets.

---

## Overview

This is a restart and architectural rewrite of the earlier `old-prototype-game` branch. It shifts focus from a specific game to a general-purpose, extensible framework.

- All logic is written in 2D space and mapped into 3D scenes.
- Built as a set of Unity assemblies for modular compilation and minimal rebuilds.
- Gameplay is driven by a ruleset-defined state machine and command/event system.
- Designed to allow fast iteration on different rulesets for future game projects.

> ✳️ This framework also serves as the groundwork for a personal passion project — a solo digital cube rail deckbuilder I hope to release someday.

> ⚠️ While designed with modularity, this framework reflects my own specific design philosophy optimized for a solo, turn-based hex/card game. It may not generalize easily to other game genres, but is shared as a complete example of applied system design in Unity.


---

## Structure

The repository includes:

- `Assets/Modules/` – Independent systems such as input, hex coordinates, card interaction, etc.
- `Assets/Scripts/RulesSets/PrototypeGame/` – A working example ruleset that shows how to integrate all modules into a full game loop.

---

## Key Modules

- **CommonEngine** – Input handling, drag interaction, raycasting layers, prefab management, object pooling, and camera control.
- **HexSystem** – Hex coordinate handling based on [Red Blob Games' hex grid guide](https://www.redblobgames.com/grids/hexagons/), including layout utilities and coordinate-to-world mapping.
- **CardSystem** – Manages visual card hand, drag-and-drop interaction, and drop targets.
- **TurnBasedHexEngine** – A minimal command and state machine layer for managing logic and scene state transitions.

---

## PrototypeGame Ruleset

A minimal sample ruleset inspired by cube rail mechanics:

- Defines turn structure using a custom `IStateMachine` implementation.
- Dispatches commands to mutate logic state and update the scene via request events.
- Uses separate logic/scene state managers for both map and card domains.
- Demonstrates a closed gameplay loop: play card → execute effect → next state.
- Not a full game — intended as a testbed to simulate rules and interaction patterns.

---

## Implementation Notes

- Game logic and visuals are strictly separated.
- Commands trigger request events, which update logic state and then scene state.
- All modules are compiled as separate assemblies for faster iteration.
- Scene state classes are accessed only through events, acting as black-box receivers.
- Input is funneled through a central input manager exposed by `CommonServices`.

---

## Demo

Gameplay overview screenshot:  
![Gameplay Snapshot](ReadmeScreenshots/PrototypeSnapshot.png)

Video demonstration (build → produce + retrieve → transport):  
[![Watch on YouTube](https://img.youtube.com/vi/PlEBeimexf8/hqdefault.jpg)](https://www.youtube.com/watch?v=PlEBeimexf8)

---

## Usage

- Clone the repo and open in Unity.
- Review the modules under `Assets/Modules/` for reusable systems.
- Use the `PrototypeGame` as a template to build a new ruleset by:
  - Creating a custom implementation of `IRulesSet`
  - Defining your command types, validators, and logic/scene state managers
  - Injecting your `IRulesSet` instance into the scene's `GameSceneManager`, which manages runtime flow
  - Optionally adding a `RulesLoader` MonoBehaviour to the scene to handle the construction and injection of your ruleset

> 🔄 **Multiple rulesets can coexist in the same repository.**
> The framework determines which ruleset to run at runtime via dependency injection into the `GameSceneManager`.
> This is the entry point for the game loop.
> The included `RulesLoader` in the `PrototypeGame` ruleset demonstrates how to build the ruleset root and pass it to the manager.

