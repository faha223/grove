﻿namespace Grove.Core.Ai.TimingRules
{
  public class MainSteps : TimingRule
  {
    public override bool ShouldPlay(TimingRuleParameters p)
    {
      return Turn.Step == Step.FirstMain || Turn.Step == Step.SecondMain;
    }
  }
}