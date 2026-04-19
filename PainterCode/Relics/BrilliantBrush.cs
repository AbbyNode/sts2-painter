using MegaCrit.Sts2.Core.Models.Relics;

namespace Painter.PainterCode.Relics;

/// <summary>
/// Boss relic. When you Paint a random Color (Rainbow), Paint an additional one.
/// </summary>
public class BrilliantBrush : PainterRelic
{
    public override RelicRarity Rarity => RelicRarity.Boss;

    // TODO: Hook into CanvasState.PaintColor's Rainbow resolution so that
    // when a Rainbow color is resolved, an extra random color is painted.
}
