using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animation_Controller : MonoBehaviour
{
    Animator _Animator;
    // Start is called before the first frame update
    void Start()
    {
        _Animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
