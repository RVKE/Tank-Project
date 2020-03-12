using UnityEngine;

public enum AcquirableItem
{
    SCRAPMETAL,
    WOOD,
    COAL,
    UNREFINEDOIL,
    BOLT,
    STRUCTURE,
    MECHANISM,
    SYSTEM,
    ARMOR,
    MOBILITY,
    FIREPOWER,
    RADAR,
    HEATER,
    INTEGRITY,
    REFUEL,
    ENERGY,
    FULLENERGY
}

public class GameManager : MonoBehaviour {

    #region Variables

    public bool isLooting;

    [Header("References")]

    public Transform playerTransform;

    public static GameManager instance;

    [Header("Player Variables")]
    public float integrityAmount;
    public float energyAmount;
    public float fuelAmount;

    public int scrapMetalAmount;
    public int woodAmount;
    public int coalAmount;
    public int unrefinedOilAmount;

    public int boltAmount;
    public int structureAmount;
    public int mechanismAmount;
    public int systemAmount;

    [Header("Player Level")]

    public int armorLevel;
    public int mobilityLevel;
    public int firePowerLevel;
    public int radarLevel;
    public int heaterLevel;

    private PlayerInput input;

    #endregion

    void Awake ()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        integrityAmount = energyAmount = fuelAmount = 100;
        armorLevel = mobilityLevel = firePowerLevel = radarLevel = heaterLevel = 1;
    }

    public void MakePurchase(PurchaseCard purchaseCard)
    {
        if(scrapMetalAmount - purchaseCard.scrapMetalCost >= 0 
            && woodAmount - purchaseCard.woodCost >= 0
            && coalAmount - purchaseCard.coalCost >= 0
            && unrefinedOilAmount - purchaseCard.unrefinedOilCost >= 0
            && boltAmount - purchaseCard.boltCost >= 0
            && structureAmount - purchaseCard.structureCost >= 0
            && mechanismAmount - purchaseCard.mechanismCost >= 0
            && systemAmount - purchaseCard.systemCost >= 0)
        {
            if (purchaseCard.acquirableItem == AcquirableItem.BOLT)
                boltAmount += purchaseCard.itemQuantity;
            if (purchaseCard.acquirableItem == AcquirableItem.STRUCTURE)
                structureAmount += purchaseCard.itemQuantity;
            if (purchaseCard.acquirableItem == AcquirableItem.MECHANISM)
                mechanismAmount += purchaseCard.itemQuantity;
            if (purchaseCard.acquirableItem == AcquirableItem.SYSTEM)
                systemAmount += purchaseCard.itemQuantity;
            if (purchaseCard.acquirableItem == AcquirableItem.ARMOR)
                armorLevel += purchaseCard.itemQuantity;
            if (purchaseCard.acquirableItem == AcquirableItem.MOBILITY)
                mobilityLevel += purchaseCard.itemQuantity;
            if (purchaseCard.acquirableItem == AcquirableItem.FIREPOWER)
                firePowerLevel += purchaseCard.itemQuantity;
            if (purchaseCard.acquirableItem == AcquirableItem.RADAR)
                radarLevel += purchaseCard.itemQuantity;
            if (purchaseCard.acquirableItem == AcquirableItem.HEATER)
                heaterLevel += purchaseCard.itemQuantity;
            if (purchaseCard.acquirableItem == AcquirableItem.INTEGRITY)
                integrityAmount += purchaseCard.itemQuantity;
            if (purchaseCard.acquirableItem == AcquirableItem.REFUEL)
                fuelAmount += purchaseCard.itemQuantity;
            if (purchaseCard.acquirableItem == AcquirableItem.ENERGY)
                energyAmount += purchaseCard.itemQuantity;
            if (purchaseCard.acquirableItem == AcquirableItem.FULLENERGY)
                energyAmount += purchaseCard.itemQuantity;


            //Substract resources
            scrapMetalAmount -= purchaseCard.scrapMetalCost;
            woodAmount -= purchaseCard.woodCost;
            coalAmount -= purchaseCard.coalCost;
            unrefinedOilAmount -= purchaseCard.unrefinedOilCost;
            boltAmount -= purchaseCard.boltCost;
            structureAmount -= purchaseCard.structureCost;
            mechanismAmount -= purchaseCard.mechanismCost;
            systemAmount -= purchaseCard.systemCost;
        } else
        {
            //Not enough resources
        }
    }
}
