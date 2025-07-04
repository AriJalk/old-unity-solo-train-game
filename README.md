# Turn-Based Hex Strategy Framework for Unity

Welcome to the Turn-Based Hex Strategy Framework — a modular, extensible foundation for building **3D turn-based hex strategy games** in **Unity**, using a clean separation of logic and scene, and a ruleset-driven architecture.

---

## Overview

This project offers a set of reusable **Unity modules** and a working **PrototypeGame** ruleset that demonstrate how to structure a turn-based hex game cleanly and flexibly.

> 🧭 This framework is a **restart and architectural evolution** of the older `old-prototype-game` branch, redesigned from the ground up for better modularity, scalability, and clarity.

The framework is designed for:
- Easy iteration on gameplay rules.
- Clear separation between **logic state** and **scene state**.
- 2D abstract logic running in a **3D Unity scene**.
- Fully modular design, allowing developers to pick and integrate only the systems they need.

---

## Key Modules

- **CommonEngine**  
  Provides foundational utilities for input handling, camera controls, raycasting, drag interaction, prefab instantiation, object pooling, and other general-purpose game behaviors.

- **HexSystem**  
  Offers a coordinate system and layout tools based on Red Blob Games’ hex grid standard. Converts between logical coordinates and 3D world positions, enabling spatially coherent hex tile placement.

- **CardSystem**  
  Lightweight system for hand-based card interaction. Includes drag/drop behavior, visual management of card objects, and drop targets.

- **TurnBasedHexEngine**  
  A minimal turn-based orchestration layer. It includes:
  - A **CommandManager** to coordinate game actions.
  - A **StateMachineManager** to manage the lifecycle and flow of the ruleset state machine.
  - Interfaces for rule-specific state machines and logic.
  - The **HexGridController** for scene-level tile handling.

  ⚠️ This module does **not** define a built-in event system — instead, it provides reusable components to be **driven by your custom ruleset**.


---

## PrototypeGame Ruleset

The **PrototypeGame** demonstrates how to combine the core modules into a functional Unity ruleset:

- Implements a cube-rails-inspired game on a hex map with card-based interactions.
- Shows how to define a ruleset as a **state machine** and execute logic using **commands**.
- Separates logic and scene into their own state/manipulator layers, with all interactions driven by **command requests**.
- Handles all game flow within the ruleset — including the event system, validation, and context transitions.
- Uses 2D logical coordinates and systems that are **mapped to a 3D Unity scene** to produce modern board game visuals.

---

## Design Philosophy

- **2D logic / 3D world:** Game mechanics operate in simple, cacheable logic systems while being visually rendered in 3D.
- **Ruleset-first development:** Rules are written as a flow of state transitions and commands, allowing gameplay evolution without breaking structure.
- **Modularity and separation of concerns:** Core systems are organized into assemblies to keep logic, scene, and input isolated and composable.

---

## Getting Started

- Look into the `Modules` folder to explore reusable Unity systems.
- Navigate to `Scripts/RulesSets/PrototypeGame` for a working example of how to define a complete game.
- Use `CommandManager`, state machines, and rule-specific events to define your own unique ruleset and flow.

---

This project is great for Unity developers seeking a clean way to build board-style turn-based strategy games with flexible rules, reusable code, and a logical separation of gameplay and visuals.

---

*Ready to build your own strategy game? Start with PrototypeGame and customize away!*
