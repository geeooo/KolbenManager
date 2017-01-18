using Kolben.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolben.Controller.Restaurant.NSRecipes
{
    public class AddRecipeController : BaseController
    {
        private VMRecipe _currentRecipe;

        public VMRecipe CurrentRecipe
        {
            get { return _currentRecipe; }
            set
            {
                if (_currentRecipe != value)
                {
                    _currentRecipe = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddRecipeController()
        {
            CurrentRecipe = new VMRecipe();
        }

        protected override void Display()
        {
            
        }

        protected override void InitCommands()
        {
            
        }

        protected override async Task Search()
        {
            
        }
    }
}
