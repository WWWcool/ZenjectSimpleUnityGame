using UnityEngine;
using Zenject;

public class BottomController : MonoBehaviour
{
    private GameController _gameController;

    [Inject]
    public void Construct(GameController gameController)
    {
        _gameController = gameController;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _gameController.ReduceLife();
    }
}
