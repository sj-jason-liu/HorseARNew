using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalloutLabel : MonoBehaviour
{
    public LineRenderer line;
    public GameObject targetObj;

    // Start is called before the first frame update
    void Start()
    {
        line.startWidth = 1f;
        line.endWidth = 0.01f;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(UIManager.Instance.currentAnim == 1)
        {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, targetObj.transform.position);
        }
        else
        {
            line.enabled = false;
        }
    }
}
