using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private void Awake()
    {//get the target by name (its in the player prefab
        if (target == null)
        {
            target = GameObject.Find("VacuumNozzleLocation").transform;
        }
    }

    void Update()
    {
        float x = target.position.x;
        float y = target.position.y;
        float z = target.position.z;
        Quaternion rotation = target.rotation;

        transform.position = new Vector3(x, y, z);
        transform.rotation = rotation;
    }
}
