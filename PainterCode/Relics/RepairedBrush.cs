using MegaCrit.Sts2.Core.Entities.Relics;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Relics;

/// <summary>
/// Boss relic. Replaces Bent Brush. At the start of each combat, Darken the Canvas.
/// </summary>
public class RepairedBrush : PainterRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    public override Task BeforeCombatStart()
    {
        Flash();
        CanvasManager.Current.DarkenCanvas();
        return Task.CompletedTask;
    }
}
