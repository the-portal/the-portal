using UnityEngine;
using System.Collections;

public class SpriteToTexture2D : MonoBehaviour
{
	// Update is called once per frame
	public static Texture2D Convert (Sprite sprite)
	{
		var texture2D = new Texture2D( (int)sprite.rect.width, (int)sprite.rect.height );
		var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x, 
											 (int)sprite.textureRect.y, 
											 (int)sprite.textureRect.width, 
											 (int)sprite.textureRect.height );
		texture2D.SetPixels( pixels );
		texture2D.Apply();

		return texture2D; 
	}
}

