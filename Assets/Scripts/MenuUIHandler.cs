using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// すべてのデフォルト・スクリプトよりも**後**に実行されるスクリプトを設定する。
// UIを設定する前に他のものを初期化する必要があるかもしれないので、これはUIに役立ちます。
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
	[SerializeField] private Text bestScore;
	[SerializeField] private InputField playersName;

	void SetBestScoreAndPlayersName()
	{
		GameData gameData = GameData.Instance;
		bestScore.text = gameData.GetBestScoreText();
		playersName.text = gameData.CurrentPlayersName;
	}

	// Start is called before the first frame update
	void Start()
	{
		SetBestScoreAndPlayersName();
	}

	//// Update is called once per frame
	//void Update()
	//{

	//}

	public void StartNew()
	{
		GameData.Instance.CurrentPlayersName = playersName.text;
		SceneManager.LoadScene(1);
	}

	public void Exit()
	{
		GameData.Instance.Save();

#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
#else
		Application.Quit(); // original code to quit Unity player
#endif
	}
}
