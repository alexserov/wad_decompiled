// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.IScroll
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll


using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public interface IScroll
  {
    void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount);

    void ScrollHorizontal(ScrollAmount amount);

    void ScrollVertical(ScrollAmount amount);

    void SetScrollPercent(double horizontalPercent, double verticalPercent);

    bool HorizontallyScrollable { get; }

    bool VerticallyScrollable { get; }

    double HorizontalScrollPercent { get; }

    double VerticalScrollPercent { get; }

    double HorizontalViewSize { get; }

    double VerticalViewSize { get; }
  }
}
