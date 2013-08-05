﻿namespace Grove.UserInterface.Decisions
{
  using System;
  using System.Linq;
  using Gameplay.Targeting;
  using Gameplay.Zones;
  using SelectTarget;
  using Shell;

  public class DiscardCards : Gameplay.Decisions.DiscardCards
  {
    public IShell Shell { get; set; }
    public ViewModel.IFactory DialogFactory { get; set; }

    protected override void ExecuteQuery()
    {
      var parameters = new TargetValidatorParameters
        {
          MinCount = Count,
          MaxCount = Count,
          Message = String.Format("Select {0} card(s) to discard.", Count),
          IsValidTarget = p => Filter(p.Target.Card()),
          IsValidZone = p => p.ZoneOwner == CardsOwner && p.Zone == Zone.Hand
        };

      var targetValidator = new TargetValidator(parameters);
      targetValidator.Initialize(Game, Controller);

      var dialog = DialogFactory.Create(new SelectTargetParameters
        {
          CanCancel = false,
          Validator = targetValidator
        });

      Shell.ShowModalDialog(dialog, DialogType.Small, InteractionState.SelectTarget);
      Result = dialog.Selection.ToList();
    }
  }
}