using UnityEngine;

public class SineWave : ProcessingLite.GP21
{
    [SerializeField]
    float xspacing = 0.18f;
    float direction = 0.0f;
    [SerializeField]
    float amplitude = 1.4f;
    [SerializeField]
    float period = 20f;
    float dx;
    float[] yvaluesSin;
    float[] yvaluesCos;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        dx = (Mathf.Pow(Mathf.PI, 2) / period) * xspacing;
        yvaluesSin = new float[100];
        yvaluesCos = new float[100];
    }

    void Update()
    {
        Background(0);
        CalculateWave();
        RenderWave();
    }

    void CalculateWave()
    {
        direction += 0.1f;

        float x = direction;
        for (int i = 0; i < yvaluesSin.Length; i++)
        {
            yvaluesSin[i] = Mathf.Sin(x) * amplitude;
            x += dx;
        }

        for (int i = 0; i < yvaluesCos.Length; i++)
        {
            yvaluesCos[i] = Mathf.Cos(x) * amplitude;
            x += dx;
        }
    }

    void RenderWave()
    {
        NoStroke();
        Fill(255,0,0);

        for (int i = 0; i < yvaluesSin.Length; i++)
        {
            Ellipse(i * xspacing, Height / 2 + yvaluesSin[i], 0.1f, 0.1f);
        }

        Fill(126,122,133);

        for (int i = 0; i < yvaluesCos.Length; i++)
        {
            Ellipse(i * xspacing, Height / 2 + yvaluesCos[i], 0.1f, 0.1f);
        }
    }
}