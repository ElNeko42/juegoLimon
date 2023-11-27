using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextControl : MonoBehaviour
{
    public TextMeshProUGUI nombreTextMesh;

    void Start()
    {
        nombreTextMesh.text = GameController.instance.playerName;

    }

}
