using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Relics;

/// <summary>
/// Boss relic. Gain [E] at the start of your turn. At the end of your turn,
/// remove all Colors on the Canvas.
/// </summary>
public class BrokenBrush : PainterRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side,
        CombatState combatState)
    {
        if (side != CombatSide.Player)
            return;

        Flash();
        await PlayerCmd.GainEnergy(1, Owner);
    }

    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Player)
            return Task.CompletedTask;

        Flash();
        CanvasManager.Current.ClearColors();
        return Task.CompletedTask;
    }
}
