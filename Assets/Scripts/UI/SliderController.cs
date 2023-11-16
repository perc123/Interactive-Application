using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public GameObject sections;
    public float maxTime = 220f;
    public Color mainColor = Color.white;
    public Color redColor = Color.red;
    public Color yellowColor = Color.yellow;
    public Color greenColor = Color.green;
    public int sectionLength = 5;
    private AnimationManager animationManager;
    private Dictionary<int, GameObject> sectionObjects = new Dictionary<int, GameObject>();
    private Dictionary<int, List<float>> sectionTimes = new Dictionary<int, List<float>>();
    private Gradient gradient;

    void Start()
    {
        slider.maxValue = maxTime;
        int numSections = Mathf.CeilToInt(maxTime / sectionLength);
        float sectionWidth = slider.GetComponent<RectTransform>().rect.width / numSections;

        gradient = new Gradient();
        gradient.colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(mainColor, 0f),
            new GradientColorKey(greenColor, 0.33f),
            new GradientColorKey(yellowColor, 0.66f),
            new GradientColorKey(redColor, 1f)
        };

        // Create GameObjects (Sections) based on length
        for (int i = 1; i <= numSections; i++)
        {
            GameObject section = new GameObject("Section " + i);
            section.transform.SetParent(sections.transform);
            RectTransform rect = section.AddComponent<RectTransform>();
        
            float anchorMinX = (i - 1) * sectionWidth / slider.GetComponent<RectTransform>().rect.width;
            float anchorMaxX = i * sectionWidth / slider.GetComponent<RectTransform>().rect.width;
            rect.anchorMin = new Vector2(anchorMinX, 0);
            rect.anchorMax = new Vector2(anchorMaxX, 1);
            rect.pivot = new Vector2(0, 0);
            rect.sizeDelta = Vector2.zero;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            Image image = section.AddComponent<Image>();
            image.color = gradient.Evaluate(0f); // Set initial color to mainColor

            sectionObjects[i] = section;
        }

        animationManager = FindObjectOfType<AnimationManager>();

        CheckSectionTimes();
    }

    void Update()
    {
        CheckSectionTimes();
    }

    private void CheckSectionTimes()
    {
        sectionTimes.Clear();

        if (animationManager == null || sectionObjects.Count == 0)
        {
            return;
        }

        int maxSectionTimes = 0;

        foreach (float startTime in animationManager._startTimes_queenSong)
        {
            int section = Mathf.FloorToInt(startTime / sectionLength) + 1;
            if (!sectionTimes.ContainsKey(section))
            {
                sectionTimes[section] = new List<float>();
            }
            sectionTimes[section].Add(startTime);

            if (sectionTimes[section].Count > maxSectionTimes)
            {
                maxSectionTimes = sectionTimes[section].Count;
            }
        }

        // Coloring sections based on numTimes
        foreach (KeyValuePair<int, GameObject> sectionObject in sectionObjects)
        {
            int sectionIndex = sectionObject.Key;
            int numTimes = sectionTimes.ContainsKey(sectionIndex) ? sectionTimes[sectionIndex].Count : 0;
            float ratio = maxSectionTimes > 0 ? (float)numTimes / maxSectionTimes : 0f;
            Image sectionImage = sectionObject.Value.GetComponent<Image>();
            sectionImage.color = gradient.Evaluate(ratio);
        }
    }

}
