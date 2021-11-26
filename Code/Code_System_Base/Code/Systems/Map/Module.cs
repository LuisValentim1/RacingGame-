using System;
using System.Collections;
using UnityEngine;

public class Module : MonoBehaviour {

    // Variables
    public int squares_rows_quant;
    public int squares_collumns_quant;
    public Square[,] squares;
    public int squares_size;

    // Methods
    public bool IsSquareEmpty() {
        return false;
    }

    public Square GetEmptySquare() {
        return null;
    }

}