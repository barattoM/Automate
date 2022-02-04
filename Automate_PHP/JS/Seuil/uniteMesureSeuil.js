var divUniteMesure = document.querySelectorAll('.uniteMesure');
var selectNature = document.querySelector('#nature');
changeUnite(selectNature);

selectNature.addEventListener('change', function(){
    changeUnite(this);
});

function changeUnite(selectNature){
    var nature = selectNature.selectedOptions[0].value;
    switch (nature) {
        case "1":
            $unite = "°C";
            break;
        case "2":
            $unite = "Db";
            break;
        case "3":
            $unite = "lux";
            break;
        default:
            $unite = "";
            break;
    }
    divUniteMesure.forEach(element => {
        console.log(element)
        element.innerHTML = $unite;
    });
};