using UnityEngine;

public class PatrolController : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int _currentPointIndex;
    public Vector3 GetRandomPoint()
    {
        int randomIndex = Random.Range(0, _patrolPoints.Length);
        return _patrolPoints[randomIndex].position;
    }
    public Vector3 MoveToNextPoint()
    {
        if (_patrolPoints.Length == 0)
            return Vector3.zero;
        Vector3 nextPoint = _patrolPoints[_currentPointIndex].position;
        _currentPointIndex++;
        if (_currentPointIndex >= _patrolPoints.Length)
            _currentPointIndex = 0;
        return nextPoint;
    }
}
