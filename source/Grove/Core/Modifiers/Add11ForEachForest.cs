﻿namespace Grove.Core.Modifiers
{
  using System.Linq;
  using Infrastructure;
  using Messages;
  using Zones;

  public class Add11ForEachForest : Modifier, IReceive<ZoneChanged>, IReceive<ControllerChanged>
  {
    private Increment _increment = new Increment(0);
    private Power _power;
    private Toughness _tougness;

    public void Receive(ControllerChanged message)
    {
      if (message.Card.Is("forest") || message.Card == Source)
      {
        _increment.Value = GetForestCount(Source.Controller);
      }
    }

    public void Receive(ZoneChanged message)
    {
      if (!IsForestControlledBySpellOwner(message.Card))
        return;

      if (message.From == Zone.Battlefield)
      {
        _increment--;
      }

      else if (message.To == Zone.Battlefield)
      {
        _increment++;
      }
    }

    public override void Apply(Power power)
    {
      _power = power;
      power.AddModifier(_increment);
    }

    public override void Apply(Toughness toughness)
    {
      _tougness = toughness;
      toughness.AddModifier(_increment);
    }

    public override Modifier Initialize(ModifierParameters p, Game game)
    {
      base.Initialize(p, game);
      _increment.Initialize(game.ChangeTracker);
      _increment.Value = GetForestCount(Source.Controller);
      return this;
    }

    protected override void Unapply()
    {
      _power.RemoveModifier(_increment);
      _tougness.RemoveModifier(_increment);
    }

    private static int GetForestCount(Player player)
    {
      return player.Battlefield.Count(x => x.Is("forest"));
    }

    private bool IsForestControlledBySpellOwner(Card permanent)
    {
      return permanent.Is("forest") &&
        permanent.Controller == Source.Controller;
    }
  }
}