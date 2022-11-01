using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class AssetBuilder : EditorWindow {

//	[MenuItem("Assets/Create LevelDataMaster")]
//	static void createLevelMasterData() 
//	{
//        createLevel(-1);
//	}

//    static void createLevel(int i)
//    {
//        string path = Path.Combine(Application.dataPath, "Resources/levels/level.asset");
//        string assetName = AssetDatabase.GenerateUniqueAssetPath(path);
//
//        LevelDataMaster obj = ScriptableObject.CreateInstance<LevelDataMaster>();
//        if (i > -1)
//        {
//            obj.level = i;
//            obj.clipName = "clips/ADOFAI" + (LevelLoader.GetWorld(i) + 1) + "_";
//            obj.world = LevelLoader.GetWorld(i);
//        }
//
//        AssetDatabase.CreateAsset(obj, assetName);
//        AssetDatabase.SaveAssets ();
//        AssetDatabase.Refresh();
//    }

//	[MenuItem("Assets/Create ClipData")]
//	static void createClipData() 
//	{
//        createClip(-1);
//	}

//    private static void createClip(int idx)
//    {
//        string path = Path.Combine(Application.dataPath, "Resources/clips/clip.asset");
//        string assetName = AssetDatabase.GenerateUniqueAssetPath(path);
//
//        ClipData obj = ScriptableObject.CreateInstance<ClipData>();
//        if (idx > -1)
//        {
//            int world = LevelLoader.GetWorld(idx);
//            obj.addoffset = LevelLoader.m_addOffsets[idx];
//            obj.bpm = LevelLoader.m_bpms[idx];
//            obj.caption = LevelLoader.m_captions[idx];
//            obj.clip = null;
//            obj.levelData = LevelLoader.m_leveldata[idx];
//            obj.multiplier = LevelLoader.m_multipliers[idx];
//            obj.volume = LevelLoader.m_worldVolumes[world];
//        }
//        AssetDatabase.CreateAsset(obj, assetName);
//        AssetDatabase.SaveAssets ();
//        AssetDatabase.Refresh();
//    }

	[MenuItem("Assets/Create Color Pallete")]
	static void createColorScheme() 
	{
		string path = Path.Combine(Application.dataPath, "Resources/pallete/pal.asset");
		string assetName = AssetDatabase.GenerateUniqueAssetPath(path);

		ColourScheme obj = ScriptableObject.CreateInstance<ColourScheme>();

		AssetDatabase.CreateAsset(obj, assetName);
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();
	}

//    [MenuItem("Assets/Create All Clips")]
//    static void createAllClips()
//    {
//        for (int i = 0; i < LevelLoader.m_captions.Length; ++i)
//        {
//            createClip(i);
//        }
//    }

//    [MenuItem("Assets/Create All Levels")]
//    static void createAllLevels()
//    {
//        for (int i = 0; i < LevelLoader.m_captions.Length; ++i)
//        {
//            createLevel(i);
//        }
//    }

    [MenuItem("Assets/Create Sprite Data")]
    static void createSpriteData() 
    {
        string path = Path.Combine(Application.dataPath, "Resources/sprites/spr.asset");
        string assetName = AssetDatabase.GenerateUniqueAssetPath(path);

        SpriteData obj = ScriptableObject.CreateInstance<SpriteData>();

        AssetDatabase.CreateAsset(obj, assetName);
        AssetDatabase.SaveAssets ();
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/Clear UserData")]
    public static void clearUserData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
