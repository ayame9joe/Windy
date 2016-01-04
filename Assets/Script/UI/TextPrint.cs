using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextPrint : MonoBehaviour {

	float letterPause = 0.05f;
	private string word;
	private string printText;
	private int i, j = 0;

	//---Story Line---
	private string[] Text = {
		"回来啦。",
		"这次的速度不慢啊。",
		"果然什么都不记得呢。",
		"早就说过",
		"结界就是这样无法穿越的存在啊。",
	};

	public Text txtPrint;

	// Use this for initialization
	void Start () {

		TextChange ();
	
	}
	
	// Update is called once per frame
	void Update () {

		txtPrint.text = printText;
		TextMoveOn ();
	
	}

	void TextChange () {
		word = "";
		word = Text [i];
		printText = "";
		StartCoroutine (TypeText ());
	}

	IEnumerator TypeText () {
		foreach (char letter in word.ToCharArray()) {
			printText += letter;
			yield return new WaitForSeconds(letterPause);
		}

		printText += "";
		j++;
	}

	void TextMoveOn () {
		if (Input.GetMouseButtonDown(0))
		{                    
			//检测对话显示完没有 i = j 就是还没有显示完
			if (i == j)
			{
				letterPause = 0.0f;     //加快显的速度，让对话速度显示完
			}
			else
			{
				//检测对话语句是否超出了最大限制，超出了就DO STH.
				if (i < Text.Length - 1)
				{
					letterPause = 0.05f;
					i++;
					TextChange();
				}
				else
				{
					//DO STH.
					
				}
				
			}                                          
		}          
	}
}
