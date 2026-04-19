using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Cards.Uncommon;

public class EnjoyedEpiphany() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        // Find a random card with the Paint keyword from the card pool
        var cardPool = Owner.Character.CardPool;
        var paintCards = cardPool.AllCards
            .Where(c => c.Keywords.Contains(PainterKeywords.Paint))
            .ToList();

        if (paintCards.Count == 0)
            return;

        var randomCard = paintCards[Random.Shared.Next(paintCards.Count)];
        var newCard = CombatState!.CreateCard(randomCard, Owner);
        newCard.EnergyCost.SetThisTurn(0);
        await CardPileCmd.AddGeneratedCardToCombat(newCard, PileType.Hand, true);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
