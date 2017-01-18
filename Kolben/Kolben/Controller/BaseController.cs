using Kolben.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kolben.Controller
{
    public abstract class BaseController : INotifyPropertyChanged
    {
        #region Attributes
        private ObservableCollection<ObservableCollection<ActionData>> _actions;
        #endregion

        #region Getters / Setters

        public ObservableCollection<ObservableCollection<ActionData>> Actions
        {
            get { return _actions; }
            set
            {
                if (_actions != value)
                {
                    _actions = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion


        protected BaseController()
        {
        }

        #region Inits
        protected virtual async Task Init()
        {
            //Loader.Instance.Loading = true;

            InitReferences();

            InitCommands();
            await Search();

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Display();
            });

            //Loader.Instance.Loading = false;
        }

        /// <summary>
        /// This method is used to get information from database
        /// </summary>
        /// <returns></returns>
        protected virtual async Task Search()
        {

        }

        protected virtual void InitReferences()
        {
            Actions = new ObservableCollection<ObservableCollection<ActionData>>();
        }

        protected virtual void InitCommands()
        {

        }

        #endregion

        protected virtual void Display()
        {

        }

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Dispose()
        {

        }
    }
}
