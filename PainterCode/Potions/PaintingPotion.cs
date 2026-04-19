using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Cards.Status;

namespace Painter.PainterCode.Potions;

/// <summary>
/// Common potion. Add a copy of the Canvas (Painting card) into your hand.
/// </summary>
public class PaintingPotion : PainterPotion
{
    public override PotionRarity Rarity => PotionRarity.Common;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.Self;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        var combatState = Owner.Creature.CombatState;
        if (combatState == null) return;
        var painting = combatState.CreateCard<Painting>(Owner);
        await CardPileCmd.AddGeneratedCardToCombat(painting, PileType.Hand, true);
    }
}
