using UnityEngine;
using UnityEngine.UI;

public enum AcquirableItem
{
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

    [Header("References")]
    public Transform playerTransform;

    public Text scrapMetalAmountText;
    public Text woodAmountText;
    public Text coalAmountText;
    public Text unrefinedOilAmountText;

    public Text boltAmountText;
    public Text structureAmountText;
    public Text mechanismAmountText;
    public Text systemAmountText;

    public static GameManager instance;

    [Header("Player Variables")]
    public float integrityAmount;
    public float energyAmount;
    public float fuelAmount;

    public int scrapMetalAmount;
    public int woodAmount;
    public int coalAmount;
    public float unrefinedOilAmount;

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
        integrityAmount = 100;
        energyAmount = 100;
        fuelAmount = 100;
    }

    void Update()
    {
        UpdateText();
    }

    void BuyItem()
    {

    }

    void UpdateText()
    {
        scrapMetalAmountText.text = "SCRAPMETAL: " + scrapMetalAmount;
        woodAmountText.text = "WOOD: " + woodAmount;
        coalAmountText.text = "COAL: " + coalAmount;
        unrefinedOilAmountText.text = "UNREFINED OIL: " + unrefinedOilAmount;
        boltAmountText.text = "BOLTS: " + boltAmount;
        structureAmountText.text = "STRUCTURES: " + structureAmount;
        mechanismAmountText.text = "MECHANISMS: " + mechanismAmount;
        systemAmountText.text = "SYSTEMS: " + systemAmount;
    }
}
