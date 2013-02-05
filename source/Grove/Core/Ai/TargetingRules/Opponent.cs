﻿namespace Grove.Core.Ai.TargetingRules
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Targeting;

  public class Opponent : TargetingRule
  {
    protected override IEnumerable<Targets> SelectTargets(TargetingRuleParameters p)
    {
      var candidates = p.Candidates<Player>()
        .Where(x => x == p.Controller.Opponent);

      return Group(candidates, 1);
    }
  }
}