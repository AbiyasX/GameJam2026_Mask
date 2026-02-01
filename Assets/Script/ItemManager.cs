using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set;}

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
}
