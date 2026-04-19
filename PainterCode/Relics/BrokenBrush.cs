using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Relics;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Relics;

/// <summary>
/// Boss relic. Gain [E] at the start of your turn. At the end of your turn,
/// remove all Colors on the Canvas.
/// </summary>
public class BrokenBrush : PainterRelic
{
    public override RelicRarity Rarity => RelicRarity.Boss;

    public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side,
        CombatState combatState)
    {
        if (side != CombatSide.Player)
            return Task.CompletedTask;

        Flash();
        // TODO: Grant 1 Energy via the appropriate energy API
        // e.g. await EnergyCmd.GainEnergy(choiceContext, 1);
        return Task.CompletedTask;
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
