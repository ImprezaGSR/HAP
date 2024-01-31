using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    public Animator animator;
    private string currentState;
    PhotonView view;
    private bool allowInteraction;
    private HTManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<HTManager>().GetComponent<HTManager>();
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
        Debug.Log(PhotonNetwork.PlayerList);
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine){
            rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))*speed;
        }
        if(Input.GetKeyDown(KeyCode.Z)){
            allowInteraction = true;
        }
        if(Input.GetKeyUp(KeyCode.Z)){
            allowInteraction = false;
        }
    }

    void FixedUpdate()
    {
        if(view.IsMine){
            animator.SetFloat("xVelocity", rb.velocity.x);
            animator.SetFloat("yVelocity", rb.velocity.z);
            animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));
            animator.SetFloat("yVelocityAbs", Mathf.Abs(rb.velocity.z));
            if(Mathf.Abs(rb.velocity.x)>Mathf.Abs(rb.velocity.z)){
                animator.SetBool("xVelAbsGreaterThanYVelAbs",true);
            }else if (Mathf.Abs(rb.velocity.x)<Mathf.Abs(rb.velocity.z)){
                animator.SetBool("xVelAbsGreaterThanYVelAbs",false);
            }
            animator.SetBool("allVel0",Mathf.Abs(rb.velocity.x)==Mathf.Abs(rb.velocity.z) && Mathf.Abs(rb.velocity.z) == 0);
            animator.SetBool("allVel1",Mathf.Abs(rb.velocity.x)==Mathf.Abs(rb.velocity.z) && Mathf.Abs(rb.velocity.z) == 1f * speed);

            float xVelocity = rb.velocity.x;
            float yVelocity = rb.velocity.z;
            float xVelocityAbs = Mathf.Abs(rb.velocity.x);
            float yVelocityAbs = Mathf.Abs(rb.velocity.z);

            if(xVelocityAbs > yVelocityAbs && xVelocity > 0){
                ChangeAnimationState("WalkRight");
            }else if (xVelocityAbs > yVelocityAbs && xVelocity < 0){
                ChangeAnimationState("WalkLeft");
            }else if (xVelocityAbs < yVelocityAbs && yVelocity > 0){
                ChangeAnimationState("WalkUp");
            }else if (xVelocityAbs < yVelocityAbs && yVelocity < 0){
                ChangeAnimationState("WalkDown");
        }
        }
        // animator.SetFloat("xVelocity", rb.velocity.x);
        // animator.SetFloat("yVelocity", rb.velocity.z);
        // animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));
        // animator.SetFloat("yVelocityAbs", Mathf.Abs(rb.velocity.z));
        // if(Mathf.Abs(rb.velocity.x)>Mathf.Abs(rb.velocity.z)){
        //     animator.SetBool("xVelAbsGreaterThanYVelAbs",true);
        // }else if (Mathf.Abs(rb.velocity.x)<Mathf.Abs(rb.velocity.z)){
        //     animator.SetBool("xVelAbsGreaterThanYVelAbs",false);
        // }
        // animator.SetBool("allVel0",Mathf.Abs(rb.velocity.x)==Mathf.Abs(rb.velocity.z) && Mathf.Abs(rb.velocity.z) == 0);
        // animator.SetBool("allVel1",Mathf.Abs(rb.velocity.x)==Mathf.Abs(rb.velocity.z) && Mathf.Abs(rb.velocity.z) == 1f * speed);

        // float xVelocity = rb.velocity.x;
        // float yVelocity = rb.velocity.z;
        // float xVelocityAbs = Mathf.Abs(rb.velocity.x);
        // float yVelocityAbs = Mathf.Abs(rb.velocity.z);

        // if(xVelocityAbs > yVelocityAbs && xVelocity > 0){
        //     ChangeAnimationState("WalkRight");
        // }else if (xVelocityAbs > yVelocityAbs && xVelocity < 0){
        //     ChangeAnimationState("WalkLeft");
        // }else if (xVelocityAbs < yVelocityAbs && yVelocity > 0){
        //     ChangeAnimationState("WalkUp");
        // }else if (xVelocityAbs < yVelocityAbs && yVelocity < 0){
        //     ChangeAnimationState("WalkDown");
        // }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Collectable")){
            Destroy(other.gameObject);
        }
        if(other.CompareTag("BedLux")){
            if(allowInteraction){
                if(manager.isLux){
                    manager.ActivateDialogue("You laid youself in the bed...");
                    manager.ActivateUpgradeUI();
                }else{
                    manager.ActivateDialogue("Looks like this bed belongs to the other patient...");
                }
                allowInteraction = false;
            }
        }
        if(other.CompareTag("BedTenebris")){
            if(allowInteraction){
                if(!manager.isLux){
                    manager.ActivateDialogue("You laid youself in the bed...");
                    manager.ActivateUpgradeUI();
                }else{
                    manager.ActivateDialogue("Looks like this bed belongs to the other patient...");
                }
                allowInteraction = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("BedLux")){
            manager.DeactivateUpgradeUI();
        }
        if(other.CompareTag("BedTenebris")){
            manager.DeactivateUpgradeUI();
        }
    }

    void ChangeAnimationState(string newState){
        if(currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}
