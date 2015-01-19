declare @i int = 0
declare @totalLines int = 500
declare @date datetime = GETDATE();
declare @minutes tinyint
declare @level tinyint

while @i < @totalLines
begin
	
	select @level = ROUND(((3) * RAND()), 0)
	select @minutes = ROUND(((59 - 1) * RAND() + 1), 0)
	set @date = DATEADD(n, @minutes, @date)

	insert [log]([date], severity, [message]) 
	values(@date, @level, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.')

	set @i = @i + 1
end

select * from [log] 