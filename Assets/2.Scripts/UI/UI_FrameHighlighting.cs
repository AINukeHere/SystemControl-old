using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FrameHighlighting : MonoBehaviour
{
    [SerializeField]
    private RectTransform targetTr;
    [SerializeField]
    private RectTransform UpFrame;
    [SerializeField]
    private RectTransform DownFrame;
    [SerializeField]
    private RectTransform LeftFrame;
    [SerializeField]
    private RectTransform RightFrame;

    [SerializeField]
    private Color frameColor = Color.white;

    [SerializeField]
    private bool bOverlap = false;

    public float frameWidth = 0.2f;



    public bool flashEffect = false;
    public int flashCount = -1;
	private float timeLine = 1.0f;


    //타겟의 픽셀 퍼 유닛
    float targetPixelPerUnit;
    float myPixelPerUnit;
    float aspratio;

    void OnEnable()
    {
        UpFrame.GetComponent<Image>().color = frameColor;
        DownFrame.GetComponent<Image>().color = frameColor;
        LeftFrame.GetComponent<Image>().color = frameColor;
		RightFrame.GetComponent<Image>().color = frameColor;
		UpFrame.gameObject.SetActive(false);
		DownFrame.gameObject.SetActive(false);
		LeftFrame.gameObject.SetActive(false);
		RightFrame.gameObject.SetActive(false);
        if (targetTr != null)
        {
            targetPixelPerUnit = targetTr.GetComponent<Image>().sprite.pixelsPerUnit;
            myPixelPerUnit = UpFrame.GetComponent<Image>().sprite.pixelsPerUnit;
			aspratio = myPixelPerUnit / targetPixelPerUnit;
			FixPosition();
		}
    }
	void Start()
	{
		FixPosition ();
	}
    void Update()
    {
        FixPosition();
        if (flashEffect)
        {
            if (timeLine <= 0)
            {
                if (flashCount == -1)
                {
                    timeLine = 1.0f;
                }
                else if (flashCount > 0)
                {
                    flashCount--;
                    timeLine = 1.0f;
                }
            }
            timeLine -= Time.deltaTime;
            if (timeLine > 0.5f)
            {
                UpFrame.gameObject.SetActive(false);
                DownFrame.gameObject.SetActive(false);
                LeftFrame.gameObject.SetActive(false);
                RightFrame.gameObject.SetActive(false);
            }
            else
            {
                UpFrame.gameObject.SetActive(true);
                DownFrame.gameObject.SetActive(true);
                LeftFrame.gameObject.SetActive(true);
                RightFrame.gameObject.SetActive(true);
            }
        }
    }
    void FixPosition()
    {
        if (targetTr != null)
        {
            float yScale = targetTr.rect.height * 0.5f;
            float xScale = targetTr.rect.width * 0.5f;
            //겹치지 않고 둘러싸는 스타일
            if (!bOverlap)
            {
                //위치 : 타겟위치 + 타겟y스케일 + 프레임두깨*0.5
				UpFrame.position = targetTr.position + (yScale * aspratio * aspratio + frameWidth*0.5f) * targetTr.up;
                UpFrame.sizeDelta = new Vector2(targetTr.rect.width * aspratio * aspratio + frameWidth*2, frameWidth);

				DownFrame.position = targetTr.position - (yScale * aspratio * aspratio + frameWidth*0.5f) * targetTr.up;
                DownFrame.sizeDelta = new Vector2(targetTr.rect.width * aspratio * aspratio + frameWidth*2, frameWidth);

				LeftFrame.position = targetTr.position - (xScale * aspratio * aspratio + frameWidth*0.5f) * targetTr.right;
                LeftFrame.sizeDelta = new Vector2(frameWidth, targetTr.rect.height * aspratio * aspratio + frameWidth*2);

				RightFrame.position = targetTr.position + (xScale * aspratio * aspratio + frameWidth*0.5f) * targetTr.right;
                RightFrame.sizeDelta = new Vector2(frameWidth, targetTr.rect.height * aspratio * aspratio + frameWidth*2);
            }
            else
            {
                //위치 : 타겟위치 + 타겟y스케일 + 프레임두깨*0.5
				UpFrame.position = (targetTr.position + (yScale * aspratio * aspratio - frameWidth * 0.5f) * targetTr.up);
                UpFrame.sizeDelta = new Vector2(targetTr.rect.width, frameWidth);

				DownFrame.position = (targetTr.position - (yScale * aspratio * aspratio - frameWidth * 0.5f) * targetTr.up);
                DownFrame.sizeDelta = new Vector2(targetTr.rect.width, frameWidth);

				LeftFrame.position = (targetTr.position - (xScale * aspratio * aspratio - frameWidth * 0.5f) * targetTr.right);
                LeftFrame.sizeDelta = new Vector2(frameWidth, targetTr.rect.height);

				RightFrame.position = (targetTr.position + (xScale * aspratio * aspratio - frameWidth * 0.5f) * targetTr.right);
                RightFrame.sizeDelta = new Vector2(frameWidth, targetTr.rect.height);
			}
			UpFrame.rotation = DownFrame.rotation = LeftFrame.rotation = RightFrame.rotation = targetTr.rotation;
        }
    }
}
