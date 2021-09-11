using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants 
{
    public const string  VERTICAL = "Vertical";
    public const string  HORIZONTAL = "Horizontal";
    public const string  MOUSE_X = "Mouse X";
    public const string  MOUSE_Y = "Mouse Y";
    public const float  PLAYERS_GRAVITY = 9.8f;

    [Header("Inputs Controls")]
    public const KeyCode KEY_FREE_VISION = KeyCode.LeftAlt;
    [Header("Properties")]
    public const float PROPERTY_FIELDOFVIEW = 60;

    [Header("Inputs Movements")]
    public const KeyCode KEY_FORWARD = KeyCode.W;
    public const KeyCode KEY_RIGHT = KeyCode.D;
    public const KeyCode KEY_LEFT = KeyCode.A;
    public const KeyCode KEY_DOWNWARDS = KeyCode.S;
    [Header("Layers")]
    public const int LAYER_ESTRUCTURES = 6;
    [Header("TAGS")]
    public const string TAG_SUELO = "Suelo";

    [Header("Animations Parameters")]
    public const string  ANIMATOR_HORIZONTAL = "HORIZONTAL";
    public const string  ANIMATOR_VERTICAL = "VERTICAL";
    public const string  ANIMATOR_SPEED = "SPEED";
    public const string  ANIMATOR_AIMING = "AIMING";
}
