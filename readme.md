# DHTMLX Scheduler with ASP.NET MVC

Backend example for [DHTMLX Scheduler](https://dhtmlx.com/docs/products/dhtmlxScheduler/) implemented with ASP.NET MVC 5, Web API 2 and Entity Framework.

**Related tutorial**:
[https://docs.dhtmlx.com/scheduler/integrations/dotnet/howtostart-dotnet/](https://docs.dhtmlx.com/scheduler/integrations/dotnet/howtostart-dotnet/)


## Features

- Basic CRUD with REST data processor
- Dynamic loading by date range
- Recurring events using the rrule-based engine (Scheduler 7.1+)
- Web API exception filter for unified error responses
- XSS protection on the event `text` field

## Requirements

- Visual Studio 2022 with ".NET Framework Project and item templates"
- .NET Framework 4.x
- IIS Express / SQL Server LocalDB

## How to run

Clone the repository:

```
git clone https://github.com/DHTMLX/scheduler-howto-dotnet.git
cd scheduler-howto-dotnet
```

Open `DHX.Scheduler.Web.sln` in Visual Studio 2022, restore NuGet packages and run the project (F5).

The database is created and seeded automatically by `SchedulerInitializer` (`DropCreateDatabaseIfModelChanges`) on the first run and re-created whenever the model changes.

## Routes

- `/Home/Basic` - basic CRUD sample
- `/Home/Recurring` - recurring events sample (rrule-based)

## Tutorial

A complete tutorial is available at https://docs.dhtmlx.com/scheduler/howtostart_dotnet.html.

## License

Source code in this repository is released under the **MIT License**.

DHTMLX Scheduler is a commercial library - use under a valid [DHTMLX license](https://dhtmlx.com/docs/products/licenses.shtml) or evaluation agreement.

## Useful links

- [DHTMLX Scheduler product page](https://dhtmlx.com/docs/products/dhtmlxScheduler/)
- [Documentation](https://docs.dhtmlx.com/scheduler/)
- [Blog](https://dhtmlx.com/blog/)
- [Forum](https://forum.dhtmlx.com/)
