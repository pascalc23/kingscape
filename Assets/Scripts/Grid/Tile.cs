using TMPro;
using UnityEngine;

namespace Assets.Scripts.Grid
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TextMeshPro debugText;
        [SerializeField] private TileType type;

        public TileType Type => type;

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
        }

        private void UpdateName()
        {
            _coordinates = GetCoordinatesFromPosition();
            string tileName = $"({_coordinates.x},{_coordinates.y})";
            name = tileName;
            debugText.text = tileName;
        }

        private void Start()
        {
            _gridManager.RegisterTile(_coordinates, this);
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
    }
}