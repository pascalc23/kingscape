using UnityEngine;

namespace Assets.Scripts.Game.GridObjects.Pawns
{
    public class RedirectPawn : MovingGridItem, IInteractable
    {
        [SerializeField] private Vector2Int redirectToVector;

        public override void OnFinish()
        {
            base.OnFinish();
            AnimateDestroy();
        }

        public void Interact(GridItem target)
        {
            if (target is MovingGridItem)
            {
                Debug.Log($"'{target.name}' just ran into me - I'm changing his direction to {redirectToVector}");
                ((MovingGridItem)target).ChangeDirection(redirectToVector);
            }
        }
    }
}