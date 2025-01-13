using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class BeadieStaticManager
{
    //create the list with number amount of Beadie versions there are (V1, V2, etc)
    public static List<float> SlowedBeadieSpeed = new List<float> { 0f, 0f};

    public static float GetSlowedSpeed(int index)
    {
        return SlowedBeadieSpeed[index];
    }

    public static void SetSlowedSpeed(int index, float value)
    {
        SlowedBeadieSpeed[index] = value;
    }
}
