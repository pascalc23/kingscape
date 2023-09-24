namespace Game.GridObjects.Pawns
{
    public class PusherPawn : MovingGridItem
    {
        protected override void OnHalt()
        {
            base.OnHalt();

            // Check if item ahead is pushable - if so, push it and die
            if (gridManager.TryPush(this, GetNextCoordinate(), forwardVector))
            {
                DestroySelf();
            }
        }
    }
}