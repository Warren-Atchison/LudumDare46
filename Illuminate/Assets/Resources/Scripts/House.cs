using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Interactable
{
    public float enterTime;
    public float progress;

    override public float GetInteractTime()
    {
        return enterTime;
    }

    override public float GetProgress()
    {
        return progress;
    }

    public override void SetProgress(float progress)
    {
        this.progress = progress;
    }
}
