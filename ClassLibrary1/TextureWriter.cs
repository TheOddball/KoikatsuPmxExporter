using System;
using System.IO;
using UnityEngine;

namespace PmxLib
{
	// Token: 0x02000031 RID: 49
	public class TextureWriter
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0001504C File Offset: 0x0001324C
		public static Texture2D Render2Texture2D(RenderTexture renderTexture)
		{
			int width = renderTexture.width;
			int height = renderTexture.height;
			Texture2D texture2D = new Texture2D(width, height, 5, false);
			RenderTexture.active = renderTexture;
			texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000150A4 File Offset: 0x000132A4
		public static void WriteTexture2D(string path, Texture tex)
		{
			Texture2D texture2D = null;
			bool flag = tex is RenderTexture;
			if (flag)
			{
				texture2D = TextureWriter.Render2Texture2D(tex as RenderTexture);
			}
			else
			{
				texture2D = (tex as Texture2D);
			}
			Texture2D texture2D2 = new Texture2D(texture2D.width, texture2D.height, 5, false);
			try
			{
				Color[] pixels = texture2D.GetPixels();
				texture2D2.SetPixels(pixels);
				byte[] bytes = texture2D2.EncodeToPNG();
				File.WriteAllBytes(path, bytes);
			}
			catch (Exception ex)
			{
				texture2D.filterMode = 0;
				RenderTexture temporary = RenderTexture.GetTemporary(texture2D.width, texture2D.height);
				temporary.filterMode = 0;
				RenderTexture.active = temporary;
				Graphics.Blit(texture2D, temporary);
				Texture2D texture2D3 = new Texture2D(texture2D.width, texture2D.height);
				texture2D3.ReadPixels(new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), 0, 0);
				texture2D3.Apply();
				RenderTexture.active = null;
				texture2D = texture2D3;
				Color[] pixels = texture2D.GetPixels();
				texture2D2.SetPixels(pixels);
				byte[] bytes2 = texture2D2.EncodeToPNG();
				File.WriteAllBytes(path, bytes2);
			}
		}
	}
}
