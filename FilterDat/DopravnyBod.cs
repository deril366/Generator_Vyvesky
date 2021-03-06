﻿using System;
using System.Linq;
using Service_Konektor.Entity;
using Service_Konektor.poseidon;

namespace FilterDat
{
    public static class DopravnyBod
    {
        /// <summary>
        /// Vybere z pola stanic podla idcka vsetky stanice ktoré sú na danej trase
        /// </summary>
        /// <param name="idBodov"></param>
        /// <param name="dopravneBody"></param>
        /// <returns></returns>
        public static MapDopravnyBod[] NajdiDopravneBody(int[] idBodov, MapDopravnyBod[] dopravneBody)
        {
            MapDopravnyBod[] body = dopravneBody.Where(c => idBodov.Contains(c.ID)).Select(c => c).ToArray();
            return body;
        }

        /// <summary>
        /// Vytvorý text z názvami staníc a práchodom do danej stanice pre vlak ktorý má danú aktualnu trasu 
        /// </summary>
        /// <param name="aktualnaTrasa"></param>
        /// <param name="body"></param>
        /// <param name="dopravneBody"></param>
        /// <returns></returns>
        public static string VytvorTextZoSmeru(MapTrasaBod aktualnaTrasa, MapTrasaBod[] body, MapDopravnyBod[] dopravneBody)
        {

            MapTrasaBod[] bodyStanicPredDist =
                 body.Where(c => c.Poradi < aktualnaTrasa.Poradi && c.AktCisloVlaku == aktualnaTrasa.AktCisloVlaku)
                     .GroupBy(c => c.BodID).Select(c => c.First()).OrderBy(c => c.Poradi).Select(c => c).ToArray();
            string text ="";
            for (int i = 0; i < bodyStanicPredDist.Length; i++)
            {
                if (i == bodyStanicPredDist.Length - 1)
                {
                    text += string.Format("{0}({1:%h}.{1:%m})", NajdiNazovDopBodu(bodyStanicPredDist[i].BodID,dopravneBody), TimeSpan.FromSeconds(bodyStanicPredDist[i].CasPrijazdu));
                }
                else
                {
                    text += string.Format("{0}({1:%h}.{1:%m}) - ", NajdiNazovDopBodu(bodyStanicPredDist[i].BodID, dopravneBody), TimeSpan.FromSeconds(bodyStanicPredDist[i].CasPrijazdu));
                }
            }
            return text;
        }

        /// <summary>
        /// Vytvorý text z názvami staníc a práchodom do danej stanice pre vlak ktorý má danú aktualnu trasu 
        /// </summary>
        /// <param name="aktualnaTrasa"></param>
        /// <param name="body"></param>
        /// <param name="dopravneBody"></param>
        /// <returns></returns>
        public static string VytvorTextOdchodovZoSmeru(MapTrasaBod aktualnaTrasa, MapTrasaBod[] body, MapDopravnyBod[] dopravneBody)
        {
            MapTrasaBod[] bodyStanicPredDist =
                body.Where(c => c.Poradi > aktualnaTrasa.Poradi && c.AktCisloVlaku == aktualnaTrasa.AktCisloVlaku)
                    .GroupBy(c => c.BodID).Select(c => c.First()).OrderBy(c => c.Poradi).Select(c => c).ToArray();


            string text = "";
            for (int i = 0; i < bodyStanicPredDist.Length; i++)
            {
                if (i == bodyStanicPredDist.Length - 1)
                {
                    text += string.Format("{0}({1:%h}.{1:%m})", NajdiNazovDopBodu(bodyStanicPredDist[i].BodID, dopravneBody), TimeSpan.FromSeconds(bodyStanicPredDist[i].CasPrijazdu));
                }
                else
                {
                    text += string.Format("{0}({1:%h}.{1:%m}) - ", NajdiNazovDopBodu(bodyStanicPredDist[i].BodID, dopravneBody), TimeSpan.FromSeconds(bodyStanicPredDist[i].CasPrijazdu));
                }
            }
            return text;
        }

        /// <summary>
        /// zistí názdov dopravného bodu podla jeho id
        /// </summary>
        /// <param name="idBodu"></param>
        /// <param name="dopravneBody"></param>
        /// <returns></returns>
        private static string NajdiNazovDopBodu(int idBodu, MapDopravnyBod[] dopravneBody)
        {
            //var b = dopravneBody[0].
            MapDopravnyBod db = dopravneBody.FirstOrDefault(c => c.ID == idBodu);
            return db?.Nazov;
        }

    }
}
