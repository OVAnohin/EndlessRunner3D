using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRemoveWall : MonoBehaviour
{
    [SerializeField] private float _coolDownTime;
    [SerializeField] private GameObject _wallChecker;

    private bool _isCoolDown = false;

    public void TryRemoveWall()
    {
        if ( _isCoolDown == false)
        {
            StartCoroutine(SetWallCheckerOn());
            StartCoroutine(RemoveWall());
        }
    }

    private IEnumerator RemoveWall()
    {
        float elapsedTime = _coolDownTime;
        _isCoolDown = true;
        _wallChecker.SetActive(true);

        Debug.Log("Boom");

        while (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            yield return null;
        }

        _wallChecker.SetActive(false);
        Debug.Log("BoomEnd");
        _isCoolDown = false;
    }

    private IEnumerator SetWallCheckerOn()
    {
        _wallChecker.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        _wallChecker.SetActive(false);
    }
}
