using Kolben.ViewModels;
using KolbenService;
using KolbenService.Database.Entities.Typeof;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolben.Controller.Restaurant.Settings.NSTypeofAccountingAccount
{
    public class TypeofAccountingAccountDetailController : BaseController
    {
        #region Attributes
        private VMTypeofAccountingAccount _typeofAccountingAccount;
        #endregion

        #region Getters / Setters
        public VMTypeofAccountingAccount TypeofAccountingAccount
        {
            get { return _typeofAccountingAccount; }
            set
            {
                if (_typeofAccountingAccount != value)
                {
                    _typeofAccountingAccount = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public TypeofAccountingAccountDetailController(ref VMTypeofAccountingAccount typeofAccountingAccount)
        {          
            Init(typeofAccountingAccount);
        }

        private async void Init(VMTypeofAccountingAccount typeofAccountingAccount)
        {
            await base.Init();

            TypeofAccountingAccount = typeofAccountingAccount;
            TypeofAccountingAccount.PropertyChanged += TypeofAccountingAccount_PropertyChanged;
        }

        private async void TypeofAccountingAccount_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var currentTypeofAccountingAccount = (VMTypeofAccountingAccount)sender;
            var typeofAccountingAccount = new TypeofAccountingAccount() { Id = currentTypeofAccountingAccount.Id, Name = currentTypeofAccountingAccount.Name, Prefix = currentTypeofAccountingAccount.Prefix };
            await KolbenServiceUnit.TypeofAccountingAccountService.Update(typeofAccountingAccount);
        }
    }
}
