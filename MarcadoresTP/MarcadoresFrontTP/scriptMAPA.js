let POINTS = [];
document.addEventListener('DOMContentLoaded', async function () 
{
    await GETPOINTS();

    Cargarpuntos();
})

function Cargarpuntos()
{
    for(let i = 0; i< POINTS.length; i++)
    {
        const item = POINTS[i];
        var point = new H.map.Marker({ lat: item.latitud, lng: item.longitud });
        point.setData(item.info);
        var bubble = new H.ui.InfoBubble({ lat: item.latitud, lng: item.longitud }, {
            content: `<b>${item.info}</b>`
          });
        
          ui.addBubble(bubble);

    }
}

async function GETPOINTS()
{
    const url = "https://localhost:7026/obtenerMarcadores";
    try
    {
        const response = await fetch(url,
            {
                method: "GET",
                headers: {
                    'Content-Type': 'application/json'
                }
            });
        let data = await response.json();
        if(response.ok)
        {
            POINTS.length = 0;
            POINTS = data.litadoMarcadores;
        }
    }
    catch(e)
    {
        console.error('Error:', e);
        alert("Ocurrió un error al realizar la peticion")

    }
}


var platform = new H.service.Platform({
    'apikey': '{q6HDQHDeYbwCEJYvfF5oNrpcHwrSf17EghOcz9ZZf1Q}'
  });


  var defaultLayers = platform.createDefaultLayers();




var map = new H.Map(
    document.getElementById('mapContainer'),
    defaultLayers.vector.normal.map,
    {
      zoom: 12,
      center: { lat: -31.458901, lng: -64.2048168 }
    });




var ui = H.ui.UI.createDefault(map, defaultLayers, 'es-ES');


var mapEvents = new H.mapevents.MapEvents(map);
var mapSettings = ui.getControl('mapsettings');
mapSettings.setAlignment('top-right')

var behavior = new H.mapevents.Behavior(mapEvents);
map.addEventListener('tap', function(evt) {
        console.log(evt.type, evt.currentPointer.type);
});










