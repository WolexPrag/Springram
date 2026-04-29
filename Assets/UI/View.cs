using UnityEngine;
using UnityEngine.UI;
using R3;
public class View : MonoBehaviour
{
    [SerializeField] private Button _play;
    public Observable<Unit> OnPlayClick { get; private set; }

    public void Awake()
    {
       OnPlayClick = _play.OnClickAsObservable();
    }


}