using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public bool firstClick = true;
    public string currentPlayer = "White";
    public GameObject tilePrefab;

    public GameObject pawnPrefab, knightPrefab, bishopPrefab, rookPrefab, queenPrefab, kingPrefab;

    public GameObject circlePrefab;

    public Material whiteMat, blackMat;
    public Material whitePiece, blackPiece;

    public GameObject[,] squares = new GameObject[8, 8];
    GameObject[] pieceArrangement;

    [HideInInspector]
    public static string[] alphabet = new string[] { "a", "b", "c", "d", "e", "f", "g", "h" };

    public void CreateBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                squares[i, j] = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity);

                if (i % 2 != 0 && j % 2 != 0 || i % 2 == 0 && j % 2 == 0)
                {
                    squares[i, j].GetComponent<Renderer>().material = blackMat;
                }
                else
                {
                    squares[i, j].GetComponent<Renderer>().material = whiteMat;
                }

                squares[i, j].transform.SetParent(gameObject.transform);
                squares[i, j].name = alphabet[i] + (j+1);
            }
        }
    }

    public void SetupPieces()
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
            newPawnWhite.tag = "White"; 

            //white pieces
            GameObject newPieceWhite = Instantiate(pieceArrangement[i], squares[i, 0].transform);
            newPieceWhite.gameObject.GetComponent<Piece>().white = true;
            newPieceWhite.GetComponent<Renderer>().material = whitePiece;
            newPieceWhite.tag = "White";

            //black pawns
            GameObject newBlackPawn = Instantiate(pawnPrefab, squares[i, 6].transform);
            newBlackPawn.gameObject.GetComponent<Piece>().white = false;
            newBlackPawn.GetComponent<Renderer>().material = blackPiece;
            newBlackPawn.tag = "Black";

            //black pieces
            GameObject newBlackPiece = Instantiate(pieceArrangement[i], squares[i, 7].transform);
            newBlackPiece.gameObject.GetComponent<Piece>().white = false;
            newBlackPiece.GetComponent<Renderer>().material = blackPiece;
            newBlackPiece.tag = "Black";
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //calculating where the player clicked
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int AMX = (int)Mathf.FloorToInt(mousePos.x + 0.5f);
            int AMY = (int)Mathf.FloorToInt(mousePos.y + 0.5f);

            if (firstClick == true)
            {
                if (squares[AMX, AMY].transform.childCount > 0 && squares[AMX, AMY].transform.GetChild(0).CompareTag(currentPlayer)) //clicking on own piece
                {
                    circlePrefab.transform.position = new Vector3(AMX, AMY, 0);
                    firstClick = false;
                }
            }
            else
            {
                if (squares[AMX, AMY].transform.childCount > 0 && squares[AMX, AMY].transform.GetChild(0).CompareTag(currentPlayer)) //clicking on own piece
                {
                    circlePrefab.transform.position = new Vector3(AMX, AMY, 0);
                }
                else if (squares[AMX, AMY].transform.childCount > 0 && !squares[AMX, AMY].transform.GetChild(0).CompareTag(currentPlayer)) //clicking on enemy piece
                {
                    GameObject capturedPiece = squares[AMX, AMY].transform.GetChild(0).gameObject;
                    Destroy(capturedPiece);
                    squares[(int)circlePrefab.transform.position.x, (int)circlePrefab.transform.position.y].transform.GetChild(0).transform.position = new Vector3(AMX, AMY, 0);
                    squares[(int)circlePrefab.transform.position.x, (int)circlePrefab.transform.position.y].transform.GetChild(0).transform.SetParent(squares[AMX, AMY].transform, true);
                    circlePrefab.transform.position = new Vector3(-10, -10, 0);
                    firstClick = true;
                    if (currentPlayer == "White")
                    {
                        currentPlayer = "Black";
                    }
                    else currentPlayer = "White";
                }
                else //clicking on empty square
                {
                    if (circlePrefab.transform.position.x >= 0 && circlePrefab.transform.position.y >= 0 && circlePrefab.transform.position.x < 8 && circlePrefab.transform.position.y < 8)
                    {
                        Piece legalMovesScript = squares[(int)circlePrefab.transform.position.x, (int)circlePrefab.transform.position.y].transform.GetChild(0).GetComponent<Piece>();
                        if (legalMovesScript.legalMovesY.Contains((int)AMY - (int)circlePrefab.transform.position.y))
                        {
                            squares[(int)circlePrefab.transform.position.x, (int)circlePrefab.transform.position.y].transform.GetChild(0).transform.position = new Vector3(AMX, AMY, 0);
                            squares[(int)circlePrefab.transform.position.x, (int)circlePrefab.transform.position.y].transform.GetChild(0).transform.SetParent(squares[AMX, AMY].transform, true);
                            circlePrefab.transform.position = new Vector3(-10, -10, 0);
                            firstClick = true;
                            if (currentPlayer == "White")
                            {
                                currentPlayer = "Black";
                            }
                            else currentPlayer = "White";
                        }
                    }
                }
            }
        }
    }
}
