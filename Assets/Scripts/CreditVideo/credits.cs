using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class credits : MonoBehaviour
{
    public List<string> creditListRocketmann;
    public List<string> creditListDontStopMeNow;
    public TMP_Text tmpTextRocketman;
    public TMP_Text tmpTextQueen;
    public Transform textParent; // parent transform for instantiating new texts

    public float speed = 0.02f; // adjust this value to get your desired speed
    public float distanceBetweenTexts = 1.0f; // distance between two texts
    public float delayBetweenTexts = 1.0f; // delay between each text starting to move

    private Vector3 queenTextStartPos = new Vector3(-0.368f, 0.27f, 0);
    private Vector3 rocketmanTextStartPos = new Vector3(0.368f, 0.27f, 0);
    
    private Vector3 queenTextEndPos = new Vector3(-0.368f, -0.52f, 0);
    private Vector3 rocketmanTextEndPos = new Vector3(0.368f, -0.52f, 0);
    
    [SerializeField] private AnimationManager animationManager;

    private List<TMP_Text> tmpTexts = new List<TMP_Text>();

    public void OnEnable()
    {
        ClearLeftOverNames();
        creditListRocketmann = animationManager.creditListRocketman;
        creditListDontStopMeNow = animationManager.creditListDontStopMeNow;

        StartCoroutine(AnimateCredits(tmpTextQueen, queenTextStartPos, queenTextEndPos, creditListDontStopMeNow));
        StartCoroutine(AnimateCredits(tmpTextRocketman, rocketmanTextStartPos, rocketmanTextEndPos, creditListRocketmann));
    }

    private void ClearLeftOverNames()
    {
        foreach (TMP_Text text in tmpTexts)
        {
            if (text != null)
            {
                Destroy(text.gameObject);
            }
        }

        tmpTexts.Clear();
    }

    private IEnumerator AnimateCredits(TMP_Text templateText, Vector3 startPos, Vector3 endPos, List<string> credits)
    {
        for (int i = 0; i < credits.Count; i++)
        {
            // create new text object
            TMP_Text newText = Instantiate(templateText, textParent);
            newText.text = credits[i];
            newText.transform.localPosition = startPos;
            tmpTexts.Add(newText);

            // start moving the text after a delay
            StartCoroutine(AnimateText(newText, endPos));
            
            yield return new WaitForSeconds(delayBetweenTexts);
        }
    }

    private IEnumerator AnimateText(TMP_Text text, Vector3 endPos)
    {
        while (text.transform.localPosition != endPos)
        {
            text.transform.localPosition = Vector3.MoveTowards(text.transform.localPosition, endPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}