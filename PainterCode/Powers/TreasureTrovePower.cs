using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Painter.PainterCode.Powers;

public class TreasureTrovePower : PainterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext,
        CombatState combatState)
    {
        if (Owner.Player != player)
            return Task.CompletedTask;

        Flash();

        // TODO: Generate random Uncommon cards and add to hand.
        // Use CombatState.CreateCard and CardPileCmd.AddGeneratedCardToCombat
        // to add random Uncommon cards from the player's card pool to hand.

        return Task.CompletedTask;
    }
}
