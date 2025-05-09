using medicaltraining.assetstore.ScenarioConfiguration.Serialization;
using medicaltraining.assetstore.ScenarioConfiguration.Serialization.Utils;
using System.IO;
using System;
using UnityEngine;
using System.Collections.Generic;

public class SerializableNaevi : SerializableLesion
{
    public float Spread;
    public float Elevation;
    public List<SerializableHighResNaevi> HighRes = new List<SerializableHighResNaevi>();

    public SerializableNaevi()
    {
        
    }
    public SerializableNaevi(Naevi naevi, string folderPath, IFileWriter fileWriter) : base(naevi, folderPath, fileWriter)
    {
        Spread = naevi.Spread;
        Elevation = naevi.Elevation;

        foreach(Naevi.HighResNaevi highResNaevi in naevi.HighRes)
        {
            HighRes.Add(new SerializableHighResNaevi(folderPath, highResNaevi));
        }
    }

    public new Naevi Deserialize(string folderPath)
    {
        Naevi naevi = new Naevi();
        base.Deserialize(naevi, folderPath);
        naevi.Spread = Spread;
        naevi.Elevation = Elevation;

        naevi.HighRes = new List<Naevi.HighResNaevi>();
        foreach(SerializableHighResNaevi highres in HighRes)
        {
            naevi.HighRes.Add(highres.Deserialize(folderPath));
        }
        Debug.Log("naevi high res count " + naevi.HighRes.Count);
        return naevi;
    }
    
    public override string GetSubfolderPath() => FolderStructure.Naevi;

    public class SerializableHighResNaevi
    {
        public float[] Position;
        public string Texture;
        
        public SerializableHighResNaevi() {}
        public SerializableHighResNaevi(string folderPath, Naevi.HighResNaevi highResNaevi)
        {
            Position = new float[] { highResNaevi.Position.x, highResNaevi.Position.y};
            Texture = Path.Combine(GetSubfolderPath(), Guid.NewGuid().ToString());
            string targetPath = Path.Combine(folderPath, Texture);

            FileInfo file = new FileInfo(targetPath);
            file.Directory.Create(); // If the directory already exists, this method does nothing.

            Debug.Log(Path.Combine(folderPath,GetSubfolderPath()));

            File.Copy(Path.Combine(FolderStructure.NaeviHighRes, highResNaevi.Texture + ".png"), targetPath, true);
        }

        public Naevi.HighResNaevi Deserialize(string folderPath)
        {
            Naevi.HighResNaevi highRes = new Naevi.HighResNaevi();
            highRes.Position = new Vector2( Position[0], Position[1] );
            highRes.Texture = Texture;
            return highRes;
        }

        public string GetSubfolderPath() => FolderStructure.Naevi;
    }
}
