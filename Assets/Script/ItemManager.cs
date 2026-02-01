using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set;}

    public int itemsCollected;
    public bool isValid;

    [Header("References")]
    [SerializeField] private ItemData itemList;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckItem(int id)
    {
        Days currentDay = itemList.days[0];

        foreach (var items in currentDay.items)
        {
            if (items.itemID == id)
            {
                items.isCollected = true;
                Debug.Log("item collected");
                return; 
            }
        }   
    }

    public void CheckCollection()
    {
        Days currentDay = itemList.days[0];

        foreach (var items in currentDay.items)
        {
            if (items.isCollected == true)
            {
                itemsCollected += 1;
            }
            else if (items.isCollected == true && items.isMainObjective == true)
            {
                isValid = true;
            }
        }
    }
}
