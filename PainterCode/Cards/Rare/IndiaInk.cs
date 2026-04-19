using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Rare;

public class IndiaInk() : PainterCard(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("energy", 1m)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await PlayerCmd.GainEnergy(DynamicVars["energy"].IntValue, Owner);
        CanvasManager.Current.DarkenCanvas();
    }

    protected override void OnUpgrade()
    {
        DynamicVars["energy"].UpgradeValueBy(1m);
    }
}
