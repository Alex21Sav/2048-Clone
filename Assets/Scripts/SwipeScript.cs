using UnityEngine;
public class SwipeScript : MonoBehaviour
{
    [SerializeField] private Field _filed;
    private float _fingerStartTime = 0.0f;
    private Vector2 _fingerStartPos = Vector2.zero;

    private bool _isSwipe = false;
    private float _minSwipeDist = 50.0f;
    private float _maxSwipeTime = 0.5f;


    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        _isSwipe = true;
                        _fingerStartTime = Time.time;
                        _fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        /* The touch is being canceled */
                        _isSwipe = false;
                        break;

                    case TouchPhase.Ended:

                        float gestureTime = Time.time - _fingerStartTime;
                        float gestureDist = (touch.position - _fingerStartPos).magnitude;

                        if (_isSwipe && gestureTime < _maxSwipeTime && gestureDist > _minSwipeDist)
                        {
                            Vector2 direction = touch.position - _fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                // the swipe is horizontal:
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                // the swipe is vertical:
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if (swipeType.x != 0.0f)
                            {
                                if (swipeType.x > 0.0f)
                                {
                                    // MOVE RIGHT
                                    _filed.OnInput(Vector2.right);
                                }
                                else
                                {
                                    // MOVE LEFT
                                    _filed.OnInput(Vector2.left);
                                }
                            }
                            if (swipeType.y != 0.0f)
                            {
                                if (swipeType.y > 0.0f)
                                {
                                    // MOVE UP
                                    _filed.OnInput(Vector2.up);
                                }
                                else
                                {
                                    // MOVE DOWN
                                    _filed.OnInput(Vector2.down);
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}
