//Tutorial I used for reference
//https://www.youtube.com/playlist?list=PLoaQriBLXcb2-L3G9Q1G0d4TcU_Gv8SFt

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public Transform floatPoint;
    public float launchSpeed;

    Camera cam;

    GameObject GravTarget;
    Rigidbody GravTargetRig;

    public float weaponRange = 12f;

    bool isAttracting;
    bool isLaunching;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
            isAttracting = true;
        else if(Input.GetButtonUp("Fire1"))
            isAttracting = false;

        if (isAttracting)
        {
            if (Input.GetButtonDown("Fire2"))
                isLaunching = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (isAttracting)
            Attract();
        else if (!isAttracting)
            Release();

        if (isLaunching)
            Throw();
    }

    public void Attract()
    {
        RaycastHit hit;

        if (GravTarget == null)
        {
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weaponRange))
            {
                if (hit.transform.tag == "CanGrab")
                {
                    GravTarget = hit.transform.gameObject;
                    GravTargetRig = GravTarget.GetComponent<Rigidbody>();
                    GravTarget.transform.position = Vector3.MoveTowards(GravTarget.transform.position, floatPoint.position, 0.3f);
                    GravTargetRig.useGravity = false;
                }
                
            }

        }
        else
        {
            GravTarget.transform.position = Vector3.MoveTowards(GravTarget.transform.position, floatPoint.position, 0.3f);
        }
    }

    public void Release()
    {
        if(GravTarget != null)
        {
            GravTargetRig.useGravity = true;
            GravTarget = null;
        }
    }

    public void Throw()
    {
        if(GravTargetRig != null)
        {
            GravTargetRig.useGravity = true;
            GravTargetRig.AddForce(floatPoint.transform.up * launchSpeed, ForceMode.Impulse);
            GravTarget = null;
            isLaunching = false;
        }
    }
}
