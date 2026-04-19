using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Painter.PainterCode.Potions;

/// <summary>
/// Common potion. Add a copy of the Canvas (Painting card) into your hand.
/// </summary>
public class PaintingPotion : PainterPotion
{
    public override PotionRarity Rarity => PotionRarity.Common;

    public override Task OnUse(PlayerChoiceContext ctx)
    {
        // TODO: Create a Painting card from the current canvas state and add it to hand.
        // Use CombatState.CreateCard and CardPileCmd.AddGeneratedCardToCombat
        // once the potion's access to combat state is confirmed.
        return Task.CompletedTask;
    }
}
