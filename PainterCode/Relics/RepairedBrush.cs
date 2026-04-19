using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Relics;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Relics;

/// <summary>
/// Boss relic. Replaces Bent Brush. At the start of each combat, Darken the Canvas.
/// </summary>
public class RepairedBrush : PainterRelic
{
    public override RelicRarity Rarity => RelicRarity.Boss;

    public override Task AfterCombatStart(PlayerChoiceContext choiceContext, CombatState combatState)
    {
        Flash();
        CanvasManager.Current.DarkenCanvas();
        return Task.CompletedTask;
    }
}
