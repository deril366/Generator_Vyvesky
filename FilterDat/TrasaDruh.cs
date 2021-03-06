﻿using System.Linq;
using Service_Konektor.Entity;
using Service_Konektor.poseidon;

namespace FilterDat
{
   public static class TrasaDruh
    {
        /// <summary>
        /// zistí aký druh vlaku je vlak s daným idVlaku
        /// </summary>
        /// <param name="idVlaku"></param>
        /// <param name="druhy"></param>
        /// <returns></returns>
        public static string NajdiDruhVlaku(int idVlaku, MapTrasaDruh[] druhy)
        {
            MapTrasaDruh druh = druhy.FirstOrDefault(c => c.VlakID == idVlaku);
            return druh?.Druh;
        }

        /// <summary>
        /// zistí či vlak s daným idVlaku je vlak ktorý prepravuje osoby
        /// </summary>
        /// <param name="idVlaku"></param>
        /// <param name="druhy"></param>
        /// <returns></returns>
        public static bool ZisitiSpravnyDruhVlaku(int idVlaku, MapTrasaDruh[] druhy)
        {
            switch (NajdiDruhVlaku(idVlaku, druhy))
            {
                case "EC":
                case "IC":
                case "EN":
                case "Ex":
                case "R":
                case "Os":
                case "Sp":
                case "Sv":
                    return true;
                default:
                    return false;
            }
        }
    }
}
