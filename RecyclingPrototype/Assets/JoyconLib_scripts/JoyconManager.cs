using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class JoyconManager : MonoBehaviour
{

    // Settings accessible via Unity
    public bool EnableIMU = true;
    public bool EnableLocalize = true;
    public byte LEDs = 0xff;

    public Joycon[] j;
    static JoyconManager instance;

    public static JoyconManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
        j = new Joycon[2];

        //0 is right
        j[0] = new Joycon();
        //1 is left
        j[1] = new Joycon();
    }

    void Start()
    {
        j[0].Attach(false, leds_: LEDs, imu: EnableIMU, localize: EnableLocalize);
        j[1].Attach(true, leds_: LEDs, imu: EnableIMU, localize: EnableLocalize);
        j[0].Begin();
        j[1].Begin();
    }

    void Update()
    {
        foreach (Joycon joycon in j)
        {
            joycon.Update();
        }
    }

    void OnApplicationQuit()
    {
        foreach (Joycon joycon in j)
        {
            joycon.Detach();
        }
    }
}
