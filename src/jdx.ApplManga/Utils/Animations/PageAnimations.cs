using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace jdx.ApplManga.Utils.Animations {

    public static class PageAnimations {
        /// <summary>
        /// Adds a slide from bottom and fade in animation
        /// </summary>
        /// <param name="page">The page to add the animation to</param>
        /// <param name="duration">Animation length in seconds</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromBottomAsync(this Page page, float duration) {
            var storyboard = new Storyboard();

            storyboard.AddSlideFromBottom(duration, page.WindowHeight);
            storyboard.AddFadeIn(duration);
            storyboard.Begin(page);

            page.Visibility = Visibility.Visible;

            // Wait for the animation to finish
            await Task.Delay((int)duration * 1000);
        }

        /// <summary>
        /// Adds a slide to bottom and fade out animation
        /// </summary>
        /// <param name="page">The page to add the animation to</param>
        /// <param name="duration">Animation length in seconds</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToBottomAsync(this Page page, float duration) {
            var storyboard = new Storyboard();

            storyboard.AddSlideToBottom(duration, page.WindowHeight);
            storyboard.AddFadeOut(duration);
            storyboard.Begin(page);

            page.Visibility = Visibility.Visible;

            // Wait for the animation to finish
            await Task.Delay((int)duration * 1000);
        }

        /// <summary>
        /// Fades in page into view
        /// </summary>
        /// <param name="page">The page to add the animation to</param>
        /// <param name="duration">Animation length in seconds</param>
        /// <returns></returns>
        public static async Task FadeInAsync(this Page page, float duration) {
            var storyboard = new Storyboard();

            storyboard.AddFadeIn(duration);
            storyboard.Begin(page);

            page.Visibility = Visibility.Visible;

            // Wait for the animation to finish
            await Task.Delay((int)duration * 1000);
        }

        /// <summary>
        /// Fades out page from view
        /// </summary>
        /// <param name="page">The page to add the animation to</param>
        /// <param name="duration">Animation length in seconds</param>
        /// <returns></returns>
        public static async Task FadeOutAsync(this Page page, float duration) {
            var storyboard = new Storyboard();

            storyboard.AddFadeOut(duration);
            storyboard.Begin(page);

            page.Visibility = Visibility.Visible;

            // Wait for the animation to finish
            await Task.Delay((int)duration * 1000);
        }
    }
}
