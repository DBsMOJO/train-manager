/*--------------------------------------------------------------
 *				HTBLA-Leonding / Class: 5.ACIF
 *--------------------------------------------------------------
 *              DBsMOJO, 13.11.2024
 *--------------------------------------------------------------
 * Notes:
 * 
 *--------------------------------------------------------------
 */

namespace TrainManager;

public class PassengerCar : Carriage
{
    #region fields

    private int _numberOfPassengers;

    #endregion

    #region properties

    protected double PricePerTicket { get; set; }

    public int NumberOfPassengers
    {
        get => _numberOfPassengers;
        set => _numberOfPassengers = Math.Min(value, MAX_PASSENGERS_PER_CAR);
    }

    #endregion

    #region constructor

    public PassengerCar(double emptyWeight, int carriageNumber, int numberOfPassengers, double pricePerTicket)
        : base(emptyWeight, carriageNumber)
    {
        NumberOfPassengers = numberOfPassengers;
        PricePerTicket = pricePerTicket;
    }

    #endregion


    #region methods

    public override double GetProfit() => _numberOfPassengers * PricePerTicket - COST_PER_PASSENGER_CAR;

    public override double GetFullWeight() => _numberOfPassengers * AVG_WEIGHT_PER_PASSENGER + EmptyWeight;

    #endregion
}