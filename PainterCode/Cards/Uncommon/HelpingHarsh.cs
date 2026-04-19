using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Uncommon;

public class HelpingHarsh() : PainterCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(7m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(ctx);

        // Paint a random non-special color (color choice UI pending Godot scene)
        var paintCount = IsUpgraded ? 2 : 1;
        CanvasManager.Current.PaintColor(PaintColor.Rainbow, paintCount);
    }

    protected override void OnUpgrade()
    {
        // Upgrade: Paint chosen color twice instead of once (handled in OnPlay)
    }
}
