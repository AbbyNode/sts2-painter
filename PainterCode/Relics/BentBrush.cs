using MegaCrit.Sts2.Core.Entities.Relics;

namespace Painter.PainterCode.Relics;

/// <summary>
/// Starter relic. The first time you Paint each combat, Paint 1 more of that
/// Color. Refreshes whenever you shuffle your draw pile.
/// </summary>
public class BentBrush : PainterRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    // The extra-paint logic is driven by CanvasState.FirstPaintSinceReshuffle.
    // TODO: Hook into the PaintColor flow so that when FirstPaintSinceReshuffle
    // is true and this relic is owned, one additional color is painted.
    // Also reset FirstPaintSinceReshuffle = true on draw pile shuffle.
}
