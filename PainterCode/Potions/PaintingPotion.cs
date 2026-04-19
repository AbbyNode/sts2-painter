using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Painter.PainterCode.Potions;

/// <summary>
/// Common potion. Add a copy of the Canvas (Painting card) into your hand.
/// </summary>
public class PaintingPotion : PainterPotion
{
    public override PotionRarity Rarity => PotionRarity.Common;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.Self;

    protected override Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        // TODO: Create a Painting card from the current canvas state and add it to hand.
        // Use CombatState.CreateCard and CardPileCmd.AddGeneratedCardToCombat
        // once the potion's access to combat state is confirmed.
        return Task.CompletedTask;
    }
}
