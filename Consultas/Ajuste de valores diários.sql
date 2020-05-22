use covid
set rowcount 0
update DadosMS set CasosDia=isnull(Casos,0), ObitosDia=isnull(Obitos,0), RecuperadosDia = isnull(Recuperados,0), EmAcompanhamentoDia=isnull(EmAcompanhamento,0)
from DadosMS 
	inner join (
select 
	id
	, Data
	, CasosAcumulados 
	, CasosAcumulados-lag(CasosAcumulados) over (order by  data) Casos
	, ObitosAcumulados
	, ObitosAcumulados-lag(ObitosAcumulados) over (order by  data) Obitos
	, RecuperadosAcumulados
	, RecuperadosAcumulados-lag(RecuperadosAcumulados) over (order by  data) Recuperados
	, EmAcompanhamentoAcumulados
	, EmAcompanhamentoAcumulados-lag(EmAcompanhamentoAcumulados) over (order by  data) EmAcompanhamento
from DadosMS
where PaisId=76) a on DadosMS.Id = a.Id


update DadosMS set CasosDia=isnull(Casos,0), ObitosDia=isnull(Obitos,0), RecuperadosDia = isnull(Recuperados,0), EmAcompanhamentoDia=isnull(EmAcompanhamento,0)
from DadosMS 
	inner join (
select 
	id
	, Data
	, CasosAcumulados 
	, CasosAcumulados-lag(CasosAcumulados) over (partition by estadoid order by  data) Casos
	, ObitosAcumulados
	, ObitosAcumulados-lag(ObitosAcumulados) over (partition by estadoid order by  data) Obitos
	, RecuperadosAcumulados
	, RecuperadosAcumulados-lag(RecuperadosAcumulados) over (partition by estadoid order by  data) Recuperados
	, EmAcompanhamentoAcumulados
	, EmAcompanhamentoAcumulados-lag(EmAcompanhamentoAcumulados) over (partition by estadoid order by  data) EmAcompanhamento
from DadosMS
where EstadoId is not null)
a on DadosMS.Id = a.Id


update DadosMS set CasosDia=isnull(Casos,0), ObitosDia=isnull(Obitos,0), RecuperadosDia = isnull(Recuperados,0), EmAcompanhamentoDia=isnull(EmAcompanhamento,0)
from DadosMS 
	inner join (
select 
	id
	, Data
	, CasosAcumulados 
	, CasosAcumulados-lag(CasosAcumulados) over (partition by MunicipioId order by  data) Casos
	, ObitosAcumulados
	, ObitosAcumulados-lag(ObitosAcumulados) over (partition by MunicipioId order by  data) Obitos
	, RecuperadosAcumulados
	, RecuperadosAcumulados-lag(RecuperadosAcumulados) over (partition by MunicipioId order by  data) Recuperados
	, EmAcompanhamentoAcumulados
	, EmAcompanhamentoAcumulados-lag(EmAcompanhamentoAcumulados) over (partition by MunicipioId order by  data) EmAcompanhamento
from DadosMS
where MunicipioId is not null)
a on DadosMS.Id = a.Id


