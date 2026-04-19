using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Uncommon;

public class DoubleDemolition() : PainterCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(7m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var enemies = CombatState?.GetCreaturesOnSide(CombatSide.Enemy);
        if (enemies != null)
        {
            for (var hit = 0; hit < 2; hit++)
                await CreatureCmd.Damage(ctx, enemies, DynamicVars.Damage.IntValue, default, Owner.Creature);
        }

        if (CanvasManager.Current.IsChromatic)
        {
            var distinctColors = CanvasManager.Current.GetDistinctColors();
            foreach (var color in distinctColors)
                CanvasManager.Current.PaintColor(color);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
    }
}
