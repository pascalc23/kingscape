using TMPro;
using UnityEngine;

namespace Assets.Scripts.Grid
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TextMeshPro debugText;
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