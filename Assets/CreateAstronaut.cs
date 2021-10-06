using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;

public class GameInfo : MonoBehaviour
{
	public string name;
	public int viewNum;
	public int objectNum;
	public Color color;
	public int posX;
	public int posZ;

	public GameInfo(string name_, int viewNum_)
	{
		name = name_;
		viewNum = viewNum_;
		//color = UnityEngine.Random.ColorHSV(0.0f, 1.0f);
		//posX = UnityEngine.Random.Range(-40, 40);
		//posZ = UnityEngine.Random.Range(-40, 40);
	}
}

public class TwitchCrawler : MonoBehaviour
{
	public static int totalObjectNum = 500;
	public List<GameInfo> gameList = new List<GameInfo>();

	public void getRatio()
	{
		int result = 0;
		int map_width = 100;

		for (int i = 0; i < gameList.Count; i++)
		{
			result += gameList[i].viewNum;
		}

		for (int i = 0; i < gameList.Count; i++)
		{
			gameList[i].objectNum = (int)(((double)gameList[i].viewNum / (double)result) * totalObjectNum);

			float color_range = (float)(i + 1) / (gameList.Count + 2);
			gameList[i].color = UnityEngine.Random.ColorHSV(color_range, color_range, 1.0f, 1.0f);

			gameList[i].posX = (int)((float)(i % 5) / 5 * map_width) + map_width / 10 - 50;
			gameList[i].posZ = (int)((float)(i / 5) / 5 * map_width) + map_width / 10 - 50;
		}
	}

	public void insertGame(string name, int number)
	{
		gameList.Add(new GameInfo(name, number));
	}

	public int stringToInt(string info)
	{
		string[] contents = info.Split('.');
		int result = 0;

		if (contents.Length == 1)
		{
			int multiply = 10000;

			int i = 0;
			for (i = 0; contents[0][i] >= '0' && contents[0][i] <= '9'; i++) ;
			for (int j = i - 1; j >= 0; j--)
			{
				result += (contents[0][j] - '0') * multiply;
				multiply *= 10;
			}
		}
		else
		{
			result += Convert.ToInt32(contents[0]) * 10000;
			int multiply = 1000;
			for (int i = 0; ; i++)
			{
				if (contents[1][i] < '0' || contents[1][i] > '9') break;
				result += (contents[1][i] - '0') * multiply;
				multiply /= 10;
			}

		}
		return result;
	}

	public void parsing(String html)
	{
		string[] text = html.Split('\n');
		for (int i = 0; i < text.Length; i++)
		{
			string[] text_child = text[i].Split(' ');
			if (text_child[0].Equals("시청자"))
			{
				insertGame(text[i - 1], stringToInt(text_child[1]));
			}
		}
	}

	public void crawling()
	{
		var path = Application.streamingAssetsPath;
		using (IWebDriver driver = new ChromeDriver(path))
		{
			driver.Url = "https://www.twitch.tv/directory?sort=VIEWER_COUNT";
			Thread.Sleep(3000);

			var elements = driver.FindElement(By.ClassName("ScTower-sc-1dei8tr-0"));

			parsing(elements.Text);
		}
	}
}

public class CreateAstronaut : MonoBehaviour
{
    public GameObject projectile;
	public TwitchCrawler crawler;
	public List<List<GameObject>> status = new List<List<GameObject>>();


    // Start is called before the first frame update
    void Start()
    {
		crawler = new TwitchCrawler();
		crawler.crawling();
		crawler.getRatio();
		
		for (int i = 0; i < crawler.gameList.Count; i++)
        {
			status.Add(new List<GameObject>());
			for (int j = 0; j < crawler.gameList[i].objectNum; j++)
            {
				float x = crawler.gameList[i].posX + UnityEngine.Random.Range(-10, 10);
				float z = crawler.gameList[i].posZ + UnityEngine.Random.Range(-10, 10);
				GameObject newPrefab = Instantiate(projectile, new Vector3(x, 0, z), Quaternion.identity);

				status[i].Add(newPrefab);

				//change color
				GameObject child = newPrefab.transform.GetChild(1).gameObject;
				Renderer rend = child.GetComponent<Renderer>();
				var met = rend.materials;
				rend.materials[1].color = crawler.gameList[i].color;

				//change text
				GameObject textChild = newPrefab.transform.GetChild(3).gameObject;
				TextMesh newText = textChild.GetComponent<TextMesh>();
				newText.text = crawler.gameList[i].name;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
