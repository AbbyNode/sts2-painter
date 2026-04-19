using MegaCrit.Sts2.Core.Entities.Relics;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Relics;

/// <summary>
/// Boss relic. When you Paint a random Color (Rainbow), Paint an additional one.
/// </summary>
public class BrilliantBrush : PainterRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    public override Task BeforeCombatStart()
    {
        CanvasManager.Current.ExtraRainbowEnabled = true;
        return Task.CompletedTask;
    }
}
