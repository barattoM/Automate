var main = document.querySelector('.AccueilContenu');

var DateNow = new Date().toISOString().split('T')[0];

var reqGraphTemperatures = new XMLHttpRequest();

window.onload = function () {
    var points = [];
    
    var stockChart = new CanvasJS.StockChart("graphiqueTemperature",{//id de l'element html qui doit accueillir le graph
      title:{
        text:"Températures statistique"
      },  
      charts: [{//le graph a proprement parlé
        axisY: [{//va affecter tous ce qui est sur l'axe vertical
            title:"Degrès °C"
        }],
        axisX: {//va affecter tous ce qui est sur l'axe horizontal
			labelFormatter: function (e) {//permet de formater les labels sur l'axe en question
				return CanvasJS.formatDate( e.value, "DD/MM/YYYY HH:mm");
			}}
            ,

        data: [
            {  
                type: "line",//le type de graphique
                dataPoints : points//la variable ou chercher les données (un tableau d'objet. les objets en question doivent avoir en attribut un x ou label et un y)
            }  
        ]
      }],
      rangeSelector:{//inputs alterant le navigator
          label:"interval",//pour renomer le range Selector
          buttons:[//por redéffinir les boutons du range Selector
              {
                label:"30min",
                range:30,
                rangeType:"minute"
              },
              {
                label:"10min",
                range:10,
                rangeType:"minute"
              },
              {
                label:"1H",
                range:1,
                rangeType:"hour"
              },
              {
                label:"tout",
                range:1,
                rangeType:"all"
              }
            ],
          inputFields:{

            valueFormatString:"DD/MM/YYYY HH:mm",//formatage des dates pour les afficher proprement dans les inputs

          }
      },
      navigator: {//partie basse permetant de se mouvoir dans le graph
        slider: {
          minimum: DateNow+' 00:00:00',//position de l'interval initial   /!\en js les mois commencent à 0
          maximum: DateNow+' 23:59:59'
        },
        axisX:{
            minimum: DateNow+' 10:00:00',//interval max du navigator
            maximum: DateNow+' 15:59:59'
        }
      }
    });
    
    reqGraphTemperatures.open('POST', 'index.php?page=AccueilTemperatureAPI', true);
    reqGraphTemperatures.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    reqGraphTemperatures.send(["DateNow="+DateNow]);

    reqGraphTemperatures.onreadystatechange = function(event) {
        if (this.readyState === XMLHttpRequest.DONE) {
            if (this.status === 200) {
                console.log(this.responseText);
                reponse = JSON.parse(this.responseText);
                // main.innerHTML += this.responseText;
                console.log(reponse);

                for(var i = 0; i < reponse.length; i++){//remplissage des données apres coup (indispensable vu qu'on va etre asychrone)
                points.push({x: new Date(reponse[i].DateTemperature), y: Number(reponse[i].ValeurTemperature)});			
                }	
                stockChart.render(); 
        
            } else {
            console.log("Status de la réponse: %d (%s)", this.status, this.statusText);
            }
        }
    }
      
}