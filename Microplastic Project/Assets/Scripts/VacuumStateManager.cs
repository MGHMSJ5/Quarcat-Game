using UnityEngine;

public class VacuumStateManager : MonoBehaviour
{
    public static bool IsVacuumEquipped { get; private set; } = false;

    // Called when the vacuum is equipped
    public void EquipVacuum()
    {
        IsVacuumEquipped = true;
        Debug.Log("Vacuum equipped!");
    }

    public void ResetVacuumState()
    {
        IsVacuumEquipped = false;
        Debug.Log("Vacuum state reset!");
    }
}
