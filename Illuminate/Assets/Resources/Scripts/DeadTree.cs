﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTree : Interactable
{
    public float chopTime;
    public float progress;

    override public float GetInteractTime()
    {
        return chopTime;
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
