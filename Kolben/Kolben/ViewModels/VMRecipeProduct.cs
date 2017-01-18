using KolbenService.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kolben.ViewModels
{
    public class VMRecipeProduct : INotifyPropertyChanged
    {
        private int _id;
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


        private int _idRecipe;
        public int IdRecipe
        {
            get { return _idRecipe; }
            set
            {
                if (_idRecipe != value)
                {
                    _idRecipe = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _idProduct;
        public int IdProduct
        {
            get { return _idProduct; }
            set
            {
                if (_idProduct != value)
                {
                    _idProduct = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal? _unitQuantity;
        public decimal? UnitQuantity
        {
            get { return _unitQuantity; }
            set
            {
                if (_unitQuantity != value)
                {
                    _unitQuantity = value;
                    OnPropertyChanged();
                    ComputePrice();
                }
            }
        }

        private decimal? _kgQuantity;
        public decimal? KgQuantity
        {
            get { return _kgQuantity; }
            set
            {
                if (_kgQuantity != value)
                {
                    _kgQuantity = value;
                    OnPropertyChanged();
                    ComputePrice();
                }
            }
        }

        private VMProduct _product;
        public VMProduct Product
        {
            get { return _product; }
            set {
                if(_product != value)
                {
                    _product = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged();
                }
            }
        }

        public VMRecipeProduct()
        {

        }

        public VMRecipeProduct(RecipeProduct recipeProduct)
        {
            Id = recipeProduct.Id;
            IdRecipe = recipeProduct.IdRecipe;
            IdProduct = recipeProduct.IdProduct;

            KgQuantity = recipeProduct.KgQuantity;
            UnitQuantity = recipeProduct.UnitQuantity;

            if(recipeProduct.Product != null)
            {
                Product = new VMProduct(recipeProduct.Product);
            }

            ComputePrice();
        }

        public void ComputePrice()
        {
            if (Product == null)
                return;

            if (KgQuantity > 0)
            {
                Price = Math.Round(Product.KgPrice * KgQuantity.Value, 2);
            }
            else if (UnitQuantity > 0)
            {
                Price = Math.Round(Product.UnitPrice * UnitQuantity.Value, 2);
            }
            else
            {
                Price = 0;
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
