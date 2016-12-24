using UnityEngine;
using System.Collections;

public class GLMyDraw : MonoBehaviour
{
    public Material rectMat;
    public bool m_draw = true;
    public static GLMyDraw Instance;
    // Use this for initialization
    void Start()
    {
        Instance = this;
        rectMat = new Material("Shader \"Lines/Colored Blended\" {" +
            "SubShader { Pass { " +
            "    Blend SrcAlpha OneMinusSrcAlpha " +
            "    ZWrite Off Cull Off Fog { Mode Off } " +
            "    BindChannels {" +
            "      Bind \"vertex\", vertex Bind \"color\", color }" +
            "} } }");//生成画线的材质    
        rectMat.hideFlags = HideFlags.HideAndDontSave;
        rectMat.shader.hideFlags = HideFlags.HideAndDontSave;
    }


    const int W = 20;
    const int H = 13;
    const int CELL = 50;
    const int X_BEGIN = -640 + 100;
    const int Y_BEGIN = -360 + 30;
    const float Z = 0;
    //Color EmptyColor = new Color(0, 0, 0, 0);
    //Camera c;

    /*void OnPostRender()
    {
        GL.Clear(true, true, Color.black);

        //GL.Clear(true, true, EmptyColor);
        //GL.ClearWithSkybox(true,c);
        GL.MultMatrix(transform.localToWorldMatrix);

    }*/

    void drawGrid()
    {
        //GL.Clear(true, true, Color.black);
        Color clr = Color.green;
        clr.a = 0.8f;

        rectMat.SetPass(0);
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
//         GL.Begin(GL.QUADS);
//         GL.Color(clr);
//         GL.Vertex(new Vector3(X_BEGIN + W * CELL, Y_BEGIN + H * CELL, Z));
//         GL.Vertex(new Vector3(X_BEGIN + W * CELL, Y_BEGIN, Z));
//         GL.Vertex(new Vector3(X_BEGIN, Y_BEGIN, Z));
//         GL.Vertex(new Vector3(X_BEGIN, Y_BEGIN + H * CELL, Z));
//         GL.End();
        for (int i = 0; i <= W; i++)
        {
            //  |
            GL.Begin(GL.LINES);
            if (i % 5 == 0)
            {
                GL.Color(Color.white);
            }
            else
            {
                GL.Color(Color.gray);
            }
            GL.Vertex(new Vector3(X_BEGIN + i * CELL, Y_BEGIN, Z));
            GL.Vertex(new Vector3(X_BEGIN + i * CELL, Y_BEGIN + H * CELL, Z));
            GL.End();
        }

        for (int i = 0; i <= H; i++)
        {
            //  -
            GL.Begin(GL.LINES);
            if (i % 5 == 0)
            {
                GL.Color(Color.white);
            }
            else
            {
                GL.Color(Color.gray);
            }
            GL.Vertex(new Vector3(X_BEGIN, Y_BEGIN + i * CELL, Z));
            GL.Vertex(new Vector3(X_BEGIN + W * CELL, Y_BEGIN + i * CELL, Z));
            GL.End();
        }
        GL.PopMatrix();
    }

    void OnGUI()
    {
        if (m_draw)
        {
            drawGrid();
        }
    }
}
