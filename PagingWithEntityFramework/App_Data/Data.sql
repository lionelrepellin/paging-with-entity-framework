declare @i int = 0
declare @totalLines int = 500
declare @date datetime = GETDATE();
declare @minutes tinyint
declare @level varchar(10)
declare @serverName varchar(50)
declare @errorMsg varchar(100)


-- random error messages
declare @texts as table(id int identity(1,1), msg varchar(500))

insert @texts(msg)
select 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.'
  union
select 'Narrabimus peiores titulo acerbum dissimilem auribus in apud huius.'
  union
select 'Ideo urbs venerabilis post superbas efferatarum astute ut hoc.'
  union
select 'Auxerunt haec vulgi sordidioris audaciam, quod cum quae definit.'


-- random error level
declare @errorLevel as table(id int identity(1,1), msg varchar(10))

insert @errorLevel(msg)
select 'Warning'
  union
select 'Error'
  union
select 'Fatal'


while @i < @totalLines
begin	
	-- random server name
	select @serverName = 'Server_' + CAST(ROUND(((5) * RAND()), 0) AS VARCHAR)

	-- random minutes to add to the current date
	select @minutes = ROUND(((59 - 1) * RAND() + 1), 0)
	set @date = DATEADD(n, @minutes, @date)

	-- random error message
	select @errorMsg = msg
	from @texts
	where id = ROUND(((4) * RAND()), 0)

	-- random error level
	select @level = msg
	from @errorLevel
	where id = ROUND(((3) * RAND()), 0)

	-- insert data
	insert [log]([date], [server], severity, [message]) 
	values(@date, @serverName, @level, @errorMsg)

	set @i = @i + 1
end

select count(0) from [log]
select * from [log] 
