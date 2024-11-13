/*--------------------------------------------------------------
 *				HTBLA-Leonding / Class: 5.ACIF
 *--------------------------------------------------------------
 *              Daniel Binder, 13.11.2024
 *--------------------------------------------------------------
 * Notes:
 *
 *--------------------------------------------------------------
 */

namespace TrainManager;

public abstract class Carriage
{
    #region fields

    public const int MAX_PASSENGERS_PER_CAR = 200; //Maximale Anzahl der Passagiere je Passagierwagon

    protected const double AVG_WEIGHT_PER_PASSENGER = 0.06; //Durchschnittsgewicht eines Passagiers (in Tonnen)
    protected const double MAX_CARGOWEIGHT_PER_CAR = 99.0; //Maximale Fracht in Tonnen je Frachtwagon
    protected const double COST_PER_PASSENGER_CAR = 300.0; //Kosten je Wagon
    protected const double COST_PER_CARGO_CAR = 250.0; //Kosten je Frachtwagon
    
    private const int LENGH_OF_THE_CARRIAGE_NUMBER = 8;

    private int _carriageNumber;

    #endregion

    #region properties

    protected double EmptyWeight { get; private set; }

    public int CarriageNumber
    {
        get => _carriageNumber;
        private set => _carriageNumber = ValidateCarriageNumber(value) ? value : 1;
    }

    #endregion

    #region constructor

    protected Carriage(double emptyWeight, int carriageNumber)
    {
        EmptyWeight = emptyWeight;
        CarriageNumber = carriageNumber;
    }

    #endregion

    #region methods

    public abstract double GetProfit();
    public abstract double GetFullWeight();

    #endregion

    #region helper-methods

    private bool ValidateCarriageNumber(int carriageNumber)
    {
        return CalculateCarriageLength(carriageNumber) == LENGH_OF_THE_CARRIAGE_NUMBER &&
               IsWeightedDigitSumValid(carriageNumber);
    }

    private bool IsWeightedDigitSumValid(int carriageNumber)
    {
        int squereNumber = 0;

        for (int i = CalculateCarriageLength(carriageNumber); i > 0; i--)
        {
            squereNumber += carriageNumber % 10 * i;
            carriageNumber /= 10;
        }

        return squereNumber % 10 == 0;
    }

    private int CalculateCarriageLength(int carriageNumber) => carriageNumber.ToString().Length;

    #endregion
}