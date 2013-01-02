﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Cards.Costs;
  using Core.Cards.Effects;
  using Core.Dsl;
  using Core.Targeting;

  public class EliteArchers : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return Card
        .Named("Elite Archers")
        .ManaCost("{5}{W}")
        .Type("Creature Human Soldier Archer")
        .Text("{T}: Elite Archers deals 3 damage to target attacking or blocking creature.")
        .FlavorText("Arrows fletched with the feathers of angels seldom miss their mark.")
        .Power(3)
        .Toughness(3)
        .Timing(Timings.Creatures())
        .Abilities(
          ActivatedAbility(
            "{T}: Elite Archers deals 3 damage to target attacking or blocking creature.",
            Cost<Tap>(),
            Effect<DealDamageToTargets>(e => e.Amount = 3),
            TargetValidator(TargetIs.AttackerOrBlocker(), ZoneIs.Battlefield()),
            targetSelectorAi: TargetSelectorAi.DealDamageSingleSelector(3),
            timing: Timings.DeclareBlockers()
            )
        );
    }
  }
}