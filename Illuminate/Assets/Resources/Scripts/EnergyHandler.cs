using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyHandler : MonoBehaviour
{
    public float tickRate;
    public float fireBurnRate;

    private float ticker;

    private float totalEnergyPercentage;
    private float totalEnergyCapacity;

    private Dictionary<string, float> fires;

    private void Awake()
    {
        ticker = Time.time;

        totalEnergyPercentage = 0f;
        totalEnergyCapacity = 0f;

        fires = new Dictionary<string, float>();
    }

    private void Update()
    {
        Tick();
    }

    private void Tick()
    {
        float curTime = Time.time;
        if(curTime - ticker >= tickRate)
        {
            ticker = curTime;
            TickAll();
        }
    }

    private void TickAll()
    {
        TickFires();
    }

    private void TickFires()
    {
        Dictionary<string, float> newFires = new Dictionary<string, float>();
        foreach (KeyValuePair<string, float> fire in fires)
        {
            if (fires[fire.Key] - fireBurnRate <= 0f)
            {
                Destroy(GameObject.Find(fire.Key));
            }
            else
            {
                newFires.Add(fire.Key, fires[fire.Key] - fireBurnRate);
            }
        }

        fires = newFires;

    }
    
    public bool AddSource(string goName,  float value = 0f)
    {
        if (goName.Contains("Fire"))
        {
            fires.Add(goName, value);
            return true;
        }

        return false;
    }

    public bool UpdateSource(string goName, float value)
    {
        if (goName.Contains("Fire"))
        {
            fires[goName] = value;
            return true;
        }

        return false;
    }

    public bool RemoveSource(string goName)
    {
        if (goName.Contains("Fire"))
        {
            return fires.Remove(goName);
        }

        return false;
    }
    public float GetValue(string goName)
    {
        if (goName.Contains("Fire"))
        {
            float value;
            fires.TryGetValue(goName, out value);
            return value;
        }

        return -1f;
    }

    public float GetPercentOfCapacity()
    {
        return 0f;
    }
}
