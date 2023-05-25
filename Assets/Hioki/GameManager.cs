using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    /// <summary>コイン（スコア）</summary>
    [SerializeField]int _coine = 0;
    /// <summary>ゲーム中のタイマー</summary>
    [SerializeField]float _timer = 0;
    /// <summary>ゲームステート</summary>
    [SerializeField]GameState _state = GameState.Idle;

    /// <summary>ゲームステートのプロパティ</summary>
    public GameState State { get { return _state; } set { _state = value; } }

    /// <summary>コインのプロパティ</summary>
    public int Coine { get { return _coine; } }

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
        if(_state == GameState.Game)
        _timer += Time.deltaTime;

    }

    public void AddCoine()
    {
        _coine++;
    }

}

public enum GameState
{
    Idle,
    Game,
    Goal,
}
