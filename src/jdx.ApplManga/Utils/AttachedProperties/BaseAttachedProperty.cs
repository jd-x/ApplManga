using System;
using System.Windows;

namespace jdx.ApplManga.Utils.AttachedProperties {
    /// <summary>
    /// Replaces the default WPF attached property
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    /// <typeparam name="Property"></typeparam>
    public abstract class BaseAttachedProperty<Parent, Property> where Parent : new()  {
        #region Public properties

        public static Parent Instance { get; private set; } = new Parent();

        #endregion

        #region Public events

        /// <summary>
        /// Fires when the values changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

        /// <summary>
        /// Fires when the value changes (even when the value is the same)
        /// </summary>
        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };

        #endregion
        
        #region Attached property definitions

        /// <summary>
        /// The attached property for this class
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "Value",
            typeof(Property),
            typeof(BaseAttachedProperty<Parent, Property>),
            new UIPropertyMetadata(
                default(Property),
                new PropertyChangedCallback(OnValuePropertyChanged),
                new CoerceValueCallback(OnValuePropertyUpdated)
                ));

        /// <summary>
        /// The callback event when the <see cref="ValueProperty"/> is changed
        /// </summary>
        /// <param name="d">The UI element that had its property modified</param>
        /// <param name="e">The event's arguments</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            // Call the parent function
            (Instance as BaseAttachedProperty<Parent, Property>)?.OnValueChanged(d, e);

            // Call event listeners
            (Instance as BaseAttachedProperty<Parent, Property>)?.ValueChanged(d, e);
        }

        /// <summary>
        /// The callback event when the <see cref="ValueProperty"/> is changed (even when the value is the same)
        /// </summary>
        /// <param name="d">The UI element that had its property modified</param>
        /// <param name="e">The event's arguments</param>
        /// <returns></returns>
        private static object OnValuePropertyUpdated(DependencyObject d, object value) {
            // Call the parent function
            (Instance as BaseAttachedProperty<Parent, Property>)?.OnValueUpdated(d, value);

            // Call event listeners
            (Instance as BaseAttachedProperty<Parent, Property>)?.ValueUpdated(d, value);

            return value;
        }

        /// <summary>
        /// Returns the attached property
        /// </summary>
        /// <param name="d">The UI element to get the property from</param>
        /// <returns></returns>
        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);

        /// <summary>
        /// Sets the attached property
        /// </summary>
        /// <param name="d">The UI element to get the property from</param>
        /// <param name="value">The value to set the property to</param>
        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);

        #endregion

        #region Event methods

        /// <summary>
        /// The method called when any attached property of this type is modified
        /// </summary>
        /// <param name="d">The UI element that this property was changed for</param>
        /// <param name="e">The event's arguments</param>
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        /// <summary>
        /// The method called when any attached property of this type is modified  (even when the value is the same)
        /// </summary>
        /// <param name="d">The UI element that this property was changed for</param>
        /// <param name="e">The event's arguments</param>
        public virtual void OnValueUpdated(DependencyObject sender, object value) { }

        #endregion
    }
}
