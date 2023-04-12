# Inventory Trade System MVC

![ScreenShot](https://raw.githubusercontent.com/LeoNick1/Images-for-repos/master/InventoryTradeSystem.png)

Реализация окна торговца с использованием инвентарей персонажей.
Данная реализация создавалась 
в рамках выполнения тестового задания на позицию Unity-разработчика.

Проект выполнен в версии Unity 2020.3.44f1.

Видеообзор: https://www.youtube.com/watch?v=1bZQ2WtBANw
## Требования тестового задания:

Реализовать экран торговца, состоящий из 3х частей:
слева сетка предметов игрока, справа сетка предметов  
торговца, посередине  отображено количество золота игрока.

При этом:
- предметы имеют заданную стоимость;
- предметы можно покупать и продавать Drag’n’Drop;
- виджет количества золота должен быть обособлен;
- количество золота изменяется при покупке и продаже;
- чтобы купить предмет необходимо иметь достаточно золота;
- купленные предметы появляются у игрока;
- проданные предметы появляются у торговца;
- купленные предметы продаются дешевле цены покупки.

## Управление в игре:

Предметы перемещаются по слотам с помощью мыши.

- "S" - сохранить игру;
- "L" - загрузить игру;
- "N" - начать новую игру.

## Примечания:

При загрузке новой игры считываются данные из дефолтных SO-профилей  
в папке Assets\Project\Resources\DefaultProfiles.

По умолчанию игра сохраняется при выходе. Отменить сохранение  
можно через редактирование поля скрипта объекта "DataPersister" на сцене. 

Также можно начать новую игру, сохранить текущую или же загрузить сохраненную  
в рантайме с помощью соответствующих клавиш(см. раздел "управление в игре").

## Структура проекта:

Проект выполнен в рамках паттерна MVC.
Ниже перечислены ключевые скрипты(Assets\Project\Scripts).

GameStarter.cs - точка входа в приложение, здесь происходит инициализация игры.

### Персонажи:
Character.cs - класс персонажей в игре.  
CharacterBase.cs - база данных с персонажами.  
CharacterFactory - простая фабрика для создания персонажей.  
Owner.cs - enum, выполняющий роль id для персонажей.

### Предметы:
Item.cs - основной класс предмета.  
ItemDataSO.cs - SO с информацией о предмете(имя, спрайт, id, стоимость).  
ItemDB - база дынных предметов.

### Инвентарь:
Inventory.cs - модель инвентаря персонажа. Хранение и перемещение предметов в слотах.  
InventoryView.cs - представление инвентаря со слотами. UI-класс, через который игрок  
манипулирует предметами в слотах.

### Торговая система:
TradeSystem.cs - модель торговой системы, здесь происходит подготовка и проведение сделок.  
TradeSystemView.cs - представление торгового окна, включает представление инвентарей,  
отображает информацию о предметах, также отображает виджет  
с количеством золота игрока.

### Банк:
Bank.cs - отвечает за проведение операций с золотом персонажей.

### Виджет количества золота игрока:
PlayerBalanceView.cs - представление баланса игрока. Обновляется  
через соответствующий контроллер из данных счета игрока.

### Система сохранений и загрузки:
GameData.cs - класс данных игры для сериализации.  
GamePersister.cs - загрузка и сохранение данных игры.  
JsonSaver.cs - класс сериализации данных в JSON-формат.  
SOProfileParser - загрузка дефолтных данных из SO и конвертация в формат GameData.

### Дефолтные значения игры:
DefaultCharProfileSO.cs - стартовые профили персонажей игры.  
DefaultItemInfo.cs - формат предметов для инвентаря стартового профиля.

## Использованные ассеты:
https://assetstore.unity.com/packages/2d/gui/icons/free-rpg-icons-111659,  
https://assetstore.unity.com/packages/2d/gui/icons/food-icons-pack-70018  - иконки предметов.  
https://assetstore.unity.com/packages/2d/gui/dark-brown-gui-kit-201086 - текстура слота инвентаря.  
https://assetstore.unity.com/packages/2d/gui/icons/gui-parts-159068 - фон торгового окна и его заголовка.
