using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScroll : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public UnityEngine.UI.Text outText;

    public System.Action<int, int> onMove = null;

    private int x = 0;
    private int y = 0;

    private const int HEIGHT = 5;
    private const int WIDTH = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 viewPos = cam.WorldToViewportPoint(player.transform.position);
        Vector2 viewPos = player.transform.position - cam.gameObject.transform.position;
        //Debug.Log(viewPos.ToString());
        int i = cam.scaledPixelWidth;
        float height = 2 * HEIGHT;
        float width = 2 * WIDTH;
        //Debug.Log(i);

        if(viewPos.x > WIDTH)
        {
            cam.gameObject.transform.Translate(width, 0, 0);
            x += 1;
            checkMove();
        } else if(viewPos.x < -WIDTH)
        {
            cam.gameObject.transform.Translate(-width, 0, 0);
            x -= 1;
            checkMove();
        }

        if(viewPos.y > HEIGHT)
        {
            cam.gameObject.transform.Translate(0, height, 0);
            y += 1;
            checkMove();
        } else if(viewPos.y < -HEIGHT)
        {
            cam.gameObject.transform.Translate(0, -height, 0);
            y -= 1;
            checkMove();
        }
    }

    private void checkMove()
    {
        onMove?.Invoke(x, y);
    }

    public void SetOnMove(System.Action<int, int> a)
    {
        onMove = a;
    }

}
