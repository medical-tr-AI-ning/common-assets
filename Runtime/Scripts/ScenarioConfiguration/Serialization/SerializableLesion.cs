using System;
using System.IO;
using medicaltraining.assetstore.ScenarioConfiguration.Serialization.Utils;
using Runtime.Utils;
using UnityEngine;

public class SerializableLesion
{
    public float[] TextureCoord;
    public string Shape;
    public float Size;

    
    //Empty Constructor for Newtonsoft Json
    public SerializableLesion()
    {
    }
    public SerializableLesion(Lesion lesion, string folderPath, IFileWriter fileWriter)
    {
        Debug.Log($"Lesion {lesion}");
        TextureCoord = new float[2] {lesion.TextureCoord.x, lesion.TextureCoord.y};
        Shape = Path.Combine(GetSubfolderPath(), Guid.NewGuid().ToString());
        string targetPath = Path.Combine(folderPath, Shape);
        fileWriter.WriteFile(targetPath, lesion.Shape);
        Size = lesion.Size;
    }

    public Lesion Deserialize(string folderPath)
    {
        Lesion lesion = new Lesion();
        Deserialize(lesion, folderPath);
        return lesion;
    }

    public void Deserialize(Lesion lesion, string folderPath)
    {
        lesion.TextureCoord = new Vector2(TextureCoord[0], TextureCoord[1]);
        var filePath = Path.Combine(folderPath, Shape);
        lesion.Shape = ImageUtils.DeserializeImageToRenderTexture(filePath);
        lesion.Size = Size;
    }

    public virtual string GetSubfolderPath() => String.Empty;
}
