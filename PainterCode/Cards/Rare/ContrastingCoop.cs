using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Rare;

public class ContrastingCoop() : PainterCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        CanvasManager.Current.PaintColor(PaintColor.Red);
        CanvasManager.Current.PaintColor(PaintColor.Blue);
        CanvasManager.Current.PaintColor(PaintColor.Green);
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
        EnergyCost.UpgradeBy(-1);
    }
}
