﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Cards.Costs;
  using Core.Cards.Effects;
  using Core.Dsl;
  using Core.Mana;
  using Core.Targeting;

  public class BarrinMasterWizard : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return Card
        .Named("Barrin, Master Wizard")
        .ManaCost("{1}{U}{U}")
        .Type("Legendary Creature Human Wizard")
        .Text("{2}, Sacrifice a permanent: Return target creature to its owner's hand.")
        .FlavorText(
          "'Knowledge is no more expensive than ignorance, and at least as satisfying.'{EOL}—Barrin, master wizard")
        .Power(1)
        .Toughness(1)
        .Timing(Timings.FirstMain())
        .Abilities(
          ActivatedAbility(
            "{2}, Sacrifice a permanent: Return target creature to its owner's hand.",
            Cost<PayMana, Sacrifice>(cost => cost.Amount = 2.Colorless()),
            Effect<PutToHand>(),
            effectValidator: TargetValidator(
              TargetIs.Card(x => x.Is().Creature), 
              ZoneIs.Battlefield(),
              text: "Select a creature to bounce."),
            costValidator: TargetValidator(
                TargetIs.Card(controller: Controller.SpellOwner),
                ZoneIs.Battlefield(),
                text: "Select a permanent to sacrifice.",
                mustBeTargetable: false),
            targetSelectorAi: TargetSelectorAi.SacPermanentToBounce(),
            timing: Any(Timings.InstantRemovalTarget()))
        );
    }
  }
}