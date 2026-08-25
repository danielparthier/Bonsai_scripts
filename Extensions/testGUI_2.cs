using Bonsai;
using Hexa.NET.ImGui;
using Hexa.NET.ImPlot;
using System;
using System.ComponentModel;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;

[Combinator]
[Description("Draws the ImGui window, menu, color editor, plot, and scrolling text.")]
public class MyFirstTool
{
    public MyFirstTool()
    {
        Color = new Vector4(0.4f, 0.7f, 0.0f, 1.0f);
        SampleCount = 100;
        LogText = "This is some scrolling text.";
    }

    public Vector4 Color { get; set; }
    public float[] Samples { get; set; }
    public string LogText { get; set; }
    public bool CloseRequested { get; private set; }
    public int SampleCount { get; set; }

    public IObservable<TSource> Process<TSource>(IObservable<TSource> source)
    {
        return Observable.Create<TSource>(observer =>
        {
            return source.Subscribe(
                value =>
                {
                    if (ImGui.Begin("My First Tool"))
                    {
                        if (ImGui.BeginMenuBar())
                        {
                            if (ImGui.BeginMenu("File"))
                            {
                                if (ImGui.MenuItem("Close"))
                                {
                                    CloseRequested = true;
                                }
                                ImGui.EndMenu();
                            }
                            ImGui.EndMenuBar();
                        }

                        ImGui.ColorEdit4("Color", ref Color);
                        ImGui.Separator();

                        if (ImPlot.BeginPlot("Sample Plot"))
                        {
                            ImPlot.PlotLine("my samples", Samples, Samples.Length);
                            ImPlot.EndPlot();
                        }

                        ImGui.BeginChild("ScrollingRegion", new Vector2(0, 200), true);
                        ImGui.Text(LogText);
                        ImGui.EndChild();
                    }
                    ImGui.End();

                    observer.OnNext(value);
                },
                observer.OnError,
                observer.OnCompleted);
        });
    }
}