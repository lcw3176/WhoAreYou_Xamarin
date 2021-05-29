# WhoAreYou_Xamarin
Microprocessor TeamProject Mobile Application

### 기술 스택, 개발 환경
* C#, Xamarin Forms
* MVVM, Singleton
* minSdk: 23(6.0 Marshmallow)
* targetSdk: 29(10.0 Q)
* WebSocket, NewtonsoftJson, SHA-256 Encryption 
* Visual Studio 2019

### 작동 과정 및 페이지 설명
* 회원가입
    - 아이디와 비밀번호 서버로 전송, 중복 체크 후 승인
    - 비밀번호는 어플에서 SHA-256 암호화 후 전송

* 로그인(첫 접속)
    - 아이디와 비밀번호 서버로 전송, 비밀번호는 암호화 후 전송
    - 값이 유효하면 JWT 발급, App Property에 Token값 저장

* 로그인(두번째 접속 이상)
    - App Property에서 JWT 읽어온 후 서버에 Validation Check 요청
    - 유효한 Token이면 자동 로그인

* 기기 목록
    - 인덱스 번호 클릭 시 바로 기록 조회 가능

* 기록 보기
    - ListView에 열리고 닫힌 기록 표시됨

* 옵션 설정
    - Notification으로 어떤 종류의 알람이 울릴 지 설정
    - 로그아웃 시 Property의 Token 값 삭제

### 작동 모습 
- - -
#### 로그인, 회원가입, Notification 등록
<div>
    <image src="https://user-images.githubusercontent.com/59993347/118150242-4bac5a00-b44d-11eb-9dd6-7bfacefb3b39.jpg" width="30%">
    <image src="https://user-images.githubusercontent.com/59993347/118150244-4cdd8700-b44d-11eb-8951-2c26bb92f67c.jpg" width="30%">
    <image src="https://user-images.githubusercontent.com/59993347/118150249-4d761d80-b44d-11eb-98cb-1ee0bd1d0648.jpg" width="30%">
</div>

- - -
#### 기기목록, 기록보기, 옵션설정
<div>
    <image src="https://user-images.githubusercontent.com/59993347/118150246-4d761d80-b44d-11eb-863e-c654bcef47ed.jpg" width="30%">
    <image src="https://user-images.githubusercontent.com/59993347/118150254-4ea74a80-b44d-11eb-8520-ee037d3fb0b4.jpg" width="30%">
    <image src="https://user-images.githubusercontent.com/59993347/118150259-4f3fe100-b44d-11eb-9f1b-b15bc5daf4ce.jpg" width="30%">
</div>

- - -
#### 블루투스 연결, 와이파이 연결, 푸시 알람
<div>
    <image src="https://user-images.githubusercontent.com/59993347/120073825-f1f69180-c0d4-11eb-8f50-ecb28404cc93.jpg" width="30%">
    <image src="https://user-images.githubusercontent.com/59993347/120073828-f3c05500-c0d4-11eb-97e5-cf33fd05b72f.jpg" width="30%">
    <image src="https://user-images.githubusercontent.com/59993347/120073830-f4f18200-c0d4-11eb-8fda-bf48b1bef5d0.jpg" width="30%">
</div>