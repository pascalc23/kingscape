using Assets.Scripts.Game.GridObjects;
using Assets.Scripts.Grid.Tiles.Types;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.Game.Grid.Tiles
{
    public class Tile : GridItem
    {
        [SerializeField] private TextMeshPro debugText;
        [SerializeField] private TileTypeSO type;
        [FormerlySerializedAs("renderer")] [SerializeField]
        private MeshRenderer meshRenderer;

        protected override void OnStart()
        {
            gridManager.RegisterTile(this);
        }

        protected override void Initialize()
        {
            UpdateName();
            UpdateMaterial();
        }

        private void UpdateMaterial()
        {
            meshRenderer.material = type.material;
        }

        private void UpdateName()
        {
            string tileName = $"({Coordinates.x},{Coordinates.y})";
            name = tileName;
            debugText.text = tileName;
        }
    }
}