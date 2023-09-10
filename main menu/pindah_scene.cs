using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pindah_scene : MonoBehaviour
{
    public void scene(string scene)
    {
        Application.LoadLevel (scene);
    }
}
