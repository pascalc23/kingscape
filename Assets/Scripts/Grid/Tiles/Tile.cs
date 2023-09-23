using Assets.Scripts.Grid.Tiles.Types;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Grid.Tiles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TextMeshPro debugText;
        [SerializeField] private TileTypeSO type;
        [SerializeField] private MeshRenderer renderer;

        private GridManager _gridManager;
        private Vector2Int _coordinates;

        private void Awake()
        {
            _gridManager = GridManager.Instance;
            Initialize();
        }

        private void OnValidate()
        {
            UpdateName();
            UpdateMaterial();
        }

        private void Start()
        {
            _gridManager.RegisterTile(_coordinates, this);

            UpdateName();
            UpdateMaterial();
        }

        private void Initialize()
        {
            _coordinates = GetCoordinatesFromPosition();
        }

        private Vector2Int GetCoordinatesFromPosition()
        {
            var position = transform.position;
            return new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
        }

        private void UpdateMaterial()
        {
            renderer.material = type.material;
        }

        private void UpdateName()
        {
            _coordinates = GetCoordinatesFromPosition();
            string tileName = $"({_coordinates.x},{_coordinates.y})";
            name = tileName;
            debugText.text = tileName;
        }
    }
}