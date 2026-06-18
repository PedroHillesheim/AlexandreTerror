using UnityEngine;

public class PatrolController : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    private Transform _nemesis;
    private int _currentPointIndex;

    private void Start()
    {
        _nemesis = GameController.Instance.NemesisTransform;
    }
    public Vector3 GetRandomPoint()
    {
        int randomIndex = Random.Range(0, _patrolPoints.Length);
        return _patrolPoints[randomIndex].position;
    }
    public Vector3 MoveToNextPoint()
    {
        if (_patrolPoints.Length == 0)
            return Vector3.zero;
        Vector3 nextPoint = _patrolPoints[_currentPointIndex].localPosition;
        _currentPointIndex++;
        if (_currentPointIndex >= _patrolPoints.Length)
            _currentPointIndex = 0;
        return nextPoint;
    }
    public Vector3 GetClosestPoint()
    {
        if (_patrolPoints == null || _patrolPoints.Length == 0 || _nemesis == null)
            return Vector3.zero;

        Transform closest = _patrolPoints[0];
        float minDist = (_nemesis.position - closest.position).sqrMagnitude;

        for (int i = 1; i < _patrolPoints.Length; i++)
        {
            float dist = (_nemesis.position - _patrolPoints[i].position).sqrMagnitude;

            if (dist < minDist)
            {
                minDist = dist;
                closest = _patrolPoints[i];
            }
        }

        return closest.position;
    }
}