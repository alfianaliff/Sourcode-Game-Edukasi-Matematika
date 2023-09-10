using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingPlayer : MonoBehaviour
{
    public float MovementSpeed = 3f;
    public float TurningSpeed = 3f; 
    public float minDistance = 4f;
    public float maxDistance = 8f;
    

    GameObject player;

    public GameObject animatedCharacter;
    Animator animator;

    bool isChasing;
    // Start is called before the first frame update
    void Start()
    {
        isChasing = false;
        player = GameObject.Find ("PlayerArmature");
        animator = animatedCharacter.GetComponent<Animator>(); 
    }

    // Euclidean Distance Mengukur jarak
    void Update()
    {
        float distance = Vector3.Distance(transform.position,player.transform.position);//mengukur jarak musuh ke player

        if (distance < minDistance){
            isChasing = true;
            animator.SetBool("isChasing",true); // memanggil fungsi dari animator ketika musuh mengejar player
    
        }
         if (distance > maxDistance){
            isChasing = false;
             animator.SetBool("isChasing",false);

        }
        if (isChasing){ //untuk bergerak ke player

            Vector3 lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * TurningSpeed);
            transform.position += transform.forward * Time.deltaTime * MovementSpeed;
        }
    }
}
