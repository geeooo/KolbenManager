using Kolben.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolben.Controller.Restaurant.Settings.NSTypeofAccountingAccount
{
    public class TypeofAccountingAccountAddController : BaseController
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

        public TypeofAccountingAccountAddController()
        {
            TypeofAccountingAccount = new VMTypeofAccountingAccount();

            Init();
        }
    }
}
