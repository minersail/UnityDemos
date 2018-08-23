using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryIK2 : MonoBehaviour
{
    public Transform leftOrigin;
    public Transform rightOrigin;

    public Transform frontOrigin;
    public Transform backOrigin;

    Animator anim;

    Vector3 leftPoint;
    Vector3 rightPoint;
    float leftIKWeight;
    float rightIKWeight;

    bool left;
    float time;

    float animSpeed = 3.0f; // Speed multiplier for hand reaching animation;

    Vector3 turnStart;

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
            float range = 0.6f;
            RaycastHit hitData;

            Vector3 castOrigin = left ? leftOrigin.position : rightOrigin.position;
            Vector3 directionX = left ? -transform.right : transform.right;
            Vector3 directionZ = transform.forward;

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
            else
            {
                Vector3 raycastAngle = left ? Quaternion.AngleAxis(-125, transform.up) * transform.forward : Quaternion.AngleAxis(125, transform.up) * transform.forward;

                if (Physics.Raycast(backOrigin.position, raycastAngle, out hitData, 5.0f))
                {
                    if (hitData.normal == transform.forward)
                    {
                        anim.SetTrigger(left ? "TurnLeft" : "TurnRight");
                        turnStart = transform.eulerAngles;
                    }
                }
            }
        }

        if (!touchWall)
        {
            leftIKWeight = Mathf.Lerp(leftIKWeight, 0, Time.deltaTime * ikLerpSpeed);
            rightIKWeight = Mathf.Lerp(rightIKWeight, 0, Time.deltaTime * ikLerpSpeed);
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("TurnRight") && !anim.GetCurrentAnimatorStateInfo(0).IsName("TurnLeft"))
        {
            RaycastHit hitData2;

            if (Physics.Raycast(frontOrigin.position, transform.forward, out hitData2, 0.6f))
            {
                anim.SetTrigger(left ? "TurnRight" : "TurnLeft");

                turnStart = transform.eulerAngles;
            }
        }
        else
        {
            anim.ResetTrigger("TurnRight");
            anim.ResetTrigger("TurnLeft");
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

    // Calculate a semicircular arc around a point
    private Vector3 GetCircularOffset(Vector3 direction, bool clockwise)
    {
        float animRadius = 0.2f;

        if (clockwise)
            return (-direction * (Mathf.Max(Mathf.Sin(time * animSpeed), 0) * animRadius)) + (transform.forward * ((Mathf.Cos(time * animSpeed) + 1) * animRadius));
        else
            return (-direction * (Mathf.Max(Mathf.Cos(time * animSpeed), 0) * animRadius)) + (transform.forward * ((Mathf.Sin(time * animSpeed) + 1) * animRadius));
    }

    // Calculate rotation of hand as function of time
    private Vector3 GetHandRotationOffset(bool leftHand)
    {
        float Zmod = leftHand ? 180 : 0;
        
        Vector3 rot = new Vector3(-90, 90, (transform.eulerAngles.y + Zmod));

        if (leftHand)
            rot.z += ((Mathf.Cos(time * animSpeed) + 1) / 2) * 90;
        else
            rot.z -= ((Mathf.Cos(time * animSpeed) + 1) / 2) * 90;

        return rot;
    }

    private void OnAnimatorMove()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("TurnRight"))
        {
            float rotAdd = anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f ? 90 : 90 * anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

            transform.eulerAngles = new Vector3(0, turnStart.y + rotAdd, 0);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("TurnLeft"))
        {
            float rotAdd = anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f ? 90 : 90 * anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

            transform.eulerAngles = new Vector3(0, turnStart.y - rotAdd, 0);
        }
        else
        {
            transform.position = anim.rootPosition;
        }
    }
}
