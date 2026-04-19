using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;
using Painter.PainterCode.Powers;

namespace Painter.PainterCode.Cards.Uncommon;

public class BlankBane() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("cursed", 3m)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var enemies = CombatState?.GetCreaturesOnSide(CombatSide.Enemy);
        if (enemies != null)
            foreach (var enemy in enemies)
                await PowerCmd.Apply<CursedPower>(enemy, DynamicVars["cursed"].IntValue, Owner.Creature, this);

        if (CanvasManager.Current.IsClear)
        {
            if (IsUpgraded)
            {
                CanvasManager.Current.PaintColor(PaintColor.Gray);
            }
            else
            {
                // TODO: Generate GrayGloom card and add to hand once the status card class is created
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["cursed"].UpgradeValueBy(1m);
    }
}
