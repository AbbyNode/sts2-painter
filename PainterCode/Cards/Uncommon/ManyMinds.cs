using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Uncommon;

public class ManyMinds() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("draw", 3m)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var canvas = CanvasManager.Current;

        if (canvas.IsClear)
        {
            // Shuffle your discard pile into your draw pile
            var discardPile = PileType.Discard.GetPile(Owner);
            foreach (var card in discardPile.Cards.ToList())
                await CardPileCmd.Add(card, PileType.Draw, CardPilePosition.Random, this);
        }

        await CardPileCmd.Draw(ctx, DynamicVars["draw"].IntValue, Owner);

        if (canvas.IsChromatic)
        {
            // Exhaust 1 card from hand
            var selected = await CommonActions.SelectSingleCard(this, new("", "PAINTER-MANYMINDS.exhaust"), ctx, PileType.Hand);
            if (selected != null)
                await CardPileCmd.Add(selected, PileType.Exhaust, CardPilePosition.None, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["draw"].UpgradeValueBy(1m);
    }
}
