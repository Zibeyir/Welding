using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    Camera cam;
    Color32[] brushColors;

    public MeshRenderer meshRenderer;//Boyayacağımız obje
    public Texture2D brush;//Fırça texture'ı
    public Vector2Int textureArea;//x:1024, y:1024
    Texture2D texture;

    private void Start()
    {
        texture = new Texture2D(textureArea.x, textureArea.y, TextureFormat.ARGB32, false);
        meshRenderer.material.mainTexture = texture;
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) //Sol tıka basılı tuttukça boyayacak
        {
            RaycastHit hitInfo;
            //cam, kullandığımız kamera (Camera classı)
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                //Debug.Log(hitInfo.textureCoord); //0-1 arasında UV koordinatı
                Paint(hitInfo.textureCoord);
            }
        }
    }

    private void Paint(Vector2 coordinate)
    {
        coordinate.x *= texture.width;//0-1 değerini tam nokta piksellere çevirdik
        coordinate.y *= texture.height;//Yani 0-1024 yaptık
        Color32[] textureC32 = texture.GetPixels32();
        Color32[] brushC32 = brush.GetPixels32();

        //Fırçanın ortasının koordinatları, kare fırça ise float da olabilir
        Vector2Int halfBrush = new Vector2Int(brush.width / 2, brush.height / 2);

        /*for (int x = 0; x < brush.width; x++)
        {
            int xPos = x - halfBrush.x + (int)coordinate.x;
            for (int y = 0; y < brush.height; y++)
            {
                int yPos = y - halfBrush.y + (int)coordinate.y;
                if (brush.GetPixel(x, y).a != 0)
                {
                    if (texture.GetPixel(xPos, yPos).r > brush.GetPixel(x, y).r)
                    {
                        texture.SetPixel(xPos, yPos, brush.GetPixel(x, y));
                    }
                }
            }
        }*/

        for (int x = 0; x < brush.width; x++)
        {
            int xPos = x - halfBrush.x + (int)coordinate.x;
            if (xPos < 0 || xPos >= texture.width)
                continue;
            for (int y = 0; y < brush.height; y++)
            {
                int yPos = y - halfBrush.y + (int)coordinate.y;
                if (yPos < 0 || yPos >= texture.height)
                    continue;
                if (brushC32[x + (y * brush.width)].a > 0f)
                {
                    int tPos =
                        xPos + //X (U) posizyonu
                        (texture.width * yPos); //Y (V) Posizyonu
                    textureC32[tPos] = brushC32[x + (y * brush.width)];

                    if (brushC32[x + (y * brush.width)].r < textureC32[tPos].r)
                        textureC32[tPos] = brushC32[x + (y * brush.width)];
                }
            }
        }

        texture.SetPixels32(textureC32);//Array'i set ettik
        texture.Apply();//Değişikliklerin uygulanması için
    }
}


/*for (int x = 0; x < brush.width; x++)
{
    int xPos = x - halfBrush.x + (int)coordinate.x;
    if (xPos < 0 || xPos >= textureArea.x)
        continue;
    for (int y = 0; y < brush.height; y++)
    {
        int yPos = y - halfBrush.y + (int)coordinate.y;
        if (yPos < 0 || yPos >= textureArea.y)
            continue;
        if (brushC32[x + (y * brush.width)].a == 0)
            continue;

        int texPoint = xPos + (textureArea.x * yPos);
        if(brushC32[x + (y * brush.width)].r < textureC32[texPoint].r)
            textureC32[texPoint] = brushC32[x + (y * brush.width)];
    }
}*/