using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookTask
{
    internal class BookComparerByTag : Comparer<Book>
    {
        private readonly PropertyInfo _selectedProperty;

        public BookComparerByTag(string tagName)
        {
            foreach (var prop in typeof(Book).GetProperties())
            {
                if (prop.Name == tagName)
                {
                    this._selectedProperty = prop;
                    break;
                }
            }

            if (this._selectedProperty is null || !typeof(IComparable).IsAssignableFrom(this._selectedProperty.PropertyType))
            {
                throw new ArgumentException($"Unable to compare by {tagName}");
            }
        }

        public override int Compare(Book x, Book y)
        {
            dynamic valueFromX = x is null ? null : this._selectedProperty.GetValue(x);
            dynamic valueFromY = y is null ? null : this._selectedProperty.GetValue(y);

            return
                object.ReferenceEquals(x, y) ? 0 :
                x is null ? -1 :
                y is null ? 1 :
                valueFromX is null && !(valueFromY is null) ? -1 :
                !(valueFromX is null) && valueFromY is null ? 1 :
                valueFromX.CompareTo(valueFromY) != 0 ? valueFromX.CompareTo(valueFromY) :
                Default.Compare(x, y);
        }
    }
}
