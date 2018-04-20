using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
    [SerializeField] private int walkSpeed;
    [SerializeField] private int runSpeed;
    public AnimationClip[] faceAnimationClips;
    Animator animator;
    public float x;
    public float y;
    public int sens;
    float comboTime;
    bool isCombo;
    public string animName="Idle";
    public int combo=100000;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (isCombo)
        {
            comboTime += Time.deltaTime;
        }
        if (comboTime > 0.5f)
        {
            comboTime = 0;
            isCombo = false;
            combo = 100000;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Combo(0);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Combo(1);
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("ATTACK")) {            
            animator.SetInteger("Combo", combo);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Moving(runSpeed);
            }
            else
            {
                Moving(walkSpeed);
            }
        }
    }
    
    void Combo(int buttonNum)
    {
        if (combo % 10==0)
        {
            combo+= buttonNum+1;
        }
        else if(combo % 100/10 == 0)
        {
            combo += (buttonNum + 1)*10;
        }
        else if (combo % 1000 / 100 == 0)
        {
            combo += (buttonNum + 1) * 100;
        }
        else if (combo % 10000 / 1000 == 0)
        {
            combo += (buttonNum + 1) * 1000;
        }
        else if (combo % 100000 / 10000 == 0)
        {
            combo += (buttonNum + 1) * 10000;
        }
        if (!isCombo)
        {
            isCombo = true;
            if (buttonNum == 0)
            {
                animator.Play("Jab");
                combo += 2;
            }
            else
            {
                animator.Play("Hikick");
                combo += 2;
            }
        }
        else
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.IsTag("ATTACK"))
            {
                if (info.normalizedTime > 0.9f)
                {
                    animator.Play(NextCombo());
                }
            }
            else
            {
                animator.Play(NextCombo());
            }
        }
        animName = "ATTACK";
    }
    string NextCombo()
    {
        string comboName = null;
        switch (combo)
        {
            case 100013:
                break;
        }
        
        return comboName;
    }
    void MovingAnimCtrl(Vector3 move,int speed)
    {
        //animator.SetInteger("Speed", (int)(move.magnitude * speed));
        //animator.SetInteger("Horizontal", (int)x);
        string tempName = null;
        if (move.sqrMagnitude == 0)
        {
            tempName = "Idle";
            
        }
        else
        {
            if (speed >= 3)
            {
                tempName = "RUN_F";
            }
            else
            {
                tempName = "WALK_F";
            }
            //if (y > 0.5 || y < -0.5)
            //{
            //    if (x > 0)
            //    {
            //        tempName += "R";
            //    }
            //    else if (x < 0)
            //    {
            //        tempName += "L";
            //    }
            //    else
            //    {
            //        tempName += "F";
            //        //if (y > 0)
            //        //{
            //        //    tempName += "F";
            //        //}
            //        //else
            //        //{
            //        //    tempName += "B";
            //        //}
            //    }
            //}
            //else
            //{
            //    tempName += "F";
            //}            
            
        }
        if (!animName.Equals(tempName))
        {            
            animName = tempName;
            animator.CrossFade(tempName, 0.1f);
            //animator.Play(tempName);
        }
        
        
        
    }
    void Moving(int speed)
    {
        
        Vector3 temp = Vector3.forward * y + Vector3.right * x;
        transform.LookAt(temp + transform.position);
        temp.Normalize();
        
        MovingAnimCtrl(temp,speed);
        
        
        transform.position+=transform.forward * temp.sqrMagnitude * speed*Time.deltaTime;

        
    }
}
