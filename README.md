# Withrium

  - 2023서일대 팀 Withrium 졸업작품
  - 웹과 함께 진행한 프로젝트
  - 거리에 제약이 없는 커뮤니케이션 서비스 제공
  - 개발 기간 ( 2023.01.01 ~ 2023.10.11 )

## 개요 

 Withrium에서는 최대 20명까지 지원되는 멀티환경이 구축되어 있습니다. 이러한 환경에서 사용자는 다양한 사람과 자유롭게 소통할 수 있고, 
 또한 같은 관심사를 가진 사람들끼리 방을 만들어 소통할 수 있습니다. 각 월드에서는 웹 페이지 기능을 사용하여 검색 및 영상 시청을 할 수 있고, 그림판 기능을 사용하여 필기 및 그림을 그려 기록을 남길 수도 있습니다. 또한 월드 내에는 사용자와 상호작용이 가능한 오브젝트들도 있기 때문에
 사용자들에게 즐거움을 더해줍니다. </br>
 이렇게 다양한 활동이 가능한 Withrium에서 여러 사람들과 공간의 제약없이 만남을 즐겨보세요. 

## 개발 동기 

  - SNS만으로는 원활한 소통의 한계가 존재한다.
  - 오프라인 소통을 할 때는 서로 지역이 달라 만나기 힘든 경우가 있고, 시간상의 문제도 존재한다.
  - 따라서 공간의 제약을 줄이면서 시간문제도 해결할 수 있는 커뮤니케이션이 있으면 좋겠다고 생각했습니다.

## 개발 인원

  ### 박준 ( https://github.com/parkjun-0521 )
  - Photon을 활용하여 멀티 환경을 구축
  - DB와 통신 가능한 백엔드 구축 : https://github.com/parkjun-0521/GraduatedProject-Backserver-
  - DB에서 유저정보를 받아와 유니티에서 처리하는 모든 작업 ( 로그인 및 팀 정보, 팀 방 입장 및 생성 )
  - 캐릭터 움직임 및 카메라 시점 동기화
  - 실시간 채팅 시스템과 음성 채팅 구현
  - 로비 맵과 공개방( 광장맵 ) 디자인 및 제작
  - 유니티 UI 및 옵션 제작
  
  ### 강형준 (https://github.com/Kanghyoengjun)
  - 플레이어 이동 및 점프 구현
  - 맵 및 오브젝트 제작
  - 캐릭터,오브젝트 애니메이션 제작 
  - 캐릭터 아바타 디자인 및 제작 
    
## 기술 스택 

  - VS Code 
  - 3Ds Max
  - Unity ( 2021.3.8f1 )

## 개발 언어 

  - C#
  - JavaScript ( 웹뷰 )

## 개발 일정

  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%9D%BC%EC%A0%95_1.png" alt="Image Error" width="75%" height="80%" /><img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%9D%BC%EC%A0%95_2.png" alt="Image Error" width="16%" height="16%" />
  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%9D%BC%EC%A0%95_3.png" alt="Image Error" width="78%" height="80%" />

  - 이후 7월 ~ 10월 기간은 추가적인 기능 구현과 버그 수정 및 테스트 기간

