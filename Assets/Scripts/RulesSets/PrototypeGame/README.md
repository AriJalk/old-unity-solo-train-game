# PrototypeGame Ruleset Architecture

The **PrototypeGame** ruleset serves as a foundational template demonstrating how to combine all core modules into a functioning turn-based, hex-grid logistics/card game. It showcases the basic architecture and flow you can extend for your own game.

---

## High-Level Overview

The ruleset implements two main domains:

- **Card Domain** — manages the state and visual representation of cards.
- **Map Domain** — manages the hex grid tiles and game map objects.

Each domain has **logic** and **scene** layers, each with state and manager classes to encapsulate their responsibilities.

---

## Key Components

### Logic Layer
- **LogicCardState & LogicCardStateManager**  
  Maintain and manipulate the game’s card state.

- **LogicMapState & LogicMapStateManager**  
  Maintain and manipulate the game’s map state.

### Scene Layer
- **SceneCardState & SceneCardStateManager**  
  Listen to events and update card visuals accordingly.

- **SceneMapState & SceneMapStateManager**  
  Listen to events and update map visuals accordingly.

- **SceneCardStateManipulator & SceneMapStateManipulator**  
  These manipulators handle direct manipulation of scene objects (e.g., animations, user interaction), but they are internal to their respective scene managers and not exposed elsewhere.

---

## Event Flow

1. **Commands** trigger changes through the `CommandRequestEvents`.  
2. **Command Request Handlers** receive these events and call the appropriate **logic managers** to update game state.  
3. After logic updates, **scene events** are fired to notify the scene managers.  
4. **Scene managers** listen to these events and update visuals or manipulate scene objects through their internal manipulators, which are hidden from the rest of the program.  
5. This keeps **logic and scene layers decoupled** — the scene managers act as “black boxes,” only interacting through events.

---

## State Machine Integration

- A **state machine** drives the gameplay flow by dispatching commands based on the current game state.  
- It listens for game events and changes states accordingly.  
- This enforces clear **rules flow** and centralizes control of game logic progression.

---

## Summary

The PrototypeGame ruleset is a complete, modular example of combining:

- **Logic + Scene separation**  
- **Event-driven communication**  
- **Command pattern for state manipulation**  
- **State machine for game flow control**

It provides a solid base to build complex turn-based strategy games on top of the core framework modules.

---

For detailed implementation, see the source in `/Scripts/RulesSets/PrototypeGame`.
