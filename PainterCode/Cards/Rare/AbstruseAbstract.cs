using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Painter.PainterCode.Cards.Rare;

public class AbstruseAbstract() : PainterCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        // TODO: Create a Painting card from the current Canvas state and add it to hand
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        // TODO: Exhaust → Exhaustive once Exhaustive keyword is available; keep Exhaust for now
    }
}
