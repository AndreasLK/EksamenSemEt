using System;
using System.Collections.Generic;
using System.Text;

namespace FitHubUI
{
    public static class HoldRepository
    {
        // Global liste over hold
        public static List<Planlægning.Hold> HoldListe { get; set; } = new List<Planlægning.Hold>();
    }
}
