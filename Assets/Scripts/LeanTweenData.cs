using UnityEngine;

[System.Serializable]
public class LeanTweenData<T>
{
	public T targetGameObject;
    public float startValue;
    public float targetValue;
    public float duration;
    public AnimationCurve curve;
}
