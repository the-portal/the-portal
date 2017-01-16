using UnityEngine;
using System.Collections;

public class LoadImage : MonoBehaviour
{
	public static IEnumerator UpLoadImageFromPath (string imagePath,string table,string where,string sceneToLoad)
	{
		WWW www = new WWW(imagePath);
        yield return www;
        Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
        //LoadImageIntoTexture compresses JPGs by DXT1 and PNGs by DXT5     
        www.LoadImageIntoTexture(texTmp);
		yield return texTmp;
//		ImageUploader.UploadPNG(texTmp,table,where,sceneToLoad);
	}
}

