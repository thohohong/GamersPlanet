using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;

public class GameInfo
{
	public string name;
	public int viewNum;
	public int radius;
	public int objectNum;
	public Color color;
	public Vector3 pos;
	public float vel;
	public Vector3 dir;
	public GameObject Ground;
	public Rigidbody GroundRigid;
	public bool isFalling;
	public GameObject InfoWindow;

	public GameInfo(string name_, int viewNum_)
	{
		name = name_;
		viewNum = viewNum_;
		
		color = UnityEngine.Random.ColorHSV(0.0f, 1.0f);
		vel = UnityEngine.Random.Range(-1.0f, 1.0f);
		dir = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f));
		//posX = UnityEngine.Random.Range(-40, 40);
		//posZ = UnityEngine.Random.Range(-40, 40);
	}
}

public class TwitchCrawler
{
	public static int totalObjectNum = 500;
	public List<GameInfo> gameList = new List<GameInfo>();

	public void getRatio()
	{
		int result = 0;
		int map_width = 300;

		for (int i = 0; i < gameList.Count; i++)
		{
			result += gameList[i].viewNum;
		}

		for (int i = 0; i < gameList.Count; i++)
		{
			gameList[i].objectNum = (int)(((double)gameList[i].viewNum / (double)result) * totalObjectNum);
			gameList[i].radius = (int)Math.Sqrt(gameList[i].objectNum);

			float x = UnityEngine.Random.Range(map_width * -0.5f, map_width * 0.5f);
			float y = UnityEngine.Random.Range(map_width * -0.5f, map_width * 0.5f);
			float z = UnityEngine.Random.Range(map_width * -0.5f, map_width * 0.5f);
			gameList[i].pos = new Vector3(x, y, z);
			
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

	public GameObject Ground;
	public List<GameObject> Grounds = new List<GameObject>();
	public float min_Distance = 30f;

	public GameObject infoWindow;
	public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
		crawler = new TwitchCrawler();
		crawler.crawling();
		crawler.getRatio();
		
		for (int i = 0; i < crawler.gameList.Count; i++)
        {
			status.Add(new List<GameObject>());
			
			//initiallize Ground(spaceship)
			crawler.gameList[i].Ground = Instantiate(Ground, crawler.gameList[i].pos, Quaternion.identity);
			crawler.gameList[i].Ground.transform.localScale += new Vector3(crawler.gameList[i].radius, crawler.gameList[i].radius, crawler.gameList[i].radius);

			GameObject GroundBody = crawler.gameList[i].Ground.transform.GetChild(0).gameObject;
			GroundBody.GetComponent<Renderer>().materials[0].color = crawler.gameList[i].color;
			crawler.gameList[i].GroundRigid = crawler.gameList[i].Ground.GetComponent<Rigidbody>();

			//initialize InfoWindow
			crawler.gameList[i].InfoWindow = Instantiate(infoWindow, crawler.gameList[i].pos + new Vector3(0, crawler.gameList[i].radius, 0), Quaternion.identity);
			crawler.gameList[i].InfoWindow.transform.rotation = Player.transform.rotation;

			crawler.gameList[i].InfoWindow.transform.SetParent(crawler.gameList[i].Ground.transform, true);
			crawler.gameList[i].InfoWindow.transform.position += new Vector3(0, crawler.gameList[i].radius + 10f, 0);

			// info - title

			crawler.gameList[i].InfoWindow.transform.GetChild(0).GetComponent<TextMesh>().text
				= crawler.gameList[i].name.Length > 18 ? crawler.gameList[i].name.Substring(0, 18) + "..." : crawler.gameList[i].name;
			// info - Watching num
			crawler.gameList[i].InfoWindow.transform.GetChild(2).GetComponent<TextMesh>().text = (crawler.gameList[i].viewNum).ToString();
			// info - rank
			crawler.gameList[i].InfoWindow.transform.GetChild(4).GetComponent<TextMesh>().text = (i + 1).ToString();

			//initialize Astro
			for (int j = 0; j < crawler.gameList[i].objectNum; j++)
            {
				float x = crawler.gameList[i].pos.x + UnityEngine.Random.Range(crawler.gameList[i].radius * -1.5f, crawler.gameList[i].radius * 1.5f);
				float y = crawler.gameList[i].pos.y + 0.2f;
				float z = crawler.gameList[i].pos.z + UnityEngine.Random.Range(crawler.gameList[i].radius * -1.5f, crawler.gameList[i].radius * 1.5f);
				GameObject newPrefab = Instantiate(projectile, new Vector3(x, y, z), Quaternion.identity);

				//status[i].Add(newPrefab);
				newPrefab.transform.SetParent(crawler.gameList[i].Ground.transform, true);

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
		// Move SpaceShip
		for (int i = 0; i < crawler.gameList.Count; i++)
        {
			crawler.gameList[i].Ground.transform.position += crawler.gameList[i].dir / 60;
			crawler.gameList[i].InfoWindow.transform.rotation = Player.transform.rotation;
			
			for(int j = 0; j < crawler.gameList.Count; j++)
            {
				if (i != j)
                {
					if ((crawler.gameList[i].radius + crawler.gameList[j].radius) * 2 > Vector3.Distance(crawler.gameList[i].Ground.transform.position, crawler.gameList[j].Ground.transform.position))
					{
						if (!crawler.gameList[i].isFalling)
						{
							crawler.gameList[i].dir = crawler.gameList[i].Ground.transform.position - crawler.gameList[j].Ground.transform.position;
							crawler.gameList[i].dir /= 60f;
							crawler.gameList[i].isFalling = true;
						}
					}
					else crawler.gameList[i].isFalling = false;
                }
            }
			
		}

	}
}
