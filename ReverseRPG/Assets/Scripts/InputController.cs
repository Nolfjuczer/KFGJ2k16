using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class InputController : Singleton<InputController>
{
    public Vector3 GetLeftStick(EGamePad pad)
    {
        Vector3 movement = Vector3.zero;
        switch (pad)
        {
            case EGamePad.Pad1:
                movement.x = Input.GetAxis("HorizontalLeft1");
                movement.y = Input.GetAxis("VerticalLeft1");
                break;
            case EGamePad.Pad2:
                movement.x = Input.GetAxis("HorizontalLeft2");
                movement.y = Input.GetAxis("VerticalLeft2");
                break;
        }
        return movement;
    }

    public Vector3 GetRightStick(EGamePad pad)
    {
        Vector3 movement = Vector3.zero;
        switch (pad)
        {
            case EGamePad.Pad1:
                movement.x = Input.GetAxis("HorizontalRight1");
                movement.y = Input.GetAxis("VerticalRight1");
                break;
            case EGamePad.Pad2:
                movement.x = Input.GetAxis("HorizontalRight2");
                movement.y = Input.GetAxis("VerticalRight2");
                break;
        }
        return movement;
    }

    public bool GetLeftBumper(EGamePad pad)
    {
        switch (pad)
        {
            case EGamePad.Pad1:
                return Input.GetButton("BumperLeft1");
            case EGamePad.Pad2:
                return Input.GetButton("BumperLeft2");
        }
        return false;
    }

    public bool GetRightBumper(EGamePad pad)
    {
        switch (pad)
        {
            case EGamePad.Pad1:
                return Input.GetButton("BumperRight1");
            case EGamePad.Pad2:
                return Input.GetButton("BumperRight2");
        }
        return false;
    }

    public bool GetX(EGamePad pad)
    {
        switch (pad)
        {
            case EGamePad.Pad1:
                return Input.GetButton("X1");
            case EGamePad.Pad2:
                return Input.GetButton("X2");
        }
        return false;
    }

    public bool GetLeftTrigger(EGamePad pad)
    {
        switch (pad)
        {
            case EGamePad.Pad1:
                if (Input.GetAxis("Triggers1") < -0.2f)
                    return true;
                else
                    return false;
            case EGamePad.Pad2:
                if (Input.GetAxis("Triggers2") < -0.2f)
                    return true;
                else
                    return false;
        }
        return false;
    }

    public bool GetRightTrigger(EGamePad pad)
    {
        switch (pad)
        {
            case EGamePad.Pad1:
                if (Input.GetAxis("Triggers1") > 0.2f)
                    return true;
                else
                    return false;
            case EGamePad.Pad2:
                if (Input.GetAxis("Triggers2") > 0.2f)
                    return true;
                else
                    return false;
        }
        return false;
    }

    //public void Update()
    //{
    //    Debug.Log(GetLeftStick(EGamePad.Pad1));
    //}
}

public enum EGamePad
{
    Pad1 = 0,
    Pad2 = 1
}
