﻿namespace Grove.Gameplay.Triggers
{
  using System;
  using Abilities;
  using Infrastructure;
  using Messages;
  using Zones;

  public class OnZoneChanged : Trigger, IReceive<ZoneChanged>
  {
    private readonly Func<Card, TriggeredAbility, Game, bool> _filter;
    private readonly Zone _from;
    private readonly Zone _to;

    private OnZoneChanged() {}

    public OnZoneChanged(Zone @from = Zone.None, Zone to = Zone.None,
      Func<Card, TriggeredAbility, Game, bool> filter = null)
    {
      _from = from;
      _to = to;
      _filter = filter ?? ((card, ability, game) => ability.OwningCard == card);
    }

    public void Receive(ZoneChanged message)
    {
      if (!_filter(message.Card, Ability, Game))
        return;

      if (message.From == Zone.None)
        return;

      if (_from == Zone.None && _to == message.To)
        Set(message);
      else if (_from == message.From && _to == message.To)
        Set(message);
      else if (_from == message.From && _to == Zone.None)
        Set(message);
    }
  }
}