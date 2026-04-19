using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Painter.PainterCode.Cards.Uncommon;

public class InsaneInspiration() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        // TODO: Play the top card of your draw pile.
        // If it's a Painting, repeat this effect.
        // This requires draw-and-play logic which is complex to implement.
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        // TODO: Reduce cost to 0 once cost upgrade API is available
    }
}
