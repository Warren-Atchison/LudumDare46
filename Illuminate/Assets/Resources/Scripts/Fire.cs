using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : LightSource
{
    public float maxFireStrength;
    public float curFireStrength;
    public float lightRadius;
    public float checkFireTime;
    public float progress;

    private GameObject energyHandlerGameObject;
    private EnergyHandler energyHandler;

    private float prevVal;

    private void Start()
    {
        energyHandlerGameObject = GameObject.Find("EnergyHandler");
        energyHandler = energyHandlerGameObject.GetComponent<EnergyHandler>();

        AddToEnergyHandler();
    }

    private void Update()
    {
        base.UpdateTouchingGround();

        if (prevVal != GetValue())
        {
            prevVal = GetValue();
            curFireStrength = prevVal;
            Debug.Log(prevVal);
        }
    }

    public override bool AddToEnergyHandler()
    {
        return energyHandler.AddSource(name, maxFireStrength);
    }
    public override float GetLightRadius()
    {
        return lightRadius;
    }

    public override float GetValue()
    {
        return energyHandler.GetValue(name);
    }

    public override bool Place()
    {
        throw new System.NotImplementedException();
    }

    public override bool Remove()
    {
        throw new System.NotImplementedException();
    }

    public override float GetInteractTime()
    {
        return checkFireTime;
    }

    public override float GetProgress()
    {
        return progress;
    }

    public override void SetProgress(float progress)
    {
        this.progress = progress;
    }

    private void AddLogToFire()
    {
        float currentBurnTime = GetValue();
        float newBurnTime = currentBurnTime + (0.333f * maxFireStrength);

        if (newBurnTime > maxFireStrength)
            newBurnTime = maxFireStrength;

        Debug.Log(currentBurnTime + " : " + newBurnTime);

        energyHandler.UpdateSource(name, newBurnTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.CheckTouchingGround(collision);

        if (collision.gameObject.tag.Equals("Item") && collision.gameObject.name.Contains("Log"))
            AddLogToFire();

        if (!collision.gameObject.tag.Equals("Player") && !collision.gameObject.tag.Equals("Ground"))
            Destroy(collision.gameObject);
    }
}
