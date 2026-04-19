using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Cards.Status;
using Painter.PainterCode.Powers;

namespace Painter.PainterCode.Cards.Uncommon;

public class SweepingSadness() : PainterCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(12, ValueProp.Move),
        new IntVar("cursed", 4m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);

        var enemies = CombatState?.GetCreaturesOnSide(CombatSide.Enemy);
        if (enemies != null)
            foreach (var enemy in enemies)
                await PowerCmd.Apply<CursedPower>(enemy, DynamicVars["cursed"].IntValue, Owner.Creature, this);

        var grayGloom = CombatState!.CreateCard<GrayGloom>(Owner);
        await CardPileCmd.AddGeneratedCardToCombat(grayGloom, PileType.Hand, false);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
        DynamicVars["cursed"].UpgradeValueBy(1m);
    }
}
