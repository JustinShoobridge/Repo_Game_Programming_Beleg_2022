using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseButtonSize : MonoBehaviour
{
    public void IncreaseSize()
    {
        gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f); 
    }

    public void DecreaseSize()
    {
        gameObject.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
    }
}
