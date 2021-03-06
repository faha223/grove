﻿namespace Grove.Costs
{
  using System;

  public class SpellCostModifier : CostModifier
  {
    private readonly IManaAmount _amount;
    private readonly bool _less;
    private readonly Func<Card, CostModifier, bool> _filter;

    private SpellCostModifier() {}

    public SpellCostModifier(IManaAmount amount, Func<Card, CostModifier, bool> filter = null, bool less = true)
    {
      _amount = amount;
      _less = less;
      _filter = filter ?? delegate { return true; };
    }

    public override IManaAmount GetActualCost(IManaAmount amount, ManaUsage manaUsage, Card card)
    {
      if (manaUsage != ManaUsage.Spells)
        return amount;

      if (_filter(card, this))
      {
        return _less ? amount.Remove(_amount) : amount.Add(_amount);
      }

      return amount;
    }
  }
}