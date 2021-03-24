using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool white;
    public List<int> legalMoves;

    void Start()
    {
        if (gameObject.name == "Rook(Clone)" && white)
        {
            for (int i = 1; i < 8; i++)
            {
                legalMoves.Add(i);
            }
        }
        if (gameObject.name == "Pawn(Clone)" && white)
        {
            legalMoves.Add(1);
        }
    }
}
