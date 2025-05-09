using System.IO;

namespace medicaltraining.assetstore.ScenarioConfiguration.Serialization
{
    public static class FolderStructure
    {
        public const string Naevi = "Naevi";
        public const string Melanoma = "Melanoma";

        public static string PathologyVariant(int variant) => $"Case {variant}";
        public const string PathologyPreset = "PathologyPreset";
        public const string SkinTexture = "SkinTexture";
        public const string HeightTexture = "HeightTexture";

        public static string NaeviHighRes = Path.Combine("Assets","Textures","NaeviHighRes");
    }
}