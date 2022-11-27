
# DuView
DuView has English UI resources. But, no English documents yet. Sorry 😭

This application is for reading a zipped book or stored images in folder as a book. Only used .NET6 WinForm. You can use freely whole resources as your wish.

---

# 두뷰(DuView)
*이미지 책*을 보는 프로그램이예요! (그니깐 **만화뷰어**라고 보면 되요)

*두부*가 아녜요, **두뷰**입니다. 그냥 편하게 *두부*라고 불러도 좋아요. 😭

오픈소스 뷰어가 없는거 같아 만들어 봤구요, 대충 보기에 좋아요. 지금 사용하고 있는 프레임워크는 .NET7인데 잘되는거 같아요. 
그리고 아직 GIF는 재생이 안되요. 윈폼에 관련 기능이 있는건 봤는데, 아니 손이 안가고 있어서... 이럴스가... 😥

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
    * WEBP (애니메이션 안됨)

## 사용키 
| 키      | 명령              | 보조키             |
|--------|-----------------|-----------------|
| F3/F4  | 책 열기 / 닫기       | CTRL+W(닫기)      
| CTRL+C | 이미지 복사          ||
| F      | 화면 최대화          | CTRL+ENTER      |
| ↑      | 한장 앞으로          | ,               |
| ↓      | 한장 뒤로           | ./              |
| ←      | 앞 페이지로          ||
| →      | 뒷 페이지로          | Num0            |
| Home   | 첫번째 페이지로        ||
| End    | 마지막 페이지로        ||
| PgUp   | 10장 앞 페이지로      ||
| PgDn   | 10장 뒷 페이지로      | Back            |
| Enter  | 페이지 선택          ||
| [      | 이름순 정렬로 앞 책     | Browser Back    |
| ]      | 이름순 정렬 뒷 책      | Browser Forward |
| DEL    | 열려 있는 파일 삭제     ||
| INS | 열려 있는 파일 이동     | Num+            |
| F2     | 열려 있는 파일 이름 바꾸기 ||
| F11    | 외부 프로그램으로 열어보기  ||
| F12    | 설정              ||
| ESC    | 끝내기             ||

***한장 앞뒤로**(↑↓)와 **앞뒤 페이지로**(←→)의 차이점은 두쪽을 나란히 볼 때 동작이 다른데, 앞뒤 페이지는 두쪽을 함께 넘기고 한장 앞뒤로는 무조건 한쪽만 넘어갑니다. 가끔 페이지 숫자가 뒤틀린 책이 있어 있는 기능입니다*


## 설정
***설정 기능**이 **갱신**되었는데 문서에 **추가를 안했어요***

열어 보시면... 대충 아실거예요