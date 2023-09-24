namespace Game.GridObjects
{
    /// <summary>
    /// An inactive grid item is something that has a place on the grid and can be interacted with, but does not actively move or interact itself
    /// </summary>
    public abstract class InactiveGridItem : GridItem
    {
        protected override void OnStart()
        {
            base.OnStart();
            Initialize(GetCoordinatesFromWorldPosition());
        }
    }
}