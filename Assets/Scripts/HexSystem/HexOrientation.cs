using UnityEngine;

namespace HexSystem
{
    public class HexOrientation
    {
        public static HexOrientation PointyLayout
        {
            get
            {
                return new HexOrientation(
                    Mathf.Sqrt(3f), Mathf.Sqrt(3f) / 2f, 0f, 3f / 2f,
                    Mathf.Sqrt(3f) / 3f, -1f / 3f, 0f, 2f / 3f,
                    0.5f);
            }
        }

        public static HexOrientation FlatLayout
        {
            get
            {
                return new HexOrientation(
                    3f / 2f, 0f, Mathf.Sqrt(3f) / 2f, Mathf.Sqrt(3f),
                    2f / 3f, 0f, -1f / 3f, Mathf.Sqrt(3f) / 3f,
                    0f);
            }
        }

        public float F0;
        public float F1;
        public float F2;
        public float F3;
        public float B0;
        public float B1;
        public float B2;
        public float B3;

        public float StartAngle;

        public HexOrientation(float f0, float f1, float f2, float f3, float b0, float b1, float b2, float b3, float startAngle)
        {
            F0 = f0;
            F1 = f1;
            F2 = f2;
            F3 = f3;
            B0 = b0;
            B1 = b1;
            B2 = b2;
            B3 = b3;
            StartAngle = startAngle;
        }
    }
}