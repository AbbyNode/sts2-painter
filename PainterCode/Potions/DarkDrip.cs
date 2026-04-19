using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Potions;

/// <summary>
/// Common potion. Darken the Canvas.
/// </summary>
public class DarkDrip : PainterPotion
{
    public override PotionRarity Rarity => PotionRarity.Common;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.Self;

    protected override Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        CanvasManager.Current.DarkenCanvas();
        return Task.CompletedTask;
    }
}
