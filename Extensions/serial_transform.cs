using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("Packs pin, frequency, and duration into a 5-byte serial packet for the Arduino.")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class serial_transform
{
    public IObservable<byte[]> Process(IObservable<Tuple<int, int, int>> source)
    {
        return source.Select(value =>
        {
            int pin = value.Item1;
            int frequency = value.Item2;
            int duration = value.Item3;

            return new byte[]
            {
                (byte)pin,
                (byte)(frequency >> 8),
                (byte)(frequency & 0xFF),
                (byte)(duration >> 24),
                (byte)(duration >> 16),
                (byte)(duration >> 8),
                (byte)(duration & 0xFF)
            };
        });
    }
}