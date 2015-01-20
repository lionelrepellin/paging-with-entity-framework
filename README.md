Paging with Entity Framework
============================

## Table of contents

- [Requirements](#requirements)
- [Quick start](#quick-start)
- [Bugs and feature](#bugs-and-feature)
- [Documentation](#documentation)
- [Glimpse](#glimpse)
- [Files description](#files-description)
- [Creator](#creator)
- [Copyright and license](#copyright-and-license)


## Requirements

- SQL Server Database
- .NET Framework 4.5 (should work with 4.0)
- Visual Studio 2013 Express

## Quick start

- Download the last release
- Put your database settings into Web.config
- Create table with App_Data/CreateTable.sql
- Insert a lot of data into the table with App_Data/Data.sql
- Run the Web App from Visual Studio with F5 !

## Bugs and feature

- No bug. No problem ! ;)
- No other feature planned for now.

## Documentation

There are two goals for this project :

1. Use Entity Framework (Code First with Enum) and learn how to paging results
2. Create a nice and customizable navigation bar from scratch

I hope this navigation bar will be integrated in your projects !

#### How does it work ?

1. You need to know : 
- the current page number
- the total number of pages (total line number divided by line number per page)
- the controller url to retrieve the result
- you can display the previous and next buttons
- you can display the first and last buttons too

2. Have a look at the two defined constants in the NavBar class
- ```MAX_PAGE_TO_DISPLAY``` : currently 11 pages will be displayed
- ```PAGES_AROUND_CURRENT``` : 5 pages are defined to be displayed before and after the current page (if possible!)

3. Let's go create a new navigation bar !
- ```var navbar = new NavBar(1, 30, "/Home/Get?page");```

4. And now get the result simply
- ```navbar.Draw();```

#### What this is not

This code should be good but it is just a try. Use this code at your own risk !

## Glimpse

You can activate [Glimpse](http://getglimpse.com/) to view SQL queries like this :

- Launch the App from Visual Studio (F5)
- Go to url : http://localhost:[PORT]/Glimpse.axd
- Click on : "Turn Glimpse On" button


## Files description

```
PagingEntityFramework
├── App_Data
│   ├── CreateTable.sql
│   └── Data.sql
├── Controllers
│   └── HomeController.cs
├─── DAL
│   └── Context.cs
├── Domain
│   └── Error.cs
├── Helpers
│   ├── CustomHelpers.cs
│   └── NavBar.cs
├── Model
│   └── ErrorModel.cs
└── Views
    └── Home
        └── Index.cshtml
```

#### *.sql

Create table and insert data.

#### HomeController.cs

The main controller. Index method load the first page of errors. Get method is used to navigate through pages with page number passed in paremeter. 

Don't forget to set the linesPerPage variable.

#### Context.cs

A very simple context used to :

- retrieve just some errors from database
- retrieve the total number of errors contained in the table

#### Errors.cs

Represents the data table (Code First with database)

#### CustomHelpers.cs

Create a new NavBar object with some arguments :

- the current page number
- the total pages number (calculated in the Model)
- controller url

#### NavBar.cs

All the logic to create a navigation bar (with Bootstrap theme) 

See the code and modify this :

```
private const int MAX_PAGE_TO_DISPLAY = 11;
private const int PAGES_AROUND_CURRENT = 5;
```

#### ErrorModel.cs

It calculates the total number of pages to display (total number of lines in the table divided by number of lines per page)

#### Index.cshtml

The simpliest view I had ever write !

## Creator

Lionel Repellin inspired by himself !

## Copyright and license

Code released under [the MIT license](https://github.com/twbs/bootstrap/blob/master/LICENSE). 