# ChilloutVR-LocalTest
ChilloutVR本地测试工具

## 功能
 - 跳过登录（在线功能全部停用）
 - 本地测试模型
 - 本地测试世界

## 安装方法
 - 首先，你需要为ChilloutVR安装上[MelonLoader](https://github.com/LavaGang/MelonLoader)模组加载器
 - 然后[下载工具文件](https://github.com/CMoyuer/ChilloutVR-LocalTest/releases/latest)
 - 将 **ChilloutVR-LocalTest.dll** 放到游戏的 **ChilloutVR/Mods** 目录下
 - 打开Unity，并安装 **ChilloutVR-LocalTest.unitypackage** 到你的工程里（需要提前安装好ChilloutVR SDK）
 - 安装完成
 
## 使用方法
### 模型
 - 打开ChilloutVR游戏
 - 点击Unity菜单中的 **"Moyuer -> CVR_LocalTest -> Avatar"** 选项，会出现插件窗口
 - 选择你想测试的模型，并点击 **"Start Test"** 按钮
 - 稍等一会，弹窗提示成功后，模型就加载到游戏里了
 
### 世界
 - 打开ChilloutVR游戏
 - 在Unity中，打开要测试的地图场景
 - 点击Unity菜单中的 **"Moyuer -> CVR_LocalTest -> World"** 选项
 - 稍等一会，弹窗提示成功后，游戏就自动加载进地图了
 
## 截图

![A](https://user-images.githubusercontent.com/51113234/181872735-acbc883c-8048-44ac-98f9-c373b3c72fea.png)

![B](https://user-images.githubusercontent.com/51113234/181878552-5ca782a9-cf49-4c61-be62-ee9fa6670fff.png)

### 版本更新

**V0.4 2022年8月8日**
 - 特别感谢NotAKidoS帮助完善以下功能
 - 添加“加载之前测试的Avatar”功能
 - 测试世界时要求有一个"CVR World"组件，以避免误触
 - 优化整理代码

**V0.3 2022年8月2日**
- 支持“跳过登录”功能（离线测试）
- 整理代码结构

**V0.2.1 2022年7月31日**
 - 修复了模型缩放不起作用的Bug
 - 将本地测试模型的Transform移动到在线模型同一位置下

**V0.2 2022年7月30日**
 - 支持本地测试地图功能

**V0.1 2022年7月29日**
 - 首次发布
 - 支持本地测试模型功能
