using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionController : MonoBehaviour
{
    [SerializeField]
    GameObject[] cullingGroup = new GameObject[0];

    [SerializeField]
    bool bolStartDisabled = false;
    bool bolCulled = false;

    
    void Start()
    {
        if(bolStartDisabled)
            ToggleCullingGroup(false);
    }


    // MAIN TOGGLE CALL
    public void ToggleCullingGroup(bool enable)
    {
        if(enable)
        {
            foreach(GameObject obj in cullingGroup)
            {
                obj.SetActive(true);
                bolCulled = false;
            }
        }
        else
        {
            foreach(GameObject obj in cullingGroup)
            {
                obj.SetActive(false);
                bolCulled = true;
            }
        }
    }


    // TRIGGER FUNCTIONALITY
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
            ToggleCullingGroup(true);
    }
    

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
            ToggleCullingGroup(false);
    }
}
