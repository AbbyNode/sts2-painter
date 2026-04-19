using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Painter.PainterCode.Cards.Uncommon;

public class SideSculpture() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        // TODO: Create a Painting card from the current canvas state and add it to hand.
        // Use CombatState.CreateCard and CardPileCmd.AddGeneratedCardToCombat
        // once the Painting card generation system is available.
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        // TODO: Improve the generated Painting once the system is available
    }
}
