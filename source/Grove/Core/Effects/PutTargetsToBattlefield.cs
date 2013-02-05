﻿namespace Grove.Core.Effects
{
  using System.Linq;
  using Grove.Core.Decisions;
  using Grove.Core.Decisions.Results;
  using Grove.Core.Targeting;
  using Grove.Core.Zones;

  public class PutTargetsToBattlefield : Effect, IProcessDecisionResults<ChosenCards>
  {
    public bool MustSacCreatureOnResolve;
    public bool Tapped;

    public void ResultProcessed(ChosenCards results)
    {
      PutValidTargetsToBattlefield();
    }

    protected override void ResolveEffect()
    {
      if (MustSacCreatureOnResolve)
      {
        if (Controller.Battlefield.Creatures.Count() == 0)
          return;

        SacCreatureAndPutValidTargetsToBattlefield();
        return;
      }

      PutValidTargetsToBattlefield();
    }

    private void SacCreatureAndPutValidTargetsToBattlefield()
    {
      Game.Enqueue<SelectCardsToSacrifice>(Controller, p =>
        {
          p.MinCount = 1;
          p.MaxCount = 1;
          p.Validator = card => card.Is().Creature;
          p.Zone = Zone.Battlefield;
          p.Text = FormatText("Select a creature to sacrifice");
          p.ProcessDecisionResults = this;
        });
    }

    private void PutValidTargetsToBattlefield()
    {
      foreach (var target in ValidEffectTargets)
      {
        var card = target.Card();

        Controller.PutCardToBattlefield(card);

        if (Tapped)
        {
          card.Tap();
        }
      }
    }
  }
}