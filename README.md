# Joymoji

## 1) 기획의도
이모지는 자신의 감정을 표현하는 수단으로 각종 sns에서 사용되고 있습니다. 이제 이모지는 2D뿐 아니라 3D로 더욱 역동적이고 다양한 감정을 표현할 수 있게 되었습니다. 사용자는 자신의 감정을 이모지에 담아 나만의 이모지를 만듭니다.  저희는 보다 많은 사람들이 사용자의 동작을 따라하는 body tracking emoji를 개발하고자 했습니다. 주어진 예시에서 이모지를 선택하여 사용하는 것이 아닌, '나'를 중심으로 자신의 동작을 따라하는 나만의 이모지에 게임성을 더했습니다.

## 2) 작품 설명
Joymoji는 사용자의 몸을 움직이며 만드는 '나만의 바디 이모티콘 프로그램'입니다. Kinect라는 카메라가 사용자의 전신을 감지하면 그 움직임에 따라 직접 선택한 캐릭터가 움직입니다. 캐릭터와 움직이는 배경은 3D로 제작되었으며, 사용자가 모두 선택할 수 있습니다. GIF, 이미지 형태로 다양하게 만들어집니다. 이렇게 만들어진 나만의 바디 이모티콘은사용자의 휴대폰으로 접근해 저장하거나 공유할 수 있습니다.

## 3) 시스템 아키텍쳐
![그림1](https://user-images.githubusercontent.com/31615715/66995363-d742f500-f109-11e9-80ef-b1de54401f3d.png)

## 4) 동작 예시
<img width=400 src="https://user-images.githubusercontent.com/31615715/66995457-035e7600-f10a-11e9-990c-2dd28b88cf72.PNG"><img width=400 src="https://user-images.githubusercontent.com/31615715/66995586-3d2f7c80-f10a-11e9-8671-ddb9aed35420.PNG">
<img width=400 src="https://user-images.githubusercontent.com/31615715/66995676-5e906880-f10a-11e9-9a99-397168006234.PNG"><img width=400 src="https://user-images.githubusercontent.com/31615715/66995725-723bcf00-f10a-11e9-809e-1860263aee3e.PNG">


## Library 
* [Kinect-Inputmodule](https://github.com/paganini24/Kinect-Inputmodule) : Hand cursor 감지 및 사용
* [getsocial-capture](https://github.com/getsocial-im/getsocial-capture) : gif 확장자를 이용한 result 파일 생성
* [Kinect SDK](https://www.microsoft.com/en-us/kinectforwindows/develop/default.aspx) : kinect 2.0 Unity SDK
