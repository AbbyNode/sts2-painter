using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Uncommon;

public class YellowYearning() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new IntVar("paint", 1m),
        new IntVar("energy", 1m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await PlayerCmd.GainEnergy(DynamicVars["energy"].IntValue, Owner);
        CanvasManager.Current.PaintColor(PaintColor.Yellow, DynamicVars["paint"].IntValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["energy"].UpgradeValueBy(1m);
    }
}
