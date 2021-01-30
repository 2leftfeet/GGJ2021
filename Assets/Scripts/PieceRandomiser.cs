using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PieceRandomiser : MonoBehaviour
{
    public PieceDatabase pieceDatabase;

    [SerializeField] private int count;
    
    [SerializeField] bool shouldRotate = false;
    
    [ContextMenu("Randomize Pieces")]
    public void RandomizePieces()
    {
        var totalNumberOfPieces = transform.childCount;

        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(DestroyImmediate);
        
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(pieceDatabase.pieces[Random.Range(0, pieceDatabase.pieces.Count - 1)], transform);
            obj.transform.localPosition = new Vector3(i * 3, 0, 0);

            if (!shouldRotate)
                obj.transform.rotation = Quaternion.Euler(-90, 0, 0);
            else
            {
                if (Random.Range(0, 10) > 5)
                {
                    obj.transform.rotation = Quaternion.Euler(-90, 0, 180);
                }
                else
                {
                    obj.transform.rotation = Quaternion.Euler(-90, 0, 0);
                }
            }
        }}
}