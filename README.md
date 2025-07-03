# 项目代码规范

本文档为 Unity 2D 游戏项目的代码规范，旨在统一团队编码风格、提升可维护性并减少沟通成本。

---

## 1. 目录结构约定

```
YourUnityGame/
├── Assets/                  # 源代码与资源
│   ├── _Scripts/            # C# 脚本文件 -- 简单项目随便放
│   │   ├── Managers/        # GameManager、AudioManager 等
│   │   ├── Behaviours/      # HeroController、EnemyController 等
│   │   ├── UI/              # 所有 UI 相关脚本
│   │   ├── Utils/           # 非常通用方法
│   ├── Textures/            # 图片、图标、图集
│   ├── Prefabs/             # 预制件
│   └── Scenes/              # 场景
└── README.md                # 本文档
```

---

## 2. 命名规范

* **类名**：PascalCase（首字母大写），如 `GameManager`、`HeroController`。
* **方法名**：PascalCase（首字母大写），如 `InitializeGame()`、`OnPlayerDeath()`。
* **字段／属性**：（首字母小写），如 `camelCase`
* **常量**：全大写加下划线，如 `private const float MOVE_SPEED = 5f;`
* **接口**：首字母 I 开头，如 `IEnemyBehavior`。
* **枚举**：PascalCase，如 `public enum GameState { Start, Playing, GameOver }`。
* **事件**：以 `On` 前缀，如 `public event Action OnGameStart;`
* **脚本文件名**：与类名一致，如 `HeroBehavior.cs`。

---

## 3. 注释与文档

* 公开方法、属性、事件使用 XML 注释：

  ```csharp
  /// <summary>
  /// 初始化游戏管理器
  /// </summary>
  public void InitializeGame() { ... }
  ```
* 复杂逻辑或算法注释需写清目的与思路。
* TODO、FIXME 标签标记尚未完成或待修复部分：

  ```csharp
  // TODO: 增加对象池逻辑
  // FIXME: 修复子弹碰撞判定漏判问题
  ```

---

## 4. 代码风格

* 缩进：使用 4 个空格。
* 每行不超过 120 字符。
* 大括号风格：Allmen 风格

  ```csharp
  if (condition)
  {
      DoSomething();
  }
  else
  {
      DoOther();
  }
  ```
* 空行：

  * 方法之间保留一行空行。
  * 类成员（字段、属性、方法）之间保留一行。

---

## 5. Unity 特殊约定

* **资源引用**：所有 `SerializeField` 字段必须为 `private`，并在 Inspector 中赋值：

  ```csharp
  [SerializeField] private Text _scoreText;
  ```
* **UI 管理**：所有 UI 文案、数值更新逻辑集中在 `UIManager` 或 `GameManager` 中，不在单个面板脚本中管理场景数据。

---

## 6. 分支与提交规范

* **分支模型**：

  * `master`：稳定版本，每次发布都需打 Tag。
  * `develop`：开发主干，合并 feature 分支。
  * `feature/xxx`：功能分支，如 `feature/score-system`。
* **Pull Request**：

  * 标题格式：`[feature] 添加积分系统`。
  * 描述：写明改动内容、测试情况、关联 Issue。

---

*Nobodiiiii*
*2025-07-03*
