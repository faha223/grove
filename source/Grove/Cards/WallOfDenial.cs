﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Cards;
  using Core.Dsl;

  public class WallOfDenial : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return Card
        .Named("Wall of Denial")
        .ManaCost("{1}{W}{U}")
        .Type("Creature - Wall")
        .Text("{Defender}, {Flying}{EOL}{Shroud}(This creature can't be the target of spells or abilities.)")
        .FlavorText("It provides what every discerning mage requires—time to think.")
        .Power(0)
        .Toughness(8)
        .Abilities(
          Static.Flying,
          Static.Defender,
          Static.Shroud
        );
    }
  }
}