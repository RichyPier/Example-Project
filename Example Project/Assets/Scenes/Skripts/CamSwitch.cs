using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public Camera mainCam;
    public Camera skyCam;

    public bool mCam;
    public bool sCam;

    // Start is called before the first frame update
    void Start()
    {
        // Evtl. eine bestimmte Cam aus der camKeyList enabled setzen
        mainCam.enabled = mCam;
        skyCam.enabled = sCam;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            mainCam.enabled = true;
            skyCam.enabled = false;
        }
        if(Input.GetKeyDown("2"))
        {
            mainCam.enabled = false;
            skyCam.enabled = true;
        }
    }
}
