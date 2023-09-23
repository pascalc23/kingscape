using Assets.Scripts.Game.GridObjects;
using Assets.Scripts.Grid.Tiles.Types;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game.Grid.Tiles
{
    public class Tile : GridItemBase
    {
        [SerializeField] private TextMeshPro debugText;
        [SerializeField] private TileTypeSO type;
        [SerializeField] private MeshRenderer meshRenderer;

        public TileTypeSO Type => type;

        private void Awake()
        {
            Coordinates = GetCoordinatesFromWorldPosition();
        }

        protected override void OnStart()
        {
            gridManager.RegisterTile(this);
            UpdateVisuals();
        }

        protected void OnValidate()
        {
            UpdateVisuals();
        }

        public void UpdateVisuals()
        {
            UpdateCoordinates();
            UpdateName();
            UpdateMaterial();
        }

        private void UpdateCoordinates()
        {
            Coordinates = GetCoordinatesFromWorldPosition();
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