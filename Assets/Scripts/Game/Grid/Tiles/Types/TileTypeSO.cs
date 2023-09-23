using UnityEngine;

namespace Assets.Scripts.Grid.Tiles.Types
{
    [CreateAssetMenu(fileName = "Tile Type", menuName = "Kingscape/Tile Type", order = 1)]
    public class TileTypeSO : ScriptableObject
    {
        [Tooltip("Name of the tile type")]
        public string title;
        [Tooltip("Can pawns and NPCs walk on it?")]
        public bool isWalkable;
        [Tooltip("Material to use for visualizing the tile")]
        public Material material;
    }
}