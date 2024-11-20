using UnityEngine;

[CreateAssetMenu(fileName = "Entorno", menuName = "Entornos/Nuevo Entorno")]
public class TemaSO : ScriptableObject
{
    [Header("Tema de Entorno - Setea un entorno para el juego")]
    public string Name;
    public Sprite Sprite;
    public Material Material;
    public GameObject EnvironmentPrefab;
    
}
