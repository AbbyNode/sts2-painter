using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Cards.Common;

public class CrushingCanvas() : PainterCard(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(0m, ValueProp.Move),
        new IntVar("bonus", 3m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var paintingCount = CountPaintingCards();
        var bonus = DynamicVars["bonus"].IntValue;
        var baseDmg = Upgraded ? 1 : 0;
        DynamicVars.Damage.SetValue(baseDmg + bonus * paintingCount);

        await CommonActions.CardAttack(this, play.Target).Execute(ctx);
    }

    private int CountPaintingCards()
    {
        var player = Owner.Player;
        if (player == null) return 0;

        var count = 0;
        foreach (var card in player.Hand)
            if (card.Keywords.Contains(PainterKeywords.Painting)) count++;
        foreach (var card in player.DiscardPile)
            if (card.Keywords.Contains(PainterKeywords.Painting)) count++;
        foreach (var card in player.DrawPile)
            if (card.Keywords.Contains(PainterKeywords.Painting)) count++;
        return count;
    }

    protected override void OnUpgrade()
    {
        DynamicVars["bonus"].UpgradeValueBy(2m);
    }
}
