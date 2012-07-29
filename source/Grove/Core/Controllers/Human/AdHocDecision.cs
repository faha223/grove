﻿namespace Grove.Core.Controllers.Human
{
  using Ui.Shell;

  public class AdHocDecision<T> : Controllers.AdHocDecision<T> where T : class
  {
    public IShell Shell { get; set; }
    public Ui.SelectTarget.ViewModel.IFactory TargetDialog { get; set; }

    protected override void ExecuteQuery()
    {
      Result = QueryUi(this);
    }
  }
}