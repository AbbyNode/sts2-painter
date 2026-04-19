using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Powers;

namespace Painter.PainterCode.Potions;

/// <summary>
/// Rare potion. Apply 6 Cursed to a target.
/// </summary>
public class CursedPotion : PainterPotion
{
    public override PotionRarity Rarity => PotionRarity.Rare;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.AnyEnemy;

    private const int CursedAmount = 6;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        if (target != null)
            await PowerCmd.Apply<CursedPower>(target, CursedAmount, Owner.Creature, null);
    }
}
