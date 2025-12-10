using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FitHubUI
{
    public static class KundestyringStaticRepository
    {
        public static BindingList<Kunde> Kunder { get; set; } = new BindingList<Kunde>();
    }
}