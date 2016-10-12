using UnityEngine;
using System.Collections;

public class DayLight : MonoBehaviour
{
    public Color tintColor = Color.white;

    private Material mat;

    void Awake()
    {
        mat = new Material(Shader.Find("Hidden/WholeScreenTint"));
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        mat.color = tintColor;
        Graphics.Blit(src, dest, mat);
    }
}
