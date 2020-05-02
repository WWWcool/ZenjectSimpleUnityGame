using Blocks;
using Common;
using LeaderBoard;
using Racket;
using UI;
using UnityEngine;
using Zenject;

public class GameDataModel
{
    public int levelIndex = 0;
    public int score = 0;
    public int lives = 2;
}

public class GameController : MonoBehaviour
{
    [SerializeField] private RacketController _racket;
    
    private bool _gameStarted = false;
    
    private GameDataModel _gameDataModel;
    private BlockManager _blockManager;
    private InputController _inputController;
    private GameUIController _gameUiController;
    private EndGamePopupController _endGamePopupController;
    private LeaderBoardManager _leaderBoardManager;
    private LeaderBoardController _leaderBoardController;

    [Inject]
    public void Construct(
        BlockManager blockManager,
        InputController inputController,
        GameUIController gameUiController, 
        EndGamePopupController endGamePopupController,
        LeaderBoardManager leaderBoardManager,
        LeaderBoardController leaderBoardController
    )
    {
        _blockManager = blockManager;
        _inputController = inputController;
        _gameUiController = gameUiController;
        _endGamePopupController = endGamePopupController;
        _leaderBoardManager = leaderBoardManager;
        _leaderBoardController = leaderBoardController;
    }

    public void ReduceLife()
    {
        _gameDataModel.lives--;
        UpdateUI();
        OnNewLife();
    }
    
    private void Start()
    {
        _gameDataModel = new GameDataModel();
        OnNewLevel();
        UpdateUI();
        _leaderBoardManager.LoadLB();
    }

    private void OnEnable()
    {
        _inputController.onDrag += OnDrag;
        _inputController.onPointerClick += OnClick;
        _blockManager.onLevelEnd += OnNewLevel;
        _blockManager.onScoreAdd += OnScoreAdd;
        _endGamePopupController.onClick += OnNewHighScore;
    }

    private void OnDisable()
    {
        _inputController.onDrag -= OnDrag;
        _inputController.onPointerClick -= OnClick;
        _blockManager.onLevelEnd -= OnNewLevel;
        _blockManager.onScoreAdd -= OnScoreAdd;
        _endGamePopupController.onClick -= OnNewHighScore;
    }

    private void OnClick()
    {
        if (!_gameStarted)
        {
            _gameStarted = true;
            _racket.RunBall();
        }
    }

    private void OnDrag(Vector2 delta)
    {
        if (_gameStarted)
        {
            _racket.Move(delta);
        }
    }

    private void OnNewLevel()
    {
        _racket.ReInit();
        if (!_blockManager.InitLevel(_gameDataModel.levelIndex++))
        {
            GameOver();
        };
        _gameStarted = false;
    }

    private void OnNewLife()
    {
        if (_gameDataModel.lives <= 0)
        {
            GameOver();
        }
        else
        {
            _racket.ReInit();
            _gameStarted = false;
        }
    }

    private void OnScoreAdd(int score)
    {
        _gameDataModel.score += score;
        UpdateUI();
    }

    private void OnNewHighScore(string playerName)
    {
        _leaderBoardManager.AddNewScore(playerName, _gameDataModel.score);
        _leaderBoardController.Show();
    }

    private void UpdateUI()
    {
        _gameUiController.UpdateUI(_gameDataModel.lives, _gameDataModel.score);
    }
    
    private void GameOver()
    {
        if (_leaderBoardManager.IsScoreHigh(_gameDataModel.score))
        {
            _endGamePopupController.Show(_gameDataModel.score);
        }
        else
        {
            _leaderBoardController.Show();
        }
    }
}
