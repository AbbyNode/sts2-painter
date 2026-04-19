using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
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

    private const int CursedAmount = 6;

    public override async Task OnUse(PlayerChoiceContext ctx)
    {
        // TODO: Implement target selection and apply Cursed to selected enemy.
        // The exact potion target selection and CombatState access API needs to be
        // identified from CustomPotionModel. Expected usage:
        // await PowerCmd.Apply<CursedPower>(target, CursedAmount, ownerCreature, this);
        await Task.CompletedTask;
    }
}
