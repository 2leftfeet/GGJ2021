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

    public float frequencyChangeSpeed = 1.0f;
    public float clickAngleDelta = 7.0f;

    bool radioEnabled = false;

    bool radioClicked = false;


    Quaternion clickedRotation;
    Quaternion unclickedRotation;

    FPSController controller;
    StationManager stationManager;

    void Start()
    {
        unclickedRotation = radioSwitchCol.transform.localRotation;
        clickedRotation = Quaternion.Euler(unclickedRotation.eulerAngles.x, unclickedRotation.eulerAngles.y + clickAngleDelta, unclickedRotation.eulerAngles.z);

        controller = GetComponentInParent<FPSController>();
        stationManager = GetComponent<StationManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            radioEnabled = !radioEnabled;
            radio.SetActive(radioEnabled);

            //if radio enabled, camera movement disabled and vice versa
            controller.SetCameraMovement(!radioEnabled);
            stationManager.SetRadioBackpacked(!radioEnabled);
        }

        if(!radioEnabled) return;

        Ray ray = handCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 50f, handsLayerMask))
        {
            if(hit.collider == radioDialCol)
            {
                //Mouse over on dial
                if(Mathf.Abs(Input.mouseScrollDelta.y) > 0.0f)
                {
                    var frequencyChangedBy = stationManager.ChangeFrequency(Input.mouseScrollDelta.y * frequencyChangeSpeed);
                    radioDialCol.transform.Rotate(Vector3.forward * (-frequencyChangedBy) * 3.0f);
                }
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
            radioSwitchCol.transform.localRotation = Quaternion.Lerp(radioSwitchCol.transform.localRotation, clickedRotation, Time.deltaTime * 2.0f);
        }
        else
        {
            radioSwitchCol.transform.localRotation = Quaternion.Lerp(radioSwitchCol.transform.localRotation, unclickedRotation, Time.deltaTime * 2.0f);
        }
    }
}
