using UnityEngine;
using UnityEditor;

public class PlayerPrefsResetter
{
    // Creates a button at the very top of your Unity screen: Tools -> Reset Best Time
    [MenuItem("Tools/Reset Best Time")]
    public static void ResetBestTime()
    {
        PlayerPrefs.DeleteKey("BestTime");
        Debug.Log("TESTING: 'BestTime' has been deleted from PlayerPrefs!");
    }

    // Creates a second button to wipe EVERYTHING (Last run, best run, volume settings, etc.)
    [MenuItem("Tools/Clear ALL PlayerPrefs")]
    public static void ClearAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("TESTING: ALL PlayerPrefs have been completely wiped!");
    }
}