using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sem1BackupForms.Forms
{
    public class SortableBindingList<T>: BindingList<T>
    {
        private bool isSortedValue;
        private ListSortDirection sortDirectionValue;
        private PropertyDescriptor sortPropertyValue;

        public SortableBindingList(IList<T> list ): base(list){}

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => isSortedValue;
        protected override PropertyDescriptor? SortPropertyCore => sortPropertyValue;
        protected override ListSortDirection SortDirectionCore => sortDirectionValue;
        

        // At bruge => returnere værdien istedet for at sætte det (=) som det ligner
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            var interfaceType = prop.PropertyType.GetInterface("IComparable");

            if (interfaceType == null && prop.PropertyType.IsValueType)
            {
                var underlyingType = Nullable.GetUnderlyingType(prop.PropertyType);
                if (underlyingType != null)
                {
                    interfaceType = underlyingType.GetInterface("IComparable");
                }
            }

            if (interfaceType != null)
            {
                sortPropertyValue = prop;
                sortDirectionValue = direction;

                IEnumerable<T> query = base.Items;

                if (direction == ListSortDirection.Ascending)
                {
                    query = query.OrderBy(i => prop.GetValue(i));
                } else
                {
                    query = query.OrderByDescending(i => prop.GetValue(i));
                }

                int newIndex = 0;
                foreach (object item in query)
                {
                    this.Items[newIndex] = (T)item;
                    newIndex++;
                }

                isSortedValue = true;

                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));

            }
        }


        protected override void RemoveSortCore()
        {
            isSortedValue = false;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }
}
