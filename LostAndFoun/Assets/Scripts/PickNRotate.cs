using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickNRotate : MonoBehaviour
{
    private PlayerMovement pm;
    public float distanceToSee;
    RaycastHit whatIHit;
    private GameObject player;
    private Transform item;
    private Vector3 itemOriginPos;
    private Quaternion itemOriginRot;
    public float RotationSpeed;


    LayerMask interactable;


    void Start()
    {
        pm = this.transform.parent.gameObject.GetComponent<PlayerMovement>();
        interactable = LayerMask.GetMask("Interactable");
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask pickable = LayerMask.GetMask("Pickable");

        GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Image>().color = Color.white;
        GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Image>().transform.localScale = new Vector3(0.1f , 0.1f , 0.1f);

        if(Input.GetKeyDown(KeyCode.Q) && !pm.enabled)
        {
            pm.enabled = true;
            item.transform.parent = null;
            item.position = itemOriginPos;
            item.rotation = itemOriginRot;
            GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Image>().enabled = true;
            transform.GetComponent<MouseLook>().enabled = true;
        }

        if(Physics.Raycast(this.transform.position , this.transform.forward , out whatIHit , distanceToSee , pickable))
        {

            GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Image>().color = Color.red;
            GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Image>().transform.localScale = new Vector3(0.25f , 0.25f , 0.25f);

            if(Input.GetKeyDown(KeyCode.E) && pm.enabled)
            {
                item = whatIHit.transform;
                itemOriginPos = item.position;
                itemOriginRot = item.rotation;
                item.transform.LookAt(this.transform , -Vector3.up);
                item.transform.parent = this.transform;
                item.transform.localPosition = new Vector3(0 , 0 , 0.7f);
                pm.enabled = false;
                GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Image>().enabled = false;
                transform.GetComponent<MouseLook>().enabled = false;

            }
        }

        if(!pm.enabled)
            item.transform.Rotate((Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime) , (Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime) , 0 , Space.Self);


    }
}
