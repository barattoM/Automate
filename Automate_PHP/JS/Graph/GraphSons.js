const port=44340;
now= new Date();


// window.onload = function () {
  GetApiSon();
    var son = [];
    var seuilBas = [];
    var seuilHaut = [];

    var stockChart = new CanvasJS.StockChart("graphiqueSon",{//id de l'element html qui doit accueillir le graph
      title:{
        text:"valeur du son"
      }, 
      charts: [{//le graph a proprement parlé
        axisY: [{//va affecter tous ce qui est sur l'axe vertical
            title:"Db"
        }],
        axisX: {//va affecter tous ce qui est sur l'axe horizontal
			labelFormatter: function (e) {//permet de formater les labels sur l'axe en question
				return CanvasJS.formatDate( e.value, "DD/MM/YYYY HH:mm");
			}}
            ,

        data: [
            {  
                type: "line",//le type de graphique
                dataPoints : son//la variable ou chercher les données (un tableau d'objet. les objets en question doivent avoir en attribut un x ou label et un y)
            },
            {  
              type: "line",//le type de graphique
              dataPoints : seuilHaut//la variable ou chercher les données (un tableau d'objet. les objets en question doivent avoir en attribut un x ou label et un y)
            },
            {  
              type: "line",//le type de graphique
              dataPoints : seuilBas//la variable ou chercher les données (un tableau d'objet. les objets en question doivent avoir en attribut un x ou label et un y)
            }  
        ]
      }],
      rangeSelector:{//inputs alterant le navigator
          label:"interval",//pour renomer le range Selector
          buttons:[//por redéffinir les boutons du range Selector
              {
                label:"10min",
                range:10,
                rangeType:"minute"
              },
              {
                label:"30min",
                range:30,
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
          minimum: now.setHours(now.getHours()-12),//position de l'interval initial   /!\en js les mois commencent à 0
          maximum: now.setHours(Number(now.getHours())+12)
        },
        axisX:{
            minimum:now.setHours(now.getHours()-12),//interval max du navigator
            maximum:now.setHours(Number(now.getHours())+12)
        }
      }
    });
    
    
    function GetApiSon(){
      let requ = new XMLHttpRequest();    
      requ.open('GET', 'https://localhost:'+port+'/api/Afpa_Sons', true);
      requ.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
      requ.send();
      requ.onreadystatechange = function(event) {
        if (this.readyState === XMLHttpRequest.DONE) {
          if (this.status === 200) {
            let reponse=JSON.parse(this.responseText);
            console.log(reponse);
            for(var i = 0; i < reponse.length; i++){//remplissage des données apres coup (indispensable vu qu'on va etre asychrone)
              son.push({x: new Date(reponse[i].dateSon), y: Number(reponse[i].valeurSon)});			
            }
            console.log(son) ;    
            stockChart.render();
          } else {
            console.log("Status de la réponse: %d (%s)", this.status, this.statusText);
          }
        }
      }
    }