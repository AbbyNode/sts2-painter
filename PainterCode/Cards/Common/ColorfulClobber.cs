using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Common;

public class ColorfulClobber() : PainterCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    private static readonly PaintColor[] NonSpecialColors =
        Enum.GetValues<PaintColor>().Where(c => !c.IsSpecial()).ToArray();

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(9m, ValueProp.Move),
        new IntVar("paint", 2m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(ctx);

        var canvas = CanvasManager.Current;
        if (canvas.IsClear)
        {
            var count = DynamicVars["paint"].IntValue;
            for (var i = 0; i < count; i++)
            {
                var randomColor = NonSpecialColors[Random.Shared.Next(NonSpecialColors.Length)];
                canvas.PaintColor(randomColor);
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
        DynamicVars["paint"].UpgradeValueBy(1m);
    }
}
