﻿using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour {

    public Text PointsText;
    int points;
    public Text MovesText;
    int moves;

    /// <summary>
    /// Initialize the class
    /// Set points and moves to 0 by default
    /// </summary>
    /// <param name="_points"></param>
    /// <param name="_moves"></param>
    public void Init(int _points = 0, int _moves = 0)
    {
        SetPoints(_points);
        SetMoves(_moves);
    }

    /// <summary>
    /// Set and display _points on interface
    /// </summary>
    /// <param name="_points"></param>
    public void SetPoints(int _points)
    {
        points = _points;
        PointsText.text = points.ToString();
    }

    /// <summary>
    /// Sum _points to actual amount
    /// and display in interface
    /// </summary>
    /// <param name="_points"></param>
    public void AddPoints(int _points)
    {
        SetPoints(points + _points);
    }

    /// <summary>
    /// Set and display _moves on interface
    /// </summary>
    /// <param name="_points"></param>
    public void SetMoves(int _moves)
    {
        moves = _moves;
        MovesText.text = moves.ToString();
    }

    /// <summary>
    /// Sum _moves to actual amount of moves
    /// Add 1 by default
    /// </summary>
    /// <param name="_moves"></param>
    public void AddMoves(int _moves = 1)
    {
        SetMoves(moves + _moves);
    }
}
