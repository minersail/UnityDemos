﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryIK2 : MonoBehaviour
{
    public Transform leftOrigin;
    public Transform rightOrigin;

    Animator anim;

    Vector3 leftPoint;
    Vector3 rightPoint;
    float leftIKWeight;
    float rightIKWeight;

    public Vector3 rot;

    bool left;
    float time;

    float animSpeed = 3.0f; // Speed multiplier for hand reaching animation;

    void Start ()
    {
        anim = GetComponent<Animator>();
        left = true;
	}
	
	void Update ()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            left = !left;
        }

        bool touchWall = false;
        float ikLerpSpeed = 3.0f;

        if (Mathf.Abs(anim.GetFloat("Velocity")) > 0.2f)
        {
            float range = 3.0f;
            RaycastHit hitData;

            Vector3 castOrigin = left ? leftOrigin.position : rightOrigin.position;
            Vector3 directionX = left ? Vector3.left : Vector3.right;
            Vector3 directionZ = Vector3.forward;

            if (Physics.Raycast(castOrigin, directionX, out hitData, range))
            {
                bool forward = anim.GetFloat("Velocity") > 0;

                if (left)
                {
                    leftPoint = hitData.point + GetCircularOffset(directionX, !forward);
                    leftIKWeight = Mathf.Lerp(leftIKWeight, 1, Time.deltaTime * ikLerpSpeed);
                    rightIKWeight = Mathf.Lerp(rightIKWeight, 0, Time.deltaTime * ikLerpSpeed);
                }
                else
                {
                    rightPoint = hitData.point + GetCircularOffset(directionX, !forward);
                    rightIKWeight = Mathf.Lerp(rightIKWeight, 1, Time.deltaTime * ikLerpSpeed);
                    leftIKWeight = Mathf.Lerp(leftIKWeight, 0, Time.deltaTime * ikLerpSpeed);
                }
                touchWall = true;
            }
        }

        if (!touchWall)
        {
            leftIKWeight = Mathf.Lerp(leftIKWeight, 0, Time.deltaTime * ikLerpSpeed);
            rightIKWeight = Mathf.Lerp(rightIKWeight, 0, Time.deltaTime * ikLerpSpeed);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftPoint);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftIKWeight);

        anim.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.Euler(GetHandRotationOffset(true)));
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftIKWeight);

        anim.SetIKPosition(AvatarIKGoal.RightHand, rightPoint);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightIKWeight);

        anim.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.Euler(GetHandRotationOffset(false)));
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightIKWeight);
    }

    private Vector3 GetCircularOffset(Vector3 direction, bool clockwise)
    {
        float animRadius = 0.2f;

        if (clockwise)
            return (-direction * (Mathf.Max(Mathf.Sin(time * animSpeed), 0) * animRadius)) + (Vector3.forward * ((Mathf.Cos(time * animSpeed) + 1) * animRadius));
        else
            return (-direction * (Mathf.Max(Mathf.Cos(time * animSpeed), 0) * animRadius)) + (Vector3.forward * ((Mathf.Sin(time * animSpeed) + 1) * animRadius));
    }

    private Vector3 GetHandRotationOffset(bool leftHand)
    {
        float Z = leftHand ? -90 : 90;
        float Ymod = leftHand ? 1 : -1;

        Vector3 minRot = new Vector3(-45, 0, Z);
        Vector3 maxRot = new Vector3(-90, 90 * Ymod, Z);

        return minRot + ((maxRot - minRot) * (Mathf.Cos(time * animSpeed) + 1) / 2);
    }
}
