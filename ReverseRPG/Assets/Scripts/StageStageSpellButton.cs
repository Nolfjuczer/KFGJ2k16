using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[Serializable]
public class StageStageSpellButton : MonoBehaviour
{
    public Image IMG;
    public bool Disabled;

    public StageStageSpellButton Left;
    public StageStageSpellButton Right;
    public StageStageSpellButton Down;
    public StageStageSpellButton Up;
}
