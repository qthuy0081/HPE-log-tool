using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HPE_Log_Tool.ViewModels
{
    /// <summary>
    /// Base class for all ViewModel classes in the application.
    /// It provides support for property change notifications
    /// and has a DisplayName property.  This class is abstract.
    /// </summary>
    /// 
    public abstract class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        protected BaseViewModel()
        {
        }

        /// <summary>
        /// Returns the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// Child classes can override this method to perform
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {
        }



        public void RaisePropertyChanged(string propertyName)
        {
            var pc = PropertyChanged;
            if (pc != null)
                pc(this, new PropertyChangedEventArgs(propertyName));
        }
        #region Close Window Command
        public Action CloseAction { get; set; }
        RelayCommand _cmdCloseWindow;
        public ICommand cmdCloseWindow
        {
            get
            {
                if (_cmdCloseWindow == null)
                {
                    _cmdCloseWindow = new RelayCommand(param => CloseWindow(), param => CanCloseWindow());
                }
                return _cmdCloseWindow;
            }
        }

        public event Action CloseWindowRequest;

        public virtual void CloseWindow()
        {
            Action action = this.CloseWindowRequest;
            if (action != null)
            {
                //BaseModel.UpdateLogoutTracking();
                action();
            }
        }

        public virtual bool CanCloseWindow()
        {
            return true;
        }

        #endregion Close Window Command

        #region Minimize Window Command

        RelayCommand _cmdMinimizeWindow;
        public ICommand cmdMinimizeWindow
        {
            get
            {
                if (_cmdMinimizeWindow == null)
                {
                    _cmdMinimizeWindow = new RelayCommand(param => MinimizeWindow(), param => CanMinimizeWindow());
                }
                return _cmdMinimizeWindow;
            }
        }

        public event Action MinimizeWindowRequest;

        public virtual void MinimizeWindow()
        {
            MinimizeWindowRequest?.Invoke();
        }

        public virtual bool CanMinimizeWindow()
        {
            return true;
        }

        #endregion Minimize Window Command

    }
}
