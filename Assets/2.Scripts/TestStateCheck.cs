using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Runtime.InteropServices;

public class TestStateCheck : MonoBehaviour
{
    static bool bShowOne = false;
    void Awake()
    {
        StartCoroutine(Read("http://210.119.81.117/~tugame/16110138/SystemControlTestUserList"));

    }
    IEnumerator Read(string url)
    {
        System.Windows.Forms.MessageBoxButtons button = System.Windows.Forms.MessageBoxButtons.OK;
        
        while (true)
        {
            // Start a download of the given URL
            using (WWW www = new WWW(url))
            {
                // Wait for download to complete
                yield return www;

                if (www.text.StartsWith("On"))
                {
                    if (!bShowOne)
                    {
                        bShowOne = true;
                        //System.Windows.Forms.MessageBox.Show("서버에서 접속을 허가했습니다.", "Notice", button);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("서버에서 접속을 거부했습니다.", "Notice", button);
                    UnityEngine.Application.Quit();
                }
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
