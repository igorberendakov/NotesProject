# NotesProject

## Для запуска сервера(backend) нужно:
## 1) изменить NotesDbConnection в .\Backend\NotesApp.WebApi\appsettings.Development.json в соответствии с сервером БД
## 2) изменить .\Backend\NotesApp.WebApi\Properties\launchSettings.json - 
## установить для https профиля переменные: "launchUrl": "api/swagger" и "applicationUrl": "https://localhost:3001;http://localhost:3000"
## или установить константу baseApiUrl в файле .\Frontend\src\environment.ts равным значею applicationUrl из launchSettings.json
## 3) выполнить миграцию для проекта NotesApp.Infrastructure с помощью команды update-database
## 4) выполнить сборку решения NotesApp и запустить
## 5) запуск angular-приложения: в терминале перейти в папку .\frontend
## 6) выполнить команду npm i, после завершения установки выполнить npm start