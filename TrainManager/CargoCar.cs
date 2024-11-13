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

public class CargoCar : Carriage
{
    #region properties

    private double CargoWeight { get; set; }
    private double PricePerTon { get; set; }

    #endregion

    #region constructor

    public CargoCar(double emptyWeight, int carriageNumber, double cargoWeight, double pricePerTon)
        : base(emptyWeight, carriageNumber)
    {
        CargoWeight = ValidateCargoWeight(cargoWeight);
        PricePerTon = pricePerTon;
    }

    #endregion

    #region methods

    public override double GetProfit() => CargoWeight * PricePerTon - COST_PER_CARGO_CAR;

    public override double GetFullWeight() => EmptyWeight + CargoWeight;

    #endregion

    #region helper-methods

    private double ValidateCargoWeight(double cargoWeight) => Math.Min(cargoWeight, MAX_CARGOWEIGHT_PER_CAR);

    #endregion
}
