namespace Euphrates
{
    public interface IState
	{
		void OnEnter();
		void Tick(float deltaTime);
		void OnExit();
	}
}
