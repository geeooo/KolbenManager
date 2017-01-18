using GalaSoft.MvvmLight.Command;
using Kolben.Utils;
using Kolben.ViewModels;
using Kolben.Views.Restaurant.Settings.NSTypeofTVA;
using KolbenService;
using KolbenService.Database.Entities.Typeof;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Kolben.Controller.Restaurant.Settings.NSTypeofTVA
{
    public class TypeofTVAController : BaseController
    {
        #region Attributes
        private List<VMTypeofTVA> _localTypeofTVAs;
        private ObservableCollection<VMTypeofTVA> _typeofTVAs;
        private VMTypeofTVA _currentTypeofTVA;

        private ActionData _addNewTypeofTVAActionData;
        private ActionData _deleteTypeofTVAActionData;
        #endregion

        #region Getters / Setters
        public ObservableCollection<VMTypeofTVA> TypeofTVAs
        {
            get { return _typeofTVAs; }
            set
            {
                if (_typeofTVAs != value)
                {
                    _typeofTVAs = value;
                    OnPropertyChanged();
                }
            }
        }
        public VMTypeofTVA TypeofTVA
        {
            get { return _currentTypeofTVA; }
            set
            {
                if (_currentTypeofTVA != value)
                {
                    _currentTypeofTVA = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public TypeofTVAController()
        {
            Init();
        }

        private async void Init()
        {
            await base.Init();

            TypeofTVA = TypeofTVAs.FirstOrDefault();
        }

        protected override void Display()
        {
            TypeofTVAs = new ObservableCollection<VMTypeofTVA>(_localTypeofTVAs);
        }

        protected override void InitCommands()
        {
            _addNewTypeofTVAActionData = new ActionData("Nouveau type de TVA", Symbol.Add, new RelayCommand<object>(o => AddTypeofTVACommandExecute()));
            _deleteTypeofTVAActionData = new ActionData("Supprimer le type de TVA", Symbol.Delete, new RelayCommand<object>(o => DeleteTypeofTVACommandExecute(), o => TypeofTVACanExecute()));


            Actions = new ObservableCollection<ObservableCollection<ActionData>>()
            {
                new ObservableCollection<ActionData>()
                {
                    _addNewTypeofTVAActionData
                },
                new ObservableCollection<ActionData>()
                {
                    _deleteTypeofTVAActionData
                }
            };
        }

        protected override async Task Search()
        {
            var typeOfTVAs = await KolbenServiceUnit.TypeofTVAService.GetAll();
            _localTypeofTVAs = typeOfTVAs.Select(toTVA => new VMTypeofTVA(toTVA)).ToList();
        }

        #region Commands
        private async void AddTypeofTVACommandExecute()
        {
            var dialog = new ContentDialog()
            {
                Title = "Ajout d'un nouveau type de TVA",
                Content = new TypeofTVAAddPage()
            };

            dialog.PrimaryButtonText = "Enregistrer";
            dialog.SecondaryButtonText = "Annuler";
            dialog.IsPrimaryButtonEnabled = true;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var vmTypeofTVA = ((TypeofTVAAddController)((TypeofTVAAddPage)dialog.Content).DataContext).TypeofTVA;
                var idTypeofTVA = await KolbenServiceUnit.TypeofTVAService.Add(new TypeofTVA() { Name = vmTypeofTVA.Name, Value = vmTypeofTVA.Value });
                var typeofTVA = await KolbenServiceUnit.TypeofTVAService.GetSingle(idTypeofTVA);

                var newVmTypeofTVA = new VMTypeofTVA(typeofTVA);
                _localTypeofTVAs.Add(newVmTypeofTVA);

                TypeofTVAs.Add(newVmTypeofTVA);
                TypeofTVAs = new ObservableCollection<VMTypeofTVA>(TypeofTVAs.OrderBy(toTVA => toTVA.Name));
                TypeofTVA = newVmTypeofTVA;
            }
        }

        private async void DeleteTypeofTVACommandExecute()
        {
            var messageDialog = new MessageDialog("Voulez-vous supprimer le type de TVA ?");

            messageDialog.Commands.Add(new UICommand(
       "Oui", async (o) =>
       {
           await KolbenServiceUnit.TypeofTVAService.Delete(TypeofTVA.Id);
           await Search();
           Display();
           TypeofTVA = TypeofTVAs.FirstOrDefault();
       }));

            messageDialog.Commands.Add(new UICommand(
                "Non"));

            messageDialog.ShowAsync();
        }

        private bool TypeofTVACanExecute()
        {
            return TypeofTVA != null && TypeofTVA.Id > 0;
        }
        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "TypeofTVA")
            {
                if (_deleteTypeofTVAActionData != null)
                {
                    _deleteTypeofTVAActionData.Command.RaiseCanExecuteChanged();
                }
            }
        }
    }
}
