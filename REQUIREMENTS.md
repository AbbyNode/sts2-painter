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
  - [Common Cards (20)](#common-cards-20)
  - [Uncommon Cards (39)](#uncommon-cards-39)
  - [Rare Cards (17)](#rare-cards-17)
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
| **Starting Deck** | 5× Strike, 5× Defend, 1× Neo-Neutralize *(originally included Darken Drawing + Brushing Bash instead)* |
| **Starting Relic** | Broken Brush — effect resets on deck shuffle (see [Relics](#relics)) |
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

- **Simplified to 7 colors** — removed Magenta, Aqua, Pink, and Rainbow; replaced Black with Gray
- **Color mixing system** — Green, Purple, and Orange are now created by mixing two primary colors instead of being standalone
- **Paintings cost 1 energy** (was 0) — major balance change to prevent infinite combos
- **Limited to 1 Painting per turn** — another anti-infinite measure
- **Darken** reworked from "double all stacks" to incremental effectiveness boost
- **Cursed** reworked as a debuff mechanic (damage when painting) instead of a non-decreasing poison
- **Pink color removed** ("Makes her much more likely to actually die over the course of a run, even if it was fun" — Jezzared)
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
- **Purple Pain** — deals damage and/or gains Block instantly AND paints Purple

This pattern creates cards that both have immediate combat impact and build toward future Painting creation.

### The "Contrasting" Group

Common cards in the "Contrasting" group provide a base effect (attack or block) plus paint 2 different colors. These were given "additional effects for a better base card" in balance patches. Their upgrades no longer cost 0 (was changed when paintings went from 0 to 1 cost).

---

## Keywords

| Keyword | Description | Design Notes |
|---|---|---|
| **Paint [Color]** | Add the specified color to the Canvas. Triggers Cursed damage on affected enemies. | Core mechanic |
| **Darken** | Increase the Canvas darkness level by 1. Boosts Painting effects but increases energy cost by 1. | Encourages "big painting" strategy |
| **Clear** | This effect activates **only if the Canvas has no colors on it**. | Encourages playing order strategy — use Clear effects before painting. Cards with Clear were buffed when painting was limited to 1/turn. |
| **Chromatic** | This effect activates **only if the Canvas has exactly 2 different colors** (regardless of the total amount of paint). | Encourages specific color combinations. Red+Blue Chromatic is considered the strongest archetype. |
| **Cursed** | A debuff applied to enemies. While Cursed, the enemy takes damage **each time the player paints** (adds a color to Canvas). Amount does NOT decrease each turn (unlike Poison). | Very powerful scaling mechanic. Community feedback: "should prevent Curse from going down permanently" for Satanic Surge. |

---

## Paint Colors

### Primary Colors

| Color | Effect on Painting | Theme | Stacking |
|---|---|---|---|
| **Red** | Deal damage to a target enemy | Offense / Fire | More Red = more damage |
| **Blue** | Gain Block | Defense / Water | More Blue = more Block |
| **Yellow** | Draw cards. **Painting auto-exhausts** when played. | Draw / Lightning | More Yellow = more draw. Exhaust is the balancing cost. |

### Mixed Colors

When two primary colors are combined on the Canvas, they create a mixed color with combined effects:

| Color | Mix | Effect on Painting | Theme | Key Difference |
|---|---|---|---|---|
| **Green** | Blue + Yellow | Draw cards. **No auto-exhaust.** Reduced cost feel. | Growth / Nature | Unlike Yellow, Green paintings stay in play — this was the source of infinite combos in early versions |
| **Purple** | Red + Blue | Deal damage AND gain Block | Mystical / Arcane | Most consistent archetype per community ("Chromatic red/blue is too strong") |
| **Orange** | Red + Yellow | Deal damage AND draw cards | Volatile / Multi-hit | Combines offense with card advantage |

### Special Color

| Color | Effect | Theme | Design Intent |
|---|---|---|---|
| **Gray** | Negative — created Painting adds Wounds to deck or applies other penalties | Corruption / Waste | Intentionally bad. Diamsword: "In its design it is supposed to be bad, something you should avoid." Comparable to Ironclad's Wound generation but without Ironclad's Wound synergies (Evolve, Fire Breathing). Gray can be useful for: Cursed damage triggers, Chromatic procs, and rare lethal opportunities. |

> **Note on color mixing:** When you add a Red and a Blue paint to the canvas, the resulting painting gains Purple's combined effect (damage + block). Adding multiple of the same color (e.g., Red + Red) intensifies that color's effect. Darkening further amplifies all effects.
>
> **Community note on balance:** "Chromatic red/blue is too strong, it's the most consistent deck with extremely high win ratio, the other viable deck being random painting + curse which scales ultra exponentially through turns." (pisaprofile) This suggests Purple + Cursed are the two dominant archetypes.

---

## Cards

The mod adds **76 collectible cards** total: 20 Common, 39 Uncommon, 17 Rare.

> **Legend:**
> - ✅ = Name and core effect confirmed from author (Diamsword) posts or direct community references
> - 🔶 = Name confirmed but exact numbers/effect text need verification from mod files
> - ❓ = Not publicly documented — needs extraction from mod files
> - Values in parentheses like `7(9)` mean `base(upgraded)`

### Starter Cards

| Card Name | Type | Cost | Effect | Notes |
|---|---|---|---|---|
| **Strike** | Attack | 1 | Deal 6 damage | Standard starter |
| **Defend** | Skill | 1 | Gain 5 Block | Standard starter |
| **Neo-Neutralize** | Skill | 0 | Apply 1 Weak. Paint 1 Gray. | Current starter (replaced Darken Drawing). New art was added in a later update. Painting Gray is an intentional downside. |
| ~~**Darken Drawing**~~ | Skill | 1 | Darken the Canvas. | Original starter card, **removed from starting deck** and replaced by Neo-Neutralize. jhleviathan: "Darken Drawing... you are buffing your painting for 2 energy (1 to play the card and 1 because the cost of painting is increased), which means you are doing 8-16 damage for 2 energy." May have been moved to the collectible pool. |
| ~~**Brushing Bash**~~ | Attack | 2 | Deal ~19 damage (with starter relic). Paint colors to Canvas. | Original starter card alongside Darken Drawing. jhleviathan: "Brushing Bash which now deals 19 damage for 3 energy (2 to play the card itself and 1 to play the painting) with the starting relic. 19 damage for 3 is almost the same as playing 3 unupgraded strikes." The 19 damage includes the relic bonus; base damage is lower. |

### Common Cards (20)

| # | Card Name | Type | Cost | Effect | Status |
|---|---|---|---|---|---|
| 1 | **Contrasting Strike** | Attack | 1 | Deal damage. Paint 2 different colors. *(Part of "Contrasting" group)* Upgrade no longer costs 0. | ✅ |
| 2 | **Contrasting Guard** | Skill | 1 | Gain Block. Paint 2 different colors. *(Part of "Contrasting" group)* Upgrade no longer costs 0. | ✅ |
| 3 | **Cursed Clash** | Attack | 1 | Deal 7(9) damage. Apply Cursed to target. | ✅ |
| 4 | **Prismatic Puncture** | Attack | 1 | Deal 4(6) damage. Paint 1 color of your choice. | ✅ |
| 5 | **Red Rage** | Attack | 1 | Deal damage twice (instant effect). Paint 1 Red. *(Follows "do color effect + paint color" pattern)* | ✅ |
| 6 | **Purple Pain** | Attack | 1 | Deal damage AND gain Block (instant effect). Paint 1 Purple. *(Follows "do color effect + paint color" pattern. Applies ~3 Curse to single target.)* | ✅ |
| 7 | **Grumpy Graffiti** | Attack | 1 | Deal ~12 damage. Chromatic: Paint 1 Gray. *(Comparable to Wild Strike — high damage common with conditional downside. Was changed in a balance patch. Jezzared: "either strictly better if you have anything other than two paint colors, or strictly worse if you have two colors on the canvas".)* | ✅ |
| 8 | **Double Draw** | Skill | 1 | Draw 2 cards. *(Buffed in balance patch)* | ✅ |
| 9 | **Brushing Bash** | Attack | 2 | Deal ~19 damage (with starter relic). Paint colors. *(jhleviathan: "19 damage for 3 energy (2 to play the card itself and 1 to play the painting) with the starting relic" — suggesting the card itself costs 2 and the resulting painting costs 1 more.)* | ✅ |
| 10 | **Helping Harsh** | Attack/Skill | ❓ | *(New card added in balance patch — exact effect not publicly documented)* | 🔶 |
| 11 | **Soft Stimulation** | Skill | ❓ | *(Buffed in balance patch — paint/block related. Exact effect not publicly documented)* | 🔶 |
| 12–20 | *(9 additional commons)* | Various | ❓ | *Names and effects not publicly documented. Based on naming patterns (alliterative naming convention used by all confirmed cards) and mechanics, likely include more single-color paint cards for each color, and Clear/Chromatic synergy commons.* | ❓ |

### Uncommon Cards (39)

| # | Card Name | Type | Cost | Effect | Status |
|---|---|---|---|---|---|
| 1 | **Crushing Canvas** | Attack | ❓ | Deal damage based on the number of colors on the Canvas. Upgraded version (Crushing Canvas+) was buffed. | ✅ |
| 2 | **Sweeping Sadness** | Skill | 2 | Gain 12(15) Block. Apply 4 Cursed to target. Add 1 Gray Gloom to your hand. *(2 energy cost confirmed from Balance thread. Jezzared: "2 energy and just gained me 11 block and applied 4 curse without the wound" — suggesting the Wound via Gray Gloom is the card's downside for its combined block+curse power.)* | ✅ |
| 3 | **Palette Pick** | Skill | ❓ | Choose a color to Paint. If you choose Yellow, Exhaust this card. *(Key combo enabler — can paint ANY color including Gray. Does NOT exhaust for non-Yellow colors, enabling repeated use. SparkleApple: "I mostly spam 0 costs draw 2+ thanks to palette pick".)* | ✅ |
| 4 | **Iconic Idea** | Skill | ❓ | Paint 1 Green. Gain 3 Block. *(Was "Paint more Green" before rebalance — now paints 1 less Green but gives Block to compensate)* | ✅ |
| 5 | **Colored Collision** | Attack | ❓ | *(Buffed in balance patch — paint/damage related. Exact effect not publicly documented)* | 🔶 |
| 6 | **Ending Effort** | Skill | ❓ | *(Buffed in balance patch. Exact effect not publicly documented)* | 🔶 |
| 7 | **Innovating Ink** | Skill | ❓ | *(Buffed in balance patch — paint/ink related. Exact effect not publicly documented)* | 🔶 |
| 8 | **Rainbow Razor** | Attack | ❓ | *(Buffed in balance patch — rainbow/multi-color damage. "Rainbow paintings" are referenced by Jezzared as "very fun" but "too consistent" since colors were reduced from 10 to 7.)* | 🔶 |
| 9 | **Safe Space** | Skill | ❓ | *(Buffed in balance patch — defensive card. Exact effect not publicly documented)* | 🔶 |
| 10 | **Satanic Surge** | Skill | ❓ | Apply Cursed to target. *(Community feedback: "should prevent Curse from going down permanently" — suggesting Cursed may have a decay mechanic that this card should override)* | ✅ |
| 11 | **Blank Bane** | Attack | ❓ | Apply 3 Cursed to a single target. Clear: Apply 3 Cursed to ALL enemies instead. Adds 1 Gray Gloom to your hand. *(Jezzared: "Nothing applies as much curse as Blank Bane does, even to single targets... The base card, 3 curse to one target, would put it more in line with the other curse cards at its cost (Cursed Clash, Purple Pain)")* | ✅ |
| 12 | **Pollock's Potluck** | Skill | ❓ | Paint multiple random colors. First color painted is now Red (was any color before). Upgraded version: Innate. *(Named after Jackson Pollock. jhleviathan: "The upgraded version combined with starter relic makes one of the most frustrating anti-synergy" — because Innate means it's played first each combat, wasting the once-per-shuffle relic bonus.)* | ✅ |
| 13 | **Super Schooling** | Skill | ❓ | Gain Block. *(Reworked — was originally a different type, now is a block-giving Skill)* | ✅ |
| 14 | **Many Minds** | Power | ❓ | *(New card added in update alongside Glorious Gallery rework. Exact power effect not publicly documented)* | 🔶 |
| 15 | **Yellow Yowling** | Skill | ❓ | Paint Yellow. Provides a TURBO-like power spike. *(Jezzared: "the best designed card the artist has involving gray is actually Yellow Yowling. It provides that TURBO-esque spike of power that players gravitate towards, at the cost of lots of clutter." Likely paints Yellow — draws cards — but also paints Gray, creating short-term power at cost of deck pollution.)* | ✅ |
| 16–39 | *(24 additional uncommons)* | Various | ❓ | *Names and effects not publicly documented. Based on naming convention (alliterative), likely include: more Clear/Chromatic synergy cards, additional Cursed-application cards, multi-paint cards, canvas manipulation cards, and cards following the "color action + paint" pattern for remaining colors.* | ❓ |

### Rare Cards (17)

| # | Card Name | Type | Cost | Effect | Status |
|---|---|---|---|---|---|
| 1 | **Mysterious Masterpiece** | Power | ❓ | *(Temporarily removed due to save/reload bug where the card would be replaced by Madness. Later added back. Exact power effect not publicly documented.)* | 🔶 |
| 2 | **Glorious Gallery** | Power | ❓ | Returns Paintings from discard/exhaust pile to hand (similar to Defect's All for One). *(Reworked with "a better effect" per Diamsword. jhleviathan: "imagine the defect card All for One now returns all your 1-cost cards instead of 0" — confirming the original effect returned 0-cost paintings, which became useless when paintings changed to cost 1. The rework presumably updated the cost filter.)* | ✅ |
| 3 | **Hue Huffer** | Power | ❓ | Each time you create a Painting, Paint 1 additional random color on it. Stacks with multiple plays. *(Bug reported: didn't stack properly even with multiple plays, only gave 1 paint per painting regardless.)* | ✅ |
| 4–17 | *(14 additional rares)* | Various | ❓ | *2 rare cards were temporarily removed during balancing. Jezzared mentions "rare cards that paint a lot" as desirable follow-ups to Blank Bane, suggesting some rares are heavy-painting cards. Community mentions painting-synergy powers: "painting based on the number of paintings played", "drawing extra cards when you draw a painting", "dealing damage when you draw a painting", "dealing more damage the more paintings you have" (Taril). Some rares likely include cards like Darken Drawing (original starter, moved to collectible pool or removed).* | ❓ |

### Status / Generated Cards

| Card Name | Type | Cost | Effect | Notes |
|---|---|---|---|---|
| **Gray Gloom** | Status | 0 | Paint 2 Gray. Add 1 Wound to your deck. | Added to hand by Sweeping Sadness, Blank Bane, and possibly other cards. Diamsword: "I thought I'd do my own Wound but for the artist." Jezzared argues it's "actually just better to have in hand than it is to play" because using it creates 3+ dead cards (2 Gray paintings + Wound) vs just 1 dead card in hand. Community suggestion: change to "Paint 3 Gray" instead of "2 Gray + 1 Wound" to make it a burst-damage option for Cursed targets. |
| **Painting** | Generated | 1+ | Effects determined by canvas colors. Cost = 1 (base) + darken level. | Created by right-clicking the Canvas. Yellow paintings auto-exhaust. Canvas clears after creation. Limited to 1 per turn. |
| **Wound** | Status | Unplayable | Standard STS Wound (Unplayable). | Generated by Gray Gloom and possibly Gray-painted Paintings. |

---

## Relics

**Total: 4 relics** (1 Base, 3 Boss)

### Base Relic (Starting Relic)

| Relic Name | Rarity | Effect (Reconstructed) | Status |
|---|---|---|---|
| **Broken Brush** | Starter | Enhances the first Painting you create after each deck shuffle. *(Effect "resets on deck shuffle" per Diamsword. jhleviathan: "Brushing Bash deals 19 damage for 3 energy with the starting relic" — suggesting the relic adds damage/effectiveness to the first painting created. Anti-synergy with Pollock's Potluck+ (Innate) noted: the Innate card triggers first each combat, potentially wasting the relic bonus on random paint.)* | ✅ |

### Boss Relics

| # | Relic Name | Rarity | Effect | Status |
|---|---|---|---|---|
| 1 | **Fixed Brush** | Boss | Upgraded version of Broken Brush. *(Bug reported: "I boss swapped into the Broken Brush. I then picked Fixed Brush from the first boss chest. The Fixed Brush doesn't do anything." — confirming the name and that it's offered as a boss relic upgrade. The bug was three-fold: boss swap shouldn't give upgraded relic, picking a second upgrade shouldn't be possible, and the second brush had no effect.)* | ✅ |
| 2 | *(Boss Relic 2)* | Boss | *(Not publicly documented)* | ❓ Need extraction |
| 3 | *(Boss Relic 3)* | Boss | *(Not publicly documented)* | ❓ Need extraction |

> **Community-suggested relic ideas** (from Steam comments by AvangionQ — may or may not be in the mod):
>
> - "The first time per combat you create a painting, gain 1 energy"
> - "The first time per combat you play a painting, draw 1 card"
> - "The first time per combat you exhaust a painting, discard it instead"
> - "For every 5 times you Paint a color, Paint one more randomly"
> - "Every 10th Painting you play, plays twice (retain count after combat)"

---

## Potions

**Total: 3 potions** (1 Common, 1 Uncommon, 1 Rare)

Potions were added in a later update. Diamsword confirmed: "Ah yes we also have 3 potions now."

| # | Potion Name | Rarity | Effect | Status |
|---|---|---|---|---|
| 1 | *(Common Potion)* | Common | *(Not publicly documented — likely paint/canvas related)* | ❓ Need extraction |
| 2 | *(Uncommon Potion)* | Uncommon | *(Not publicly documented)* | ❓ Need extraction |
| 3 | *(Rare Potion)* | Rare | *(Not publicly documented)* | ❓ Need extraction |

---

## Powers

Powers are persistent buffs gained by playing Power-type cards. Known powers in the mod:

| Power Name | Source Card | Effect | Status |
|---|---|---|---|
| **Hue Huffer** | Hue Huffer (Rare) | Each Painting you create has 1 additional random color painted on it. Stacks with multiple plays. | ✅ |
| **Glorious Gallery** | Glorious Gallery (Rare) | Returns Paintings from discard/exhaust pile to hand (similar to Defect's All for One). Reworked after paintings changed from 0 to 1 cost. | ✅ |
| **Many Minds** | Many Minds (Uncommon) | *(New power added in update — exact effect TBD)* | 🔶 |
| **Mysterious Masterpiece** | Mysterious Masterpiece (Rare) | *(Exact effect TBD — was temporarily removed due to save/reload bug)* | 🔶 |
| **Cursed** (debuff) | Applied by Cursed Clash, Satanic Surge, etc. | Enemy takes X damage each time the player paints a color onto the Canvas. Does not naturally decrease (unlike Poison). | ✅ |

> **Note:** Community member Taril's comment mentions several power-like effects that may be separate powers or card effects: "painting based on the number of paintings played", "drawing extra cards when you draw a painting", "dealing damage when you draw a painting", "dealing more damage the more paintings you have". These suggest additional powers exist that synergize with painting-spam strategies.

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
| Prismatic Puncture | 3(4) → 4(6) damage | Early balance |
| Sweeping Sadness | 11(13) → 12(15) Block | Early balance |
| Crushing Canvas+ | Buffed (details unknown) | Balance pass |
| Double Draw | Buffed (details unknown) | Balance pass |
| Iconic Idea | Less Green paint, +3 Block | Balance rework |
| Colored Collision | Buffed (details unknown) | Balance pass |
| Ending Effort | Buffed (details unknown) | Balance pass |
| Innovating Ink | Buffed (two consecutive patches) | Balance pass |
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
2. **Phase 2: Starter Deck** — Strike, Defend, Neo-Neutralize, base relic
3. **Phase 3: Common Cards** — All 20 common cards (start with confirmed: Contrasting Strike/Guard, Cursed Clash, Prismatic Puncture, Red Rage, Purple Pain, Grumpy Graffiti, Double Draw, Brushing Bash)
4. **Phase 4: Uncommon Cards** — All 39 uncommon cards (start with confirmed: Crushing Canvas, Sweeping Sadness, Palette Pick, Iconic Idea, Satanic Surge, Blank Bane, Pollock's Potluck, Super Schooling, Yellow Yowling)
5. **Phase 5: Rare Cards** — All 17 rare cards (start with confirmed: Hue Huffer, Glorious Gallery, Mysterious Masterpiece)
6. **Phase 6: Powers** — Hue Huffer, Glorious Gallery, Many Minds, Mysterious Masterpiece, Cursed (debuff)
7. **Phase 7: Relics** — Broken Brush (starter) + Fixed Brush (boss) + 2 unknown boss relics
8. **Phase 8: Potions** — 3 potions
9. **Phase 9: Polish** — Art, balance, localization, bug fixes, Canvas UI improvements

### Data Gaps

> ⚠️ **Important:** 29 collectible card names have been confirmed from public sources (Diamsword's comments, community discussions, discussion threads). The remaining 47 collectible cards have no publicly available names or effects. Additionally, 5 starter/status/generated card names are confirmed.
>
> **Information gathered from:**
>
> - [Steam Workshop page](https://steamcommunity.com/sharedfiles/filedetails/?id=2808845989) — mod description
> - [Steam Workshop comments](https://steamcommunity.com/sharedfiles/filedetails/comments/2808845989) (52 comments) — Diamsword's patch notes and community feedback
> - [Steam Workshop discussions](https://steamcommunity.com/sharedfiles/filedetails/discussions/2808845989) (4 threads) — Balance, Bugs, Suggestions, Gray Discussion
> - [Frost Prime YouTube playthrough](https://www.youtube.com/watch?v=9bsf_l5XJuA)
> - [naoe\_ note.com article](https://note.com/naoe_/n/n6e7bd996c821) — original Vex version mechanics
> - Community discussions (Jezzared, SparkleApple, jhleviathan, Taril, pisaprofile, drewmarc, etc.)
>
> **To get the remaining card data, you will need to:**
>
> 1. Subscribe to the STS1 mod on Steam and browse the in-game card library, OR
> 2. Download the mod's `.jar` file via Steam Workshop and extract the localization JSON files (jar files are ZIP archives — the localization data is in a `localization/eng/` folder inside), OR
> 3. Use [SteamCMD](https://developer.valvesoftware.com/wiki/SteamCMD) to download: `steamcmd +login anonymous +workshop_download_item 646570 2808845989 +quit` — requires whitelisting `client-update.steamstatic.com` and `*.steamserver.net` domains, OR
> 4. Use [DepotDownloader](https://github.com/SteamRE/DepotDownloader) to download: `DepotDownloader -app 646570 -pubfile 2808845989` — requires whitelisting `*.steamserver.net` domain and Steam CM server IPs on port 443/27017-27050, OR
> 5. Contact Diamsword (the mod author) directly via [Steam Workshop](https://steamcommunity.com/id/diamsword/myworkshopfiles/?appid=646570) for permission and card data
>
> **Confirmed data summary:**
>
> | Category | Confirmed | Total | Coverage |
> |---|---|---|---|
> | Collectible card names | 29 | 76 | 38% |
> | Cards with exact stat values | 5 | 76 | 7% |
> | Starter/status card names | 5 | 5 | 100% |
> | Relics documented | 2 | 4 | 50% |
> | Potions documented | 0 | 3 | 0% |
>
> **Network requirements for SteamCMD/DepotDownloader (tested 2026-04-18):**
>
> | Domain | Purpose | Status |
> |---|---|---|
> | `api.steampowered.com` | Steam Web API | ✅ Accessible |
> | `steamcommunity.com` | Workshop pages | ✅ Accessible |
> | `store.steampowered.com` | Store pages | ✅ Accessible |
> | `steamcdn-a.akamaihd.net` | SteamCMD installer download | ✅ Accessible |
> | `client-update.steamstatic.com` | SteamCMD self-update/verification | ❌ Blocked |
> | `*.steamserver.net` | Steam CM (Connection Manager) servers | ❌ Blocked |
> | Steam CM IPs (162.254.x.x, 205.196.x.x) on ports 443/27017-27050 | Direct CM connections | ❌ Blocked |

---

*Last updated: 2026-04-18*
