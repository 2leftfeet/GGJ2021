using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask handsLayerMask = default;

    public GameObject radio;

    public Camera handCamera;
    public Collider radioDialCol;
    public Collider radioSwitchCol;

    public float scrollSpeed = 5.0f;
    public float clickAngleDelta = 7.0f;

    bool radioEnabled = false;

    bool radioClicked = false;


    Quaternion clickedRotation;
    Quaternion unclickedRotation;

    void Start()
    {
        unclickedRotation = radioSwitchCol.transform.localRotation;
        clickedRotation = Quaternion.Euler(unclickedRotation.eulerAngles.x, unclickedRotation.eulerAngles.y + clickAngleDelta, unclickedRotation.eulerAngles.z);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            radioEnabled = !radioEnabled;
            radio.SetActive(radioEnabled);
        }

        if(!radioEnabled) return;

        Ray ray = handCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider == radioDialCol)
            {
                //Mouse over on dial
                radioDialCol.transform.Rotate(Vector3.forward * Input.mouseScrollDelta.y * scrollSpeed);
            }
            else if(hit.collider == radioSwitchCol)
            {
                //Mouse over on switch
                if(Input.GetMouseButtonDown(0))
                {
                    radioClicked = !radioClicked;
                }
            }
        }

        if(radioClicked)
        {
            radioSwitchCol.transform.localRotation = Quaternion.Lerp(radioSwitchCol.transform.localRotation, clickedRotation, Time.deltaTime);
        }
        else
        {
            radioSwitchCol.transform.localRotation = Quaternion.Lerp(radioSwitchCol.transform.localRotation, unclickedRotation, Time.deltaTime);
        }
    }
}
