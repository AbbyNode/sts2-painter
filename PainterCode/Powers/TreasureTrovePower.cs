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

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext,
        CombatState combatState)
    {
        if (Owner.Player != player)
            return;

        Flash();

        // Generate Amount random Uncommon cards from the card pool and add to hand
        var cardPool = player.Character.CardPool;
        var uncommonCards = cardPool.AllCards
            .Where(c => c.Rarity == CardRarity.Uncommon)
            .ToList();

        if (uncommonCards.Count == 0)
            return;

        for (var i = 0; i < Amount; i++)
        {
            var randomCard = uncommonCards[Random.Shared.Next(uncommonCards.Count)];
            var newCard = combatState.CreateCard(randomCard, player);
            await CardPileCmd.AddGeneratedCardToCombat(newCard, PileType.Hand, true);
        }
    }
}
