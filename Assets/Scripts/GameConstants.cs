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
    public const KeyCode KEY_SPRINT = KeyCode.LeftShift;
    public const KeyCode KEY_JUMP = KeyCode.Space;
    public const KeyCode KEY_COLLECT = KeyCode.E;
    public const string BUTTON_FIRE1 = "Fire1";
    public const string BUTTON_FIRE2 = "Fire2";
    [Header("Layers")]
    public const int LAYER_ESTRUCTURES = 6;
    [Header("TAGS")]
    public const string TAG_SUELO = "Suelo";
    public const string TAG_BALON = "Balon";
    public const string TAG_PLAYER = "Player";

    [Header("Animations Parameters")]
    public const string  ANIMATOR_PARAMETER_HORIZONTAL = "HORIZONTAL";
    public const string  ANIMATOR_PARAMETER_VERTICAL = "VERTICAL";
    public const string  ANIMATOR_PARAMETER_SPEED = "SPEED";
    public const string  ANIMATOR_PARAMETER_AIMING = "AIMING";
    public const string  ANIMATOR_PARAMETER_SPRINT_MULTIPLIER = "SPRINT MULTIPLIER";
    public const string  ANIMATOR_PARAMETER_PLAYER_JUMPED = "JUMPED";
    [Header("Animations Layers")]
    public const string  ANIMATOR_LAYER_SPRINT = "Sprinting";
    public const string  ANIMATOR_LAYER_HORIZONTAL = "Horizontal";
    public const string  ANIMATOR_LAYER_AIMING = "Aiming";
    public const string  ANIMATOR_LAYER_PUNCH = "Punch";
    public const string  ANIMATOR_LAYER_JUMP = "Jumping";
    public const string  ANIMATOR_LAYER_JUMPTOP = "JumpingTop";
}
