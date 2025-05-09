using System;
using System.Collections.Generic;
using System.IO;
using medicaltraining.assetstore.ScenarioConfiguration.Serialization.Utils;
using Runtime.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace medicaltraining.assetstore.ScenarioConfiguration.Serialization
{
    [Serializable]
    public class SerializablePathologyVariant
    {
        public SerializablePathologyData Pathology;
        public PathologyVariant.AnamnesisData Anamnesis;
        public AgentConfig Agent;

        public SerializablePathologyVariant()
        {
        }

        public SerializablePathologyVariant(PathologyVariant pathologyVariant, string folderPath, IFileWriter fileWriter)
        {
            Anamnesis = pathologyVariant.Anamnesis;
            Pathology = new SerializablePathologyData(pathologyVariant.Pathology, folderPath, fileWriter);
            Agent = pathologyVariant.Agent;
        }

        public PathologyVariant Deserialize(string folderPath)
        {
            return new PathologyVariant
            {
                Agent = Agent,
                Anamnesis = Anamnesis,
                Pathology = Pathology.Deserialize(folderPath)
            };
        }

        [Serializable]
        public class SerializablePathologyData
        {
            public string PathologyID;
            public List<string> Textures = new List<string>();
            public List<string> HeightTextures = new List<string>();
            public List<SerializableMelanoma> Melanoma = new List<SerializableMelanoma>();
            public List<SerializableNaevi> Naevi = new List<SerializableNaevi>();
            public bool MelanomaEnabled { get; set; }
            public bool NaeviEnabled { get; set; }

            // public List<string> PathologyDescriptions;

            //Empty Constructor to use for Newtonsoft
            public SerializablePathologyData()
            {
            }

            private void AddTexture(string baseFolder, RenderTexture imageData, string folderPath, IFileWriter fileWriter)
            {
                string textureKey = Path.Combine(baseFolder, Guid.NewGuid().ToString());
                Textures.Add(textureKey);
                string fullPath = Path.Combine(folderPath, textureKey); // Combine folderPath and textureKey
                fileWriter.WriteFile(fullPath, imageData);
                
            }

            private void AddHeightTexture(string baseFolder, RenderTexture imageData, string folderPath, IFileWriter fileWriter)
            {
                string textureKey = Path.Combine(baseFolder, Guid.NewGuid().ToString());
                HeightTextures.Add(textureKey);
                string fullPath = Path.Combine(folderPath, textureKey); // Combine folderPath and textureKey
                fileWriter.WriteFile(fullPath, imageData);
                
            }

            public SerializablePathologyData(PathologyData pathologyData, string folderPath, IFileWriter fileWriter)
            {
                foreach (var melanoma in pathologyData.Melanoma)
                {
                    Melanoma.Add(new SerializableMelanoma(melanoma, folderPath, fileWriter));
                }

                foreach (var naevi in pathologyData.Naevis)
                {
                    Naevi.Add(new SerializableNaevi(naevi, folderPath, fileWriter));
                }

                for(int i = 0; i < pathologyData.Modified.Length; i++)
                {
                    AddTexture(FolderStructure.SkinTexture, pathologyData.Modified[i], folderPath, fileWriter);
                    AddHeightTexture(FolderStructure.SkinTexture, pathologyData.ModifiedHeight[i], folderPath, fileWriter);
                }

                NaeviEnabled = pathologyData.NaeviEnabled;
                MelanomaEnabled = pathologyData.MelanomaEnabled;
            }

            public PathologyData Deserialize(string folderPath)
            {
                PathologyData pathologyData = new PathologyData();

                pathologyData.Modified = new RenderTexture[Textures.Count];
                pathologyData.ModifiedHeight = new RenderTexture[Textures.Count];

                for(int i = 0; i < pathologyData.Modified.Length; i++)
                {
                    pathologyData.Modified[i] = ImageUtils.DeserializeImageToRenderTexture(Path.Combine(folderPath, Textures[i]));
                    pathologyData.ModifiedHeight[i] = ImageUtils.DeserializeImageToRenderTexture(Path.Combine(folderPath, HeightTextures[i]));
                }

                pathologyData.Melanoma = new List<Melanoma>();
                foreach (var mela in Melanoma)
                {
                    pathologyData.Melanoma.Add(mela.Deserialize(folderPath));
                }

                pathologyData.Naevis = new List<Naevi>();
                foreach (var naevi in Naevi)
                {
                    pathologyData.Naevis.Add(naevi.Deserialize(folderPath));
                }

                pathologyData.NaeviEnabled = NaeviEnabled;
                pathologyData.MelanomaEnabled = MelanomaEnabled;

                return pathologyData;
            }
        }
    }
}