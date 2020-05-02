using System;
using UnityEngine;

namespace Racket
{
    public class RacketController : MonoBehaviour
    {
        [SerializeField] private Transform _ball;
        private Vector3 _ballRacketDelta;
        private Vector3 _initPosition;

        private void OnEnable()
        {
            _ballRacketDelta = transform.position - _ball.position;
            _initPosition = transform.position;
        }

        public void RunBall()
        {
            _ball.SetParent(transform.parent);
            var body = _ball.GetComponent<Rigidbody2D>();
            body.AddForce((new Vector2(UnityEngine.Random.Range(-1f, 1f), 1f))*6f, ForceMode2D.Impulse);
        }

        public void Move(Vector2 delta)
        {
            delta.y = 0;
            transform.position = Clamp(
                transform.position - (Vector3)delta,
                new Vector3(-2f, -10f, 0f),
                new Vector3(2f, 10f, 0f)
            );
        }

        public void ReInit()
        {
            transform.position = _initPosition;
            ReInitBall();
        }
        
        public void ReInitBall()
        {
            _ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _ball.SetParent(transform);
            _ball.localPosition = Vector3.zero - _ballRacketDelta;
        }
        
        private Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Mathf.Clamp(value.x, min.x, max.x),
                Mathf.Clamp(value.y, min.y, max.y),
                Mathf.Clamp(value.z, min.z, max.z)
            );
        }
    }
}