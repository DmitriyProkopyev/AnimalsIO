using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private UnityEvent _playButtonClicked;

    [SerializeField] private PlayerInfo[] _players;
    [SerializeField] private Text[] _texts;

    [SerializeField] private Button _leftArrow;
    [SerializeField] private Button _rightArrow;
    [SerializeField] private Button _play;

    private PlayerCamera _camera;
    
    private void OnEnable()
    {
        _camera = Camera.main.GetComponent<PlayerCamera>();

        _leftArrow.onClick.AddListener(OnArrowClicked);
        _rightArrow.onClick.AddListener(OnArrowClicked);
        _play.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnDisable()
    {
        _leftArrow.onClick.RemoveListener(OnArrowClicked);
        _rightArrow.onClick.RemoveListener(OnArrowClicked);
        _play.onClick.RemoveListener(OnPlayButtonClicked);
    }

    private void OnArrowClicked()
    {
        foreach (var player in _players)
            player.gameObject.SetActive(!player.gameObject.activeSelf);

        foreach (var text in _texts)
            text.gameObject.SetActive(!text.gameObject.activeSelf);
    }

    private void OnPlayButtonClicked()
    {
        _playButtonClicked.Invoke();

        var active = _players.First(player => player.gameObject.activeSelf);
        _camera.Initialize(active);
    }
}
