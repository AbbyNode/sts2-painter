using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Potions;

/// <summary>
/// Uncommon potion. Choose and Paint 3 Colors.
/// </summary>
public class PaintPotion : PainterPotion
{
    public override PotionRarity Rarity => PotionRarity.Uncommon;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.Self;

    private const int ColorsToPaint = 3;

    protected override Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        // Paint 3 random colors (color choice UI pending Godot scene)
        CanvasManager.Current.PaintColor(PaintColor.Rainbow, ColorsToPaint);
        return Task.CompletedTask;
    }
}
