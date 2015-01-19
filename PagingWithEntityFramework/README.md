Paging with Entity Framework
============================

## Table of contents

- [Requirements](#requirements)
- [Quick start](#quick-start)
- [Bugs and feature](#bugs-and-feature)
- [Documentation](#documentation)
- [Files description](#files-description)
- [Contributing](#contributing)
- [Creator](#creators)
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
- Run the App with F5 !

## Bugs and feature

- No bug. No problem ! ;)
- No other feature planned for now.

## Documentation



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

### *.sql

Create table and insert data

### HomeController.cs

The main controller. Index method load the first page of errors. Get method is used to navigate through pages with page number passed in paremeter. 

Don't forget to set the linesPerPage variable.

### Context.cs

A very simple context used to :

- retrieve all errors from database
- retrieve the total number of errors contained in the table

### Errors.cs

Represents the data table.

### CustomHelpers.cs

Create a new NavBar object with some arguments :

- current page
- total page number (calculated in the Model)
- controller url

### NavBar.cs

All the logic to create a navigation bar (with Bootstrap theme) 

See the code and modify this :

```
private const int MAX_PAGE_TO_DISPLAY = 11;
private const int PAGES_AROUND_CURRENT = 5;
```

### ErrorModel.cs

It calculates the total number of pages to display (total number of lines in the table divided by number of lines per page)

### Index.cshtml

The simpliest view I had ever write !

## Creator

Lionel Repellin inspired by himself !

## Copyright and license

Code released under [the MIT license](https://github.com/twbs/bootstrap/blob/master/LICENSE). 