# CardSystem

The **CardSystem** module provides basic functionality for a draggable card hand UI. It supports displaying a set of cards, interacting with them, and handling drop logic for card-based actions.

---

## 🧩 What It Offers

- **Card Object Representation**  
  `CardObjectBase` is the visual component for cards in hand. Cards can be clicked and dragged, and are linked to internal logic via GUID-based identity (`IIdentifiable`).

- **Card Hub Management**  
  `CardObjectServices` manages all card visuals in the hand — spawning, clearing, and controlling layout.

- **Drop Target System**  
  Any object implementing `ICardDropTarget` can receive dragged cards. The interface exposes:
  - `OnCardDropEvent`
  - `SetActive(bool)`
  - `OnDrop(CardObjectBase card)`

---

## 🧩 How It's Used

- Cards are manipulated by player drag input and dropped on valid targets.
- The scene-level logic is connected through the `SceneCardStateManipulator` (from the ruleset), which interacts directly with the `CardObjectServices`.

---

## 🔗 Integration Notes

- This system is only referenced internally by the scene layer of the ruleset — it's not accessed globally or reused directly.
- The card visuals are synced manually with logic state — no automatic binding layer is enforced.
- Does not handle deck logic or effects — only scene-level card interaction.

---

## ♻️ Reusability

Best suited for any game with a **hand of cards mechanic** — particularly where cards are **dragged and dropped onto the scene** to trigger effects.

---
