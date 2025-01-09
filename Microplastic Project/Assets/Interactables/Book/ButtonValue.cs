using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting.FullSerializer;
public static class ButtonValue
{
    public static List<bool> buttonList = new List<bool>();

    public static void AddToList(int count)
    {
        for (int i = 0; i < count; i++)
        {
            buttonList.Add(false);
        }
    }

    public static void SetUpgradeButton(int index,bool can)
    {
        buttonList[index] = can;
    }

    public static bool GetIfUpgraded(int index)
    {
        return buttonList[index];
    }
}
