using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kalah : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
          Scoree.coinA += 0;
          Destroy (gameObject);
        SceneManager.LoadScene("kalah"); 
    }
}