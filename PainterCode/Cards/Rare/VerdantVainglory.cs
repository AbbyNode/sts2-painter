using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Rare;

public class VerdantVainglory() : PainterCard(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    private const int MaxHandSize = 10;

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("paint", 3m)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var hand = PileType.Hand.GetPile(Owner);
        var cardsToDraw = MaxHandSize - hand.Cards.Count;
        if (cardsToDraw > 0)
            await CardPileCmd.Draw(ctx, cardsToDraw, Owner);

        CanvasManager.Current.PaintColor(PaintColor.Green, DynamicVars["paint"].IntValue);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