## 구성 화면

  ### 로그인 화면
  
  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%BA%A1%EC%B2%981.PNG" alt="Image Error" width="20%" height="10%" />
  
  - 실행하였을 때 나오는 로그인 화면
  - 다른 프로그램들과 동일하게 Tap을 누르면 아래로 이동할 수 있도록 구현
  - 계정이 없을 경우 회원가입 버튼을 클릭하면 홈페이지의 회원가입 화면이 열리도록 구현
  - 회원가입을 직접 구현할 수 있었지만 홈페이지의 비중을 높이기 위해 직접 구현하지 않음

  ### 로딩
  
  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EB%A1%9C%EB%94%A9.png" alt="Image Error" width="40%" height="50%" />

  - 화면 전환이 발생하는 경우마다 등장하는 로딩화면

  ### 로비
  
  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%ED%8F%AC%ED%83%88.PNG" alt="Image Error" width="40%" height="50%" /> <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EB%A1%9C%EB%B9%84.PNG" alt="Image Error" width="40%" height="50%" />

  - 로그인 후 가장 처음 보이는 화면
  - 왼쪽의 사진은 방을 입장하기 위한 포탈, 상호작용 버튼 없이 포탈에 접근하면 입장하기 위한 UI가 등장한다. 
  - 오른쪽 사진은 로비에 있는 거울과 웹뷰 기능, 웹뷰 기능을 사용하여 인게임 내에서도 팀을 이룰 수 있도록 한다.
  - 사진에는 없지만 이동키, 화면전환, 상호작용 키의 알림판이 있어 사용자의 불편함을 줄임

  ### 포탈 입장시 

  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%BA%90%EB%A6%AD%ED%84%B0_%EC%84%A0%ED%83%9D.PNG" alt="Image Error" width="40%" height="50%" /> <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%95%84%EB%B0%94%ED%83%80.PNG" alt="Image Error" width="40%" height="50%" />

  - 3개의 포탈에 입장시 공통으로 등장하는 UI
  - 왼쪽 사진은 캐릭터 선택 UI, 남여 캐릭터와 이벤트 캐릭터가 있고, 이벤트 캐릭터는 홈페이지에서 구매 하면 열리는 방식
  - 오른쪽 사진은 아바타를 적용한 모습, 홈페이지에서 아바타를 구매하면 유니티에서 아바타를 적용할 수 있는 기능
  - 캐릭터 선택 시 자동으로 아바타 선택창으로 이동하도록 구현하여 사용자의 불편함을 줄임

  ### 공개방 맵

  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EB%B4%84.PNG" alt="Image Error" width="40%" height="50%" /> <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%97%AC%EB%A6%84.PNG" alt="Image Error" width="40%" height="50%" /> <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EA%B0%80%EC%9D%84.PNG" alt="Image Error" width="40%" height="50%" /> <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EA%B2%A8%EC%9A%B8.PNG" alt="Image Error" width="40%" height="50%" />

  - 공개방 포탈에서 캐릭터 선택과 아바타 선택을 마친 후 등장하는 맵 선택 UI
  - 맵은 같은 구조지만 테마가 다른 4개의 공개방 맵
  - 팀과 상관없이 모든 사람이 입장 가능
  - 공개방은 최대 20명까지 멀티가 되도록 구현
  - 맵을 클릭하면 바로 방에 입장되도록 구현

  ### 팀 생성 맵

  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%B9%B4%ED%8E%98.PNG" alt="Image Error" width="40%" height="50%" /> <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%82%AC%EB%AC%B4%EC%8B%A4.PNG" alt="Image Error" width="40%" height="50%" /> <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EB%B0%A9.PNG" alt="Image Error" width="40%" height="50%" /> <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EB%8F%84%EC%84%9C%EA%B4%80.PNG" alt="Image Error" width="40%" height="50%" />

  - 팀 생성 포탈에서 캐릭터 선택과 아바타 선택을 마친 후 등장하는 맵 선택 UI
  - 서로다른 테마의 4개의 팀 방이고, 이 맵의 경우 팀장이거나 팀에 속해있는 경우에만 들어갈 수 있는 방이다.
  - 팀 방은 최대 8명까지 멀티가 되도록 구현
  - 맵을 클릭하면 바로 방에 입장되도록 구현
    
  ### 팀 방 생성

  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%ED%8C%80%EB%B0%A9.PNG" alt="Image Error" width="40%" height="50%" /> 

  - 팀 생성 포탈에서 맵을 선택한 후 등장하는 UI
  - 홈페이지에서 팀을 생성하면 그 정보를 받아 한페이지 최대 10개씩 팀 정보를 띄워준다.
  - 10개 초과시 다음 버튼을 활용하여 다음 팀을 볼 수 있다.
  - 팀 이름 버튼을 누를 시 그에 해당하는 팀 방을 만듦
    
  ### 팀 방 입장 

  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%ED%8C%80%EB%B0%A9%EC%9E%85%EC%9E%A5.PNG" alt="Image Error" width="40%" height="50%" />

  - 팀 방 입장 포탈에서 아바타 선택 후 등장하는 UI
  - 홈페이지에서 가입한 팀의 정보를 받아 한페이지에 퇴대 10개씩 가입한 팀 정보를 띄워준다.
  - 10개 초과시 다음 버튼을 활용하여 다음 팀을 볼 수 있다.
  - 팀 이름 버튼을 누를 시 그에 해당하는 팀 방으로 입장 

  ### 방 입장 후

  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%B1%84%ED%8C%85.PNG" alt="Image Error" width="28%" height="20%" /> <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EA%B7%B8%EB%A6%BC%ED%8C%90.PNG" alt="Image Error" width="40%" height="50%" />  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EB%A9%80%ED%8B%B0_%EC%A7%84%ED%96%89.PNG" alt="Image Error" width="60%" height="60%" />

  - 방 입장후 이루어지는 이벤트 
  - 실시간 채팅 기능과 음성 채팅 기능을 제공
  - 그림판의 경우 명암과 굵기 조절이 가능하고 3가지의 색을 이용하여 필기또는 그림을 그릴 수 있다. 
  - 밑 사진에서의 오른쪽 상단 UI 3개는 각 그림판, 옵션, 방 나가기 버튼 UI
  - 팀장이 팀을 만들고 방을 나갔을 경우에 모든 팀원이 방에서 나가지도록 구현
  
  ### 웹 뷰 

  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%9B%B9%EB%B7%B0.PNG" alt="Image Error" width="40%" height="50%" />

  - 웹에서 원하는 정보를 검색할 수 있고 영상 시청도 가능하다.
  - 오른쪽 상단의 앞,뒤로가기 버튼을 활용하여 원하는 페이지로 돌아갈수 있다.
  - 웹뷰 기능의 경우 팀방에서만 사용할 수있도록 구현 

  ### 옵션 

  <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%98%B5%EC%85%98.PNG" alt="Image Error" width="40%" height="50%" /> <img src="https://github.com/parkjun-0521/GraduatedProject/blob/main/image/%EC%82%AC%EC%9A%B4%EB%93%9C%EC%98%B5%EC%85%98.PNG" alt="Image Error" width="40%" height="50%" /> 

  - 모든 맵에서 ESC를 누를 시 등장하는 옵션 UI
  - 소리 버튼을 누르면 오른쪽과 같은 UI가 등장
  - 배경음악을 조절하거나 마이크, 헤드셋을 조절할 수 있다.
  - 헤드셋을 차단할 경우 마이크도 같이 차단되도록 구현하
  - 로그아웃 및 종료 버튼은 누를 시 즉시 로그아웃 되면서 게임이 종료되도록 구현
  
