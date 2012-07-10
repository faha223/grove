﻿namespace Grove.Cards
{
  using System;
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.CardDsl;
  using Core.Modifiers;
  using Core.Preventions;

  public class UrzasArmor : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return C.Card
        .Named("Urza's Armor")
        .ManaCost("{6}")
        .Type("Artifact")
        .Text("If a source would deal damage to you, prevent 1 of that damage.")
        .FlavorText(
          "'Tawnos's blueprints were critical to the creation of my armor. As he once sealed himself in steel, I sealed myself in a walking crypt.'{EOL}—Urza")
        .Timing(Timings.FirstMain())
        .Abilities(
          C.Continuous((e, c) =>
            {
              e.ModifierFactory = c.Modifier<AddDamagePrevention>(
                (m, c0) => m.Prevention = c0.Prevention<PreventDamageFromAnySource>((p, _) => p.SetAmount(1)));
              e.CardFilter = delegate { return false; };
              e.PlayerFilter = (player, armor) => player == armor.Controller;                                          
            })
        );
    }
  }
}