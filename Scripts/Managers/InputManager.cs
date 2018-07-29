using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    public bool onlyHasInput = false;
    public bool readInput = false;

    public bool fire1, fire2, fire3, fire4, _switch;
    public Dictionary<string, bool> inputBoolDic = new Dictionary<string, bool>();


   

    public float horizontal;
    public float vertical;
    public float Horizontal
    {
        get
        {
            return horizontal;
        }

        set
        {
            horizontal = value;
        }
    }

    public float Vertical
    {
        get
        {
            return vertical;
        }

        set
        {
            vertical = value;
        }
    }



    private void Awake()
    {
        inputBoolDic.Add("Fire1", fire1);
        inputBoolDic.Add("Fire2", fire2);
        inputBoolDic.Add("Fire3", fire3);
        inputBoolDic.Add("Fire4", fire4);
        inputBoolDic.Add("Switch", _switch);
    }

    private void LateUpdate()
    {
        inputBoolDic["Fire1"] = false;
        inputBoolDic["Fire2"] = false;
        inputBoolDic["Fire3"] = false;
        inputBoolDic["Fire4"] = false;
        inputBoolDic["Switch"] = false;
    }

    private void Update()
    {
        if (readInput)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                inputBoolDic["Fire1"] = true;
                fire1 = true;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                inputBoolDic["Fire2"] = true;
                fire2 = true;

            }
            if (Input.GetButtonDown("Fire3"))
            {
                inputBoolDic["Fire3"] = true;
                fire3 = true;

            }
            if (Input.GetButtonDown("Fire4"))
            {
                inputBoolDic["Fire4"] = true;
                fire4 = true;

            }
            if (Input.GetButtonDown("Switch"))
            {
                inputBoolDic["Switch"] = true;
                _switch = true;
            }
            if (onlyHasInput)
            {
                if (Input.GetAxis("Horizontal") != 0)
                {
                    Horizontal = Input.GetAxis("Horizontal");
                }
                if (Input.GetAxis("Vertical") != 0)
                {
                    Vertical = Input.GetAxis("Vertical");
                }
            }
            else
            {
                Horizontal = Input.GetAxis("Horizontal");
                Vertical = Input.GetAxis("Vertical");
            }
        }

    }
}
