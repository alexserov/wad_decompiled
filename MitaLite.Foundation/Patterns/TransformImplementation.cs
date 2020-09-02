// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.TransformImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class TransformImplementation : PatternImplementation<TransformPattern>, ITransform
  {
    public TransformImplementation(UIObject uiObject)
      : base(uiObject, TransformPattern.Pattern)
    {
    }

    public void Rotate(double degrees)
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (Rotate), new object[1]
      {
        (object) degrees
      })) != ActionResult.Unhandled)
        return;
      this.Pattern.Rotate(degrees);
    }

    public void Resize(double width, double height)
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (Resize), new object[2]
      {
        (object) width,
        (object) height
      })) != ActionResult.Unhandled)
        return;
      this.Pattern.Resize(width, height);
    }

    public void Move(double x, double y)
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (Move), new object[2]
      {
        (object) x,
        (object) y
      })) != ActionResult.Unhandled)
        return;
      this.Pattern.Move(x, y);
    }

    public bool CanRotate
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (CanRotate)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.CanRotate;
      }
    }

    public bool CanResize
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (CanResize)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.CanResize;
      }
    }

    public bool CanMove
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (CanMove)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.CanMove;
      }
    }
  }
}
