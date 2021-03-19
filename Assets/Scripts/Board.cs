using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tilePrefab;

    public GameObject pawnPrefab, knightPrefab, bishopPrefab, rookPrefab, queenPrefab, kingPrefab;

    public Material whiteMat, blackMat;
    public Material whitePiece, blackPiece;

    public GameObject[,] squares = new GameObject[8, 8];
    GameObject[] pieceArrangement;

    public float adjustedMousePosX;
    public float adjustedMousePosY;

    [HideInInspector]
    public static string[] alphabet = new string[] {"a", "b", "c", "d", "e", "f", "g", "h"};

    public void CreateBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                squares[i, j] = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity);

                if (i %2 != 0 && j % 2 != 0 || i % 2 == 0 && j % 2 == 0)
                {
                    squares[i, j].GetComponent<Renderer>().material = blackMat;
                }
                else
                {
                    squares[i, j].GetComponent<Renderer>().material = whiteMat;
                }

                squares[i, j].transform.SetParent(gameObject.transform);
                squares[i, j].name = alphabet[i] + (j + 1);
            }
        }
    }

    public void setupPieces()
    {
        pieceArrangement = new GameObject[8]
        {
            rookPrefab,        // R = 0
            knightPrefab,      // N = 1
            bishopPrefab,      // B = 2
            queenPrefab,       // Q = 3
            kingPrefab,        // K = 4
            bishopPrefab,      // B = 5
            knightPrefab,      // N = 6
            rookPrefab         // R = 7
        };

        for (int i = 0; i < 8; i++)
        {
            //white pawns
            GameObject newPawnWhite = Instantiate(pawnPrefab, squares[i, 1].transform);
            newPawnWhite.gameObject.GetComponent<Piece>().white = true;
            newPawnWhite.GetComponent<Renderer>().material = whitePiece;

            //white pieces
            GameObject newPieceWhite = Instantiate(pieceArrangement[i], squares[i, 0].transform);
            newPieceWhite.gameObject.GetComponent<Piece>().white = true;
            newPieceWhite.GetComponent<Renderer>().material = whitePiece;

            //black pawns
            GameObject newBlackPawn = Instantiate(pawnPrefab, squares[i, 6].transform);
            newBlackPawn.gameObject.GetComponent<Piece>().white = false;
            newBlackPawn.GetComponent<Renderer>().material = blackPiece;

            //black pieces
            GameObject newBlackPiece = Instantiate(pieceArrangement[i], squares[i, 7].transform);
            newBlackPiece.gameObject.GetComponent<Piece>().white = false;
            newBlackPiece.GetComponent<Renderer>().material = blackPiece;
        }
    }

    public void RotateBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                squares[i, j].gameObject.transform.localPosition = new Vector3(7 - squares[i, j].gameObject.transform.localPosition.x, 7 - squares[i, j].gameObject.transform.localPosition.y, 0);
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;
            adjustedMousePosX = mousePos.x / 100;
            adjustedMousePosY = mousePos.x / 100;
            Mathf.FloorToInt(adjustedMousePosX);
            Mathf.FloorToInt(adjustedMousePosY);
            print(adjustedMousePosX); 
            print(adjustedMousePosY);
        }
    }

}
