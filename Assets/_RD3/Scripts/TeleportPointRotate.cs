using UnityEngine;

public class TeleportPointRotate : MonoBehaviour
{

    [SerializeField]
    Transform[] teleports = new Transform[0];

    void Start()
    {
        GameManager.Instance.OnTeleportEnd += RotateTeleports;
    }

    public void RotateTeleports(Transform _transform, bool _shouldApplyRotation)
    {
        foreach(Transform x in teleports)
        {
            x.LookAt(_transform.position);

            Quaternion quat = x.rotation;

            quat.x = 0;
            quat.z = 0;

            x.rotation = quat;
        }
    }

    void OnDestroy()
    {
        GameManager.Instance.OnTeleportEnd -= RotateTeleports;
    }

}
