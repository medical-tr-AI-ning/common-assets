using System.Collections.Generic;
using UnityEngine;

namespace medicaltraining.assetstore.ScenarioConfiguration.Serialization
{
    public class PathologyData
    { 
        public RenderTexture[] Modified { get; set; }
        public RenderTexture[] ModifiedHeight { get; set; }


        public List<Melanoma> Melanoma { get; set; } = new List<Melanoma>();
        public List<Naevi> Naevis { get; set; } = new List<Naevi>();
        public bool MelanomaEnabled { get; set; }
        public bool NaeviEnabled { get; set; }
    }
}