using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    public class RedirectPawn : MovingGridItem, IInteractable
    {
        [SerializeField] private Vector2Int redirectToVector;

        public override void OnFinish()
        {
            base.OnFinish();
            AnimateDestroy();
        }

        public void Interact(GridItem source)
        {
            if (source is MovingGridItem)
            {
                Debug.Log($"'{source.name}' just ran into me - I'm changing his direction to {redirectToVector}");
                ((MovingGridItem)source).ChangeDirection(redirectToVector);
            }
        }
    }
}