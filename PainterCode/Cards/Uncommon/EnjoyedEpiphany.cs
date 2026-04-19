using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Painter.PainterCode.Cards.Uncommon;

public class EnjoyedEpiphany() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        // TODO: Find a random card which Paints a specific Color from the card pool,
        // add it to hand, and set its cost to 0 this turn.
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        // TODO: Reduce cost to 0 once cost upgrade API is available
    }
}
