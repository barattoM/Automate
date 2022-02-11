<?php
$elm = new Afpa_Temperatures($_POST);

die(var_dump($elm));

switch ($_GET['mode']) {
	case "Ajouter": {
		$elm = Afpa_TemperaturesManager::add($elm);
		break;
	}
	case "Modifier": {
		$elm = Afpa_TemperaturesManager::update($elm);
		break;
	}
	case "Supprimer": {
		$elm = Afpa_TemperaturesManager::delete($elm);
		break;
	}
}

header("location:index.php?page=ListeAfpa_Temperatures");