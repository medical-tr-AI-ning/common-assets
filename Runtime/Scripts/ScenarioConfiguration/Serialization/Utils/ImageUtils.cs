using System.IO;
using UnityEngine;

namespace Runtime.Utils
{
    public class ImageUtils
    {
        public static byte[] SerializeImage(RenderTexture rt)
        {
            return SerializeImage(Texture2DFromRenderTexture(rt));
        }

        public static byte[] SerializeImage(Texture2D texture)
        {
            return ImageConversion.EncodeToPNG(texture);
        }

        public static Texture2D Texture2DFromRenderTexture(RenderTexture rt)
        {
            // Speichere die aktuell aktive RenderTexture
            RenderTexture currentRT = RenderTexture.active;

            // Setze die aktive RenderTexture auf die übergebene RenderTexture
            RenderTexture.active = rt;

            // Erstelle eine neue Texture2D mit den gleichen Abmessungen wie die RenderTexture
            Texture2D texture2D = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);

            // Kopiere die Pixel von der aktiven RenderTexture in die neue Texture2D
            texture2D.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);

            // Wende die Änderungen an der Texture2D an
            texture2D.Apply();

            // Setze die aktive RenderTexture wieder auf die ursprüngliche zurück
            RenderTexture.active = currentRT;

            return texture2D;
        }
        
        public static Texture2D DeserializeImageToTexture2D(string filePath)
        {
            // Lade die Bilddatei als byte-Array
            byte[] imageData = File.ReadAllBytes(filePath);
            
            // Erstelle eine neue leere Texture2D
            Texture2D texture = new Texture2D(2, 2); // Größe 2x2 ist nur Platzhalter, wird automatisch angepasst
            
            // Lade die Bilddaten in die Texture2D
            ImageConversion.LoadImage(texture, imageData);
            
            return texture;
        }

        public static RenderTexture DeserializeImageToRenderTexture(string filePath)
        {
            // Verwende DeserializeImageToTexture2D, um die Texture2D zu laden
            Texture2D texture = DeserializeImageToTexture2D(filePath);

            // Erstelle eine neue RenderTexture mit den gleichen Abmessungen wie die Texture2D
            RenderTexture rt = new RenderTexture(texture.width, texture.height, 0);

            // Setze die aktive RenderTexture
            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = rt;

            // Kopiere die Pixel von der Texture2D in die RenderTexture
            Graphics.Blit(texture, rt);

            // Setze die ursprüngliche RenderTexture wieder aktiv
            RenderTexture.active = currentRT;

            return rt;
        }

    }
}