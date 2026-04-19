using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Uncommon;

public class ArdentAqua() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new IntVar("paint", 2m),
        new IntVar("dex", 1m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        CanvasManager.Current.PaintColor(PaintColor.Blue, DynamicVars["paint"].IntValue);
        await PowerCmd.Apply<DexterityPower>(Owner.Creature, DynamicVars["dex"].IntValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["dex"].UpgradeValueBy(1m);
    }
}
