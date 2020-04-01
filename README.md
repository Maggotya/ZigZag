Проект выполнен в **Unity 2019.3.6f1** с использованием **URP**.
Использованы дополнительные пакеты: **Cinemachine**, **TextMeshPro** и **Zenject**.

Делал параллельно с другими проектами, поэтому возможно где-то единый стиль не выдержан. Но я старался )

Проект содержит одну сцену - *Assets/_Game/Scenes/MainScene.unity*.

Вёрстка и настройки параметров игры были выставлены для *портретного режима отображения*. Тем не менее, в альбомном ничего страшного не случится.

Все настройки непосредственно игры хранятся в папке *ScriptableObjects*.

### GameManager:
Главный класс. Он обеспечивает всю игровую логику и обеспечивает взаимодействие между компонентами.

### InputManager:
Соответственно, контроллирует ввод. Имеет отдельно вынесенные параметры в ScriptableObject'е. Понимает нажатие клавиш, клики мыши и касания сенсорного экрана.
Взаимодействует непосредственно с шаром на поле, но можно было и пустить взаимодействие через GameManager, что, возможно, в перспективе было бы правильнее.

### UI:
Набор нескольких экранов, которые переключаются по командам из GameManager'а. Это Стартовый экран, Экран паузы, Экран завершения и Интерфейс самого геймплея.
Максимально простой скрипт, "на скорую руку" с прямыми ссылками - одноразовое решение.

### Camera:
Использует виртуальную камеру Cinemachine для плавного слежения за шариком.

### Ball: 
Сам шарик. Засчёт MoveAllower катится только по объектам, реализующим интерфейс ISurfaceToMove. Может менять траекторию. 
Так же использует класс для вычисления скорости - подерживает ускорение.

### FallArea:
Триггер-коллайдер, который засчёт PositionFollower следует за шариком. Закреплён по оси Y. При съезде шарика с платформы, ловит его и вызывает событие о том,
что шарик упал. Это вызывает завершение игры. Можно регулировать его Offset, тем самым регулируя момент срабатывания завершения игры.

### PlatformGenerator:
Компонент, который отвечает за создание платформ. Отслеживает движение шарика по платформам (за счёт событий от платформ), на основе его позиции
просчитывает позиции будущих платформ (дальность просчёта задаётся), очищает старые пройденные платформы за пределами экрана и генериурет новые платформы. 
Созданием и удалением платформ занимается IPlatformProducer. В проекте реализована простая версия. Можно так же реализовать версию с пулом объектов
и просто поменять реализацию интерфейса в классе.
Просчётом позиций для платформ занят IPositionCalculator.

### GemGenerator:
По сути аналог PlatformGenerator, но для кристаллов, которые рассталвяются на платформах. Здесь так же есть IGemProducer, который может быть реализован с пулом.
IProbabilitier определяет, можно ли ставить кристалл на очередную платформу по заданным правилам. Общение с PlatformGenerator происходит через подписки. 
Подумал, что будет правильнее разделить эти две логики. При этом BoosterHost всегда можно просто отклюить и удалить на случай, если кристаллы больше не понадобятся,
и не придётся лезть в код PlatformGenerator.

#### SaveManager:
Реализован обычным классом. С ним напрямую взаимодействует GameManager и "упаковывает" данные для будушего сохранения. Проект небольшой, поэтому посчитал уместным
не использовать класс сохранений ото всюду и не аккумулировать в него все даные для сохранений. 
Класс SaveData парсится в json, который записывается в PlayerPrefs.

> Спасибо, что дочитали :)
