# FuckMeeting+

FuckMeeting+ 是 [FuckTencentMeeting](https://github.com/Yoroion/FuckTencentMeeting) 的 GUI 重構版，是基於 .NET 8.0 的極簡 WPF 應用，用於自動加入騰訊會議，下文簡稱為 **FM+**

## 特性

- 騰訊會議定時自動入會
- Fluent Design
- 自帶 .NET 桌面運行時 (可選)
- MVVM [^1]

[^1]: 由 Microsoft Community Toolkit 提供支持，結構比較簡單，但也算是 MVVM 吧...

## 下載 FM+

下載連結在 [ClickOnce](https://clickonce.miaostay.com/FuckMeetingPlus/Publish.html) 中

## 截圖展示

![DEMO](./screenshots/NewDemo.png)

## 使用教學課程

最新的 **2024.03.05.7** 版本採用了騰訊會議的 URL 連結啟動會議，不再需要螢幕座標

1. 填寫騰訊會議號碼

2. 填寫符合格式的預定時間，年/月/日/時/分，如 `2024/03/05/09/00`

3. 儲存配置

4. 盡情享用，開始摸魚！

## 注意事項

- 點選 ⌈開始摸魚⌋ 按鈕後，如果您想要取消任務，點選右上角的 `×` 關閉即可，FM+ 不會進駐系統後台
- FM+ 檢查目前時間是否到達預定時間的週期為 15 秒

## 鳴謝

- [Microsoft.Toolkit.MVVM](https://github.com/CommunityToolkit/WindowsCommunityToolkit)
- [H.InputSimulater](https://github.com/HavenDV/H.InputSimulator)
- [WPF UI](https://github.com/lepoco/wpfui)
- [ReSharper](https://www.jetbrains.com/resharper/)

以及所有支持本計畫的朋友，你們的 Star 將幫助 FM+ 這個超微小計畫走得更遠

## 開發環境需求

如果你想要自行修改 FM+ 或為 FM+ 貢獻程式碼，你需要安裝 Visual Studio 2022 及 .NET SDK 8.0

當然，如果你覺得寫得太💩或哪個地方需要改進，歡迎指出

## 協定

FM+ 基於 AGPL v3 協議開源，修改後需要保留原作者的版權信息，查看 [協議條款](./LICENSE.txt)