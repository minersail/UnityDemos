using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatar_controller : MonoBehaviour {
    public GameObject avatar;
    public GameObject leftHand;
    public GameObject rightHand;
    public Transform head;
    public Camera avatarCamera;

    public bool useIK;
    public bool inWater;
    private float ikWeight = 1;


    private Animator anim;
    private float IK_THRESHOLD = 0.2f;
    private float IK_LERP_SCALE = 2.5f;
    private float CROUCH_HEIGHT = 1.5f;
    private float SPEED = 2.0f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        //Set the local position ofthe left and right controller
        //leftHand.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        //leftHand.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);

        //rightHand.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        //rightHand.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);


        //Get distance between player and avatar;
        Vector3 avatarPos = avatar.transform.position;
        avatarPos.y = 0;

        Vector3 cameraPos = avatarCamera.transform.position;
        cameraPos.y = 0;

        Vector3 distanceVector = cameraPos - avatarPos;
        Vector3 directionVector = distanceVector.normalized;
        Vector3 localDirection = avatar.transform.InverseTransformDirection(directionVector);
        float distance = distanceVector.magnitude;
        float maxClamp = GetSpeedFromDistance(3); // Use log10(x) + 1 function on [0, 3] for distance vs speed
        float distanceScale = Mathf.Clamp(GetSpeedFromDistance(distance), 0, maxClamp) / maxClamp; // Normalize between [0, 1]

        if (distance < IK_THRESHOLD)
            ikWeight = Mathf.Lerp(ikWeight, 1, Time.deltaTime * IK_LERP_SCALE);
        else
            ikWeight = Mathf.Lerp(ikWeight, 0, Time.deltaTime * IK_LERP_SCALE);
        
        anim.SetFloat("VelocityX", localDirection.x * distanceScale);
        anim.SetFloat("VelocityZ", localDirection.z * distanceScale);
        avatar.transform.Translate(directionVector * Time.deltaTime * distanceScale * SPEED, Space.World);
        anim.SetBool("InWater", inWater);
        //Keycode for crouched
        anim.SetBool("Crouching", avatarCamera.transform.position.y < CROUCH_HEIGHT);
    }

    private void LateUpdate()
    {
        Vector3 rotation = avatar.transform.eulerAngles;
        rotation.y = Mathf.LerpAngle(rotation.y, avatarCamera.transform.localEulerAngles.y, Time.deltaTime);
        avatar.transform.eulerAngles = rotation;
        head.rotation = avatarCamera.transform.localRotation;

    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHand.transform.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHand.transform.rotation);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeight);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.transform.position);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.transform.rotation);

    }

    /* Function to plotting speed vs distance 
     * has a downward curve so that speed drops off faster when getting closer
     */
    private float GetSpeedFromDistance(float x)
    {
        return Mathf.Log10(x) + 1; 
    }
}
