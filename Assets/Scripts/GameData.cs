using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
	public static GameData Instance;

	int bestScore;
	string bestPlayersName;
	string currentPlayersName;

	public int BestScore
	{
		get { return bestScore; }
		set { bestScore = value; }
	}
	public string BestPlayersName
	{
		get { return bestPlayersName; }
		set { bestPlayersName = value; }
	}
	public string CurrentPlayersName
	{
		get { return currentPlayersName; }
		set { currentPlayersName = value; }
	}

	[System.Serializable]
	class GameDataToSerialize
	{
		public int bestScore;
		public string bestPlayersName;

		public GameDataToSerialize(int bestScore, string bestPlayersName)
		{
			this.bestScore = bestScore;
			this.bestPlayersName = bestPlayersName;
		}
	}

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
		Load();
	}

	string GetGameDataPath()
	{
		return Application.persistentDataPath + "/gamedata.json";
	}

	public void Save()
	{
		string json = JsonUtility.ToJson(new GameDataToSerialize(bestScore, bestPlayersName));
		File.WriteAllText(GetGameDataPath(), json);
	}

	public void Load()
	{
		string path = GetGameDataPath();
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			GameDataToSerialize data = JsonUtility.FromJson<GameDataToSerialize>(json);

			bestScore = data.bestScore;
			bestPlayersName = data.bestPlayersName;
		}
	}
	/// <summary>
	/// ベストスコアが更新された場合、ベストプレイヤーを現在のプレイヤーで置き換える
	/// </summary>
	/// <param name="newScore">新しいスコア</param>
	/// <returns>ベストスコアが更新された場合true</returns>
	public bool UpdateBestScore(int newScore)
	{
		if (bestScore <= newScore)
		{
			bestScore = newScore;
			bestPlayersName = currentPlayersName;
			return true;
		}
		return false;
	}

	public string GetBestScoreText()
	{
		return "Best Score : " + bestPlayersName + " : " + bestScore;
	}
}
