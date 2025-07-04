# HexSystem

The **HexSystem** module provides the foundational data structures and utilities for working with hexagonal tile maps using axial coordinates. It’s based on [Red Blob Games' hex grid guide](https://www.redblobgames.com/grids/hexagons/), with caching for reuse and ID-based referencing.

---

## 🧩 What It Offers

- **HexCoord**  
  A unique axial coordinate identifier for hex tiles, used both in logic and as the key identifier for GameObjects.  
  - Created only through `HexCoord.GetCoord(Q, R)` to enforce **coordinate caching** and avoid redundant instances.
  - Overrides `Equals` and `GetHashCode` for safe dictionary use.

- **HexLayout**  
  Encapsulates tile placement logic in world space (offset, spacing, etc.).

- **HexCoordUtilities**  
  A static utility class for coordinate math like range, distance, neighbor lookup, etc.

---

## 🔗 Integration Notes

- **Scene ↔ Logic Bridge**: HexCoord acts as the link between logic tiles and their visual representations, removing the need for GUIDs or complex mapping.
- Independent of any specific game logic — no scene or gameplay references inside.

---

## ♻️ Reusability

Designed to be reused across any **hex-based strategy or puzzle game**.  
Stable and performant due to the caching and pure math operations.

---
