ClockForge — Аналоговые часы на 7 языках
ClockForge — коллекция из семи независимых реализаций аналоговых часов с графическим интерфейсом. Каждая версия работает на своём языке программирования и показывает текущее время с движущимися стрелками, а также предоставляет настраиваемые параметры отображения.

✨ Общие возможности
🕐 Аналоговый циферблат с часовыми метками и цифрами

⏱️ Секундная, минутная и часовая стрелки, плавно движущиеся

🎨 Настройка внешнего вида: выбор цвета циферблата, стрелок, фона

🔢 Отображение цифр (арабские или римские) или только метки

🖱️ Интерактивность: при клике на часы можно переключать цвет (опционально)

🌐 Интерфейсы:

Десктопные GUI: Python (Tkinter), Java (Swing), C# (WinForms)

Веб-приложения: JavaScript (Canvas), Go, Rust, PHP (сервер + клиент)

📋 Сравнение реализаций
Язык	Интерфейс	Настройка цвета	Римские цифры	Автообновление
Python	Tkinter GUI	✅ (кнопки)	✅	✅ (after)
JavaScript	Веб (Canvas)	✅ (кнопки)	✅	✅ (setInterval)
Go	Веб (сервер)	✅ (кнопки)	✅	✅ (клиент)
Rust	Веб (сервер)	✅ (кнопки)	✅	✅ (клиент)
Java	Swing GUI	✅ (кнопки)	✅	✅ (Timer)
C#	WinForms GUI	✅ (кнопки)	✅	✅ (Timer)
PHP	Веб (сервер)	✅ (кнопки)	✅	✅ (клиент)
🚀 Быстрый старт
Python
bash
# Tkinter встроен
python clock.py
JavaScript (браузер)
Откройте clock.html в браузере.

Go
bash
go run clock.go
# Откройте http://localhost:8080
Rust
bash
cargo run
# Откройте http://localhost:8000
Java
bash
javac AnalogClock.java && java AnalogClock
C#
bash
csc /reference:System.Windows.Forms.dll /reference:System.Drawing.dll AnalogClock.cs
AnalogClock.exe
PHP
bash
php -S localhost:8000
# Откройте http://localhost:8000/clock.php
