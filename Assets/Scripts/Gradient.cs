using System.Collections.Generic;
using UnityEngine;


namespace UnityEngine.UI
{
    [ExecuteInEditMode]
    [AddComponentMenu("UI/Effects/gradient", 14)]
    public class Gradient : BaseVertexEffect
    {
        [SerializeField]
        private Color col_1 = new Color(0f, 0f, 0f, 1f);
        [SerializeField]
        private Color col_2 = new Color(255f, 255f, 0f, 1f);

        private Text text;

        [RangeAttribute(-1, 1)]
        public float _Scale = 1f;

        public bool Horizontal = false;

        public Color color1
        {
            get { return col_1; }
            set
            {
                col_1 = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        public Color color2
        {
            get { return col_2; }
            set
            {
                col_2 = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }


        protected void ApplyGradient(List<UIVertex> verts)
        {
            for (int i = 0; i < verts.Count; ++i)
            {
                var vt = verts[i];
                if (!Horizontal)
                {
                    vt.color = Color32.Lerp(col_2, col_1, vt.position.y * (_Scale / 10));
                }
                else
                {
                    vt.color = Color32.Lerp(col_2, col_1, vt.position.x * (_Scale / 10));
                }
                verts[i] = vt;
            }
        }

        public override void ModifyVertices(List<UIVertex> verts)
        {
            if (!IsActive())
                return;

            ApplyGradient(verts);
        }
    }
}