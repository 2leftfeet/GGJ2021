using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PieceDatabase : ScriptableObject
{
    public List<GameObject> pieces = new List<GameObject>();
}