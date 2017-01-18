using Kolben.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kolben.Controls
{
    public class DynamicCommandBar : CommandBar
    {
        public static readonly DependencyProperty ActionDatasProperty =
          DependencyProperty.Register("ActionDatas",
              typeof(ObservableCollection<ObservableCollection<ActionData>>), typeof(DynamicCommandBar), new PropertyMetadata(default(ObservableCollection<ObservableCollection<ActionData>>), new PropertyChangedCallback(OnActionDatasPropertyChanged)));

        public ObservableCollection<ObservableCollection<ActionData>> ActionDatas
        {
            get { return (ObservableCollection<ObservableCollection<ActionData>>)GetValue(ActionDatasProperty); }
            set
            {
                SetValue(ActionDatasProperty, value);
            }
        }

        private static void OnActionDatasPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            var dynamicCommandBar = (DynamicCommandBar)depObj;
            dynamicCommandBar.PrimaryCommands.Clear();

            if (args.NewValue == null)
            {
                dynamicCommandBar.Visibility = Visibility.Collapsed;

                return;
            }
           
            dynamicCommandBar.CreateAppButton((ObservableCollection<ObservableCollection<ActionData>>)args.NewValue);
            dynamicCommandBar.Visibility = Visibility.Visible;
        }

        public DynamicCommandBar()
        {
            DataContext = this;
            Closing += DynamicCommandBar_Closing;
        }

        private void DynamicCommandBar_Closing(object sender, object e)
        {
            IsOpen = true;
        }


        /// <summary>
        /// Create AppBarButtons and add them to the commandBar
        /// </summary>
        /// <param name="actionsArray"></param>
        protected void CreateAppButton(ObservableCollection<ObservableCollection<ActionData>> actionsArray)
        {
            for (int i = 0; i < actionsArray.Count; i++)
            {
                //Add appBarButton to the commandBar
                foreach (var action in actionsArray[i])
                {
                    var appBarButton = new AppBarButton();
                    appBarButton.Label = action.Label;
                    appBarButton.Command = action.Command;
                    appBarButton.Icon = new SymbolIcon(action.Icon);
                    PrimaryCommands.Add(appBarButton);
                }

                //Check if we have to put separator
                if(i < actionsArray.Count - 1)
                {
                    var appBarButtonSeparator = new AppBarSeparator();
                    PrimaryCommands.Add(appBarButtonSeparator);
                }
            }
        }
    }
}
