select
p.nombre as proyecto,
pr.descripcion as producto
from producto_proyecto pp
inner join proyecto p on p.proyecto = pp.proyecto
inner join producto pr on pr.producto = pp.producto
where pp.proyecto = 1;

select 
p.nombre as proyecto, 
pr.descripcion as producto, 
fm.nombre as mensaje
from cod_mensaje cm
inner join formato_mensaje fm ON cm.cod_formato = fm.cod_formato
inner join proyecto p ON cm.proyecto = p.proyecto
inner join producto pr ON cm.producto = pr.producto;

select 
    p.nombre as proyecto,
    case when count(distinct cm.producto) = (select count(*) from producto_proyecto pp where pp.proyecto = cm.proyecto) 
         then 'Todos' 
         else max(pr.descripcion)
    end as producto,
    fm.nombre as mensaje
from cod_mensaje cm
inner join proyecto p on cm.proyecto = p.proyecto
inner join producto pr on cm.producto = pr.producto
inner join formato_mensaje fm on cm.cod_formato = fm.cod_formato
group by cm.proyecto, fm.nombre;