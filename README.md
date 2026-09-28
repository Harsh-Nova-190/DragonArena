# Dragon Arena

A small 2.5D dragon battle prototype developed in Unity as part of the Dexhigh Services Junior Unity Developer technical assessment.

The project features a player-controlled dragon fighting an AI-controlled dragon in a top-down arena. The battle focuses on responsive movement, three distinct abilities, cooldown-based combat, simple enemy AI, health management, animations, VFX, and a winner/restart flow.

---

## Gameplay

The player controls a dragon and fights an AI-controlled opponent.

### Player Abilities

| Ability | Key | Type | Damage | Cooldown |
|---|---:|---|---:|---:|
| Fire Attack | `1` | Ranged | 25 | 3 sec |
| Tail Attack | `2` | Melee | 35 | 2 sec |
| Fly Attack | `3` | Area / Air Attack | 50 | 7 sec |

### Controls

| Input | Action |
|---|---|
| `W` | Move Forward |
| `A` | Move Left |
| `S` | Move Backward |
| `D` | Move Right |
| `1` | Fire Attack |
| `2` | Tail Attack |
| `3` | Fly Attack |

The game uses keyboard-based movement rather than click-to-move.

---

## Features

- 2.5D top-down battle arena
- Player-controlled dragon
- AI-controlled enemy dragon
- Three unique combat abilities
- Ability cooldown system
- Health system
- Health bars for both dragons
- Ability cooldown UI
- Attack animations
- Movement animations
- Fly / takeoff / landing animations
- Death animations
- Fire breath VFX
- Tail impact VFX
- Hit feedback
- Enemy AI state machine
- Distance-based AI ability selection
- Winner screen
- Restart battle functionality
- URP lighting and shadows
- Arena boundaries

---

## AI Behaviour

The enemy dragon uses a simple state machine:

```text
Idle
  ↓
Chase
  ↓
Attack
