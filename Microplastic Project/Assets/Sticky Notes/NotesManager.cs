using System.Collections.Generic;

public static class NotesManager
{

    public static List<int> NoteIds = new List<int>();

    public static void AddToList(int id)
    {//add id to list if interacted with
        for (int i = 0; i < NoteIds.Count; i++)
        {
            if (NoteIds[i] == id)
            {
                return;
            }
        }

        NoteIds.Add(id);
    }

    public static bool CheckContainsId(int id)
    {
        for (int i = 0; i < NoteIds.Count; i++)
        {
            if (NoteIds[i] == id)
            {
                return true;
            }
        }
        return false;
    }
}
