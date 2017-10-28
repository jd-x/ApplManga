using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace jdx.ApplManga.Utils.Animations {
    public static class StoryboardHelpers {
        /// <summary>
        /// Adds a slide from bottom animation
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="duration">Animation length in seconds</param>
        /// <param name="offset">Animation offset from the bottom</param>
        /// <param name="decelerationRatio">Animation's rate of deceleration</param>
        /// <param name="keepMargin">Maintain the element's height while animating</param>
        public static void AddSlideFromBottom(this Storyboard storyboard, float duration, double offset, float decelerationRatio = 0.9f, bool keepMargin = true) {
            var animation = new ThicknessAnimation {
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
                From = new Thickness(0, keepMargin ? offset : 0, 0, -offset),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a slide to bottom animation
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="duration">Animation length in seconds</param>
        /// <param name="offset">Animation offset from the bottom</param>
        /// <param name="decelerationRatio">Animation's rate of deceleration</param>
        /// <param name="keepMargin">Maintain the element's height while animating</param>
        public static void AddSlideToBottom(this Storyboard storyboard, float duration, double offset, float decelerationRatio = 0.9f, bool keepMargin = true) {
            var animation = new ThicknessAnimation {
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
                From = new Thickness(0),
                To = new Thickness(0, keepMargin ? offset : 0, 0, -offset),
                DecelerationRatio = decelerationRatio
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a fade in animation
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="duration">Animation length in seconds</param>
        public static void AddFadeIn(this Storyboard storyboard, float duration) {
            var animation = new DoubleAnimation {
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
                From = 0,
                To = 1
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a fade out animation
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="duration">Animation length in seconds</param>
        public static void AddFadeOut(this Storyboard storyboard, float duration) {
            var animation = new DoubleAnimation {
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
                From = 1,
                To = 0
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            storyboard.Children.Add(animation);
        }
    }
}
