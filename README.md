# Описание

IndieInject - легковесный DI фреймворк, который может спокойно использоваться в прототипах и в крупных проектах, благодаря тому, что его легко можно адаптировать под любую архитектуру.

# Возможности

- Core зависимости и Scene зависимости
- Инъекция при помощи рефлексии в методы, поля, свойства 
- Регистрация зависимостей посредством создания фабрик из методов, которые отмечены Provide атрибутом
- Singleton и Transient типы регистрации
- Опциональная автоматическая инъекция в начальные объекты на сцене
- Создание объектов с автоматической инъекцией, если создавать объекты через Indie.Fabric.Instantiate

# Логика

### Хранение
Зависимости **хранятся** в двух контейнерах: для Core зависимостей, которые будут существовать весь жизненный цикл игры и для сцен, которые очищаются сразу после отгрузки сцены.
### Регистрация
**Регистрация** зависимостей происходит путем создания фабрик из методов в провайдере, которые обозначены атрибутом [Provide].
### Типы выдачи
Есть два **типа выдачи** зависимости: Singleton и Transient. Первый будет создавать зависимость при ее регистрации и выдавать всегда именно этот экземпляр. Второй будет создавать при каждой инъекции новый экземпляр.
### Инъекция
**Инъецкция** реализована при помощи рефлексии. Можно делать инъекцию в поля, свойства, методы, которые обозначены атрибутом Inject.
### Как инициализируются зависимости
После того, как происходит регистрация всех зависимостей из провайдеров, то идет инициалиация синглтонов и их инъекция. В Transient зависимости инъекция происходит, когда новый экземпляр создан.

# API

- IDependencyProvider - интерфейс, по которому передаются провайдеры для регистрации
- [Provide(bool isSingleton)] - атрибут, который отображает, какой метод будет использован для регистрации, как фабрика зависимости
- MonoProvider - MonoBehavior, который отнаследован от IDependecyProvider. Его можно использовать для всех провайдеров
- Core/Scene Dependencies Root - они запускают регестрацию зависимостей. Все провайдеры должны быть расположены или как дочерние объекты для Root-а, либо на них
- SceneAutoInjector - если находится на сцене, то будет сделана инъекция, во все находящиеся на сцене объекты
- Indie.Injector.Inject() - инъекция в объект
- Indie.Fabric.Instantiate() - аналог Object.Instantiate, но с автоматической инъекцией
- Indie.Fabric.OnInstantiated - событие, которое вызывается при созднии объекта
- Indie.Fabric.OnGameObjectInstantiated - событие, которое вызывается при созднии GameObject
- [Inject] - показывает, в какой метод, поле, свойство нужно сделать инъекцию
- [InjectRegion(InjectRegion region)] - атрибут, который добавляется к классу для того, чтобы показать, в какую область нужно делать инъекцию. Если атрибута нет, то будет инъекция во все

# Как пользоваться

Рассмотрим, как можно использовать Indie Inject на примере, который приведен в папке Sample. Его можно скачать отдельно в релизах

Задачи:
- Регистрация InputService-а, как Core зависимость
- Регистрация префаба игрока, его рутового объекта, его конфига для конкретной сцены
- Инъекция в фабрику, которая изначально находится на сцене
- Создание игрока через фабрику и его инъекция

```cs
[Inject] private GameObject playerPrefab;
private Transform rootTransform;
        
[Inject]
private void Constructor(Transform rootTransformForSpawnHero)
{
    rootTransform = rootTransformForSpawnHero;
}
```

```cs
[Inject] private InputService InputService { get; set; }
[Inject] private HeroConfig HeroConfig { get; set; }
```

Для начала, нам нужно создать Entry Point для примера. Создадим новую сцену и назовем ее, например, StartPoint. Теперь нам нужно создать Dependency Root для Core зависимостей. Для автоматизации их создания мы сделали специальное меню, которое называется IndieInject. Там нужно нажать Create Entry Point. Мы хотим регистрировать InputService, поэтому создадим класс InputServiceProvider и отнаследуемся от MonoProvider-а.

```cs
public class InputServiceProvider : MonoProvider
{
    [Provide(true)]
    public InputService ProvideInputService() => new();
}
```

В этом классе мы объвляем метод ProvideInputService(), который возвращает просто новый экземпляр InputService, чтобы этот метод был использован как фабрика, мы его обозначили атрибутом [Provide] и указали, что это синглтон. Добавим этот компонент на CoreDependenciesRoot, настройка Entry Point завершена. Теперь нам нужно сделать автоматический переход в SampeScene.

Рассмотрим устройство фабрики игрока:
```cs
public class HeroFactory : MonoBehaviour
{
    // === Inject in field === //
    [Inject] private GameObject playerPrefab;
    private Transform rootTransform;

    // === Inject in method === //
    [Inject]
    private void Constructor(Transform rootTransformForSpawnHero)
    {
        rootTransform = rootTransformForSpawnHero;
    }

    private void Start()
    {
        // === Instantiate + Inject in new GameObject === //
        Indie.Fabric.Instantiate(playerPrefab, rootTransform);
    }
}
```

В ней мы должны получить префаб игрока и родительского контейнера. На старте мы создаем игрока через Indie.Fabric.Instantiate()

Рассмотрим игрока:
```cs
public class Hero : MonoBehaviour
{
    // === Inject in properties === //
    [Inject] private InputService InputService { get; set; }
    [Inject] private HeroConfig HeroConfig { get; set; }

    private Rigidbody heroRigidbody;

    private void Awake() => heroRigidbody = GetComponent<Rigidbody>();

    private void Update() { /*movement*/ }
}
```
В нем нам нужно получать InputService и HeroConfig. InputService мы уже зарегестрировали как Core зависимость. Теперь осталось зарегистрировать оставшиеся.

```cs
public sealed class HeroDataProvider : MonoProvider
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform rootTransformForHero;
    [SerializeField] private HeroConfig heroConfig;

    [Provide(true)]
    public GameObject ProvidePlayerPrefab() => playerPrefab;

    [Provide(true)]
    public HeroConfig ProvidePlayerConfig() => heroConfig;

    [Provide(true)]
    public Transform RootTransformForHero() => rootTransformForHero;
    
    private void OnValidate() { /*getting data from resources*/ }
}
```
Чтобы быстро создать SceneDependenciesRoot воспользуемся кнопкой Setup Scene в меню IndieInject. Добавим дочерний объект с компонентом HeroDataProvider и все!
