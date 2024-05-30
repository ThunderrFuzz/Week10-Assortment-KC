using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimToRag : MonoBehaviour
{

    [SerializeField] Collider myCol;
    [SerializeField] float respawnTime = 5f;
    Rigidbody[] rbs;
    bool bIsRagdoll = false;
    // Start is called before the first frame update
    void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(!bIsRagdoll && collision.gameObject.tag == "Pin")
        {
            ToggleRagdoll(false);
            StartCoroutine("GetUp");
        }
    }

    private void ToggleRagdoll(bool isAnimating)
    {
        bIsRagdoll = !isAnimating;
        myCol.enabled = isAnimating;
        /*foreach(Rigidbody bone in rbs)
        {
            bone.isKinematic = isAnimating;
        }*/
        for(int i = 1; i < rbs.Length; i++) {
            rbs[i].isKinematic = isAnimating;
            rbs[i].gameObject.GetComponent<Collider>().enabled = !isAnimating;
        }
        GetComponent<Animator>().enabled = isAnimating; 
        if(isAnimating)
        {
            randomAnim();
        }
    }
    IEnumerator GetUp()
    {
        yield return new WaitForSeconds(respawnTime);
        ToggleRagdoll(true);
    }
    void randomAnim()
    {
        int rand = Random.Range(0, 2);
        Animator animator = GetComponent<Animator>();
        if(rand == 0)
        {
            animator.SetTrigger("Walk");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

 
}
