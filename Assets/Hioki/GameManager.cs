using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    /// <summary>�R�C���i�X�R�A�j</summary>
    [SerializeField]int _coine = 0;
    /// <summary>�Q�[�����̃^�C�}�[</summary>
    [SerializeField]float _timer = 0;
    /// <summary>�Q�[���X�e�[�g</summary>
    [SerializeField]GameState _state = GameState.Idle;

    /// <summary>�Q�[���X�e�[�g�̃v���p�e�B</summary>
    public GameState State { get { return _state; } set { _state = value; } }

    /// <summary>�R�C���̃v���p�e�B</summary>
    public int Coine { get { return _coine; } }

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
