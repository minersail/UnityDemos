using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryIK2 : MonoBehaviour
{
    public Transform leftOrigin;
    public Transform rightOrigin;

    public Vector3 ikrot;
    public bool lockX;

    float startX;

    Animator anim;

    Vector3 leftPoint;
    Vector3 rightPoint;

    float leftIKWeight;
    float rightIKWeight;

    float time;

    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        time += Time.deltaTime;

        /*float range = 3.0f;
        Vector3 direction = anim.GetBool("Left") ? Vector3.left : Vector3.right;

        RaycastHit leftHit;
        RaycastHit rightHit;

        if (Physics.Raycast(leftOrigin.position, direction, out leftHit, range))
        {
            if (time > 1.0f)
            {
                time = 0;
                leftPoint = leftHit.point;
            }
            leftIKWeight = Mathf.Cos(time);
        }
        else
        {
            leftIKWeight = Mathf.Lerp(leftIKWeight, 0, Time.deltaTime);
        }

        if (Physics.Raycast(rightOrigin.position, direction, out rightHit, range))
        {
            rightPoint = rightHit.point;
            rightIKWeight = 1;
        }
        else
        {
            rightIKWeight = 0;
        }*/
        float range = 3.0f;
        RaycastHit leftHit;

        if (Physics.Raycast(leftOrigin.position, Vector3.left, out leftHit, range))
        {
            leftPoint = leftHit.point + Vector3.right * (Mathf.Max(Mathf.Cos(time * 3), 0) / 5) + Vector3.forward * ((Mathf.Sin(time * 3) + 1) / 4);
            leftIKWeight = 1;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftPoint);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftIKWeight);

        anim.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.Euler(ikrot));
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

        //anim.SetIKPosition(AvatarIKGoal.RightHand, rightPoint);
        //anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightIKWeight);
    }
}
