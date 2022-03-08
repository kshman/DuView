
# DuView
DuView has English UI resources. But, no English manual yet. Sorry 😭

This application is for reading a zipped book or stored images in folder as a book. Only used .NET6 WinForm. You can use freely whole resources as your wish.



# 두뷰(DuView)
*이미지 책*을 보는 프로그램입니다. (그니깐 **만화뷰어**네요)

*두부*가 아닙니다.... **두뷰**입니다. 😭
아니 뭐 *두부*라고 해도 괜찮습니다. 무슨 차이가 있겠슴요

꿀뷰3라는 쩌는 뷰어가 있는데도 만들어 본 것은, 딴거 안쓰고 윈폼 기능만 사용해서 만들 수 있을까...해서 시작해봤습니다. 그리고, 꿀뷰3에서 자꾸 INS 키가 눌려서 쓸데없이 저장된 파일이 너무 많아, 이 기능이 없는 뷰어가 필요했습니다. 🤣

게다가, 꿀뷰3는 논오픈소스고, 다른 프리웨어/오픈소스 뷰어는 윈도우에서 한번 컴파일 하려면 진짜... 짜증납니다... 솔루션 파일 하나로 msdev로 빌드할 수 있는 그런 환경이 필요해서... 만들었습니다.

이미지 형식은 윈폼에서 지원하는 거는 다 될겁니다. GIF는 현재 해볼라고 노력중이고, WEBP는 윈폼에서 지원하지 않아서 안됩니다.

## 지원 로캘
* 한국말
* English

## 지원 파일 형식
* 디렉터리
* 압축파일
	* ZIP
* 이미지 (단독 열기 안됨)
	* JPG
	* PNG
	* GIF (애니메이션 안됨)
	* BMP

## 사용키 
|키|명령|보조키|
|----------------|-------------------------------|----------------|
|F3/F4|책 열기 / 닫기|CTRL+W(닫기)
|CTRL+C|이미지 복사||
|F|화면 최대화|CTRL+ENTER|
|↑|한장 앞으로|,|
|↓|한장 뒤로|./|
|←|앞 페이지로||
|→|뒷 페이지로|Num0|
|Home|첫번째 페이지로||
|End|마지막 페이지로||
|PgUp|10장 앞 페이지로||
|PgDn|10장 뒷 페이지로|Back|
|Enter|페이지 선택||
|[|이름순 정렬로 앞 책|Browser Back|
|]|이름순 정렬 뒷 책|Browser Forward|
|DEL|열려 있는 파일 삭제||
|F2|열려 있는 파일 이름 바꾸기||
|F11|외부 프로그램으로 열어보기||
|F12|설정||
|ESC|끝내기||

***한장 앞뒤로**(↑↓)와 **앞뒤 페이지로**(←→)의 차이점은 두쪽을 나란히 볼 때 동작이 다른데, 앞뒤 페이지는 두쪽을 함께 넘기고 한장 앞뒤로는 무조건 한쪽만 넘어갑니다. 가끔 페이지 숫자가 뒤틀린 책이 있어 있는 기능입니다*


## 설정
설정 기능이 들어갔습니다. 아직 별건 없고, 그냥 응용프로그램에서 흔히 볼 수 있는 기능입니다. 마우스 쪽에 어떤 분들은 잘 안쓰는 클릭으로 페이지 변경 켬/끔을 넣어서 이건 쓸만하겠네요

![image](https://user-images.githubusercontent.com/7216647/151709401-facc531f-2f19-457c-a1f1-f8e5a681c976.png)
|이름|설명|
|----------------|-------------------------------|
|하나의 인스턴스만 실행|말 그대로 하나만 실행합니다|
|ESC 키로 프로그램 종료|말 그대로 ESC로 끌 수 있습니다. 실수로 누를때 꺼지는거 방지|
|윈도우즈 알림 기능 사용|원래 윈도우 알림을 썼는데, 현재 이걸 끄면 알림이 안나옵니다|
|자석 윈도우 사용|창 크기 변경/이동할 때 자석으로 딱 달라 붙는거 켬/끔입니다|
|파일을 지울때 확인|말 그대로 삭제 확인으로 묻습니다|
|프로그램을 맨 위에 보이기|말 그대로 다른 창보다 위에 띄워줍니다|
|업데이트가 있으면 알려주기|안됩니다... 아직 안만들었어요|

![image](https://user-images.githubusercontent.com/7216647/151709518-952b7908-3135-485f-b2bb-655fbce929a6.png)
|이름|설명|
|----------------|-------------------------------|
|두번 눌러 최대화/최소화...|책을 열었을 때만 해당합니다. 안열었으면 책 열기 창이 나옵니다|
|마우스 버튼으로 이전/다음...|창 왼쪽 부근을 누르면 이전 페이지, 오른쪽 부근은 다음 페이지로 이동합니다|
