using System;

namespace Registreringsafgift
{
    public class Afgift
    {
        ///<summary>Returnerer afgiften for en almindelig personbil ud fra dens pris.
        /// <para>Afgiften bliver rundet af til nærmeste tal uden decimal</para>
        /// </summary>
        public int BilAfgift(int pris)
        {
            if (pris <= 0)
            {
                throw new ArgumentException("Prisen skal være højere end 0.");
            }

            if (pris <= 200000)
            {
                return Convert.ToInt32(Math.Round(pris * 0.85));
            }
            
            return Convert.ToInt32(Math.Round(pris * 1.50 - 130000));
        }

        ///<summary>Returnerer afgiften for en elektrisk personbil ud fra dens pris.
        /// <para>Afgiften bliver udregnet ud fra 20% af en almindelig personbils med tilsvarende pris' afgift </para>
        /// <para>Afgiften bliver rundet af til nærmeste tal uden decimal</para>
        /// </summary>
        public int ElBilAfgift(int pris)
        {
            int bilAfgift = BilAfgift(pris);
            return Convert.ToInt32(Math.Round(bilAfgift * 0.2));
        }
    }
}
