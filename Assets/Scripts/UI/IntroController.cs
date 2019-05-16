using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public Text titleText;
    public Text subText;

    private GameObject title;
    private GameObject sub;

    public Transform startTitle;
    public Transform midTitle;
    public Transform endTitle;

    public Transform startSub;
    public Transform midSub;
    public Transform endSub;

    public float screenTime;
    public float moveTime;

    private enum State
    {
        MoveIn, Stay, MoveOut, Done
    };

    private State current = State.MoveIn;

    private float progress = 0;

    private Dictionary<State, System.Action> states;

    //private Dictionary<int, string> texts = new Dictionary<int, string>
    //{
    //    [1] = "Everything is Different!",
    //    [2] = "Let's get this show on the road!"
    //    [3] = ""
    //};

    public int midBoss;
    public int bigBoss;

    // Start is called before the first frame update
    void Start()
    {
        title = titleText.gameObject;
        sub = subText.gameObject;

        int depth = GameStores.GetDepth();
        

        titleText.text = "Level " + depth.ToString();
        if(depth<midBoss)
        {
            subText.text = "Midboss in " + (midBoss - depth).ToString() + (midBoss-depth==1 ? " Stage" : " Stages");
        } else
        {
            subText.text = "Boss in " + (bigBoss - depth).ToString() + (bigBoss - depth == 1 ? " Stage" : " Stages");
        }


        states = new Dictionary<State, System.Action>();
        states.Add(State.MoveIn, MoveIn);
        states.Add(State.Stay, PosStay);
        states.Add(State.MoveOut, MoveOut);
        states.Add(State.Done, () =>
        {
            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        states[current]();
    }

    private void MoveIn()
    {
        progress += Time.deltaTime * (1/moveTime);

        adjustPosition(title, startTitle, midTitle, progress);
        adjustPosition(sub, startSub, midSub, progress);
        //title.transform.position = 
        if(progress >= 1)
        {
            progress = 0;
            current = State.Stay;
        }
    }

    private void PosStay()
    {
        progress += Time.deltaTime * (1 / screenTime);
        if (progress >= 1)
        {
            progress = 0;
            current = State.MoveOut;
        }
    }

    private void MoveOut()
    {
        progress += Time.deltaTime * (1 / moveTime);
        adjustPosition(title, midTitle, endTitle, progress);
        adjustPosition(sub, midSub, endSub, progress);
        if(progress>=1)
        {
            progress = 0;
            current = State.Done;
        }
    }

    private void adjustPosition(GameObject o, Transform start, Transform end, float progress)
    {
        Vector3 pos = o.transform.position;

        float x = Mathf.SmoothStep(start.position.x, end.position.x, progress);
        float y = Mathf.SmoothStep(start.position.y, end.position.y, progress);

        pos.x = x;
        pos.y = y;

        o.transform.position = pos;
    }
}
