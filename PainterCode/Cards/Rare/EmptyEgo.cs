using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Rare;

public class EmptyEgo() : PainterCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    private static readonly PaintColor[] NonSpecialColors =
        Enum.GetValues<PaintColor>().Where(c => !c.IsSpecial()).ToArray();

    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("paint", 3m)];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var canvas = CanvasManager.Current;

        if (canvas.IsClear)
        {
            var count = DynamicVars["paint"].IntValue;
            var available = NonSpecialColors.ToList();

            for (var i = 0; i < count && available.Count > 0; i++)
            {
                var index = Random.Shared.Next(available.Count);
                canvas.PaintColor(available[index]);
                available.RemoveAt(index);
            }
        }

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        DynamicVars["paint"].UpgradeValueBy(1m);
    }
}
