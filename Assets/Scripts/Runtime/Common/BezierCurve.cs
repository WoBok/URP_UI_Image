using System.Collections.Generic;
using UnityEngine;

public static class BezierCurve
{
    /// <summary>
    /// ����n+1�����Ƶ㣬�����ɵ����������������ϵĵ㣬����Խ�࣬����Խƽ��
    /// </summary>
    /// <param name="controlPoints">���Ƶ�</param>
    /// <param name="pointsCount">���ɵ������</param>
    /// <returns>�����ϵ�ļ���</returns>
    public static List<Vector3> GetCurvePoints(Vector3[] controlPoints, int pointsCount)
    {
        List<Vector3> curvePoints = new List<Vector3>();
        for (float t = 0; t <= 1; t += 1f / pointsCount)
            curvePoints.Add(GetBt(controlPoints, t));
        curvePoints.Add(controlPoints[controlPoints.Length - 1]);
        return curvePoints;
    }
    /// <summary>
    /// ����n+1���㣬��Щ��Ϊ���Ƶ㣬����tֵʱ��B(t)
    /// </summary>
    /// <param name="controlPoints">���Ƶ�</param>
    /// <param name="t">[0,1]</param>
    /// <returns>B(t)</returns>
    static Vector3 GetBt(Vector3[] controlPoints, float t)
    {
        int n = controlPoints.Length - 1;
        Vector3 curvePoint = Vector3.zero;
        for (int k = 0; k <= n; k++)
            curvePoint += Factorial(n) / (Factorial(k) * Factorial(n - k)) * Mathf.Pow(t, k) * Mathf.Pow(1 - t, n - k) * controlPoints[k];
        return curvePoint;
    }
    /// <summary>
    /// ����׳�
    /// </summary>
    /// <param name="n">����n</param>
    /// <returns>n��</returns>
    static int Factorial(int n)
    {
        if (n == 1 || n == 0)
            return 1;
        int f = n;
        for (int i = n - 1; i > 1; i--)
            f *= i;
        return f;
    }
}