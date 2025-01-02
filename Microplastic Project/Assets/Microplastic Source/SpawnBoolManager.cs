using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class SpawnBoolManager
{
    public class SpawnBools
    {
        public int id;
        public bool hasReplaced;
        public bool isSpawning;
    }

    public static List<SpawnBools> BoolsList = new List<SpawnBools>();

    public static void AddToList(int spawnId, bool replace, bool spawn)
    {
        for (int i = 0; i < BoolsList.Count; i++)
        {
            if (BoolsList[i].id == spawnId)
            {
                return;
            }
        }

        SpawnBools toAdd = new SpawnBools()
        {
            id = spawnId,
            hasReplaced = replace,
            isSpawning = spawn
        };
        BoolsList.Add(toAdd);
    }

    public static void SetHasReplaced(int id,bool replace)
    {
        BoolsList[GetListInt(id)].hasReplaced = replace;
    }

    public static bool GetHasReplaced(int id)
    {
        if (GetListInt(id) == -1)
        {
            return false;
        }
        return BoolsList[GetListInt(id)].hasReplaced;
    }
    public static void SetIsSpawning(int id, bool spawn)
    {
        BoolsList[GetListInt(id)].isSpawning = spawn;
    }

    public static bool GetIsSpawning(int id)
    {
        if (GetListInt(id) == -1)
        {
            return false;
        }
        return BoolsList[GetListInt(id)].isSpawning;
    }

    public static int GetListInt(int spawn)
    {
        for (int i = 0; i < BoolsList.Count; i++)
        {
            if (BoolsList[i].id == spawn)
            {
                return i;
            }
        }
        return -1;
    }
}
