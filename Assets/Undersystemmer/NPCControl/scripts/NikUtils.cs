using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NikUtils
{
    public class nDebug
    {
        public bool debug = false;
        public string name = "";

        public void Log(string s)
        {
            if (debug)
                Debug.Log(name + ": " + s);
        }
    }
}