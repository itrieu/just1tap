using UnityEngine;

public class Utils
{
    public static bool GetTouch()
    {
		/*
		 *  bool result = false;
        if (Application.platform == RuntimePlatform.Android && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            result = true;
        }
        return result;
		*/
		#if UNITY_ANDROID
		if(Input.touches.Length > 0)
			return Input.GetTouch (0).phase == TouchPhase.Began;
		#elif UNITY_IOS
		if(Input.touches.Length > 0)
			return Input.GetTouch (0).phase == TouchPhase.Began;
		#elif UNITY_EDITOR
		return Input.GetMouseButtonDown(0);
		#endif

		return false;

    }

    public static void Rotate2D(Transform trans, float rotation)
    {
        trans.localEulerAngles = new Vector3(trans.localEulerAngles.x, trans.localEulerAngles.y, rotation);
    }
}