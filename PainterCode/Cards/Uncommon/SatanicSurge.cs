using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Powers;

namespace Painter.PainterCode.Cards.Uncommon;

public class SatanicSurge() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("cursed", 2m)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        if (play.Target != null)
        {
            await PowerCmd.Apply<CursedPower>(play.Target, DynamicVars["cursed"].IntValue, Owner.Creature, this);
            await PowerCmd.Apply<SatanicSurgePower>(play.Target, 1, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["cursed"].UpgradeValueBy(1m);
    }
}
