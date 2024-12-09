using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicInteractLayout))]
public class InteractVacuumCleaner : MonoBehaviour
{
    [SerializeField]
    private GameObject _vacuumPrefab;

    private BasicInteractLayout _basicInteractLayout;

    void Awake()
    {
        _basicInteractLayout = GetComponent<BasicInteractLayout>();
        _basicInteractLayout._interactAction += PickUpVacuumCleaner; //Depends on function name
    }

    //unique function/interaction based on script/object
    void PickUpVacuumCleaner()
    {
        //put here the things that need to happen before destroying this interactable
        if (_vacuumPrefab != null)
        {
            Instantiate(_vacuumPrefab);
        }

        GameObject parent = this.gameObject.transform.parent.gameObject;
        Destroy(parent);

        //_basicInteractLayout.ExitANDNotInteract(true); //in case you want to only interact once
    }
}