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
public class RolPlayer : MonoBehaviour
{
    public Class classType;
    public Class clase { get => classType; }
}
