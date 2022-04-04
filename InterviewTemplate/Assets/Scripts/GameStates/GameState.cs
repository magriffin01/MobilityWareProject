namespace VideoPoker
{
    public class GameState
    {
        protected GameManager gameManager;

        public GameState(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
        
        public virtual void OnStateEntered(){}
        public virtual void OnStateExit(){}
    }
}


