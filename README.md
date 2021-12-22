# 🛸Gamers Planet
Sogang University Arts & Technology  
2021-2 Media Arts Studio - Final Project

세상에는 정말 많은 게임이 있고, 그만큼 많은 플레이어들이 있습니다.  
하지만 플레이어들을 모두 **게임**이라는 공통 분모로 엮는 것은 조금 힘들 수 있습니다.  
왜냐하면, 오버워치를 플레이하는 저는 얼마나 많은 사람들이 에이펙스를 하고 있는지 모르니까요.  

그들을 하나의 세상으로 묶어볼 수는 없을까요?

[Demo Video](https://youtu.be/cLt6OAWKBGk)  
[<img width=500 alt="Demo Video" src="https://user-images.githubusercontent.com/31800284/147139189-7b25f45e-ddc7-4b88-ab5d-26c81dcddaee.png">](https://youtu.be/cLt6OAWKBGk)

</br>

## Usage
이 프로젝트는 크롬 웹 드라이버를 포함하고 있습니다. 크롬 브라우저를 최신 버전으로 업데이트하고, `GamersPlanet\Assets\StreamingAssets` 폴더의 `chromedriver.exe`를 [ChromeDriver](https://chromedriver.chromium.org/downloads)에서 최신 버전의 Driver를 받아 대체해주세요.

</br>

## 📝Contents
* 트위치로부터 시청자 상위 30개의 게임과 해당 게임의 시청자 수 정보를 크롤링합니다. 크롤링한 정보를 바탕으로 각각의 게임에 색을 부여해 시청자 수에 따라 우주선의 크기와 우주인의 수를 조정하여 공간에 매핑합니다. 즉, 시청자 수가 많을수록 커다란 우주선과 많은 우주인의 수를 가집니다.
* 사용자는 WASD키를 이용해 이동하고, 마우스를 이용해 시점을 조절합니다. W키로 현재 보고 있는 시점 방향으로 이동합니다.

</br>

## 💻Tech
* Unity 2019 프로젝트입니다. Instant를 생성하고 화면을 처리하는 과정은 전부 unity를 이용했습니다.
* 크롤링은 NuGet의 Selenium 패키지를 이용했습니다.

</br>

## 📦Asset
* 우주인 Prefab은 Unity Asset Store의 무료 에셋, [Stylized Astronaut](https://assetstore.unity.com/packages/3d/characters/humanoids/sci-fi/stylized-astronaut-114298)을 사용하였습니다.
* 우주선 모델링은 Blender를 사용하여 만들었습니다.
<img width=500 alt="modeling" src="https://user-images.githubusercontent.com/31800284/147138914-0bf9c555-f68c-4be4-b80a-4e0b85abd612.png">

</br>

## 🎞️Screenshot & Video
### Exercise 1까지의 경과
<img width=500 alt="image1" src="https://user-images.githubusercontent.com/31800284/136149333-bb66dbc5-c98c-4377-8fe2-82792cdb12da.PNG">
<img width=500 alt="image1" src="https://user-images.githubusercontent.com/31800284/136149601-e0129969-ee4f-4992-bbd6-2bc86c8ffc25.gif">
  
### Final Project에서의 결과물
영상은 [Demo Video](https://youtu.be/cLt6OAWKBGk) 링크를 참고해주세요.  
<img width=500 alt="image1" src="https://user-images.githubusercontent.com/31800284/147139443-567d5a97-a314-415c-8bc6-3d864c828dad.png">  
<img width=500 alt="image1" src="https://user-images.githubusercontent.com/31800284/147139455-baa3ffad-c15f-4913-8b13-cd672e76f775.png">  
<img width=500 alt="image1" src="https://user-images.githubusercontent.com/31800284/147139468-568d7296-d292-4f20-a87a-ed3735b4ba2c.png">  

