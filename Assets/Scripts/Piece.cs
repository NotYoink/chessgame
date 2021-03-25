using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool white;
    public List<int> legalMovesX;
    public List<int> legalMovesY;

    void Start()
    {
        if (gameObject.name == "Rook(Clone)" && white)
        {
            for (int i = 1; i < 8; i++)
            {
                legalMovesY.Add(i);
            }
        }
        if (gameObject.name == "Rook(Clone)" && !white)
        {
            for (int i = 1; i < 8; i++)
            {
                legalMovesY.Add(-i);
            }
        }
        if (gameObject.name == "Pawn(Clone)" && white)
        {
            legalMovesY.Add(1);
            legalMovesX.Add(0);
        }
        if (gameObject.name == "Pawn(Clone)" && !white)
        {
            legalMovesY.Add(-1);
            legalMovesX.Add(0);
        }
    }
}
