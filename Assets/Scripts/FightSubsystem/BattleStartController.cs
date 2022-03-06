public class BattleStartController: BaseController
{
    private readonly BattleStartView _battleStartView;
    private readonly ProfilePlayer _profilePlayer;

    public BattleStartController(BattleStartView battleStartView, ProfilePlayer profilePlayer)
    {
        _battleStartView = battleStartView;
        _profilePlayer = profilePlayer;
    }

    public void StartBattle()
    {
        _profilePlayer.CurrentState.Value = Profile.GameState.Fight;
    }
}

