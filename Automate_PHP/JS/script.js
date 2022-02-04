// Récupération des blocs
var mainMenu = document.querySelector("#menu");
var burgerMenu = document.querySelector("#menu-burger");

/*===============================*/
/*=== Clic sur le menu burger ===*/
/*===============================*/
// Vérifie si l'événement touchstart existe et est le premier déclenché
var clickedEvent = "click"; // Au clic si "touchstart" n'est pas détecté
window.addEventListener('touchstart', function detectTouch() {
	clickedEvent = "touchstart"; // Transforme l'événement en "touchstart"
	window.removeEventListener('touchstart', detectTouch, false);
}, false);

// Créé un "toggle class" en Javascrit natif (compatible partout)
burgerMenu.addEventListener(clickedEvent, function(evt) {
	if(this.classList.contains("clicked")){
		this.classList.remove("clicked")
	}else{
		this.classList.add("clicked")
	}

	if(mainMenu.classList.contains("invisible")){
		mainMenu.classList.add("visible")
		mainMenu.classList.remove("invisible")

	}else{
		mainMenu.classList.remove("visible")
		mainMenu.classList.add("invisible")
	}
}, false);


	


