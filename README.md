# Painter — Slay the Spire 2 Character Mod

A Slay the Spire 2 character mod built on [AbbyNode/sts2-mod-template](https://github.com/AbbyNode/sts2-mod-template), using [Alchyr/ModTemplate-StS2](https://github.com/Alchyr/ModTemplate-StS2) as reference for character/card structure.

## Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Slay the Spire 2 (Early Access) installed via Steam
- [MegaDot / Godot 4.5.1 mono](https://github.com/godotengine/godot/releases/tag/4.5.1-stable) for exporting `.pck` files
- [BaseLib](https://github.com/Alchyr/STS2-BaseLib) mod loader

## Getting Started

1. Clone this repository.
2. Copy `Directory.Build.props` settings to your local machine (the file is git-ignored so it stays machine-specific):
   - Set `GodotPath` to your local MegaDot executable.
   - Optionally set `Sts2Path` if auto-detection fails.
3. Open `Painter.csproj` in your IDE or run `dotnet build` to build locally.

## Project Structure

```
├── .github/workflows/
│   ├── build.yml         # Bumps version, builds, and publishes a GitHub Release on push to main
│   └── ci.yml            # Builds on pull requests to main
├── Painter/              # Godot assets (images, scenes, localization, etc.)
│   ├── images/
│   │   ├── card_portraits/   # Card art (500x380 normal, 250x190 small)
│   │   │   └── big/          # Full card art (606x852 normal, 250x350 small)
│   │   ├── charui/           # Character UI icons
│   │   ├── powers/           # Power icons
│   │   │   └── big/
│   │   └── relics/           # Relic icons
│   │       └── big/
│   ├── localization/eng/     # English localization JSON files
│   └── mod_image.png
├── PainterCode/          # C# source code
│   ├── Cards/            # Base card class
│   ├── Character/        # Character, card pool, relic pool, potion pool
│   ├── Extensions/       # Asset path helpers
│   ├── Potions/          # Base potion class
│   ├── Powers/           # Base power class
│   ├── Relics/           # Base relic class
│   └── MainFile.cs       # Mod entry point
├── packages/             # NuGet package cache (excluded from Godot)
├── Directory.Build.props # Local machine config (git-ignored)
├── Sts2PathDiscovery.props
├── Painter.csproj
├── Painter.json          # Mod manifest
├── export_presets.cfg
└── project.godot
```

## Building

```bash
dotnet restore
dotnet build
```

## Publishing (with .pck export)

Make sure `GodotPath` is set in `Directory.Build.props`, then:

```bash
dotnet publish -c Release
```

## CI/CD

- **CI** (`ci.yml`): Triggered on pull requests to `main`. Builds the mod to verify it compiles.
- **Build** (`build.yml`): Triggered on pushes to `main`. Bumps the minor version in `Painter.json`, builds and publishes the mod, then creates a GitHub Release with a zipped artifact.
