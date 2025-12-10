using System;
using System.Collections.Generic;
using System.Text;

namespace FitHubUI
{
    public static class BookingRepository
    {
        // Key = HoldId, Value = Liste af Kunder på holdet
        public static Dictionary<int, List<Kunde>> Bookinger { get; set; }
            = new Dictionary<int, List<Kunde>>();

        public static void BookMedlem(int holdId, Kunde medlem)
        {
            if (!Bookinger.ContainsKey(holdId))
                Bookinger[holdId] = new List<Kunde>();

            // Undgå dobbeltbooking
            if (!Bookinger[holdId].Any(k => k.Id == medlem.Id))
                Bookinger[holdId].Add(medlem);
        }

        public static void AfmeldMedlem(int holdId, Kunde medlem)
        {
            if (Bookinger.ContainsKey(holdId))
                Bookinger[holdId].RemoveAll(m => m.Id == medlem.Id);
        }

        public static List<Kunde> HentTilmeldte(int holdId)
        {
            if (Bookinger.ContainsKey(holdId))
                return Bookinger[holdId];

            return new List<Kunde>();
        }
    }
}
