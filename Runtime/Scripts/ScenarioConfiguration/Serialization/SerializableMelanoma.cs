using System;
using System.IO;
using medicaltraining.assetstore.ScenarioConfiguration.Serialization;
using medicaltraining.assetstore.ScenarioConfiguration.Serialization.Utils;
using Runtime.Utils;
using UnityEngine;

public class SerializableMelanoma : SerializableLesion
{
    public float Spreading;
    public float Limitation;
    public float Elevation;
    public float Brightness;
    public float Contrast;
    public float[] ColorPosition;
    public string BaseShape;
    public float[] Position;
    public string Placement;
    public float SizeUI;


    public SerializableMelanoma()
    {
    }

    public SerializableMelanoma(Melanoma melanoma, string folderPath, IFileWriter fileWriter) : base(melanoma, folderPath, fileWriter)
    {
        Spreading = melanoma.Spreading;
        Limitation = melanoma.Limitation;
        Elevation = melanoma.Elevation;
        Brightness = melanoma.Brightness;
        Contrast = melanoma.Contrast;
        ColorPosition = new float[2] { melanoma.ColorPosition.x, melanoma.ColorPosition.y };
        Placement = melanoma.Placement;
        Position = new float[3] { melanoma.Position.x, melanoma.Position.y, melanoma.Position.z };

        BaseShape = Path.Combine(GetSubfolderPath(), Guid.NewGuid().ToString());
        string targetPath = Path.Combine(folderPath, BaseShape);
        fileWriter.WriteFile(targetPath, melanoma.Shape);
        Size = melanoma.Size;
        SizeUI = melanoma.SizeUI;
    }

    public new Melanoma Deserialize(string folderPath)
    {
        Melanoma melanoma = new Melanoma();
        base.Deserialize(melanoma, folderPath);
        melanoma.Spreading = Spreading;
        melanoma.Limitation = Limitation;
        melanoma.Elevation = Elevation;
        melanoma.Brightness = Brightness;
        melanoma.Contrast = Contrast;
        melanoma.ColorPosition = new Vector2(ColorPosition[0], ColorPosition[1]);
        melanoma.Placement = Placement;
        melanoma.Position = new Vector3(Position[0], Position[1], Position[2]);
        melanoma.SizeUI = SizeUI;
        var filePath = Path.Combine(folderPath, BaseShape);
        melanoma.BaseShape = ImageUtils.DeserializeImageToTexture2D(filePath);
        return melanoma;
    }

    public override string GetSubfolderPath() => FolderStructure.Melanoma;
}