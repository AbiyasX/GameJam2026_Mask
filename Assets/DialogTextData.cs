using UnityEngine;



[System.Serializable]
public class DialogLine
{
    public string characterName; 
    [TextArea(2, 6)]
    public string text;          
}
[CreateAssetMenu(fileName = "DialogTextData", menuName = "Dialog/DialogTextData")]
public class DialogTextData : ScriptableObject
{
    public DialogLine[] lines;
}
