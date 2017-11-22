using jdx.ApplManga.Utils.Animations;
using System.Windows;

namespace jdx.ApplManga.Utils.AttachedProperties {
    /// <summary>
    /// A base class for running animations when a boolean is set to true and reverses it when otherwise
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public abstract class AnimateBaseProperty<Parent> : BaseAttachedProperty<Parent, bool> where Parent : BaseAttachedProperty<Parent, bool>, new() {
        #region Public properties

        /// <summary>
        /// Indicates if this property has been loaded for the first time
        /// </summary>
        public bool FirstLoad { get; set; } = true;

        #endregion

        public override void OnValueUpdated(DependencyObject sender, object value) {
            // Get the framework element
            if (!(sender is FrameworkElement element))
                return;

            // If the value doesn't change, don't fire event
            if (sender.GetValue(ValueProperty) == value && !FirstLoad)
                return;

            // On initial load
            if(FirstLoad) {
                // Create a single self-unhookable event for the element's Loaded event
                RoutedEventHandler onLoaded = null;
                onLoaded = (ss, ee) => {
                    // Unhook this element
                    element.Loaded -= onLoaded;

                    // Run specified animation
                    DoAnimation(element, (bool)value);

                    FirstLoad = false;
                };

                // Hook into the Loaded event of the element
                element.Loaded += onLoaded;
            } else {
                // Run specified animation
                DoAnimation(element, (bool)value);
            }
        }

        /// <summary>
        /// Fired when its flag changes and runs the animation specified in it
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        protected virtual void DoAnimation(FrameworkElement element, bool value) { }
    }

    /// <summary>
    /// Animates a framework element to fade-in on show and
    /// fade-out on hide
    /// </summary>
    public class AnimateFadeInProperty : AnimateBaseProperty<AnimateFadeInProperty> {
        protected override async void DoAnimation(FrameworkElement element, bool value) {
            if (value)
                // Animate in
                await element.FadeInAsync(FirstLoad ? 0 : 0.2f);
            else
                // Animate out
                await element.FadeOutAsync(FirstLoad ? 0 : 0.2f);
        }
    }
}
