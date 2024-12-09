using UnityEngine;

public class InteractVacuumStateTrigger : MonoBehaviour
{
    private VacuumStateManager _vacuumStateManager;
    private BasicInteractLayout _basicInteractLayout;

    void Awake()
    {
        _vacuumStateManager = FindObjectOfType<VacuumStateManager>();

        if (_vacuumStateManager == null)
        {
            Debug.LogError("VacuumStateManager is not found in the scene!");
        }

        // Find the BasicInteractLayout and listen to the  action
        _basicInteractLayout = GetComponent<BasicInteractLayout>();
        if (_basicInteractLayout != null)
        {
            _basicInteractLayout._interactAction += EquipVacuum;
        }
        else
        {
            Debug.LogError("BasicInteractLayout component not found!");
        }
    }

    private void EquipVacuum()
    {
        // Call the EquipVacuum method in VacuumStateManager
        if (_vacuumStateManager != null)
        {
            _vacuumStateManager.EquipVacuum();
        }
    }

    void OnDestroy()
    {
        // Help with optimization
        if (_basicInteractLayout != null)
        {
            _basicInteractLayout._interactAction -= EquipVacuum;
        }
    }
}
