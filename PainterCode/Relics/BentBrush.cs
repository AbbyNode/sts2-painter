using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Relics;

/// <summary>
/// Starter relic. The first time you Paint each combat, Paint 1 more of that
/// Color. Refreshes whenever you shuffle your draw pile.
/// </summary>
public class BentBrush : PainterRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    public override Task BeforeCombatStart()
    {
        CanvasManager.Current.BonusPaintEnabled = true;
        return Task.CompletedTask;
    }

    public override Task AfterShuffle(PlayerChoiceContext choiceContext, Player shuffler)
    {
        Flash();
        CanvasManager.Current.FirstPaintSinceReshuffle = true;
        return Task.CompletedTask;
    }
}
