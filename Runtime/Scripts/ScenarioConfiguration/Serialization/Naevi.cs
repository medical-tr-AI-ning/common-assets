
using System.Collections.Generic;
using UnityEngine;

public class Naevi : Lesion
{
    public float Spread;
    public float Elevation;
    public List<HighResNaevi> HighRes;
    public Naevi()
    {
        Size = 1f;
    }

    public class HighResNaevi
    {
        public Vector2 Position;
        public string Texture;
        public HighResNaevi()
        {    }
    }
}
