using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Common;

public class PrismaticPierce() : PainterCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(4m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var distinctColors = CanvasManager.Current.GetDistinctColors().Count;

        for (var i = 0; i < distinctColors; i++)
            await CommonActions.CardAttack(this, play.Target).Execute(ctx);

        if (Owner.Player != null)
        {
            for (var i = 0; i < distinctColors; i++)
                await CardPileCmd.Draw(ctx, 1, Owner.Player);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
    }
}
