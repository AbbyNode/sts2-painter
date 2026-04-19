using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Rare;

public class YellowYowling() : PainterCard(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new IntVar("gray", 3m),
        new IntVar("yellow", 3m)
    ];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        CanvasManager.Current.PaintColor(PaintColor.Gray, DynamicVars["gray"].IntValue);
        CanvasManager.Current.PaintColor(PaintColor.Yellow, DynamicVars["yellow"].IntValue);
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        DynamicVars["gray"].UpgradeValueBy(-1m);
        DynamicVars["yellow"].UpgradeValueBy(1m);
    }
}
