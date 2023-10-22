using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditorInternal;

[CustomEditor(typeof(USplashScreen))]
public class SplashScreenEditor : Editor
{

    private ReorderableList List;
    private ReorderableList BrandList;

    void OnEnable()
    {
        List = new ReorderableList(serializedObject, serializedObject.FindProperty("m_SplashScreens"), true, true, true, true);
        List.drawHeaderCallback = OnDrawHeader;
        List.drawElementCallback = OnDrawElement;

        BrandList = new ReorderableList(serializedObject, serializedObject.FindProperty("Brands"), true, true, true, true);
        BrandList.drawHeaderCallback = OnDrawHeaderBrand;
        BrandList.drawElementCallback = OnDrawElementBrand;
    }

    void OnDrawHeader(Rect rect)
    {
        EditorGUI.LabelField(rect, new GUIContent("Sequence", "The Splash sequence"), EditorStyles.boldLabel);
        rect.x += 115;
        EditorGUI.LabelField(rect, new GUIContent("| Time", "The Time to show the splash"), EditorStyles.boldLabel);
        rect.x += 80;
        EditorGUI.LabelField(rect, new GUIContent("| Wait", "The Time for wait for next splash (if have other)"), EditorStyles.boldLabel);
    }

    void OnDrawElement(Rect rect, int index, bool isactive, bool isfocus)
    {
        var element = List.serializedProperty.GetArrayElementAtIndex(index);
        float _time = element.FindPropertyRelative("m_time").floatValue;
        float _wait = element.FindPropertyRelative("WaitForNext").floatValue;
        rect.y += 2;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("SplashUI"), GUIContent.none);
        _time = EditorGUI.FloatField(new Rect(rect.x + 105, rect.y, 75, EditorGUIUtility.singleLineHeight), new GUIContent("", "The Time to show the splash"), _time);
        _wait = EditorGUI.FloatField(new Rect(rect.x + 185, rect.y, 75, EditorGUIUtility.singleLineHeight), new GUIContent("", "The Time for wait for next splash (if have other)"), _wait);
        element.FindPropertyRelative("m_time").floatValue = _time;
        element.FindPropertyRelative("WaitForNext").floatValue = _wait;
    }

    void OnDrawHeaderBrand(Rect rect)
    {
        EditorGUI.LabelField(rect, new GUIContent("Brands", ""), EditorStyles.boldLabel);
    }

    void OnDrawElementBrand(Rect rect, int index, bool isactive, bool isfocus)
    {
        var element = BrandList.serializedProperty.GetArrayElementAtIndex(index);
        string br = element.FindPropertyRelative("BrandAcreditation").stringValue;
        rect.y += 2;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Logo"), GUIContent.none);
        br = EditorGUI.TextField(new Rect(rect.x + 105, rect.y, 220, EditorGUIUtility.singleLineHeight), new GUIContent("", "Brand Accreditation"), br);
        element.FindPropertyRelative("BrandAcreditation").stringValue = br;
    }

    public override void OnInspectorGUI()
    {
        USplashScreen splash = (USplashScreen)target;
        bool allowSceneObjects = !EditorUtility.IsPersistent(target);
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Global", EditorStyles.boldLabel);
        GUILayout.BeginVertical(EditorStyles.toolbarButton);
        splash.NextLevel = EditorGUILayout.TextField("Next Level: ", splash.NextLevel, EditorStyles.textArea);
        GUILayout.EndVertical();
        GUILayout.Space(5);
        serializedObject.Update();
        List.DoLayoutList();
        BrandList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndVertical();

        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        splash.ShowBrands = EditorGUILayout.ToggleLeft("Show Brands", splash.ShowBrands, EditorStyles.toolbarButton);
        splash.ShowLoadingProgress = EditorGUILayout.ToggleLeft("Show Loading Progress", splash.ShowLoadingProgress, EditorStyles.toolbarButton);
        splash.SkipWhenLoadLevel = EditorGUILayout.ToggleLeft("Skip When Load ", splash.SkipWhenLoadLevel, EditorStyles.toolbarButton);
        splash.HideLoadingWhenLoad = EditorGUILayout.ToggleLeft("Hide Loading When Load ", splash.HideLoadingWhenLoad, EditorStyles.toolbarButton);
        splash.ShowPercentLoadText = EditorGUILayout.ToggleLeft("Show Percent Load Text ", splash.ShowPercentLoadText, EditorStyles.toolbarButton);
        splash.TimeForSkip = EditorGUILayout.Slider("Time For Skip: ", splash.TimeForSkip, 0, 15);
        splash.SkipFadeSpeed = EditorGUILayout.Slider("Skip Fade Speed: ", splash.SkipFadeSpeed, 0, 5);
        splash.BrandsShowTime = EditorGUILayout.Slider("Brand Show Time", splash.BrandsShowTime, 0, 5);
        GUILayout.EndVertical();

        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("References", EditorStyles.boldLabel);
        splash.BrandPrefab = EditorGUILayout.ObjectField("Brand Prefab", splash.BrandPrefab, typeof(GameObject), allowSceneObjects) as GameObject;
        splash.BrandsRoot = EditorGUILayout.ObjectField("Brand Root", splash.BrandsRoot, typeof(GameObject), allowSceneObjects) as GameObject;
        splash.LoadingRoot = EditorGUILayout.ObjectField("Loading Root", splash.LoadingRoot, typeof(GameObject), allowSceneObjects) as GameObject;
        splash.BrandsPanel = EditorGUILayout.ObjectField("Brand Panel", splash.BrandsPanel, typeof(Transform), allowSceneObjects) as Transform;
        splash.SkipUI = EditorGUILayout.ObjectField("Skip UI: ", splash.SkipUI, typeof(GameObject), allowSceneObjects) as GameObject;
        splash.Black = EditorGUILayout.ObjectField("Black Screen: ", splash.Black, typeof(Image), allowSceneObjects) as Image;
        splash.ProgreesSlider = EditorGUILayout.ObjectField("Progrees Slider: ", splash.ProgreesSlider, typeof(Slider), allowSceneObjects) as Slider;
        splash.LoadingText = EditorGUILayout.ObjectField("Loading Text: ", splash.LoadingText, typeof(Text), allowSceneObjects) as Text;
        splash.BrandText = EditorGUILayout.ObjectField("Brand Text", splash.BrandText, typeof(Text), allowSceneObjects) as Text;
        splash.Loading = EditorGUILayout.ObjectField("Loading Effect: ", splash.Loading, typeof(USLoadingEffect), allowSceneObjects) as USLoadingEffect;

        GUILayout.EndVertical();
        GUILayout.EndVertical();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(splash);
        }
    }

}