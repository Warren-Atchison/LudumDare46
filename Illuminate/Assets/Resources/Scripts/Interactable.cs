using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract float GetInteractTime();
    public abstract float GetProgress();
    public abstract void SetProgress(float progress);
}
