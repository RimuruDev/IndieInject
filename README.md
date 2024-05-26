# IndieInject

IndieInject — это легковесный и простой в использовании фреймворк для внедрения зависимостей (DI) в Unity, разработанный специально для небольших проектов и быстрых прототипов. Этот фреймворк идеально подходит для создания игр для платформ, таких как Yandex Games, Poki, Crazy Games, Google Play, Apple Store и Ru Store.

## Описание

IndieInject предоставляет базовые возможности для внедрения зависимостей в поля, свойства и методы, а также поддерживает инъекцию зависимостей при спавне объектов. Он разработан для использования в Unity и заточен под быструю разработку и расширяемость, что делает его идеальным выбором для небольших проектов и веб-игр.

## Возможности

- **Простота и легковесность:** Легко интегрируется и минимально влияет на производительность.
- **Базовое внедрение зависимостей:** Инъекция в поля, свойства и методы с помощью атрибута `[Inject]`.
- **Поддержка спавна объектов:** Внедрение зависимостей при создании объектов с помощью метода `InstantiateWithDependencies`.
- **Легкая интеграция с Unity:** Использование `MonoBehaviour` для управления жизненным циклом и зависимостями объектов.

## Установка

1. Склонируйте репозиторий и добавьте папку IndieInject в ваш проект Unity.

```sh
git clone https://github.com/RimuruDev/IndieInject.git
```
2. Или скачайте последний [Release](https://github.com/RimuruDev/IndieInject/releases) и перенесите файл `.package` в ваш проект Unity.

<img width="200" alt="image" src="https://github.com/RimuruDev/IndieInject/assets/85500556/7ff69f43-705a-4d0e-a640-6d8e331df6a0">

## Примеры использования
В этом примере: инекция конфига + героя + transform родителя для героя.

---
!!!! Подробнее лучше ознакомиться в `Sample.package` закрепленным к [Release](https://github.com/RimuruDev/IndieInject/releases). 

Откройте папку Samples далее папка _Scenes сцена SampleScene.unity

---

![IndieInject-Log](https://github.com/RimuruDev/IndieInject/assets/85500556/d961a5d2-443c-4bb1-b4e0-6ac5da263aba)


### 1. Настройка `SceneContext`

Создайте `SceneContext` для регистрации зависимостей:

```csharp
using UnityEngine;

public sealed class SceneContext : MonoBehaviour, IDependencyProvider
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform rootTransformForHero;
    [SerializeField] private HeroConfig heroConfig;

    [Provide]
    public GameObject ProvidePlayerPrefab() => playerPrefab;

    [Provide]
    public InputService ProvideInputService() => new();

    [Provide]
    public HeroConfig ProvidePlayerConfig() => heroConfig;

    [Provide]
    public Transform RootTransformForHero() => rootTransformForHero;
}
```

### 2. Пример с героем и управлением

#### InputService

```csharp
using UnityEngine;

public class InputService
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    
    public static Vector3 GetInput()
    {
        var horizontal = Input.GetAxis(Horizontal);
        var vertical = Input.GetAxis(Vertical);
      
        return new Vector3(horizontal, 0, vertical);
    }
}
```

#### HeroConfig

```csharp
using UnityEngine;

[CreateAssetMenu(fileName = nameof(HeroConfig), menuName = "Config/" + nameof(HeroConfig))]
public class HeroConfig : ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 50f;
}
```

#### Hero

```csharp
using UnityEngine;

public class Hero : MonoBehaviour
{
    [Inject] private InputService inputService;
    [Inject] private HeroConfig HeroConfig;
    
    private Rigidbody heroRigidbody;
    
    private void Awake() =>
        heroRigidbody = GetComponent<Rigidbody>();
    
    private void Update()
    {
        var input = InputService.GetInput();
        var movement = input * (HeroConfig.MoveSpeed * Time.deltaTime);
      
        heroRigidbody.MovePosition(transform.position + movement);
    }
}
```

#### HeroFactory

```csharp
using UnityEngine;

public class HeroFactory : MonoBehaviour
{
    // Inject in field!
    [Inject] private GameObject playerPrefab;
    
    private Transform rootTransform;
    
    // Inject in method!
    [Inject]
    private void Constructor(Transform rootTransformForSpawnHero) =>
        rootTransform = rootTransformForSpawnHero;
    
    private void Start() =>
        IndieObject.InstantiateWithDependencies(playerPrefab, rootTransform);
}
```

### 3. Интеграция с Unity Editor

#### Создание и удаление IndieInjector через меню редактора

Вы можете создать или удалить объект с компонентом `IndieInjector` через меню Unity:

<img width="200" alt="image" src="https://github.com/RimuruDev/IndieInject/assets/85500556/7ff69f43-705a-4d0e-a640-6d8e331df6a0">

### Лицензия

Этот проект лицензирован по лицензии MIT. Подробнее см. в файле [LICENSE](LICENSE).

---

Разработано RimuruDev

---

## TODO:
- Добавить WeakReference
- Написать Readme.md
- Добавить пример с CompositionRoot
- Добавить примеры с раздилением провайдеров на контексты
- Добавить ImmortalGameObject
- Добавить более надежный способ удаления зависимостей
- Покрыть тестами
