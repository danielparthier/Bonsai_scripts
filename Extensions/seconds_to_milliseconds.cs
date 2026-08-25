using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class seconds_to_milliseconds
{
    public IObservable<int> Process(IObservable<float> source)
    {
        return source.Select(value => (int)Math.Round(value * 1000));
    }
}
