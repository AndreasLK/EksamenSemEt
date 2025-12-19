using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sem1BackupForms.Forms
{ 
    public class SortableBindingList<T>: BindingList<T> //Lavet af Andreas med stor hjælp fra Gemini
    {
        //Husker status af liste (sorteret eller ej, retning osv)
        private bool isSortedValue;
        private ListSortDirection sortDirectionValue;
        private PropertyDescriptor sortPropertyValue;

        //INIT
        public SortableBindingList(IList<T> list ): base(list){} //T er placeholder for en vilkårlig type

        //Siger at SortableBindingList selv kan sortere
        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => isSortedValue;
        protected override PropertyDescriptor? SortPropertyCore => sortPropertyValue;
        protected override ListSortDirection SortDirectionCore => sortDirectionValue;
        

        // At bruge => returnere værdien istedet for at sætte det (=) som det ligner
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction) // Det der sker når man trykker på kolonne navn
        {
            var interfaceType = prop.PropertyType.GetInterface("IComparable"); // Tjekker om data overhovedet kan sorteres

            if (interfaceType == null && prop.PropertyType.IsValueType) //Tjekker efter nullable (hvis en int for eksempel også kan være null)
            {
                var underlyingType = Nullable.GetUnderlyingType(prop.PropertyType);
                if (underlyingType != null)
                {
                    interfaceType = underlyingType.GetInterface("IComparable");
                }
            }

            if (interfaceType != null) //Hvis det er muligt at sortere så gør det
            {
                sortPropertyValue = prop; //Husk hvilken kolonne er sorteret
                sortDirectionValue = direction; // Retning af sortereing

                //Tager alle ting i listen
                IEnumerable<T> query = base.Items;

                //Sæt listen i order efter valgt sortering
                if (direction == ListSortDirection.Ascending)
                {
                    query = query.OrderBy(i => prop.GetValue(i));
                } else
                {
                    query = query.OrderByDescending(i => prop.GetValue(i));
                }

                //Sæt de nye sorteret ting ind i listen igen
                int newIndex = 0;
                foreach (object item in query)
                {
                    this.Items[newIndex] = (T)item;
                    newIndex++;
                }

                isSortedValue = true; 
                //Sig til UI at den er færdig med at sortere

                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));

            }
        }


        protected override void RemoveSortCore() //Kører hvis bruger fravælger sortering
        {
            isSortedValue = false;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }
}
