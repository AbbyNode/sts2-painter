using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Common;

public class RedRazzing() : PainterCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new IntVar("vuln", 2m),
        new IntVar("paint", 1m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var enemies = CombatState.GetCreaturesOnSide(CombatSide.Enemy);
        foreach (var enemy in enemies)
            await PowerCmd.Apply<VulnerablePower>(enemy, DynamicVars["vuln"].IntValue, Owner.Creature, this);

        if (CanvasManager.Current.IsClear)
            CanvasManager.Current.PaintColor(PaintColor.Red, DynamicVars["paint"].IntValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["paint"].UpgradeValueBy(1m);
    }
}
