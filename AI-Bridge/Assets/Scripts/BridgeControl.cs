using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BridgeControl : MonoBehaviour
{
    OffMeshLink offMeshLink;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WhicState");
        offMeshLink = GetComponent<OffMeshLink>();
        offMeshLink.activated = false;

    }

    private IEnumerator WhicState()
    {
        while (true)
        {

            if (StateControl.stateType == StateType.Attack)
            {
                GameObject[] allBridges = GameObject.FindGameObjectsWithTag("Bridge");

                foreach (GameObject bridge in allBridges)
                {
                    bridge.GetComponent<Renderer>().material.color = Color.green;

                }
                offMeshLink.activated = true;
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(1);

        }
    }


}
