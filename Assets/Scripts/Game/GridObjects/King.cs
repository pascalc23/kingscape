namespace Assets.Scripts.Game.GridObjects
{
    public class King : MovingGridItem
    {
        protected override void OnHalt()
        {
            base.OnHalt();
            GameManager.Instance.TriggerLoseCondition();
        }

        public override void OnFinish()
        {
            base.OnFinish();
            GameManager.Instance.TriggerWinCondition();
        }
    }
}