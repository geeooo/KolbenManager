using KolbenService.Database.Entities;
using KolbenService.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kolben.ViewModels
{
    public class VMRecipe : INotifyPropertyChanged
    {
        #region Attributes
        private int _id;
        private string _name;
        private RecipeCategory _recipeCategory;
        private ObservableCollection<VMRecipeProduct> _recipeProducts;
        private int _totalProducts;
        private decimal _price;

        #endregion

        #region Getters / Setters
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public RecipeCategory RecipeCategory
        {
            get { return _recipeCategory; }
            set
            {
                if (_recipeCategory != value)
                {
                    _recipeCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<VMRecipeProduct> RecipeProducts
        {
            get { return _recipeProducts; }
            set
            {
                if (_recipeProducts != value)
                {
                    _recipeProducts = value;
                    OnPropertyChanged();
                    ComputeStats();
                }
            }
        }

        public int TotalProducts
        {
            get { return _totalProducts; }
            set
            {
                _totalProducts = value;
                OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value;
                OnPropertyChanged();
            }
        }


        #endregion

        public VMRecipe()
        {

        }

        public VMRecipe(Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            RecipeCategory = recipe.RecipeCategory;

            if (recipe.RecipeProducts != null)
            {
                RecipeProducts = new ObservableCollection<VMRecipeProduct>(recipe.RecipeProducts.Select(rp => new VMRecipeProduct(rp)));
            }
            else
            {
                RecipeProducts = new ObservableCollection<VMRecipeProduct>();
            }
        }

        public void ComputeStats()
        {
            if (RecipeProducts.Any())
            {
                TotalProducts = RecipeProducts.Count();
                Price = RecipeProducts.Sum(rp => rp.Price);
            }
        }

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
