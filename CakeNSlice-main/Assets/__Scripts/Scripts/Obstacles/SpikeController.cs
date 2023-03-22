using System.Collections;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    float _timer;
    bool _isMaxSize = false;

    [Header("Attributes")]
    [SerializeField] float _growTime = 2f;
    [SerializeField] float _maxSize = 125f;
    public  IEnumerator Grow()
    {
        Vector3 startScale = transform.localScale;
        Vector3 maxScale = new Vector3(startScale.x,startScale.y,_maxSize);

        do
        {
            //Grow object
            transform.localScale =  Vector3.Lerp(startScale, maxScale, _timer/_growTime);

            //Increment timer
            _timer += Time.deltaTime;

            //yield
            yield return null;
        } while (_timer < _growTime);

        //reset the timer
        _timer = 0;
        _isMaxSize = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!_isMaxSize)
        {
            StartCoroutine(Grow());
        }
    }
}
