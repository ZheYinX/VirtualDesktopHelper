# VirtualDesktopHelper

VirtualDesktopHelper 是一款使用C#语言编写的WinForms程序，专为Windows平台设计的虚拟桌面助手工具。该程序旨在帮助用户更高效地管理多个虚拟桌面，提升多任务处理的工作效率。程序的核心功能特点是提供便捷的虚拟桌面间窗口移动、跨桌面窗口固定以及智能的桌面切换功能，让用户能够灵活组织工作空间，避免单一桌面混乱的问题。

## 功能特性

- 使用可自定义热键将活动窗口移动到左或右虚拟桌面
- 支持窗口固定功能：将特定窗口固定到所有虚拟桌面，使其在切换桌面时依然可见
- 支持 Windows 10、Windows 11，目前在Windows11 25H2上测试通过
- 自动检测 Windows 版本以使用适当的虚拟桌面 API
- 直观的热键配置界面

## 安装

### 系统要求
- Windows 10 版本 1809 或更高版本，Windows 11
- .NET Framework 4.8
- 管理员权限（用于全局热键注册）

### 下载与设置
1. 从 [发布页面](https://github.com/ZheYinX/VirtualDesktopHelper/releases) 下载最新版本
2. 将压缩包解压到首选位置
3. 运行 `VirtualDesktopHelper.exe`
4. 通过设置界面配置所需的热键

### 源码构建
1. 克隆仓库：
   ```bash
   git clone https://github.com/ZheYinX/VirtualDesktopHelper.git
   ```
2. 在 Visual Studio 中打开解决方案
3. 还原 NuGet 包
4. 在发布模式下构建解决方案
5. 可执行文件将在 `bin/Release` 目录中生成

## 使用方法

1. 启动 VirtualDesktopHelper.exe
2. 单击设置图标以配置热键
3. 设置将窗口移动到左右虚拟桌面的热键
4. 按下配置的热键将活动窗口移动到相邻虚拟桌面

### 配置
应用程序将设置存储在应用程序目录中的 INI 文件中。如有需要，您可以手动编辑配置文件。

## 贡献

我们欢迎各种贡献以改进 VirtualDesktopHelper！要参与贡献：

1. Fork 此仓库
2. 创建功能分支 (`git checkout -b feature/amazing-feature`)
3. 提交您的更改 (`git commit -m 'Add some amazing feature'`)
4. 推送到分支 (`git push origin feature/amazing-feature`)
5. 打开拉取请求

### 开发指南
- 遵循现有的代码风格和约定
- 编写清晰的提交消息
- 在提交前充分测试更改
- 如有需要请更新文档

### 报告问题
请通过 GitHub 问题跟踪器报告错误或功能请求。请包含：
- Windows 版本和构建号
- 复现问题的步骤
- 期望行为与实际行为
- 任何错误消息或日志

## 许可证

该项目根据 MIT 许可证授权 - 详见 [LICENSE](LICENSE) 文件了解详情。

## 致谢

此项目基于 Markus Scholtes 的 [VirtualDesktop](https://github.com/MScholtes/VirtualDesktop/) 项目的优秀工作。不同 Windows 版本的虚拟桌面 API 实现在 MIT 许可证下改编自该项目。

## 作者

**哲隐** (ZheYinX)

## 支持

如果您遇到问题或有疑问：
- 查看 [问题](https://github.com/ZheYinX/VirtualDesktopHelper/issues) 部分
- 如果您的问题未被解决，请创建新问题
- 欢迎为错误修复或功能增强提交拉取请求