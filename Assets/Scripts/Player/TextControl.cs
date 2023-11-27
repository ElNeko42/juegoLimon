using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextControl : MonoBehaviour
{
    public TextMeshProUGUI nombreTextMesh;
    public TextMeshProUGUI nombreMenuTextMesh;
    public TextMeshProUGUI feTextMesh;
    public TextMeshProUGUI fuerzaTextMesh;
    public TextMeshProUGUI conocimientoTextMesh;
    public TextMeshProUGUI suerteTextMesh;

    void Start()
    {
        nombreTextMesh.text = GameController.instance.playerName;
        nombreMenuTextMesh.text = GameController.instance.playerName;
        feTextMesh.text ="Fe: "+ GameController.instance.playerFaith.ToString();
        fuerzaTextMesh.text ="Fuerza: "+ GameController.instance.playerStrength.ToString();
        conocimientoTextMesh.text ="Conocimiento: "+ GameController.instance.playerKnowledge.ToString();
        suerteTextMesh.text ="Suerte: "+ GameController.instance.playerLuck.ToString();
    }

}
