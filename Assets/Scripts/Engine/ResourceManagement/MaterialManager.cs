using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class MaterialManager
    {
        private const string MATERIALS_PATH = "Materials/";

        private readonly Dictionary<Enums.GameColor, Material> _tile_materials;
        private readonly Dictionary<Enums.GameColor, Material> _wood_materials;
        public MaterialManager()
        {
            _tile_materials = new Dictionary<Enums.GameColor, Material>();
            _wood_materials = new Dictionary<Enums.GameColor, Material>();
        }

        public void LoadColorMaterials()
        {
            foreach (Enums.GameColor color in Enum.GetValues(typeof(Enums.GameColor)))
            {
                Material material = Resources.Load<Material>(MATERIALS_PATH + color + "Material");
                if (material != null)
                    _tile_materials.Add(color, material);
                else
                    Debug.LogError(color + " Material not found");
                material = Resources.Load<Material>(MATERIALS_PATH + color + "WoodMaterial");
                if (material != null)
                    _wood_materials.Add(color, material);
                else
                    Debug.LogError(color + " Wood Material not found");

            }
        }

        public Material GetColorMaterial(Enums.GameColor color)
        {
            Material material = _tile_materials[color];
            if(material != null)
                return material;            Debug.LogError(color + " Material not found");
            return null;

        }

        public Material GetWoodColorMaterial(Enums.GameColor color)
        {
            Material material = _wood_materials[color];
            if (material != null)
                return material;
            Debug.LogError(color + " Material not found");
            return null;
        }


    }
}