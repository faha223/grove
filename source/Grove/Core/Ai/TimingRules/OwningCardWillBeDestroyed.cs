﻿namespace Grove.Core.Ai.TimingRules
{
  using System;

  public class OwningCardWillBeDestroyed : TimingRule
  {        
    public override bool ShouldPlay(TimingRuleParameters p)
    {
      return CanBeDestroyed(p);
    }
  }
}