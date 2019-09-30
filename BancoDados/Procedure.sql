use educagame;

create procedure adicionar_verificacao
(
	@campo1 int,
	@campo2 int,
	@campo3 int,
	@campo4 int,
	@campo5 int,
	@campo6 int

)
as
begin try
    begin tran

        insert into TabelaDes(quest1, quest2, quest3, quest4, quest5, quest6)
        values (@campo1, @campo2, @campo3, @campo4, @campo5, @campo6)

    commit tran
end try
begin catch
    rollback tran
end catch


EXEC adicionar_verificacao 1,1,1,0,0,0

SELECT * FROM TabelaDes