////////////////////////////////////////////////////////////////////////////////
//  
// @author Benoît Freslon @benoitfreslon
// https://github.com/BenoitFreslon/Vibration
// https://benoitfreslon.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
#if UNITY_IOS && !UNITY_EDITOR
using System.Collections;
using System.Runtime.InteropServices;
#endif

public static class Vibrator
{
#if UNITY_ANDROID && !UNITY_EDITOR
	public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
	public static AndroidJavaObject vibrator =currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
	public static AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
#endif

	///<summary>
	/// Only on Android
	/// https://developer.android.com/reference/android/os/Vibrator.html#vibrate(long)
	///</summary>
	public static void Vibrate(long milliseconds)
	{
        #if UNITY_ANDROID && !UNITY_EDITOR
		vibrator.Call("vibrate", milliseconds);
        #else
        Vibrate();
        #endif
	}

	///<summary>
	/// Only on Android
	/// https://proandroiddev.com/using-vibrate-in-android-b0e3ef5d5e07
	///</summary>
	public static void Vibrate(long[] pattern, int repeat)
	{
        #if UNITY_ANDROID && !UNITY_EDITOR
		vibrator.Call("vibrate", pattern, repeat);
        #else
        Vibrate();
        #endif
	}

	///<summary>
	///Only on Android
	///</summary>
	public static void Cancel()
	{
        #if UNITY_ANDROID && !UNITY_EDITOR
		vibrator.Call("cancel");
        #endif
	}

	public static bool HasVibrator()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context");
		string Context_VIBRATOR_SERVICE = contextClass.GetStatic<string>("VIBRATOR_SERVICE");
		AndroidJavaObject systemService = context.Call<AndroidJavaObject>("getSystemService", Context_VIBRATOR_SERVICE);
		if (systemService.Call<bool>("hasVibrator"))
		{
			return true;
		}
		else
		{
			return false;
		}
#else
		return false;
#endif
	}

	public static void Vibrate()
	{
		Handheld.Vibrate();
	}
}