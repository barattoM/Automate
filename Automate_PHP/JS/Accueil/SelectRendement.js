var selectRendement = document.querySelector('select[name="RendementS"]');
var inputRendement = document.querySelector('#inputR');
const valueRendement = inputRendement.value;

selectRendement.addEventListener('change', function(){
    inputRendement.value = valueRendement / this.selectedOptions[0].value;
});