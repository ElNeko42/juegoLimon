using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngryFaceController : MonoBehaviour
{
    public Image angryFace;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
