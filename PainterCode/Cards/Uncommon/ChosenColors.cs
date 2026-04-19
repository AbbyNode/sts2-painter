using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;
using Painter.PainterCode.Cards.Status;

namespace Painter.PainterCode.Cards.Uncommon;

public class ChosenColors() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        if (CanvasManager.Current.IsChromatic)
        {
            var painting = CombatState!.CreateCard<Painting>(Owner);
            await CardPileCmd.AddGeneratedCardToCombat(painting, PileType.Hand, true);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
