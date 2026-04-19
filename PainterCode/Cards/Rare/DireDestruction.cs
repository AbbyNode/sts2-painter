using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Cards.Rare;

public class DireDestruction() : PainterCard(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("dmgPer", 12m)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var paintings = new List<CardModel>();

        foreach (var pileType in new[] { PileType.Hand, PileType.Draw, PileType.Discard })
        {
            var pile = pileType.GetPile(Owner);
            paintings.AddRange(pile.Cards.Where(c => c.Keywords.Contains(PainterKeywords.Painting)).ToList());
        }

        foreach (var painting in paintings)
            await CardPileCmd.Add(painting, PileType.Exhaust, CardPilePosition.None, this);

        var enemies = CombatState?.GetCreaturesOnSide(CombatSide.Enemy);
        if (enemies != null && paintings.Count > 0)
        {
            var dmgPer = DynamicVars["dmgPer"].IntValue;
            for (var i = 0; i < paintings.Count; i++)
            {
                var randomEnemy = enemies[Random.Shared.Next(enemies.Count)];
                await CreatureCmd.Damage(ctx, [randomEnemy], dmgPer, default, Owner.Creature);
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["dmgPer"].UpgradeValueBy(2m);
    }
}
