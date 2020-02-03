using UnityEngine;

[CreateAssetMenu(fileName = "New PurchaseCard")]
public class PurchaseCard : ScriptableObject
{
    #region Variables

    public AcquirableItem acquirableItem;

    public string description;

    [Header("Item Costs")]

    public int scrapMetalCost;
    public int coalCost;
    public int woodCost;
    public int unrefinedOilCost;

    public int boltCost;
    public int structureCost;
    public int mechanismCost;
    public int systemCost;

    #endregion
}
