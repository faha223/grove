﻿namespace Grove.Gameplay.Effects
{
  using System.Collections.Generic;
  using System.Linq;
  using Artifical.Decisions;
  using Decisions;
  using Decisions.Results;

  public class ReorderTopCards : Effect, IProcessDecisionResults<Ordering>, IChooseDecisionResults<List<Card>, Ordering>
  {
    private readonly int _count;

    private ReorderTopCards() {}

    public ReorderTopCards(int count)
    {
      _count = count;
    }

    public Ordering ChooseResult(List<Card> candidates)
    {
      return QuickDecisions.OrderTopCards(candidates, Controller);
    }

    public void ProcessResults(Ordering result)
    {
      Controller.ReorderTopCardsOfLibrary(result.Indices);
    }

    protected override void ResolveEffect()
    {
      var cards = Controller.Library
        .Take(_count)
        .ToList();

      foreach (var card in cards)
      {
        card.Peek();
      }

      Enqueue<Decisions.OrderCards>(
        controller: Controller,
        init: p =>
          {
            p.Cards = cards;
            p.ProcessDecisionResults = this;
            p.ChooseDecisionResults = this;
            p.Title = "Order cards from top to bottom";
          });
    }
  }
}