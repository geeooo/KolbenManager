using Kolben.ViewModels;
using System;
using KolbenService;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Kolben.Views.Restaurant.Settings.NSTypeofProductCategory;
using KolbenService.Database.Entities.Typeof;
using Kolben.Utils;
using Windows.UI.Popups;
using System.Runtime.CompilerServices;

namespace Kolben.Controller.Restaurant.Settings.NSTypeofProductCategory
{
    public class TypeofProductCategoryController : BaseController
    {
        #region Attributes
        private List<VMTypeofProductCategory> _localTypeofProductCategories;
        private ObservableCollection<VMTypeofProductCategory> _typeofProductCategories;
        private VMTypeofProductCategory _typeofProductCategory;

        private ActionData _addNewTypeofProductCategory;
        private ActionData _deleteTypeofProductCategory;
        #endregion

        #region Getters / Setters
        public ObservableCollection<VMTypeofProductCategory> TypeofProductCategories
        {
            get { return _typeofProductCategories; }
            set
            {
                if (_typeofProductCategories != value)
                {
                    _typeofProductCategories = value;
                    OnPropertyChanged();
                }
            }
        }
        public VMTypeofProductCategory TypeofProductCategory
        {
            get { return _typeofProductCategory; }
            set
            {
                if (_typeofProductCategory != value)
                {
                    _typeofProductCategory = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public TypeofProductCategoryController()
        {
            Init();
        }

        private async void Init()
        {
            await base.Init();

            TypeofProductCategory = TypeofProductCategories.FirstOrDefault();
        }

        protected override void Display()
        {
            TypeofProductCategories = new ObservableCollection<VMTypeofProductCategory>(_localTypeofProductCategories);
        }

        protected override void InitCommands()
        {
            _addNewTypeofProductCategory = new ActionData("Nouvelle catégorie", Symbol.Add, new GalaSoft.MvvmLight.Command.RelayCommand<object>(o => AddTypeofProductCategoryCommandExecute()));
            _deleteTypeofProductCategory = new ActionData("Supprimer la catégorie", Symbol.Delete, new GalaSoft.MvvmLight.Command.RelayCommand<object>(o => DeleteTypeofProductCategoryCommandExecute(), o => TypeofProductCategoryCanExecute()));


            Actions = new ObservableCollection<ObservableCollection<ActionData>>()
            {
                new ObservableCollection<ActionData>()
                {
                    _addNewTypeofProductCategory
                },
                new ObservableCollection<ActionData>()
                {
                    _deleteTypeofProductCategory
                }
            };
        }

        protected override async Task Search()
        {
            var typeOfProductCategories = await KolbenServiceUnit.TypeofProductCategoryService.GetAll();
            _localTypeofProductCategories = typeOfProductCategories.Select(topc => new VMTypeofProductCategory(topc)).ToList();
        }

        #region Commands
        private async void AddTypeofProductCategoryCommandExecute()
        {
            var dialog = new ContentDialog()
            {
                Title = "Ajout d'une nouvelle catégorie de produit",
                Content = new TypeofProductCategoryAddPage()
            };

            dialog.PrimaryButtonText = "Enregistrer";
            dialog.SecondaryButtonText = "Annuler";
            dialog.IsPrimaryButtonEnabled = true;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var vmTypeofProductCategory = ((TypeofProductCategoryAddController)((TypeofProductCategoryAddPage)dialog.Content).DataContext).TypeofProductCategory;
                var idTypeofProductCategory = await KolbenServiceUnit.TypeofProductCategoryService.Add(new TypeofProductCategory() { Name = vmTypeofProductCategory.Name });
                var typeofProductCategory = await KolbenServiceUnit.TypeofProductCategoryService.GetSingle(idTypeofProductCategory);

                var newVmTypeofProductCategory = new VMTypeofProductCategory(typeofProductCategory);
                _localTypeofProductCategories.Add(newVmTypeofProductCategory);

                TypeofProductCategories.Add(newVmTypeofProductCategory);
                TypeofProductCategories.OrderBy(topc => topc.Name);
                TypeofProductCategory = TypeofProductCategories.FirstOrDefault(topc => topc.Id == newVmTypeofProductCategory.Id);
            }
        }

        private async void DeleteTypeofProductCategoryCommandExecute()
        {
            var messageDialog = new MessageDialog("Voulez-vous supprimer ce type de catégorie ?");

            messageDialog.Commands.Add(new UICommand(
       "Oui", async (o) =>
       {
           await KolbenServiceUnit.TypeofProductCategoryService.Delete(TypeofProductCategory.Id);
           await Search();
           Display();
           TypeofProductCategory = TypeofProductCategories.FirstOrDefault();
       }));

            messageDialog.Commands.Add(new UICommand(
                "Non"));

            messageDialog.ShowAsync();
        }

        private bool TypeofProductCategoryCanExecute()
        {
            return TypeofProductCategory != null && TypeofProductCategory.Id > 0;
        }
        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "TypeofProductCategory")
            {
                if (_deleteTypeofProductCategory != null)
                {
                    _deleteTypeofProductCategory.Command.RaiseCanExecuteChanged();
                }
            }
        }
    }
}
