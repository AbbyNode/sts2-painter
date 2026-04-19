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

public class InnovativeInk() : PainterCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        // Take 1 damage to self
        await CreatureCmd.Damage(ctx, [Owner.Creature], 1, default, Owner.Creature);

        // Deal damage to ALL enemies
        var enemies = CombatState?.GetCreaturesOnSide(CombatSide.Enemy);
        if (enemies != null)
        {
            await CreatureCmd.Damage(ctx, enemies, DynamicVars.Damage.IntValue, default, Owner.Creature);

            // Paint 1 Red for each damaged target
            CanvasManager.Current.PaintColor(PaintColor.Red, enemies.Count);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }
}
