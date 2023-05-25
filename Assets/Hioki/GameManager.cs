using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    /// <summary>コイン（スコア）</summary>
    [SerializeField] int _coin = 0;
    /// <summary>ライフ</summary>
    [SerializeField] int _life = 5;
    /// <summary>ゲーム中のタイマー</summary>
    [SerializeField] float _timer = 0;
    /// <summary>ゲームステート</summary>
    [SerializeField] GameState _state = GameState.Idle;

    /// <summary>ゲームステートのプロパティ</summary>
    public GameState State { get { return _state; } set { _state = value; } }

    /// <summary>ライフのプロパティ</summary>
    public int Life { get { return _life; } }

    /// <summary>コインのプロパティ</summary>
    public int Coin { get { return _coin; } }

    /// <summary>タイマーのプロパティ</summary>
    public float Timar { get { return _timer; } }

    #region　シングルトン
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

        DontDestroyOnLoad(gameObject);
    }

    static void SetupInstance()
    {
        instance = FindObjectOfType<GameManager>();

        if (!instance)
        {
            GameObject go = new GameObject();
            instance = go.AddComponent<GameManager>();
            go.name = instance.GetType().Name;
            DontDestroyOnLoad(go);
        }
    }
    #endregion

    private void Start()
    {
        _state = GameState.Idle;
    }

    private void Update()
    {
        if (_state == GameState.Game)
            _timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _state = GameState.Game;
            _timer = 0;
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
            _state = GameState.GameOver;
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
