using TMPro;
using UnityEngine;

namespace Assets.Scripts.Grid
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TextMeshPro debugText;
        [SerializeField] private TileType type;

        public TileType Type => type;
        public Vector2 Position { get; private set; }

        public void Initialize(Vector2 position)
        {
            Position = position;
            name = $"({position.x}/{position.y})";
            transform.position = position;
            debugText.text = name;
        }
    }
}