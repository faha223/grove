﻿namespace Grove.Cards
{
  using System;
  using System.Collections.Generic;
  using Gameplay.Abilities;
  using Gameplay.Effects;
  using Gameplay.Misc;
  using Gameplay.Triggers;

  public class ArgothianEnchantress : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Argothian Enchantress")
        .ManaCost("{1}{G}")
        .Type("Creature Human Druid")
        .Text(
          "{Shroud}(This permanent can't be the target of spells or abilities.){EOL}Whenever you cast an enchantment spell, draw a card.")
        .Power(0)
        .Toughness(1)
        .SimpleAbilities(Static.Shroud)
        .TriggeredAbility(p =>
          {
            p.Text = "Whenever you cast an enchantment spell, draw a card.";
            p.Trigger(new OnCastedSpell((ability, card) =>
              card.Controller == ability.OwningCard.Controller && card.Is().Enchantment));
            p.Effect = () => new DrawCards(1);
            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}