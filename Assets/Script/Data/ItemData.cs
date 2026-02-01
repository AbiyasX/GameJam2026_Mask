using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public Days[] days;

    private void OnValidate()
    {
        for (int i = 0; i < days.Length; i++)
        {
            days[i].dayNum = i + 1;
        }
    }   
}

[System.Serializable]
public class Days
{
    public int dayNum;

    public Items[] items;
}

[System.Serializable]
public class Items
{
    public int itemID;
    public bool isMainObjective;
    public bool isCollected;
}