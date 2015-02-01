Paging with Entity Framework
===

## Table of contents

- [What it looks like?](#what-it-looks-like)
- [Requirements](#requirements)
- [Quick start](#quick-start)
- [Bugs and feature](#bugs-and-feature)
- [Documentation](#documentation)
- [Glimpse](#glimpse)
- [Projects description](#projects-description)
- [Contoso University project](#contoso-university-project)
- [Creator](#creator)
- [Copyright and license](#copyright-and-license)


## What it looks like?

This is the navigation bar (with Bootstrap theme). You can define the minimum number of page to be displayed. You can also activate Previous and Next buttons and / or First and Last buttons.

![Navigation Bar](https://github.com/lionelrepellin/paging-with-entity-framework/blob/master/navbar.png "Navigation Bar")


## Requirements

- SQL Server Database
- .NET Framework 4.5 (work with 4.0)
- Visual Studio 2013 Express

## Quick start

- Download the last release
- Put your database settings into Web.config
- Create table with App_Data/CreateTable.sql
- Insert a lot of data into the table with App_Data/Data.sql
- Run the Web App from Visual Studio with F5 !

## Bugs and feature

- I hope there is no bug ! Look at Paging.Tests project and add unit testing if you want.
- No other feature planned for now.

## Documentation

There are two goals for this project :

1. Use Entity Framework (Code First) and learn how to paging results
2. Create a nice and customizable navigation bar from scratch (with Bootstrap theme)
  - Each button type is customizable

I hope this project will help you in your developments.

#### What this is not

This code should be good but it is just a try. I think it should be optimized. Use this code at your own risk.

## Glimpse

You can activate [Glimpse](http://getglimpse.com/) to view SQL queries like this :

- Launch the App from Visual Studio (F5)
- Go to url : http://localhost:[PORT]/Glimpse.axd
- Click on : "Turn Glimpse On" button


## Projects description

#### Paging

The main logic of the navigation bar. Be careful the navigation bar is auto adaptive. You can put this into an assembly and use it in your projects using Bootstrap.

You can play with the constants (```MAX_PAGE_TO_DISPLAY``` and ```PAGES_AROUND_CURRENT```) in NavBar.cs but the unit testing may be broken.

```
Paging
├── Buttons
│   ├── Button.cs
│   ├── First.cs
│   ├── Last.cs
│   ├── Next.cs
│   ├── Page.cs
│   └── Previous.cs
└── NavBar.cs
```

#### Paging.Tests

Some unit testing for the navigation bar. I hope every cases have been tested.

#### PagingWithEntityFramework

The main project who drives all actions. Look at especially these files :

```
PagingEntityFramework
├── App_Data
│   ├── CreateTable.sql
│   └── Data.sql
├── Controllers
│   └── HomeController.cs
├── Helpers
│   └── CustomHelpers.cs
├── Model
│   └── ErrorModel.cs
└── Views
    └── Home
        └── Index.cshtml
```

#### PagingWithEntityFramework.Business

A very simple business layer. No particular comment.

#### PagingWithEntityFramework.DAL

The data access layer. I used inline queries and mapped entities to retrieve all data from database.

#### PagingWithEntityFramework.Domain

Juste some DTO - nothing special here.

```
PagingEntityFramework.Domain
├── Entities
│   └── Error.cs
├── ErrorResult.cs
└── SearchCriteria.cs
```

## Contoso University project

These two projects seem to do the same thing (paging with EF6, MVC4/5 and navigation bar) but what if there are million lines in the database ?
With Contoso and PagedList nugget package you retrieve all lines each time and display only a few lines per page. The application could be slow and maybe you can have a timeout. I think this is not the right way to do.

This is why I prefered to make my own paging system with two SQL queries :

- the first to retrieve only the wanted range of lines
- the second to get the total number of lines in the table (used for paging)

The original [Contoso University](http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application) project.

## Creator

Lionel Repellin inspired by himself !

## Copyright and license

Code released under [the MIT license](https://github.com/twbs/bootstrap/blob/master/LICENSE). 