using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Class
{
    Guerrero = 1,
    Mago = 2,
    Cazador = 3,
    Boticario=4,
}
public enum Habilidad
{
    Fuerza,
    Sabiduria,
    Fe,
    Suerte
}
[System.Serializable]
public struct PuntosHabilidad
{
    public int fuerza;
    public int sabiduria;
    public int fe;
    public int suerte;
}

public class RolPlayer : MonoBehaviour
{
    public Class classType;
    public Class clase { get => classType; }
    public PuntosHabilidad habilidades;

    void Start()
    {
        AsignarHabilidades();
    }

    private void AsignarHabilidades()
    {
        switch (classType)
        {
            case Class.Guerrero:
                habilidades = new PuntosHabilidad { fuerza = 10, sabiduria = 5, fe = 4, suerte = 6 };
                break;
            case Class.Mago:
                habilidades = new PuntosHabilidad { fuerza = 4, sabiduria = 10, fe = 7, suerte = 5 };
                break;
            case Class.Cazador:
                habilidades = new PuntosHabilidad { fuerza = 7, sabiduria = 6, fe = 5, suerte = 8 };
                break;
            case Class.Boticario:
                habilidades = new PuntosHabilidad { fuerza = 5, sabiduria = 7, fe = 9, suerte = 6 };
                break;
        }
    }

}
