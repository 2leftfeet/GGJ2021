using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask handsLayerMask = default;

    public GameObject radio;
    public GameObject journal;

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
    RadioPrompt radioPrompt;

    Vector3 radioStartPos = new Vector3(0.82f, -0.4f, 0.22f);
    Vector3 radioEndPos = new Vector3(0.7f, -0.19f, 1.2f);

    Vector3 journalStartPos = new Vector3(-0.5f, -0.5f, 0.2f);
    Vector3 journalEndPos = new Vector3(-0.2f, -0.14f, 0.8f);
    

    void Start()
    {
        unclickedRotation = radioSwitchCol.transform.localRotation;
        clickedRotation = Quaternion.Euler(unclickedRotation.eulerAngles.x, unclickedRotation.eulerAngles.y + clickAngleDelta, unclickedRotation.eulerAngles.z);

        controller = GetComponentInParent<FPSController>();
        stationManager = GetComponent<StationManager>();
        radioPrompt = GetComponent<RadioPrompt>();
    }

    void Update()
    {
        if(!radioClicked && Input.GetKeyDown(KeyCode.Tab))
        {
            radioEnabled = !radioEnabled;
            //radio.SetActive(radioEnabled);

            //if radio enabled, camera movement disabled and vice versa
            controller.SetCameraMovement(!radioEnabled);
            stationManager.SetRadioBackpacked(!radioEnabled);
        }

        if(radioEnabled)
        {
            radio.transform.localPosition = Vector3.Lerp(radio.transform.localPosition, radioEndPos, 2f * Time.deltaTime);
            journal.transform.localPosition = Vector3.Lerp(journal.transform.localPosition, journalEndPos, 2f * Time.deltaTime);
        }
        else
        {
            radio.transform.localPosition = Vector3.Lerp(radio.transform.localPosition, radioStartPos, 2f * Time.deltaTime);
            journal.transform.localPosition = Vector3.Lerp(journal.transform.localPosition, journalStartPos, 2f * Time.deltaTime);
        }

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
                if(Input.GetMouseButtonDown(0) && !radioClicked)
                {
                    radioClicked = true;
                    radioPrompt.EnablePrompt();
                    controller.enabled = false;
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

    public void ResetClicker()
    {
        radioClicked = false;
        controller.enabled = true;
    }
}
