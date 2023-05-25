using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int _maxCoin = 5;
    /// <summary>�R�C���i�X�R�A�j</summary>
    [SerializeField] int _coin = 0;
    /// <summary>���C�t</summary>
    [SerializeField] int _life = 5;
    /// <summary>�^�C�����~�b�g</summary>
    [SerializeField] float _timeLimit = 60f;
    /// <summary>�Q�[�����̃^�C�}�[</summary>
    [SerializeField] float _timer = 0;
    /// <summary>�Q�[���X�e�[�g</summary>
    [SerializeField] GameState _state = GameState.Idle;
    [SerializeField] GameObject _startUI = default;
    [SerializeField] GameObject _gamevoerUI = default;
    [SerializeField] GameObject _clearUI = default;

    /// <summary>�Q�[���X�e�[�g�̃v���p�e�B</summary>
    public GameState State { get { return _state; } set { _state = value; } }

    /// <summary>���C�t�̃v���p�e�B</summary>
    public int Life { get { return _life; } }

    /// <summary>�R�C���̃v���p�e�B</summary>
    public int Coin { get { return _coin; } }

    /// <summary>�^�C�}�[�̃v���p�e�B</summary>
    public float Timar { get { return _timer; } }

    #region�@�V���O���g��
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                SetupInstance();
            }

            return instance;
        }
    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);
        _state = GameState.Idle;
        _startUI.SetActive(true);
        _gamevoerUI.SetActive(false);
        _clearUI.SetActive(false);
    }

    static void SetupInstance()
    {
        instance = FindObjectOfType<GameManager>();

        if (!instance)
        {
            GameObject go = new GameObject();
            instance = go.AddComponent<GameManager>();
            go.name = instance.GetType().Name;
            //DontDestroyOnLoad(go);
        }
    }
    #endregion

    //private void Start()
    //{
    //    _state = GameState.Idle;
    //    _startUI.SetActive(true);
    //    _gamevoerUI.SetActive(false);
    //    _clearUI.SetActive(false);
    //}

    private void Update()
    {

        switch (_state)
        {

            case GameState.GameOver:
                return;

            case GameState.Goal:
                return;

            case GameState.Game:
                _timer -= Time.deltaTime;

                if (_timer <= 0)
                {
                    _gamevoerUI.SetActive(true);
                    _state = GameState.GameOver;
                }

                break;

            case GameState.Idle:

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _state = GameState.Game;
                    _timer = _timeLimit;
                    _startUI.SetActive(false);
                }

                break;

            default:
                break;

        }

    }

    public void AddCoine()
    {
        _coin++;
    }

    public void Damage(int damage)
    {
        _life -= damage;
        if (_life <= 0)
        {
            _life = 0;
            _gamevoerUI.SetActive(true);
            _state = GameState.GameOver;
        }
    }

    public void PlayerGoal()
    {

        if(_coin == _maxCoin)
        {
            _state = GameState.Goal;
            _clearUI.SetActive(true);
        }
        
    }

}

public enum GameState
{
    Idle,
    Game,
    GameOver,
    Goal,
}
