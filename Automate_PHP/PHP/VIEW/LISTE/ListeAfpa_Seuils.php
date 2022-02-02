<?php

 echo '<main>';

 echo '<div class="flex-0-1"></div>';

 echo '<div>';
 

$objets = Afpa_SeuilsManager::getList();
//Création du template de la grid
echo '<div class="grid-col-8 gridListe">';

echo '<div class="caseListe titreListe grid-columns-span-8">Liste des Afpa_Seuils </div>';
echo '<div class="caseListe grid-columns-span-8">
<div></div>
<div class="caseListe"><a href="index.php?page=FormAfpa_Seuils&mode=Ajouter"><i class="fas fa-plus"></i></a></div>
<div></div>
</div>';

echo '<div class="caseListe labelListe">Nature</div>';
echo '<div class="caseListe labelListe">SeuilBas</div>';
echo '<div class="caseListe labelListe">SeuilHaut</div>';
echo '<div class="caseListe labelListe">Temps</div>';
echo '<div class="caseListe labelListe">DateSeuil</div>';


//Remplissage de div vide pour la structure de la grid
echo '<div class="caseListe"></div>';
echo '<div class="caseListe"></div>';
echo '<div class="caseListe"></div>';

// Affichage des ennregistrements de la base de données
foreach($objets as $unObjet)
{
echo '<div class="caseListe donneeListe">'.$unObjet->getNature().'</div>';
echo '<div class="caseListe donneeListe">'.$unObjet->getSeuilBas().'</div>';
echo '<div class="caseListe donneeListe">'.$unObjet->getSeuilHaut().'</div>';
echo '<div class="caseListe donneeListe">'.$unObjet->getTemps().'</div>';
echo '<div class="caseListe donneeListe">'.$unObjet->getDateSeuil().'</div>';
echo '<div class="caseListe"> <a href="index.php?page=FormAfpa_Seuils&mode=Afficher&id='.$unObjet->getIdSeuil().'"><i class="fas fa-file-contract"></i></a></div>';
                                                    
echo '<div class="caseListe"> <a href="index.php?page=FormAfpa_Seuils&mode=Modifier&id='.$unObjet->getIdSeuil().'"><i class="fas fa-pen"></i></a></div>';
                                                    
echo '<div class="caseListe"> <a href="index.php?page=FormAfpa_Seuils&mode=Supprimer&id='.$unObjet->getIdSeuil().'"><i class="fas fa-trash-alt"></i></a></div>';
}
//Derniere ligne du tableau (bouton retour)
echo '<div class="caseListe grid-columns-span-8">
	<div></div>
	<a href="index.php?page=Accueil"><button><i class="fas fa-sign-out-alt fa-rotate-180"></i></button></a>
	<div></div>
</div>';

echo'</div>'; //Grid
echo'</div>'; //Div
echo '<div class="flex-0-1"></div>';
echo '</main>';