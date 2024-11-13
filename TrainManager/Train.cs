/*--------------------------------------------------------------
 *				HTBLA-Leonding / Class: 5.ACIF
 *--------------------------------------------------------------
 *              DBsMOJO, 13.11.2024
 *--------------------------------------------------------------
 * Notes:
 * 
 *--------------------------------------------------------------
 */

using System.ComponentModel;

namespace TrainManager
{
    public class Train
    {
        #region properties

        // private, for cheeky people who try to make the data inconsistent using the list methods:
        private List<Carriage> CarriagesList { get; set; }
        
        // Like life, you only get one chance if you have initialised it:
        public double MaxTrainWeight { get; private set; } 

        #endregion

        #region constructor

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="maxTrainWeight"></param>
        public Train(int maxTrainWeight)
        {
            CarriagesList = new();
            MaxTrainWeight = maxTrainWeight;
        }

        #endregion

        #region dependencies

        public int CountOfPassengerCars
        {
            get
            {
                int result = 0;

                foreach (Carriage? carriage in CarriagesList)
                    result += carriage is PassengerCar ? 1 : 0;

                return result;
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Berechnet das Gesamtgewicht des Zugs
        /// </summary>
        /// <returns></returns>
        public double GetTrainWeight()
        {
            double result = 0;

            foreach (Carriage? carriage in CarriagesList)
                result += carriage!.GetFullWeight();

            return result;
        }

        /// <summary>
        /// Liefert jenen Wagon zurück, der am meisten Gewinn erzielt
        /// Wenn es keinen Wagon gibt soll null zurückgeliefert werden!
        /// </summary>
        /// <returns></returns>
        public Carriage GetMostProfitableCarriage()
        {
            Carriage? result = null;

            foreach (Carriage? carriage in CarriagesList)
                result = result == null ? carriage : carriage!.GetProfit() > result.GetProfit() ? carriage : result;

            return result ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Liefert die Anzahl aller Passagiere im Zug
        /// </summary>
        /// <returns></returns>
        public int GetAmoutOfPassengersInTrain()
        {
            if (CarriagesList.Count == 0)
                throw new InvalidOperationException();

            int result = 0;

            foreach (Carriage? carriage in CarriagesList)
                result += carriage is PassengerCar passengerCar ? passengerCar.NumberOfPassengers : 0;

            return result;
        }

        ///<summary>
        ///Ein neuer Wagon wird (sortiert) in die Liste eingefügt, wenn das maximale Zuggesamtgewicht
        ///(Feld maxTrainWeight) durch den neuen Wagon nicht überschritten wird.
        ///Die Wagons sollen absteigend sortiert nach dem Gewicht in der Liste geführt werden.
        ///Der schwerste Wagon soll ganz vorne stehen! 
        /// </summary>
        /// <param name="newCar">Neuer Wagon</param>
        /// <returns>Die Methode returniert true, wenn neuer Wagon erfolgreich eingefügt wurde, sonst false.</returns>
        public bool AddCarriage(Carriage? newCar)
        {
            if (newCar == null || GetTrainWeight() + newCar.GetFullWeight() > MaxTrainWeight)
                return false;
            
                int idx = 0;

                while (idx < CarriagesList.Count && CarriagesList[idx]!.GetFullWeight() < newCar.GetFullWeight())
                    ++idx;

                CarriagesList.Insert(idx, newCar);

                return true;
        }

        /// <summary>
        /// Diese Methode lässt weitere Passagiere in einen bestimmten Passagierwagon einsteigen.
        /// Als erster Parameter wird die Nummer des Wagons übergeben, in den die Passagiere einsteigen sollen.
        /// Als zweiter Parameter wird die Anzahl der zusäztlichen Passagiere übergeben.
        /// Beachte! Durch die neuen Passagiere erhöht sich das Gewicht des Wagons und er muss möglicherweise
        /// umgereiht werden (schwerere Wagons müssen vor den leichteren gereiht werden!)
        /// Methode returniert true wenn Passagiere erfolgreich eingestiegen sind.
        /// 
        /// Falls nicht alle Passagiere in den Wagon / den Zug passen, so steigt keiner ein und es wird
        /// false zurückgeliefert. (Auch Zuggesamtgewicht beachten!)
        /// Falls Wagon nicht gefunden wird, wird ebenfalls false zurückgeliefert!
        /// 
        /// Es kann davon ausgegangen werden dass die Wagonnr. eindeutig sind!
        /// </summary>
        /// <param name="carriageNumber"></param>
        /// <param name="newPassengers"></param>
        public bool AddPassengersToCar(int carriageNumber, int newPassengers)
        {
            PassengerCar targetCarriage = GetCarriageByNummber(carriageNumber) is PassengerCar passengerCar
                ? passengerCar
                : throw new InvalidOperationException();

            if (targetCarriage.NumberOfPassengers + newPassengers <= Carriage.MAX_PASSENGERS_PER_CAR)
            {
                targetCarriage.NumberOfPassengers += newPassengers;

                CarriagesList.Remove(targetCarriage);
                AddCarriage(targetCarriage);

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Sucht des Wagon mit der übergebenen Nummer
        /// </summary>
        /// <param name="carriageNumber"></param>
        /// <returns></returns>
        public Carriage GetCarriageByNummber(int carriageNumber)
        {
            Carriage? result = null;

            foreach (Carriage? carriage in CarriagesList)
            {
                if (carriage!.CarriageNumber == carriageNumber)
                    result = carriage;
            }

            return result ?? throw new ArgumentException();
        }

        #endregion
    }
}