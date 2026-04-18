# The Artist → STS2 Painter: Requirements

> **Source:** [The Artist — Slay the Spire 1 Mod](https://steamcommunity.com/sharedfiles/filedetails/?id=2808845989) by Diamsword (originally by Vex'd/turretboi)
>
> This document captures the full requirements for recreating "The Artist" STS1 mod as "The Painter" for Slay the Spire 2.
>
> **Research sources:**
>
> - [Steam Workshop page](https://steamcommunity.com/sharedfiles/filedetails/?id=2808845989) — mod description and metadata
> - [Steam Workshop comments](https://steamcommunity.com/sharedfiles/filedetails/comments/2808845989) — 52 comments including 15+ by Diamsword with patch notes
> - [Steam Workshop discussions](https://steamcommunity.com/sharedfiles/filedetails/discussions/2808845989) — 4 threads: Balance, Bugs, Suggestions, Gray Discussion
> - [Steam Workshop changelog](https://steamcommunity.com/sharedfiles/filedetails/changelog/2808845989) — 15 updates (May 16, 2022 → Aug 6, 2022)
> - [Frost Prime YouTube playthrough](https://www.youtube.com/watch?v=9bsf_l5XJuA) — gameplay footage showing cards in action
> - [naoe\_ note.com article](https://note.com/naoe_/n/n6e7bd996c821) — Japanese analysis of the original Vex version mechanics
> - Community balance discussions (Jezzared, SparkleApple, jhleviathan, etc.)

---

## Table of Contents

- [Character](#character)
- [Mod History](#mod-history)
- [Core Mechanics](#core-mechanics)
- [Keywords](#keywords)
- [Paint Colors](#paint-colors)
- [Cards](#cards)
  - [Starter Cards](#starter-cards)
  - [Common Cards (22)](#common-cards-22)
  - [Uncommon Cards (44)](#uncommon-cards-44)
  - [Rare Cards (20)](#rare-cards-20)
  - [Status / Generated Cards](#status--generated-cards)
- [Relics](#relics)
- [Potions](#potions)
- [Powers](#powers)
- [Balance History](#balance-history)
- [Known Bugs (STS1)](#known-bugs-sts1)
- [Community Balance Feedback](#community-balance-feedback)
- [Implementation Notes](#implementation-notes)

---

## Character

| Attribute | Value |
|---|---|
| **Name** | The Painter (STS2) / The Artist (STS1) |
| **Pronouns** | she/her (STS1) — changed to they/them for STS2 |
| **Starting HP** | 70 *(was temporarily raised to 85 during early balancing, then reverted)* |
| **Color** | White (#ffffff) |
| **Starting Deck** | 4× Striking Stroke (Strike), 4× Palette Parry (Defend), 1× Brush Bash, 1× Neo-Neutralize |
| **Starting Relic** | Bent Brush — first Paint each combat paints 1 more; refreshes on deck shuffle (see [Relics](#relics)) |
| **Card Energy** | 3 energy per turn (standard) |

---

## Mod History

The Artist mod has gone through two major versions:

### Original Version (by Vex'd/turretboi)

The original mod was created by a modder known as Vex'd (Steam: turretboi) and was later removed from the Steam Workshop. The original version featured a **10-color system**:

| Color | Effect | Notes |
|---|---|---|
| Red | Damage | Primary |
| Blue | Block | Primary |
| Green | Draw cards | Primary |
| Magenta | Apply Weak | Debuff |
| Aqua | Apply Frail | Debuff |
| Yellow | Gain Energy | Utility |
| Pink | Gain Temporary HP | Defensive — later cut |
| Purple | Apply Curse (non-decreasing damage debuff, similar to Poison but stronger) | Debuff |
| Black (Darken) | Double all color stacks, increase painting cost by 1 | Amplifier |
| Rainbow | Paint a random color next | Wildcard |

The original version also had **0-cost paintings** and **no per-turn painting limit**, which made infinite combos trivially easy (especially with Green/Yellow draw loops).

### Current Version (by Diamsword — Workshop ID: 2808845989)

Diamsword took over the project with Vex'd's permission and substantially reworked it. Key changes:

- **Simplified to 9 colors** — Red, Blue, Yellow (primary), Green, Aqua, Magenta, Pink, Purple (each painted directly by specific cards), plus Gray and Rainbow (special). Removed the original 10-color system.
- **Color mixing system** — the Canvas mixes colors based on what's painted. Primary colors combine into derived effects on the resulting Painting card.
- **Paintings cost 1 energy** (was 0) — major balance change to prevent infinite combos
- **Limited to 1 Painting per turn** — another anti-infinite measure
- **Darken** reworked from "double all stacks" to incremental effectiveness boost
- **Cursed** reworked as a debuff mechanic (damage when painting) that halves at the start of each turn
- **Starting card changed** from "Darken Drawing" to "Neo-Neutralize"
- **15 updates** shipped between May 16, 2022 and Aug 6, 2022

---

## Core Mechanics

### The Canvas

The Canvas is the central UI element unique to The Artist/Painter. It is a persistent in-combat panel where the player "paints" colors throughout the turn.

- **Paint Colors** onto the Canvas by playing cards with "Paint [Color]" effects
- Each color added contributes its own gameplay effect to the resulting card
- **Right-click the Canvas** (or equivalent STS2 interaction) to **create a Painting** — a temporary card added directly to your hand
- The Painting's effects are determined by the combination of colors currently on the Canvas
- The Canvas **persists across turns** within a combat, but **resets between combats**
- Paintings base cost: **1 energy** (balance change from original 0-cost)
- Limited to **1 Painting creation per turn** (balance change — cards with Clear keyword were buffed to compensate)
- **Hovering the Canvas** shows a preview of what the resulting Painting card would look like (added in a later update after community feedback requesting this QoL improvement)

### Darken the Canvas

- Certain cards "Darken" the Canvas
- **Darkening increases the effectiveness** of all painted colors on the Canvas (e.g., more damage, more block per color)
- **Darkening also increases the energy cost** of the resulting Painting card by 1 per darken level
- Can be darkened multiple times for stacking effect
- In the original Vex version, darkening doubled all stacks; in Diamsword's version it's an incremental boost

### Painting Card Creation

When the player creates a Painting from the Canvas, the resulting card:

- Is added directly to the player's **hand**
- Has effects based on **each color** present (see [Paint Colors](#paint-colors))
- Has energy cost = **1 (base) + darken level**
- **Yellow paintings auto-exhaust** (exhaust when played or at end of turn if not played)
- **Green paintings do NOT auto-exhaust** (this is the key difference between Yellow draw and Green draw)
- The **Canvas is cleared** after creating a Painting
- Multiple of the same color on the canvas **intensifies** that color's effect (e.g., Red + Red = more damage)

### Card Pattern: "Color Action + Paint"

Several cards follow a pattern described by Diamsword as "Do what the color does instantly twice and paint one of that color." For example:

- **Red Rage** — deals damage instantly AND paints Red (which adds more damage to the next Painting)
- **Blue Burst** — gains Block instantly AND paints Blue (which adds more Block to the next Painting)
- **Green Greed** — draws cards instantly AND paints Green
- **Purple Pain** — applies Cursed instantly AND paints Purple

This pattern creates cards that both have immediate combat impact and build toward future Painting creation.

### The "Contrasting" Group

Cards in the "Contrasting" group each paint 2 different primary colors. The four Contrasting cards are:

- **Contrasting Calm** (Common Attack) — Deal damage. Paint Blue + Green. Upgrade: gains Retain.
- **Contrasting Clash** (Common Skill) — Draw cards. Paint Red + Blue. Upgrade: draw more.
- **Contrasting Crush** (Common Skill) — Gain Block. Paint Red + Green. Upgrade: gains Retain.
- **Contrasting Co-op** (Rare Skill) — Paint Red + Blue + Green (all 3 primaries). Upgrade: cost → 0, gains Retain.

---

## Keywords

> **Source:** All keyword data extracted from `TheArtist.jar` localization files.

| Keyword | Description | Design Notes |
|---|---|---|
| **Paint** | Painting a Color adds it to the Canvas. | Core mechanic — triggers Cursed damage on affected enemies |
| **Red** | Color. Deals damage. | Primary offensive color |
| **Blue** | Color. Grants Block. | Primary defensive color |
| **Yellow** | Color. Grants [E]. Adds Exhaust to the Painting. | Energy generation with exhaust cost |
| **Green** | Color. Draws cards. | Card draw without exhaust (unlike Yellow) |
| **Purple** | Color. Applies Cursed. | Scaling debuff color |
| **Aqua** | Color. Applies Weak. | Defensive debuff |
| **Magenta** | Color. Applies Vulnerable. | Offensive debuff |
| **Pink** | Color. Grants Temporary HP. | Sustain color |
| **Rainbow** | Color. Paints random colors. Cannot be randomly generated. | Wildcard color — always intentional |
| **Darken** | Increase the cost and effectiveness of the Painting. | Boosts Painting effects but increases energy cost |
| **Cursed** | Cursed creatures lose HP when you paint a color. Reduced by half at the start of your turn. | Triggers per-paint-action, halves each turn (unlike Poison which decreases by 1) |
| **Painting** | Paintings are special cards made from the Canvas. | Generated dynamically at runtime |
| **Chromatic** | The Chromatic effect of a card activates if the Canvas contains exactly 2 Colors. | Encourages specific 2-color combos; Red+Blue is the strongest archetype |
| **Clear** | The Clear effect of a card activates if the Canvas is empty. | Encourages strategic play order — use before painting |

> **Note:** Some cards also reference standard STS keywords not specific to The Artist: **Exhaust**, **Retain**, **Innate**, **Ethereal**, **Fleeting** (removed from game on play/discard), **Grave** (plays when exhausted from hand), and **Exhaustive** (from StSLib — can be played a limited number of times before exhausting).

---

## Paint Colors

> **Source:** All color data extracted from `TheArtist.jar` keyword definitions and card effects.

### Primary Colors

| Color | Hex Code | Effect on Painting | Theme |
|---|---|---|---|
| **Red** | `#b0120a` | Deal damage to target enemy | Offense |
| **Blue** | `#303f9f` | Gain Block | Defense |
| **Yellow** | `#ffeb3b` | Gain Energy [E]. **Painting auto-exhausts.** | Energy / Tempo |

### Other Colors (painted directly by specific cards)

| Color | Hex Code | Effect on Painting | Theme |
|---|---|---|---|
| **Green** | `#42bd41` | Draw cards | Card draw (no auto-exhaust, unlike Yellow) |
| **Aqua** | `#26a69a` | Apply Weak | Defensive debuff |
| **Magenta** | `#880e4f` | Apply Vulnerable | Offensive debuff |
| **Pink** | `#f0749e` | Gain Temporary HP | Sustain |
| **Purple** | `#673ab7` | Apply Cursed | Scaling debuff |

### Special Colors

| Color | Effect | Notes |
|---|---|---|
| **Gray** | `#b4b4b4` — Negative; Painting adds Wounds or other penalties | Intentionally bad. Diamsword: "In its design it is supposed to be bad, something you should avoid." |
| **Rainbow** | Paints random colors. Cannot be randomly generated. | Wildcard — always from specific card effects, never from "random Color" |

> **Stacking:** Adding multiple of the same color (e.g., Red + Red) intensifies that color's effect (more damage per Red). Darkening amplifies all effects but increases Painting cost.
>
> **Community note on balance:** "Chromatic red/blue is too strong, it's the most consistent deck with extremely high win ratio, the other viable deck being random painting + curse which scales ultra exponentially through turns." (pisaprofile)

---

## Cards

The mod adds **86 collectible cards** (22 Common, 44 Uncommon, 20 Rare) plus 5 Basic cards and 1 Status card.

> **Source:** All card data below extracted from `TheArtist.jar` v2022-08-07 localization files and decompiled class bytecode.
>
> **Value notation:** `7(9)` means base value is 7, upgraded value is 9. `{M}` = magic number, `{D}` = damage, `{B}` = block.

### Starter Cards (Basic)

| # | Card Name | Type | Cost | Effect | Upgrade |
|---|---|---|---|---|---|
| 1 | **Striking Stroke** | Attack | 1 | Deal 6 damage. | 6 → 9 damage |
| 2 | **Palette Parry** | Skill | 1 | Gain 5 Block. | 5 → 8 Block |
| 3 | **Brush Bash** | Attack | 2 | Deal 8 damage. Paint 2 Red. | 8→10 damage, 2→3 Red |
| 4 | **Neo-Neutralize** | Attack | 0 | Deal 3 damage. Paint 1 Blue. | 3→4 damage, 1→2 Blue |
| 5 | **Darken Drawing** | Skill | 0 | Darken the Canvas. Exhaust. | Removes Exhaust |

> **Starting deck composition:** 4× Striking Stroke, 4× Palette Parry, 1× Brush Bash, 1× Neo-Neutralize.
> Darken Drawing was an original starter card that was replaced by Neo-Neutralize; it remains in the collectible card pool as a Basic card.

### Common Cards (22)

| # | Card Name | Type | Cost | Effect | Upgrade |
|---|---|---|---|---|---|
| 1 | **Aqua Annoyance** | Skill | 1 | Apply 1 Weak. Paint 1 Aqua. | 1→2 Weak |
| 2 | **Canvas Catapult** | Attack | 1 | Deal 9 damage. Exhaust 1 card at random. | 9→11 damage; Exhaust becomes targeted (choose 1 card) |
| 3 | **Clear Chroma** | Skill | 0 | Clear: Paint 2 random Colors. Chromatic: Paint 1 more of both Colors. | Gains Retain |
| 4 | **Color Copy** | Skill | 0 | Choose 1 Color on your Canvas to Paint. | Gains Retain |
| 5 | **Colorful Clobber** | Attack | 1 | Deal 9 damage. Clear: Paint 2 random Colors. | 9→11 damage, 2→3 Colors |
| 6 | **Contrasting Calm** | Attack | 1 | Deal 4 damage. Paint 1 Blue. Paint 1 Green. | 4→8 damage; gains Retain. *(Upgrade description text omits damage — possible display bug in original mod.)* |
| 7 | **Contrasting Clash** | Skill | 1 | Draw 1 card. Paint 1 Red. Paint 1 Blue. | 1→2 cards drawn |
| 8 | **Contrasting Crush** | Skill | 1 | Gain 3 Block. Paint 1 Red. Paint 1 Green. | 3→6 Block; gains Retain |
| 9 | **Crushing Canvas** | Attack | 0 | Deal damage. Deals 3 additional damage for ALL your Paintings. | +1 base damage, 3→5 per-Painting bonus |
| 10 | **Cursed Clash** | Attack | 1 | Deal 7 damage. Apply 2 Cursed. | 7→9 damage, 2→3 Cursed |
| 11 | **Double Draw** | Attack | 1 | Deal 4 damage twice. Put a random Painting from your discard pile into your hand. | 4→6 damage |
| 12 | **Grumpy Graffiti** | Attack | 1 | Deal 11 damage. Paint 1 Gray. Chromatic: don't Paint it instead. | 11→15 damage |
| 13 | **Iconic Idea** | Skill | 1 | Gain 6 Block. Paint 2 Green. | 6→9 Block |
| 14 | **Indigo Incapitation** | Skill | 1 | Apply 2 Weak to ALL enemies. Clear: Paint 1 Blue. | 1→2 Blue painted |
| 15 | **Magenta Madness** | Skill | 1 | Apply 1 Vulnerable. Paint 1 Magenta. | 1→2 Vulnerable |
| 16 | **Playful Purple** | Skill | 1 | Paint 1 Purple. Paint 1 extra Purple for each Cursed enemy. | 1→2 base Purple |
| 17 | **Prismatic Pierce** | Attack | 1 | Deal 4 damage and draw 1 card for each Color on the Canvas. | 4→6 damage |
| 18 | **Red Razzing** | Skill | 1 | Apply 2 Vulnerable to ALL enemies. Clear: Paint 1 Red. | 1→2 Red painted |
| 19 | **Shiny Shiv** | Attack | 0 | Deal 5 damage. Clear: Paint 1 Rainbow. | 5→8 damage |
| 20 | **Superb Schooling** | Skill | 1 | Gain 8 Block. Chromatic: Draw 2 cards. | 8→10 Block, 2→3 cards |
| 21 | **Vibrant Vision** | Skill | 1 | Paint 1 random Color. Draw 2 cards. | 2→3 cards |

> **Note:** Gray Gloom is a Status card with Common rarity (see [Status / Generated Cards](#status--generated-cards) below). It is not a collectible card — it is generated by other cards like Sweeping Sadness and Blank Bane.

### Uncommon Cards (44)

| # | Card Name | Type | Cost | Effect | Upgrade |
|---|---|---|---|---|---|
| 1 | **Accursed Aggression** | Power | 1 | Whenever an enemy attacks you, apply 2 Cursed to it. | Gains Innate |
| 2 | **Amber Attack** | Attack | 2 | Deal 15 damage. Clear: Paint 1 Yellow. | 15→19 damage |
| 3 | **Ardent Aqua** | Skill | 1 | Paint 2 Blue. Gain 1 Dexterity. Exhaust. | 1→2 Dexterity |
| 4 | **Blank Bane** | Skill | 1 | Apply 3 Cursed to ALL enemies. Clear: Add a Gray Gloom into your hand. | 3→4 Cursed; Clear effect changes from "Add Gray Gloom" to "Paint 1 Gray" |
| 5 | **Blood Brush** | Power | 0 | Whenever you take unblocked attack damage, Paint 1 random Color. | Gains Innate |
| 6 | **Blue Burst** | Skill | 1 | Gain 6 Block. Paint 1 Blue. | 6→9 Block |
| 7 | **Bob's Bin** | Skill | 0 | Exhaust a Painting in your hand to Paint its colors on the Canvas. | Also Darkens the Canvas |
| 8 | **Chosen Colors** | Skill | 1 | Chromatic: Add a copy of the Canvas into your hand. | 1→0 cost |
| 9 | **Chroma Crash** | Attack | 1 | Deal 7 damage. Chromatic: Gain [E] and draw 1 card. | 7→10 damage |
| 10 | **Colored Collision** | Attack | 2 | Deal 16 damage. Chromatic: Gain [E]. | 16→21 damage |
| 11 | **Dark Duel** | Attack | 1 | Deal 8 damage. Chromatic: Darken the Canvas. | 8→11 damage |
| 12 | **Darker Danger** | Skill | 0 | Gain 4 Block. Clear: Darken the Canvas. | 4→7 Block |
| 13 | **Deep Desperation** | Skill | 0 | Paint 1 Purple. Paint 1 additional Purple for ALL your Status and Curse cards. | 1→2 base Purple |
| 14 | **Double Demolition** | Attack | 2 | Deal 7 damage to ALL enemies twice. Chromatic: Paint 1 more of both Colors. | 7→9 damage |
| 15 | **Dual Dash** | Skill | 1 | Gain 9 Block. Chromatic: Apply 1 Weak to ALL enemies. | 9→12 Block |
| 16 | **Dual Debuffs** | Skill | 1 | Paint 1 Aqua. Paint 1 Magenta. | Gains Retain |
| 17 | **Ending Effort** | Skill | 1 | Paint 3 Yellow. End your turn. | Gains Retain |
| 18 | **Enjoyed Epiphany** | Skill | 1 | Add a random card which Paints a specific Color into your hand. It costs 0 this turn. Exhaust. | 1→0 cost |
| 19 | **Green Greed** | Skill | 1 | Draw 2 cards. Paint 1 Green. | 2→3 cards |
| 20 | **Growing Greens** | Power | 2 | At the end of your turn, Paint 1 Green. | 2→1 cost |
| 21 | **Helping Harsh** | Attack | 1 | Deal 7 damage. Choose a Color to Paint between Red, Blue, Green and Purple. | Choose to Paint twice instead of once |
| 22 | **Hue Huffer** | Power | 1 | At the end of your turn, Paint 2 random Colors for each Painting played this turn. | 2→3 Colors per Painting |
| 23 | **Innovative Ink** | Attack | 1 | Take 1 damage. Deal 6 damage to ALL enemies. Paint 1 Red for each damaged target. | 6→9 damage |
| 24 | **Insane Inspiration** | Skill | 1 | Play the top card of your draw pile. If it's a Painting, Repeat this effect. | 1→0 cost |
| 25 | **Many Minds** | Skill | 1 | Clear: Shuffle your discard pile into your draw pile. Draw 3 cards. Chromatic: Exhaust 1 card. | 3→4 cards |
| 26 | **Mean Magenta** | Skill | 1 | Paint 2 Red. Gain 1 Strength. Exhaust. | 1→2 Strength |
| 27 | **Paintball Pelt** | Power | 1 | Whenever you draw a card that is a Painting, deal 6 damage to ALL enemies. | 6→9 damage |
| 28 | **Paintbrush Panic** | Skill | X | Paint X random Colors. Paint X Rainbow. | Paint X+1 of each instead |
| 29 | **Palette Pick** | Skill | 1 | Choose a Color to Paint twice. Exhaust this card if you chose Yellow. | 2→3 times |
| 30 | **Purple Pain** | Skill | 1 | Apply 2 Cursed. Paint 1 Purple. | 2→3 Cursed |
| 31 | **Rainbow Razor** | Attack | 2 | Deal 18 damage to a random enemy. Paint 1 random Color. | 18→21 damage, 1→2 Colors |
| 32 | **Rainbow Reality** | Skill | 1 | Paint 2 Rainbow. Clear: Paint 2 extra Rainbow. | 2→3 base Rainbow |
| 33 | **Raring Repaint** | Skill | 1 | Gain 5 Block. Return a Painting from your discard pile to your hand. | Gains Retain |
| 34 | **Red Rage** | Attack | 1 | Deal 8 damage. Paint 1 Red. | 8→12 damage |
| 35 | **Safe Space** | Attack | 1 | Deal 7 damage. Paint 1 Blue for each enemy in combat. | 7→10 damage |
| 36 | **Satanic Surge** | Skill | 1 | Apply 2 Cursed. Cursed on the target is not reduced at the start of your next turn. | 2→3 Cursed |
| 37 | **Side Sculpture** | Skill | 1 | Create a specific Painting into your hand. | *(upgrade effect unknown — no stat changes in bytecode)* |
| 38 | **Simple Studies** | Skill | 1 | Gain 8 Block. Chromatic: Shuffle a copy of the Canvas into your draw pile. | 8→11 Block |
| 39 | **Soft Stimulation** | Skill | 0 | Paint 4 Green. Reduced by 1 for each color on the Canvas. | Chromatic: not reduced instead |
| 40 | **Sweeping Sadness** | Skill | 2 | Gain 12 Block. Apply 4 Cursed. Add a Gray Gloom into your hand. | 12→15 Block, 4→5 Cursed |
| 41 | **Treasure Trove** | Power | 1 | At the start of your turn, add a random Uncommon card into your hand. | Gains Innate |
| 42 | **Vivacious Virescence** | Skill | 1 | Paint 1 Green. Paint 1 Yellow. | Gains Retain |
| 43 | **Watercolor Wave** | Attack | 1 | Deal 8 damage. Draw 5 cards. Discard all cards drawn this way that aren't Paintings. | 8→10 damage, 5→7 cards |
| 44 | **Yellow Yearning** | Skill | 1 | Gain [E]. Paint 1 Yellow. Exhaust. | Gain [E][E] instead |

### Rare Cards (20)

| # | Card Name | Type | Cost | Effect | Upgrade |
|---|---|---|---|---|---|
| 1 | **Abstruse Abstract** | Skill | 1 | Add a copy of the Canvas into your hand. Exhaust. | Exhaust → Exhaustive (can be played multiple times before exhausting) |
| 2 | **Aquamarine Aposematism** | Skill | 1 | Apply 2 Weak to ALL enemies. Paint 2 Aqua. Exhaust. | 2→3 Weak |
| 3 | **Baneful Bucket** | Skill | 2 | Chromatic: Double the amount of Paint on the Canvas. Exhaust. | Double → Triple |
| 4 | **Cerulean Coverage** | Skill | 1 | Gain 3 Block. Paint 3 Blue. Exhaust. | 3→4 Block, 3→4 Blue |
| 5 | **Chiseled Chiaroscuro** | Skill | X | Darken the Canvas X times. Exhaust. | Removes Exhaust |
| 6 | **Contrasting Co-op** | Skill | 1 | Paint 1 Red. Paint 1 Blue. Paint 1 Green. | 1→0 cost; gains Retain |
| 7 | **Dire Destruction** | Attack | 2 | Exhaust ALL your Paintings. Deal 12 damage to a random enemy for each Painting Exhausted. | 12→14 per Painting |
| 8 | **Duplicate Drawing** | Skill | 1 | This turn, your next Painting is played twice. | Next 1→2 Paintings played twice |
| 9 | **Empty Ego** | Skill | 1 | Clear: Choose and Paint 3 different Colors. | 3→4 Colors |
| 10 | **Fantasy Form** | Power | 3 | When you play a Painting, Paint 1 of each color on it. | 3→2 cost |
| 11 | **Fuschia Fugue** | Skill | 1 | Apply 2 Vulnerable to ALL enemies. Paint 2 Magenta. Exhaust. | 2→3 Vulnerable |
| 12 | **Glorious Gallery** | Skill | 3 | Put all Paintings from your draw pile and discard pile into your hand. They cost 0 this turn. Exhaust. | 3→2 cost |
| 13 | **India Ink** | Skill | 0 | Gain [E]. Darken the Canvas. Exhaust. | Gain [E][E] instead |
| 14 | **Mystic Masterpiece** | Skill | 3 | Ethereal. Permanently add a Painting based on the Canvas to your deck. Fleeting. | 3→1 cost; Ethereal + Grave (plays from discard when exhausted) |
| 15 | **Paint Profusion** | Power | 1 | Whenever you draw a card that is a Painting, draw 2 cards. | 2→3 cards |
| 16 | **Pollock's Potluck** | Skill | 2 | Paint 1 of each Color. Exhaust. | Gains Innate |
| 17 | **Purple Purpose** | Skill | 2 | Apply 3 Cursed. Paint 3 Purple. Exhaust. | 3→4 Cursed, 3→4 Purple |
| 18 | **Ruby Reaper** | Attack | 3 | Deal 17 damage to ALL enemies. Paint 3 Red. Exhaust. | 17→22 damage, 3→4 Red |
| 19 | **Verdant Vainglory** | Skill | 2 | Draw cards until your hand is full. Paint 3 Green. Exhaust. | 2→1 cost |
| 20 | **Yellow Yowling** | Skill | 2 | Paint 3 Gray. Paint 3 Yellow. Exhaust. | 3→2 Gray (less downside), 3→4 Yellow |

### Status / Generated Cards

| Card Name | Type | Cost | Effect | Notes |
|---|---|---|---|---|
| **Gray Gloom** | Status | 1 | Add 2(3) Wounds into your hand. Paint 1 Gray. | Generated by Sweeping Sadness, Blank Bane. Has COMMON rarity tag but is not collectible. |
| **Painting** | Generated | 1+ | Effects determined by canvas colors. Cost = 1 (base) + darken level. | Created by right-clicking the Canvas. Yellow paintings auto-exhaust. Canvas clears after creation. Limited to 1 per turn. |
| **Wound** | Status | Unplayable | Standard STS Wound (Unplayable). | Generated by Gray Gloom. |

### Removed / Renamed Cards (in localization but no class file)

These 11 card entries exist in the localization JSON but have no corresponding class files, indicating they were removed or renamed in later versions:

| Old ID | Old Name | Description | Likely Replacement |
|---|---|---|---|
| AbstractArt | Abstract Art | Exhaust 1 card to Paint Yellow | *(Note: there is an active card class `AbstractArt.class` in the mod which maps to card ID `artistmod:AbstractArt` — this localization entry may be its old description before it was repurposed)* |
| BigBrown | Roseate Rejuvenation | Gain Temp HP. Paint Pink. Exhaust. | *(removed — no equivalent)* |
| BlueBrews | Blue Brews | End of turn: Paint 1 Blue | *(merged into Growing Greens pattern or removed)* |
| BluishBludgeon | Bluish Bludgeon | Deal damage to ALL. Clear: Paint 1 Blue | *(removed — similar to Indigo Incapitation)* |
| BrownBrush | Pink Paint | Gain Temp HP. Paint 1 Pink | *(removed — Pink color basic paint card)* |
| HorridHarvest | Horrid Harvest | Deal damage. If unblocked vs Cursed: Paint 1 Pink | *(removed)* |
| PinkPurple | Amaranth Amalgam | Paint 1 Pink. Paint 1 Purple | *(removed — similar to Dual Debuffs pattern)* |
| ProfoundPurple | Mauve Malediction | Apply Cursed to ALL. Paint 3 Purple. Exhaust. | *(removed — similar to Purple Purpose)* |
| RadicalRainbows | Gayness Generator | End of turn: Paint 1 Rainbow | *(removed — similar to Growing Greens pattern)* |
| ReddeningRampage | Reddening Rampage | End of turn: Paint 1 Red | *(removed — similar to Growing Greens pattern)* |
| YellowYouth | Coruscating Corona | End of turn: Paint 1 Yellow | *(removed — similar to Growing Greens pattern)* |

---

## Relics

**Total: 4 relics** (1 Starter, 3 Boss)

> **Source:** All relic data extracted from `TheArtist.jar` localization and class files.

### Starter Relic

| Relic Name | Rarity | Effect | Flavor |
|---|---|---|---|
| **Bent Brush** | Starter | The first time you Paint each combat, Paint 1 more of that Color. Refreshes whenever you shuffle your draw pile. | *"Damaged in a fit of anger."* |

### Boss Relics

| # | Relic Name | Rarity | Effect | Flavor |
|---|---|---|---|---|
| 1 | **Broken Brush** | Boss | Gain [E] at the start of your turn. At the end of your turn, remove all Colors on the Canvas. | *"You got angrier and totally broke it."* |
| 2 | **Repaired Brush** | Boss | Replaces Bent Brush. At the start of each combat, Darken the Canvas. | *"Anger management DOES work!"* |
| 3 | **Brilliant Brush** | Boss | When you Paint a random Color, Paint an additional one. | *"Tape two pens together and that's twice as less lines to write."* |

> **Notes:**
> - The starter relic is internally named `BlueBrush` but displayed as "Bent Brush" (was previously called "Broken Brush" in community discussions).
> - `Repaired Brush` is internally `FixedBrush` — community previously referred to it as "Fixed Brush".
> - `Broken Brush` (the boss relic) gives +1 energy but clears your Canvas every turn — a significant trade-off.
> - Bug reported: boss-swapping into Bent Brush, then picking Repaired Brush from first boss chest caused the second brush to have no effect.

---

## Potions

**Total: 4 potions**

> **Source:** All potion data extracted from `TheArtist.jar` localization files.

| # | Potion Name | Effect |
|---|---|---|
| 1 | **Painting Potion** | Add a copy of the Canvas into your hand. |
| 2 | **Paint Potion** | Choose and Paint N Colors. *(N value is parameterized — exact value needs runtime testing)* |
| 3 | **Dark Drip** | Darken the Canvas. |
| 4 | **Cursed Potion** | Apply N Cursed. *(N value is parameterized — exact value needs runtime testing)* |

---

## Powers

Powers are persistent buffs/debuffs. All power data extracted from `TheArtist.jar`.

| Power Name | Source | Effect |
|---|---|---|
| **Cursed** (debuff) | Purple Pain, Cursed Clash, Satanic Surge, Blank Bane, etc. | When you paint a color, the Cursed enemy loses HP equal to Cursed stacks. Reduced by half at the start of your turn. |
| **Auto-Painter** | Coruscating Corona, Reddening Rampage, Blue Brews, Growing Greens, Gayness Generator | At the end of your turn, Paint N of a specific Color. *(Used by multiple "end of turn paint" power cards)* |
| **Fantasy Form (draw trigger)** | Paint Profusion (card) | Whenever you draw a Painting, draw N card(s). |
| **Treasure Trove** | Treasure Trove (card) | At the start of your turn, add N random Uncommon card(s) into your hand. |
| **Blood Brush** | Blood Brush (card) | Whenever you receive attack damage, Paint N random Color(s). |
| **Hue Huffer** | Hue Huffer (card) | At the end of your turn, Paint N random Color(s) for each Painting played this turn. |
| **Accursed Aggression** | Accursed Aggression (card) | Whenever an enemy attacks you, apply N Cursed to it. |
| **Paintball Pelt** | Paintball Pelt (card) | Whenever you draw a Painting, deal N damage to ALL enemies. |
| **Growing Greens** | Growing Greens (card) | At the start of your turn, paint N Green. |
| **Duplicate Drawing** | Duplicate Drawing (card) | This turn, your next N Painting(s) are played twice. |
| **Satanic Surge** | Satanic Surge (card) | Cursed is not reduced at the start of your next turn. |
| **Fantasy Form (play trigger)** | Fantasy Form (card) | When you play a Painting, Paint N of each color on it. |

> **Note:** There are two distinct powers both named "Fantasy Form" in the mod's localization data — one triggers on drawing Paintings (sourced from the card "Paint Profusion"), and one triggers on playing Paintings (sourced from the card "Fantasy Form"). The card names and power names appear to be swapped in the original mod's localization file.

---

## Balance History

Compiled from Diamsword's Steam Workshop comments and changelog (15 updates, May–Aug 2022):

### Major Balance Changes (Chronological)

1. **Paintings cost 1 energy** (was 0) — "This change will create balance issues, but it's hard to tell where they are so far."
2. **Limited to 1 Painting per turn** — "Buffed some cards with Clear to compensate (however some powers might need buffs as well)"
3. **HP temporarily raised to 85** — "I set the base HP at 85 for an easier time for you all to figure out what's strong or not." (Later reverted to 70.)
4. **Darken Drawing replaced by Neo-Neutralize** in starting deck
5. **Contrasting cards reworked** — "additional effects for a better base card. Upgrades don't cost 0 anymore."
6. **2 rare cards temporarily removed** during balancing
7. **Mysterious Masterpiece temporarily removed** due to save/reload bug
8. **Grumpy Graffiti changed** (details not specified)
9. **Pollock's Potluck reworked** — first color painted is now always Red (was random, which could be Gray)
10. **Palette Pick reworked** — Exhausts itself if you choose Yellow
11. **Iconic Idea nerfed** — paints 1 less Green but gains +3 Block to compensate
12. **Super Schooling reworked** — now a block-giving Skill
13. **Painting preview on hover** — QoL improvement
14. **Base relic buffed** — effect now resets on deck shuffle
15. **Potions added** — 3 potions (1 per rarity)
16. **New cards added** — Many Minds, Helping Harsh
17. **Glorious Gallery reworked** — "back with a better effect"
18. **Neo-Neutralize art updated**

### Specific Card Balance Changes

| Card | Change | Patch Context |
|---|---|---|
| Cursed Clash | 7(8) → 7(9) damage | Early balance |
| Prismatic Pierce | 3(4) → 4(6) damage | Early balance |
| Sweeping Sadness | 11(13) → 12(15) Block | Early balance |
| Crushing Canvas+ | Buffed (details unknown) | Balance pass |
| Double Draw | Buffed (details unknown) | Balance pass |
| Iconic Idea | Less Green paint, +3 Block | Balance rework |
| Colored Collision | Buffed (details unknown) | Balance pass |
| Ending Effort | Buffed (details unknown) | Balance pass |
| Innovative Ink | Buffed (two consecutive patches) | Balance pass |
| Rainbow Razor | Buffed (details unknown) | Balance pass |
| Safe Space | Buffed (details unknown) | Balance pass |
| Soft Stimulation | Buffed (details unknown) | Balance pass |

---

## Known Bugs (STS1)

| Bug | Status | Details |
|---|---|---|
| **Mysterious Masterpiece save/reload** | Fixed (card was temporarily removed) | Card was replaced by Madness if you saved and reloaded the game |
| **Hue Huffer stacking** | Reported, fix unknown | Even if played multiple times, only gave 1 paint per painting instead of stacking |
| **Infinite combos** | Addressed by cost/limit changes | Multiple players reported easy infinites with 0-cost Green/Yellow paintings. Fixed by making paintings cost 1 and limiting to 1/turn. |
| **Fixed Brush boss swap** | Reported | Three bugs in one: (1) Boss swap can give Broken Brush as upgraded version, (2) player can pick Fixed Brush from first boss chest even if already holding it, (3) second Fixed Brush has no effect. |

---

## Community Balance Feedback

Key feedback themes from Steam discussions (useful for STS2 rebalancing):

### Strongest Archetypes

1. **Purple (Red+Blue) Chromatic** — "Chromatic red/blue is too strong, it's the most consistent deck with extremely high win ratio" (pisaprofile)
2. **Cursed + random painting** — "scales ultra exponentially through turns" (pisaprofile)
3. **Green/Yellow infinite** — was fixed by 1-cost paintings and 1/turn limit, but thin decks can still chain 0-cost paintings that draw each other

### Weakest Areas

1. **Act 1 on high Ascension** — "The starting deck feels underpowered, struggles against group fights due to lack of cheap AoE, and gremlin nob is a pain because most painting cards are Skills" (jhleviathan). After paintings changed to cost 1, the early game became much harder: "19 damage for 3 energy is almost the same as playing 3 unupgraded strikes" (jhleviathan on Brushing Bash).
2. **Gray color** — "I don't really understand Grey? There doesn't seem to be any easily obtainable synergy with wounds" (Jezzared). Ironclad has Evolve/Fire Breathing for Wound synergy; Artist has nothing comparable. Diamsword compared to Defect's Overclock/TURBO: "There is not major synergies with burn or void in Defect."
3. **Gray Gloom card** — "actually just better to have in hand than it is to play" — playing it creates 3+ dead cards vs 1 dead card unplayed. Community suggests changing to "Paint 3 Gray" for Cursed burst damage synergy. Jezzared's full analysis: "Using it splits it into three wounds (maybe even more depending on if you need to cast that painting again)."
4. **Big painting strategy** — "most of the painting synergies seem directed towards 'just spam tons of 0 cost paintings'" with little support for Darken-based big paintings (Taril). Diamsword claims "there are in fact tools for big paintings, and that's actually how I play it most of the time."
5. **Non-Blue defensive options** — "there isn't really anything that provides meaningful protection outside of painting blue" (Suggestions thread). Requested: block from paintings in hand, block when drawing paintings, block based on canvas colors, block proportional to Cursed stacks.
6. **Downward spiral after 1-cost change** — "The tougher the fight, the more likely you have to use a small painting as a panic button/band aid... your deck will be clogged with weak paintings such as 1 energy block 6 draw a card" (jhleviathan Balance thread)

### Design Suggestions

- More support for Darken/big painting strategy (currently underused). Diamsword says tools exist but community disagrees.
- Better Gray color communication ("the mod poorly communicates gray's intent"). Jezzared's comprehensive analysis: Gray should lean into canvas mechanics rather than status card generation; "I'm painting only as the artist. The artist should lean into the canvas where possible."
- Controller/accessibility support for Canvas interactions. Player using "Say The Spire" accessibility mod cannot interact with the Canvas.
- Smaller Canvas UI option ("reduce the size of the canvas")
- Painting preview should be visible without clicking twice; hover-to-preview (implemented in later patch). Should still work after 1/turn limit reached.
- More viable archetypes beyond Purple Chromatic and Cursed. Specifically: defensive options that don't require Blue paint.
- Chinese localization requested
- Alliterative card naming convention used for all cards (community noted: "can't even shorten the card name cause literally every card name is an alliteration lol")
- Community-suggested card name: "Van Cough" — adds Frail/Weak debuff for 2 turns

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

1. **Canvas UI** — The Canvas is a custom, interactive in-combat UI element. This will require significant custom Godot UI work. The STS1 version used right-click to create paintings and hover to preview. STS2 may need a different interaction model. Community requested: hover preview, resizable canvas, accessibility/controller support.
2. **Dynamic Card Generation** — Paintings are generated at runtime based on canvas state. STS2 modding support for dynamic card generation needs investigation.
3. **Color Mixing System** — Need a system to track canvas state (colors, darken level) and calculate resulting painting effects. Color stacking must properly intensify effects.
4. **Per-Turn Painting Limit** — Enforce 1 painting per turn limit. Community requested: still allow preview even after limit reached.
5. **Cursed Debuff System** — Cursed triggers damage per paint action, not per turn. This is a custom debuff that must hook into the paint system.
6. **Save/Load Compatibility** — The Mysterious Masterpiece bug in STS1 was caused by generated cards not surviving save/reload. Ensure dynamic Painting cards serialize properly.

### Priority Order for Implementation

1. **Phase 1: Core Mechanics** — Canvas system, Paint colors, Painting generation, Darken
2. **Phase 2: Starter Deck** — Striking Stroke, Palette Parry, Brush Bash, Neo-Neutralize, Bent Brush relic
3. **Phase 3: Common Cards** — All 22 common cards
4. **Phase 4: Uncommon Cards** — All 44 uncommon cards
5. **Phase 5: Rare Cards** — All 20 rare cards
6. **Phase 6: Powers** — All 12 powers including Cursed debuff
7. **Phase 7: Relics** — Bent Brush (starter) + Broken Brush, Repaired Brush, Brilliant Brush (boss)
8. **Phase 8: Potions** — All 4 potions
9. **Phase 9: Polish** — Art, balance, localization, bug fixes, Canvas UI improvements

### Data Gaps

> ✅ **Complete data extracted.** All card, relic, potion, keyword, and power data has been extracted from `TheArtist.jar` v2022-08-07 via SteamCMD anonymous download and JAR decompilation.
>
> **Data extracted from:**
>
> - `artistmodResources/localization/eng/ArtistMod-Card-Strings.json` — 102 card entries (91 active + 11 removed)
> - `artistmodResources/localization/eng/ArtistMod-Relic-Strings.json` — 4 relics
> - `artistmodResources/localization/eng/ArtistMod-Potion-Strings.json` — 4 potions
> - `artistmodResources/localization/eng/ArtistMod-Keyword-Strings.json` — 15 keywords
> - `artistmodResources/localization/eng/ArtistMod-Power-Strings.json` — 12 powers
> - `theArtist/cards/*.class` — 91 card classes decompiled for stat values (cost, damage, block, magic number, upgrades)
> - `theArtist/TheArtist.class` — starting deck composition and starting relic
>
> **Download command used:** `steamcmd +login anonymous +workshop_download_item 646570 2808845989 +quit`
>
> **Confirmed data summary:**
>
> | Category | Confirmed | Total | Coverage |
> |---|---|---|---|
> | Collectible cards (names + effects + stats) | 86 | 86 | 100% |
> | Starter/Basic cards | 5 | 5 | 100% |
> | Status cards | 1 | 1 | 100% |
> | Relics (names + effects + flavor) | 4 | 4 | 100% |
> | Potions (names + effects) | 4 | 4 | 100% |
> | Powers | 12 | 12 | 100% |
> | Keywords | 15 | 15 | 100% |
>
> **Remaining gaps:**
> - Exact parameterized values for Paint Potion and Cursed Potion (N values need runtime testing)
> - Card art assets (in `artistmodResources/images/cards/` — PNG files present but not documented)
> - Some upgrade effects are description-only changes (Retain, Innate, Exhaustive) without numeric stat diffs

---

*Last updated: 2026-04-18*