## 추가적인 기능

  - 캐릭터 구별을 위해 닉네임 띄우기
  - 아바타 추가 및 아바타 동기화
  - 이벤트 캐릭터 추가 ( 홈페이지에서 상점 구매로 열리는 캐릭터 )

## 개발 실패 사항과 이유 
  
  ### 웹뷰 동기화
  - Photon View RPC 동기화를 이용하여 모든 플레이어가 하나의 웹뷰를 사용하여 서로 같은 화면을 보는것을 목표로 잡고 개발을 시작함 
  - Vupelx 에셋을 활용하여 개발을 하였고 현재 당시 Vuplex에서는 동기화 기능을 지원하지 않기에 직접 구현을 해야하는 상황
  - 에셋에서는 RPC 동기화를 할 수 없게 해둔 상태였고 캡쳐를 이용한 동기화 방식을 해보았지만 개발 지식이 부족하여 실패 
  ### 키 설정 
  - 캐릭터의 이동 방식이 InputManager의 Axes에서 정해진 키값으로 이동하는 방식으로 만들어짐
  - InputManager로 값을 줄 시 스크립트로는 Input의 값을 바꿀 수 없음
  - 따라서 이동 로직을 전부 뜯어 고쳐야 하는 상황이 발생
  - 졸업잘품 전시까지 시간이 부족하여 해당 기능을 구현하지 못함
