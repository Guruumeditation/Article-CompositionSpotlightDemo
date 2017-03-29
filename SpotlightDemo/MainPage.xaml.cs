using System;
using System.Collections.Generic;
using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml.Hosting;

namespace SpotlightDemo
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            var visualRectangle = ElementCompositionPreview.GetElementVisual(TheRectangle);
            var compositor = visualRectangle.Compositor;
            var spotLight = compositor.CreateSpotLight();
            spotLight.CoordinateSpace = visualRectangle;
            spotLight.InnerConeAngleInDegrees = 0;
            spotLight.InnerConeColor = Colors.Red;
            spotLight.OuterConeAngleInDegrees = 0;
            spotLight.OuterConeColor = Colors.Yellow;
            spotLight.Offset = new Vector3(150, 150, 100);
            spotLight.Targets.Add(visualRectangle);

            var animinnercone = compositor.CreateScalarKeyFrameAnimation();
            animinnercone.InsertKeyFrame(0.0f, 0, compositor.CreateLinearEasingFunction());
            animinnercone.InsertKeyFrame(0.5f, 30, compositor.CreateLinearEasingFunction());
            animinnercone.InsertKeyFrame(1.0f, 0, compositor.CreateLinearEasingFunction());
            animinnercone.Duration = TimeSpan.FromSeconds(4);
            animinnercone.IterationBehavior = AnimationIterationBehavior.Forever;
            animinnercone.StopBehavior = AnimationStopBehavior.LeaveCurrentValue;

            var animoutercone = compositor.CreateScalarKeyFrameAnimation();
            animoutercone.InsertKeyFrame(0.0f, 0, compositor.CreateLinearEasingFunction());
            animoutercone.InsertKeyFrame(0.5f, 45, compositor.CreateLinearEasingFunction());
            animoutercone.InsertKeyFrame(1.0f, 0, compositor.CreateLinearEasingFunction());
            animoutercone.Duration = TimeSpan.FromSeconds(4);
            animoutercone.IterationBehavior = AnimationIterationBehavior.Forever;
            animoutercone.StopBehavior = AnimationStopBehavior.LeaveCurrentValue;

            var colors = new List<Color>
            {
                Colors.Red,
                Colors.Blue,
                Colors.Green,
                Colors.Yellow,
                Colors.Pink,
                Colors.Fuchsia,
                Colors.Red
            };

            var animcolor = compositor.CreateColorKeyFrameAnimation();
            var step = 1.0f / ((float)colors.Count - 1);

            for (var i = 1; i < colors.Count; i++)
            {
                var currentstep = step * i;
                animcolor.InsertKeyFrame(currentstep - 0.0001f, colors[i - 1]);
                animcolor.InsertKeyFrame(currentstep, colors[i]);
            }

            animcolor.Duration = TimeSpan.FromSeconds(24);
            animcolor.IterationBehavior = AnimationIterationBehavior.Forever;
            animcolor.StopBehavior = AnimationStopBehavior.LeaveCurrentValue;

            spotLight.StartAnimation(nameof(spotLight.InnerConeAngleInDegrees), animinnercone);
            spotLight.StartAnimation(nameof(spotLight.OuterConeAngleInDegrees), animoutercone);
            spotLight.StartAnimation(nameof(spotLight.InnerConeColor), animcolor);
        }
    }
}
