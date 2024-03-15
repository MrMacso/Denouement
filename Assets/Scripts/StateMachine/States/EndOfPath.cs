
internal class EndOfPath : IState
{
    private readonly FollowPathAI _puppet;

    public EndOfPath(FollowPathAI puppet)
    {
        _puppet = puppet;
    }
    public void Tick() { }

    public void OnEnter() { }

    public void OnExit() => _puppet.SetActive(false);

}
