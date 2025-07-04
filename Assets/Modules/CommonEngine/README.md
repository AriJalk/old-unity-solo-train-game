# CommonEngine

The **CommonEngine** module provides essential cross-cutting systems shared across the entire game framework. It handles input, prefab management, raycasting config, material assignment, UI helpers, and more — all from a single access point: `CommonServices`.

---

## 🧩 What It Offers

- **Input System**  
  Abstracted input via `InputManager`, exposed through `CommonServices.InputEvents`. Includes current mouse position, delta, and input locking to prevent interactions during transitions or animations.

- **Prefab & Material Managers**  
  Centralized pooling and prefab instantiation (`PrefabManager`) as well as dynamic material access (`MaterialManager`).

- **Raycast Configuration**  
  `RaycastConfig` stores layer masks for various interactive objects. These are dynamically switched depending on the current game state or UI context.

- **Camera & UI Utilities**  
  Includes general-purpose components such as:
  - `DragControl`: Fullscreen drag surface for rotating an orthographic camera
  - `MovableObject`, `RotatedObject`: Simple input-driven object manipulation
  - `LockingUIPanel`, `GridSizeScaler`, and option selection UI for basic UX control

---

## 🎯 Intended Use

This module is geared toward **classic strategy games** using an **orthographic camera**, offering ready-made foundations for:
- Camera control
- Object interaction
- General-purpose scene utilities

It’s especially useful in **turn-based game structures**, when paired with the `TurnBasedHexEngine` or other rulesets.

---

## 🔗 Integration Notes

- **Access Point**: Only `CommonServices` is referenced externally; all core systems are accessed from there.
- **Encapsulated Logic**: External systems can read data like mouse input or raycast masks, but cannot modify internal logic.
- **Used In**: Referenced by rulesets to configure gameplay context, pool scene objects, and control global input behavior.

---

## ♻️ Reusability

While designed with hex-based, turn-based strategy games in mind, `CommonEngine` is general-purpose enough to support a variety of scene-driven projects that need centralized input, pooling, and utility services.

---
