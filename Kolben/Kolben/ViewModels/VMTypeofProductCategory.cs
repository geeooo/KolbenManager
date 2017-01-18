using KolbenService.Database.Entities;
using KolbenService.Database.Entities.Typeof;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kolben.ViewModels
{
    public class VMTypeofProductCategory : INotifyPropertyChanged
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

        private string _name;
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

        public VMTypeofProductCategory()
        {

        }

        public VMTypeofProductCategory(TypeofProductCategory typeofProductCategory)
        {
            Id = typeofProductCategory.Id;
            Name = typeofProductCategory.Name;
        }

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        protected bool Equals(VMTypeofProductCategory other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((VMTypeofProductCategory)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                return hashCode;
            }
        }
    }
}
