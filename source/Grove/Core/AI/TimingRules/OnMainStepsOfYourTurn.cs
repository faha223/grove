﻿namespace Grove.AI.TimingRules
{
  public class OnMainStepsOfYourTurn : TimingRule
  {
    public override bool? ShouldPlay1(TimingRuleParameters p)
    {
      return (Turn.Step == Step.FirstMain || Turn.Step == Step.SecondMain) && p.Controller.IsActive && Stack.IsEmpty;
    }
  }
}