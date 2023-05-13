
using UnityEngine;

[CreateAssetMenu(fileName = "UIDataContextObject", menuName = "UIManagerLibrary/UIDataContextObject", order = 1)]
public class UIContextDataObject : ScriptableObject
{
    public string[] Contexts;
    public bool CaseSensitiveContexts = false; //This is incase you want your contexts to be case sensitive, defaults to off
}
