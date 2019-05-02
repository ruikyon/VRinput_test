using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputTemplate<T>
{
    private Func<T> _value;
    public T Value
    {
        get
        {
            return _value();
        }
    }

    private T preValue;
    protected T PreValue
    {
        get
        {
            if (InputVive.LastUpdate == Time.frameCount)
            {
                Debug.LogError("access after update");
                return default(T);
            }

            return preValue;
        }

        set
        {
            preValue = value;
        }
    }

    public InputTemplate(Func<T> value)
    {
        _value = value;
        InputVive.AddCallBack(() =>
        {
            preValue = Value;
        });
    }

}