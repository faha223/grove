﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;
  using ReturnToHand = Effects.ReturnToHand;

  public class TragicPoet : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Tragic Poet")
        .ManaCost("{W}")
        .Type("Creature Human")
        .Text("{T}, Sacrifice Tragic Poet: Return target enchantment card from your graveyard to your hand.")
        .FlavorText(
          "I would weep, but my tears have been stolen; I would shout, but my voice has been taken. Thus, I write.")
        .Power(1)
        .Toughness(1)
        .ActivatedAbility(p =>
          {
            p.Text = "{T}, Sacrifice Tragic Poet: Return target enchantment card from your graveyard to your hand.";
            p.Cost = new AggregateCost(
              new Tap(),
              new Sacrifice());

            p.Effect = () => new ReturnToHand();
            p.TargetSelector.AddEffect(trg =>
              {
                trg.Is.Enchantment().In.YourGraveyard();
                trg.Message = "Select an enchantment in your graveyard.";
              });

            p.TimingRule(new Any(new OnFirstMain(), new OnOpponentsTurn(Step.DeclareBlockers)));            
            p.TargetingRule(new EffectRankBy(c => -c.Score));
          });
    }
  }
}