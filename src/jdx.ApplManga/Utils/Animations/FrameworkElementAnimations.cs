using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace jdx.ApplManga.Utils.Animations {
    /// <summary>
    /// Helpers for animating framework elements
    /// </summary>
    public static class FrameworkElementAnimations {

        /// <summary>
        /// Slides in an element from below while fading it in
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="duration">Animation length in seconds</param>
        /// <param name="keepMargin">Maintain the element's height while animating</param>
        /// <param name="animHeight">Animation height to animate to, returns the element's height if not specified</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromBottomAsync(this FrameworkElement element, float duration = 0.3f, bool keepMargin = true, int animHeight = 0) {
            var storyboard = new Storyboard();

            storyboard.AddSlideFromBottom(duration, animHeight == 0 ? element.ActualHeight : animHeight, keepMargin: keepMargin);

            storyboard.AddFadeIn(duration);

            storyboard.Begin(element);

            element.Visibility = Visibility.Visible;

            await Task.Delay((int)(duration * 1000));
        }

        /// <summary>
        /// Slides out an element to the bottom while fading it out
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="duration">Animation length in seconds</param>
        /// <param name="keepMargin">Maintain the element's height while animating</param>
        /// <param name="animHeight">Animation height to animate to, returns the element's height if not specified</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToBottomAsync(this FrameworkElement element, float duration = 0.3f, bool keepMargin = true, int animHeight = 0) {
            var storyboard = new Storyboard();

            storyboard.AddSlideToBottom(duration, animHeight == 0 ? element.ActualHeight : animHeight, keepMargin: keepMargin);

            storyboard.AddFadeOut(duration);

            storyboard.Begin(element);

            element.Visibility = Visibility.Visible;

            await Task.Delay((int)(duration * 1000));
        }

        /// <summary>
        /// Fades in an element into view
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="duration">Animation length in seconds</param>
        /// <returns></returns>
        public static async Task FadeInAsync(this FrameworkElement element, float duration = 0.3f) {
            var storyboard = new Storyboard();

            storyboard.AddFadeIn(duration);

            storyboard.Begin(element);

            element.Visibility = Visibility.Visible;

            await Task.Delay((int)(duration * 1000));
        }

        /// <summary>
        /// Fades out an element from view
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="duration">Animation length in seconds</param>
        /// <returns></returns>
        public static async Task FadeOutAsync(this FrameworkElement element, float duration = 0.3f) {
            var storyboard = new Storyboard();

            storyboard.AddFadeOut(duration);

            storyboard.Begin(element);

            element.Visibility = Visibility.Visible;

            await Task.Delay((int)(duration * 1000));
        }
    }
}
