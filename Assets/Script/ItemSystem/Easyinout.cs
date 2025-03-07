using System;
using System.Collections;
using UnityEngine;

public class EasyInOut : MonoBehaviour
{
    //�w�ʨ��
    public static float EaseIn(float t) => t * t; //�C���
    public static float EaseOut(float t) => 1 - (1 - t) * (1 - t); //�֨�C
    public static float EaseInOut(float t) => t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2; //�C��֨�C

    //�x���ܼƪ��w���ܤ�
    public IEnumerator ChangeValue<T>(T start, T end, float duration, Action<T> onUpdate, Func<float, float> easingFunction)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            //��ܪ��w�ʤ�k
            float easedT = easingFunction(t);

            if (typeof(T) == typeof(float))
            {
                onUpdate((T)(object)Mathf.Lerp((float)(object)start, (float)(object)end, easedT));
            }
            else if (typeof(T) == typeof(Vector3))
            {
                onUpdate((T)(object)Vector3.Lerp((Vector3)(object)start, (Vector3)(object)end, easedT));
            }
            yield return null;
        }
        onUpdate(end);
    }
}