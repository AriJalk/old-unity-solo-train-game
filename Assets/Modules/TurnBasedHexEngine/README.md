# TurnBasedHexEngine

The **TurnBasedHexEngine** is a modular foundation for building turn-based games over hex grids. It acts as a coordinator for scene logic, game state, and player interaction using a command-driven workflow and game state machines.

---

## 🧩 What It Offers

- **Command System**  
  Defines a structure for handling turn-based interactions via `ICommand` and `CommandManager`.  
  - Commands are dispatched by the `IStateMachine` to manipulate game state indirectly.
  - Execution flows through ruleset-defined **Command Request Events**, allowing logic and scene state to respond in isolation.
  - Undo functionality is supported (no redo yet).

- **State Machine Framework**  
  Uses `IStateMachine` and `StateMachineManager` to drive turn logic and enforce gameplay flow.  
  State machines dispatch commands and determine what player input is valid at each phase.

- **Scene Tile Control**  
  `HexGridController` instantiates and manages visual tile objects, built on top of the `HexSystem`.

- **Ruleset-Driven Design**  
  The engine itself is agnostic. All game-specific logic—such as what commands exist, what states should be tracked, and how events are wired—is defined inside a **Ruleset**, via the `IRulesSet` contract.

---

## 🔗 Integration Notes

- The engine doesn’t assume any domain model. Each ruleset must define:
  - Logic and scene state managers
  - Command request event handlers
  - Scene manipulators (optional)  
- Communication is done through custom events, decoupling input → logic → scene updates.

---

## ♻️ Reusability

Best suited for **hex-based, turn-oriented games** that use indirect command dispatch and benefit from modular, ruleset-defined behavior.  
Flexible enough for prototyping and scaling to multi-phase strategy games.

---

## ✅ Reference Ruleset

A working ruleset is provided as `PrototypeGame`, demonstrating how to:
- Define and bind commands
- Initialize game state
- Set up event pipelines and scene components
