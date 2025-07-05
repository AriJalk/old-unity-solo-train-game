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
[![Watch on YouTube](https://img.youtube.com/vi/FLBy0de4PSg/hqdefault.jpg)](https://www.youtube.com/watch?v=FLBy0de4PSg)

---

## Usage

- Clone the repo and open in Unity.
- Review the modules under `Assets/Modules/` for reusable systems.
- Use the `PrototypeGame` as a template to build a new ruleset by:
  - Creating a custom implementation of `IRulesSet`
  - Defining your command types, validators, and logic/scene state managers
  - Optionally adding scene-side utilities like a `RulesLoader` to construct and inject your ruleset into the runtime

> 🔄 **Multiple rulesets can coexist in the same repository.**  
> At runtime, the active ruleset is selected by injecting an `IRulesSet` implementation into the `GameSceneManager`, which serves as the scene-level orchestrator.  
> A sample `RulesLoader` is included in the `PrototypeGame` ruleset. It constructs the ruleset's root object and injects it into `GameSceneManager` at startup.
