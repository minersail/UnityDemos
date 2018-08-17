using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTest : MonoBehaviour
{
    Animator anim;

    public Transform leftPoint;
    public Transform rightPoint;

    public Vector3 leftRot;
    public Vector3 rightRot;
    
    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftPoint.position);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);

        anim.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.Euler(leftRot));
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        anim.SetIKPosition(AvatarIKGoal.RightHand, rightPoint.position);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);

        anim.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.Euler(rightRot));
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        // L
        // 0 (-90, 90, 180) | (-90, 90, 270)
        // 90 (-90, 90, -90) | (-90, 90, 0)
        // 180 (-90, 90, 0) | (-90, 90, 90)
        // 270 (-90, 90, 90) | (-90, 90, 180)

        // R
        // add 180 to Z for right
    }
}
