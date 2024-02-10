using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class ActionPathAttribute : Attribute
{
    private string path;

    public ActionPathAttribute(string path)
    {
        this.path = path;
    }

    public string Path => path;
}
