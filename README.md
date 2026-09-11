[![Unity](https://img.shields.io/badge/Unity-2022.3%2B%20%7C%20Unity%206-000000?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-9.0%2B-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Render Pipeline](https://img.shields.io/badge/URP-Universal%20Render%20Pipeline-4682B4?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/srp/Universal-Render-Pipeline)
[![Play on itch.io](https://img.shields.io/badge/Play%20on-itch.io-FA5C5C?style=for-the-badge&logo=itch.io&logoColor=white)](https://gurd62.itch.io/night-bus)
![Night Bus Game Cover](https://i.imgur.com/50VEmUQ.jpeg)

# Night Bus

A retro-styled PSX first-person psychological horror game built in Unity (C#). Players navigate an eerie bus stop across seven distinct nights, managing atmospheric tension, environmental tasks, and evasive survival mechanics.

Playable Desktop Build: [gurd62.itch.io/night-bus](https://gurd62.itch.io/night-bus)

---

## Executive Summary (Recruiter & HR Overview)

* **Role:** Solo Developer (Gameplay Programming, Systems Architecture, Level Design)
* **Engine & Language:** Unity Engine | C# (.NET)
* **Development Cycle:** 13 days initial sprint to MVP release + 2 days post-launch updates based on player feedback
* **Core Technical Highlights:**
  * **Data-Driven Architecture:** Utilized Unity `ScriptableObjects` to define night-by-night configurations, timers, and entity spawn triggers without hardcoding progression logic.
  * **Event-Driven Communication:** Implemented C# `Action` delegates across decoupled managers (`GameLogic`, `NightManager`, `UIManager`, `BusLogic`) to maintain clean single-responsibility principles and prevent memory leaks.
  * **Interface-Driven Interaction:** Built a scalable `IInteract` interface for seamless environment and prop interactions.
  * **Retro PSX Rendering:** Integrated custom vertex-snapping and affine texture mapping shaders to achieve authentic 1990s console aesthetics.
* **Status:** Fully playable, polished, and hosted on Itch.io.

---

## Premise & Gameplay

You are stranded at a desolate bus stop at night. As seven distinct nights unfold, the environment evolves—from routine waiting and trash collection to unexplainable supernatural occurrences and survival chases. 

### Core Mechanics
* **Environmental Interaction:** Collect trash, interact with bus doors, and react to atmospheric triggers.
* **Pacing & Tension:** Timed bus arrival/departure sequences demand quick decision-making under stress.
* **Survival & Evasion:** Avoid oncoming hazards and outmaneuver pursuers on final nights. Failing to board the bus or colliding with threats triggers game-over states.

---

## Technical Architecture & Design Patterns

### 1. Data-Driven Progression (`NightSO`)
Nightly variations (timer lengths, atmospheric shifts, monster spawn coordinates, moon offsets) are encapsulated within `NightSO` ScriptableObject data assets. This decouples game parameters from execution scripts, allowing rapid iteration of level design without recompiling code.

### 2. Decoupled Event System (`Action` Delegates)
Game events—such as `OnBorderHit`, `OnBusDestroy`, `OnPlayerHit`, and `OnAllTrashCollected`—are broadcast using standard C# `Action` delegates. System managers subscribe to relevant notifications during `Start()` and clean up handlers in `OnDestroy()`, ensuring memory safety and loose coupling between UI, audio, physics, and game flow.

### 3. Modular Interaction System (`IInteract`)
Interactive elements implement the `IInteract` interface. The player controller casts raycasts to query for `IInteract` implementations, enforcing polymorphism and making it trivial to add new interactive objects without modifying player controller code.

### 4. Dynamic Sound & Aesthetic Pipeline
* Dynamic audio transitions (scream triggers, footstep tracking).
* Low-poly geometry paired with PSX post-processing shaders for authentic nostalgic horror atmosphere.

---

## Controls

| Action | Control |
| :--- | :--- |
| **Movement** | W / A / S / D |
| **Look Around** | Mouse |
| **Sprint** | Left Shift |
| **Interact** | E |

---

## Engineering Retrospective & Post-Mortem

### Accomplishments
* **Rapid Prototyping:** Shipped a full, end-to-end playable loop in 13 days.
* **Clean Code Structure:** Established strong architectural boundaries early using ScriptableObjects and C# delegates.
* **Community-Driven Iteration:** Published to Itch.io, gathered player analytics and feedback, and implemented UX patches.

### Key Engineering Lessons & Future Scope
* **UI Responsiveness:** Fixed baseline layout for 320x240 retro resolution; future projects will implement anchor-based responsive UI scaling for high-DPI displays.
* **Pacing & Player Attention:** Certain environmental scares were occasionally missed by fast-moving players. Future iterations will use dynamic camera cues or subtle lighting hints to guide player focus.
* **State Persistence & Checkpoints:** Returning players to the main menu upon failure created friction. Introducing mid-night checkpoints or fast restarts would improve the player retention flow.

---

## Play the Game

Experience the project directly on Itch.io:  
[Night Bus on Itch.io](https://gurd62.itch.io/night-bus)
