using System.Collections.Generic;
using R3;

public class Presenter
{
    private List<Command> _commands = new();
    private CompositeDisposable _disposable;
    protected View _view;
    public void Init(View view)
    {
        _view = view;
        _view.OnPlayClick.Subscribe(Play).AddTo(_disposable);
    }
    private void Play(Unit unit)
    {
        for (int i = 0; i < _commands.Count; i++)
        {
            _commands[i].TryExecute();
        }
    }
}