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
        nombreTextMesh.text = GameManager.instance.player.playerName;
        nombreMenuTextMesh.text = GameManager.instance.player.playerName;
        feTextMesh.text = "Fe: " + GameManager.instance.rolPlayer.habilidades.fe.ToString();
        fuerzaTextMesh.text = "Fuerza: " + GameManager.instance.rolPlayer.habilidades.fuerza.ToString();
        conocimientoTextMesh.text = "Conocimiento: " + GameManager.instance.rolPlayer.habilidades.sabiduria.ToString();
        suerteTextMesh.text = "Suerte: " + GameManager.instance.rolPlayer.habilidades.suerte.ToString();
    }

}
