using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using jdx.ApplManga.Core.Expressions;

namespace jdx.ApplManga.Core.ViewModels {

    public class BaseViewModel : INotifyPropertyChanged {
        /// <summary>
        /// Returns whether an exception is thrown or if a Debug.Fail()
        /// is used when an invalid property name is passed to the
        /// VerifyPropertyName method. Value is false by default
        /// but subclasses used by unit tests might override this
        /// property's getter to return true.
        /// </summary>
        public bool ThrowOnInvalidPropertyName { get; private set; }

        /// <summary>
        /// Returns a warning if this object doesn't have a public
        /// property with the specified name. Used only for Debug builds.
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void VerifyPropertyName(string propertyName) {
            // Check if the property name matches a valid
            // public instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null) {
                string msg = "Invalid property name: " + propertyName;
                if (ThrowOnInvalidPropertyName) {
                    throw new Exception(msg);
                } else {
                    Debug.Fail(msg);
                }
            }
        }

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the specified property.
        /// </summary>
        /// <param name="propertyName">Property name to update, is case-sensitive.</param>
        public virtual void RaisePropertyChanged(string propertyName) {
            VerifyPropertyName(propertyName);
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Runs a command if the updating flag is unset
        /// </summary>
        /// <param name="updatingFlag">Flag for the current command state (running or not)</param>
        /// <param name="action">The action to execute if the command is not already running</param>
        /// <returns></returns>
        protected async Task RunCommandAsync(Expression<Func<bool>> updatingFlag, Func<Task> action) {
            // Check if the function is already running
            if (updatingFlag.GetPropertyValue())
                return;

            updatingFlag.SetPropertyValue(true);

            try {
                await action();
            } finally {
                updatingFlag.SetPropertyValue(false);
            }
        }
    }
}
