<?php

 echo '<main>';

 echo '<div class="flex-0-1"></div>';

 echo '<div>';
 

$objets = Afpa_UtilisateursManager::getList();
//Création du template de la grid
echo '<div class="grid-col-8 gridListe">';

echo '<div class="caseListe titreListe grid-columns-span-8">Liste des Utilisateurs </div>';
echo '<div class="caseListe grid-columns-span-8">
<div></div>
<div class="caseListe"><a href="index.php?page=FormUtilisateurs&mode=Ajouter"><i class="fas fa-plus"></i></a></div>
<div></div>
</div>';

echo '<div class="caseListe labelListe">Nom</div>';
echo '<div class="caseListe labelListe">Prenom</div>';
echo '<div class="caseListe labelListe">AdresseMail</div>';
echo '<div class="caseListe labelListe">MotDePasse</div>';
echo '<div class="caseListe labelListe">Role</div>';

//Remplissage de div vide pour la structure de la grid
echo '<div class="caseListe"></div>';
echo '<div class="caseListe"></div>';
echo '<div class="caseListe"></div>';

// Affichage des ennregistrements de la base de données
foreach($objets as $unObjet)
{
echo '<div class="caseListe donneeListe">'.$unObjet->getNom().'</div>';
echo '<div class="caseListe donneeListe">'.$unObjet->getPrenom().'</div>';
echo '<div class="caseListe donneeListe">'.$unObjet->getAdresseMail().'</div>';
echo '<div class="caseListe donneeListe">'.$unObjet->getMotDePasse().'</div>';
echo '<div class="caseListe donneeListe">'.$unObjet->getRole().'</div>';
echo '<div class="caseListe"> <a href="index.php?page=FormUtilisateurs&mode=Afficher&id='.$unObjet->getIdUtilisateur().'"><i class="fas fa-file-contract"></i></a></div>';
                                                    
echo '<div class="caseListe"> <a href="index.php?page=FormUtilisateurs&mode=Modifier&id='.$unObjet->getIdUtilisateur().'"><i class="fas fa-pen"></i></a></div>';
                                                    
echo '<div class="caseListe"> <a href="index.php?page=FormUtilisateurs&mode=Supprimer&id='.$unObjet->getIdUtilisateur().'"><i class="fas fa-trash-alt"></i></a></div>';
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