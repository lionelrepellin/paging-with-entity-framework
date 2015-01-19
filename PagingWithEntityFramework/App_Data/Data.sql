declare @i int 
set @i = 0

declare @errNum int
set @errNum = 1000

while @i < 1000
begin
	set @errNum = @errNum + 1

	insert erreur(ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage) 
	values(@errNum, @errNum/2, @i, 'proc ' + cast(@i as varchar), @i * 2, 'error dsf fs f fsfsdf')

	set @i = @i + 1
end
