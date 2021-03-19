﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Board board;
    private void Start()
    {
        board = gameObject.GetComponent<Board>();
        board.CreateBoard();
        board.setupPieces();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            board.RotateBoard();
        }
    }

}
