using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Uncommon;

public class ChosenColors() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        if (CanvasManager.Current.IsChromatic)
        {
            // TODO: Create a Painting card from the current canvas and add it to hand
            // Use CombatState.CreateCard and CardPileCmd.AddGeneratedCardToCombat
        }

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        // TODO: Reduce cost by 1 once the cost upgrade API is available
    }
}
