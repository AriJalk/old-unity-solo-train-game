using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class MaterialManager
    {
        private const string MATERIALS_PATH = "Materials/";

        private readonly Dictionary<Enums.GameColor, Material> _material_dictionary;
        public MaterialManager()
        {
            _material_dictionary = new Dictionary<Enums.GameColor, Material>();
        }

        public void LoadColorMaterials()
        {
            foreach (Enums.GameColor color in Enum.GetValues(typeof(Enums.GameColor)))
            {
                Material material = Resources.Load<Material>(MATERIALS_PATH + color + "Material");
                if (material != null)
                    _material_dictionary.Add(color, material);
                else
                    Debug.LogError(color + " Material not found");
            }
        }

        public Material GetColorMaterial(Enums.GameColor color)
        {
            Material material = _material_dictionary[color];
            if(material != null)
                return material;
            Debug.LogError(color + " Material not found");
            return null;
        }

    }
}