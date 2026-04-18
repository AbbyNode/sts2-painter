# The Artist → STS2 Painter: Requirements

> **Source:** [The Artist — Slay the Spire 1 Mod](https://steamcommunity.com/sharedfiles/filedetails/?id=2808845989) by Diamsword (originally by Vex'd)
>
> This document captures the full requirements for recreating "The Artist" STS1 mod as "The Painter" for Slay the Spire 2.

---

## Table of Contents

- [Character](#character)
- [Core Mechanics](#core-mechanics)
- [Keywords](#keywords)
- [Paint Colors](#paint-colors)
- [Cards](#cards)
  - [Starter Cards](#starter-cards)
  - [Common Cards (20)](#common-cards-20)
  - [Uncommon Cards (39)](#uncommon-cards-39)
  - [Rare Cards (17)](#rare-cards-17)
- [Relics](#relics)
- [Potions](#potions)
- [Powers](#powers)
- [Implementation Notes](#implementation-notes)

---

## Character

| Attribute | Value |
|---|---|
| **Name** | The Painter (STS2) / The Artist (STS1) |
| **Gender** | Neutral (they/them) |
| **Starting HP** | 70 |
| **Color** | White (#ffffff) |
| **Starting Deck** | 5× Strike, 5× Defend, 1× Neo-Neutralize |
| **Starting Relic** | Custom base relic (see [Relics](#relics)) |
| **Card Energy** | 3 energy per turn (standard) |

---

## Core Mechanics

### The Canvas

The Canvas is the central UI element unique to The Artist/Painter. It is a persistent in-combat panel where the player "paints" colors throughout the turn.

- **Paint Colors** onto the Canvas by playing cards with "Paint [Color]" effects
- Each color added contributes its own gameplay effect to the resulting card
- **Right-click the Canvas** (or equivalent STS2 interaction) to **create a Painting** — a temporary card added directly to your hand
- The Painting's effects are determined by the combination of colors currently on the Canvas
- The Canvas persists across turns within a combat, but resets between combats
- Paintings base cost: **1 energy** (balancing change from original 0-cost)
- Limited to **1 Painting creation per turn** (balance change)

### Darken the Canvas

- Certain cards "Darken" the Canvas
- **Darkening increases the effectiveness** of all painted colors on the Canvas (e.g., more damage, more block)
- **Darkening also increases the energy cost** of the resulting Painting card
- Can be darkened multiple times for stacking effect

### Painting Card Creation

When the player creates a Painting from the Canvas, the resulting card:

- Is added directly to the player's hand
- Has effects based on each color present (see [Paint Colors](#paint-colors))
- Has energy cost based on number of darkenings
- Is Ethereal (exhausts at end of turn if not played) or Exhaust-on-play (TBD — verify)
- The Canvas is cleared after creating a Painting

---

## Keywords

| Keyword | Description |
|---|---|
| **Paint [Color]** | Add the specified color to the Canvas |
| **Darken** | Increase the Canvas darkness level by 1 — boosts Painting effects but increases energy cost |
| **Clear** | This effect activates **only if the Canvas has no colors on it** |
| **Chromatic** | This effect activates **only if the Canvas has exactly 2 different colors** (regardless of the total amount of paint) |
| **Cursed** | A debuff applied to enemies. While Cursed, the enemy takes damage **each time the player paints** (adds a color to Canvas) |

---

## Paint Colors

### Primary Colors

| Color | Effect on Painting | Theme |
|---|---|---|
| **Red** | Deal damage | Offense / Fire |
| **Blue** | Gain Block | Defense / Water |
| **Yellow** | Draw cards (Painting exhausts itself) | Draw / Lightning |

### Mixed Colors

| Color | Mix | Effect on Painting | Theme |
|---|---|---|---|
| **Green** | Blue + Yellow | Draw cards (reduced cost, no auto-exhaust) | Growth / Poison |
| **Purple** | Red + Blue | Deal damage AND gain Block | Mystical / Arcane |
| **Orange** | Red + Yellow | Deal damage AND draw cards | Volatile / Multi-hit |

### Special Color

| Color | Effect | Theme |
|---|---|---|
| **Gray** | Negative — adds Wounds to deck or other penalties | Corruption / Waste |

> **Note on color mixing:** When you add a Red and a Blue paint to the canvas, the resulting painting gains Purple's combined effect (damage + block). Adding multiple of the same color (e.g., Red + Red) intensifies that color's effect. Darkening further amplifies all effects.

---

## Cards

The mod adds **76 collectible cards** total: 20 Common, 39 Uncommon, 17 Rare.

### Starter Cards

| Card Name | Type | Cost | Effect |
|---|---|---|---|
| **Strike** | Attack | 1 | Deal 6 damage |
| **Defend** | Skill | 1 | Gain 5 Block |
| **Neo-Neutralize** | Skill | 0 | Apply 1 Weak. Paint 1 Gray. *(replaced the original "Darken Drawing" starter)* |

### Common Cards (20)

> Cards confirmed from Steam Workshop comments and changelogs. Names marked with ✅ are confirmed from author (Diamsword) posts. Names marked with ❓ need verification from the actual mod files.

| # | Card Name | Type | Rarity | Description (Reconstructed) | Status |
|---|---|---|---|---|---|
| 1 | **Contrasting Strike** | Attack | Common | Deal damage. Paint 2 different colors. *(Part of "Contrasting" group — common cards with additional paint effects)* | ✅ Confirmed group |
| 2 | **Contrasting Guard** | Skill | Common | Gain Block. Paint 2 different colors. | ✅ Confirmed group |
| 3 | **Cursed Clash** | Attack | Common | Deal 7(9) damage. Apply Cursed to target. | ✅ Confirmed |
| 4 | **Prismatic Puncture** | Attack | Common | Deal 4(6) damage. Paint 1 color of your choice. | ✅ Confirmed |
| 5 | **Red Rage** | Attack/Skill | Common | Deal damage and Paint 1 Red. *(Part of "color action + paint" pattern)* | ✅ Confirmed |
| 6 | **Purple Pain** | Attack/Skill | Common | Deal damage and/or gain Block. Paint 1 Purple. | ✅ Confirmed |
| 7 | **Grumpy Graffiti** | Attack | Common | Deal high damage (~12). If Chromatic: Paint 1 Gray. *(Comparable to Wild Strike)* | ✅ Confirmed |
| 8 | **Double Draw** | Skill | Common | Draw 2 cards. | ✅ Confirmed (buffed) |
| 9 | **Helping Harsh** | Skill/Attack | Common | *(New card added in balance patch)* | ✅ Confirmed |
| 10 | **Soft Stimulation** | Skill | Common | *(Buffed in balance patch — paint/block related)* | ✅ Confirmed |
| 11–20 | *(Additional commons)* | Various | Common | *Names not publicly documented. Need extraction from mod files. Likely include more color-paint cards following naming patterns (e.g., Blue Brush, Yellow Yield, etc.)* | ❓ Need verification |

### Uncommon Cards (39)

| # | Card Name | Type | Rarity | Description (Reconstructed) | Status |
|---|---|---|---|---|---|
| 1 | **Crushing Canvas** | Attack | Uncommon | Deal damage based on the number of colors on the Canvas. *(Community feedback: needs buff to include exhausted paintings)* | ✅ Confirmed |
| 2 | **Sweeping Sadness** | Skill | Uncommon | Gain 12(15) Block. Add Gray Gloom to hand. | ✅ Confirmed |
| 3 | **Palette Pick** | Skill | Uncommon | Choose a color to Paint. If Yellow is chosen, Exhaust this card. | ✅ Confirmed |
| 4 | **Iconic Idea** | Skill | Uncommon | Paint Green. Gain 3 Block. *(Previously painted more Green, rebalanced)* | ✅ Confirmed |
| 5 | **Colored Collision** | Attack | Uncommon | *(Buffed in balance patch — paint/damage related)* | ✅ Confirmed |
| 6 | **Ending Effort** | Skill | Uncommon | *(Buffed in balance patch)* | ✅ Confirmed |
| 7 | **Innovating Ink** | Skill | Uncommon | *(Buffed in balance patch — paint/ink related)* | ✅ Confirmed |
| 8 | **Rainbow Razor** | Attack | Uncommon | *(Buffed in balance patch — rainbow/multi-color damage)* | ✅ Confirmed |
| 9 | **Safe Space** | Skill | Uncommon | *(Buffed in balance patch — defensive)* | ✅ Confirmed |
| 10 | **Satanic Surge** | Skill/Attack | Uncommon | Apply Cursed. *(Feedback: should prevent Curse from going down permanently)* | ✅ Confirmed |
| 11 | **Blank Bane** | Attack/Skill | Uncommon | Clear keyword synergy. Adds Gray Gloom to hand. | ✅ Confirmed |
| 12 | **Pollock's Potluck** | Skill | Uncommon | Paint multiple random colors. First color painted is Red. *(Upgraded version is Innate)* | ✅ Confirmed |
| 13 | **Super Schooling** | Skill | Uncommon | Gain Block. *(Reworked to be a block-giving Skill)* | ✅ Confirmed |
| 14 | **Many Minds** | Power/Skill | Uncommon | *(New card added in update)* | ✅ Confirmed |
| 15–39 | *(Additional uncommons)* | Various | Uncommon | *Names not publicly documented. Need extraction from mod files. Likely include more complex paint interactions, Chromatic synergy cards, Cursed synergy cards, and multi-color painting effects.* | ❓ Need verification |

### Rare Cards (17)

| # | Card Name | Type | Rarity | Description (Reconstructed) | Status |
|---|---|---|---|---|---|
| 1 | **Mysterious Masterpiece** | Power/Skill | Rare | *(Temporarily removed due to bugs with save/reload — card was replaced by Madness on reload)* | ✅ Confirmed |
| 2 | **Glorious Gallery** | Power | Rare | *(Reworked with a "better effect" per author)* | ✅ Confirmed |
| 3 | **Hue Huffer** | Power | Rare | Each time you create a Painting, Paint 1 additional color. *(Bug reported: didn't stack properly with multiple plays)* | ✅ Confirmed |
| 4–17 | *(Additional rares)* | Various | Rare | *Names not publicly documented. 2 rare cards were temporarily removed during balancing. Need extraction from mod files.* | ❓ Need verification |

### Status/Generated Cards

| Card Name | Type | Description |
|---|---|---|
| **Gray Gloom** | Status | Unplayable status card. Added to hand/deck by cards like Sweeping Sadness and Blank Bane. Playing it paints 2 Gray and adds 1 Wound. |
| **Painting** | Generated | The card created from the Canvas. Effects depend on colors painted. Cost depends on Darken level. |

---

## Relics

**Total: 4 relics** (1 Base, 3 Boss)

### Base Relic (Starting Relic)

| Relic Name | Rarity | Effect (Reconstructed) | Status |
|---|---|---|---|
| **Artist's Brush** (name TBD) | Starter | Canvas/painting synergy. *(Author note: "effect resets on deck shuffle." Likely: "The first Painting you create each shuffle cycle has enhanced effects" or similar)* | ❓ Name needs verification |

### Boss Relics

| # | Relic Name | Rarity | Effect | Status |
|---|---|---|---|---|
| 1 | *(Boss Relic 1)* | Boss | *(Not publicly documented)* | ❓ Need verification |
| 2 | *(Boss Relic 2)* | Boss | *(Not publicly documented)* | ❓ Need verification |
| 3 | *(Boss Relic 3)* | Boss | *(Not publicly documented)* | ❓ Need verification |

> **Community-suggested relic ideas** (from Steam comments, may or may not be in the mod):
> - "The first time per combat you create a painting, gain 1 energy"
> - "The first time per combat you play a painting, draw 1 card"
> - "The first time per combat you exhaust a painting, discard it instead"
> - "For every 5 times you Paint a color, Paint one more randomly"
> - "Every 10th Painting you play, plays twice (retain count after combat)"

---

## Potions

**Total: 3 potions** (1 Common, 1 Uncommon, 1 Rare)

| # | Potion Name | Rarity | Effect | Status |
|---|---|---|---|---|
| 1 | *(Common Potion)* | Common | *(Not publicly documented — likely paint/canvas related)* | ❓ Need verification |
| 2 | *(Uncommon Potion)* | Uncommon | *(Not publicly documented)* | ❓ Need verification |
| 3 | *(Rare Potion)* | Rare | *(Not publicly documented)* | ❓ Need verification |

---

## Powers

Powers are persistent buffs gained by playing Power-type cards. Known powers in the mod:

| Power Name | Source Card | Effect | Status |
|---|---|---|---|
| **Hue Huffer** | Hue Huffer (card) | Each Painting you create has 1 additional color painted on it. Stacks. | ✅ Confirmed |
| **Glorious Gallery** | Glorious Gallery (card) | *(Reworked — exact effect TBD)* | ✅ Confirmed |
| **Many Minds** | Many Minds (card) | *(New — exact effect TBD)* | ✅ Confirmed |
| **Mysterious Masterpiece** | Mysterious Masterpiece (card) | *(Exact effect TBD — temporarily removed due to bugs)* | ✅ Confirmed |
| **Cursed** (debuff) | Applied by Cursed Clash, Satanic Surge, etc. | Enemy takes X damage each time the player paints a color | ✅ Confirmed |

---

## Implementation Notes

### What Already Exists in the STS2 Repo

The repository (`AbbyNode/sts2-painter`) has the following scaffolding in place:

- ✅ Character definition (The Painter — 70 HP, neutral, white color)
- ✅ Card/Relic/Potion/Power base abstract classes
- ✅ Card pool, relic pool, potion pool configurations
- ✅ Localization framework (character strings done, card/relic/power/potion strings empty)
- ✅ Asset path system for images
- ✅ Mod manifest and build system (Godot + .NET 9.0)
- ✅ CI/CD pipeline
- ❌ No actual card implementations
- ❌ No actual relic implementations
- ❌ No actual potion implementations
- ❌ No actual power implementations
- ❌ No card/relic/potion/power art assets
- ❌ No Canvas UI system

### Key Technical Challenges for STS2

1. **Canvas UI** — The Canvas is a custom, interactive in-combat UI element. This will require significant custom Godot UI work.
2. **Dynamic Card Generation** — Paintings are generated at runtime based on canvas state. STS2 modding support for dynamic card generation needs investigation.
3. **Color Mixing System** — Need a system to track canvas state (colors, darken level) and calculate resulting painting effects.
4. **Right-click Interaction** — STS1 uses right-click on Canvas to create paintings. STS2 may need a different interaction model.
5. **Painting Limit** — Enforce 1 painting per turn limit.

### Priority Order for Implementation

1. **Phase 1: Core Mechanics** — Canvas system, Paint colors, Painting generation
2. **Phase 2: Starter Deck** — Strike, Defend, Neo-Neutralize, base relic
3. **Phase 3: Common Cards** — All 20 common cards
4. **Phase 4: Uncommon Cards** — All 39 uncommon cards
5. **Phase 5: Rare Cards** — All 17 rare cards
6. **Phase 6: Relics** — 1 base + 3 boss relics
7. **Phase 7: Potions** — 3 potions
8. **Phase 8: Polish** — Art, balance, localization, bug fixes

### Data Gaps

> ⚠️ **Important:** The complete card list with exact names, types, costs, and effects is **not publicly available** online. The STS1 mod's source code is not on GitHub, and no wiki or complete list exists. The information in this document was gathered from:
>
> - The [Steam Workshop page](https://steamcommunity.com/sharedfiles/filedetails/?id=2808845989)
> - Steam Workshop comments and discussions (by mod author Diamsword)
> - Community feedback and balance discussions
>
> **To get the complete card list, you will need to:**
>
> 1. Install the STS1 mod and browse the in-game card library, OR
> 2. Extract the mod's `.jar` file and read the localization JSON files, OR
> 3. Contact Diamsword (the mod author) directly for permission and card data
>
> Cards confirmed from author posts: ~25 of 76 total cards identified by name.

---

*Last updated: 2026-04-18*
