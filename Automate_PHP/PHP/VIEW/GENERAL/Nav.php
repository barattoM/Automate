<nav id= "menu">
<ul>
<?php
echo $nom;
    if ($nom != "ListeAfpa_Anomalies") {
        echo '<li><a href="?page=ListeAfpa_Anomalies">Anomalies</a><li>';
    }
    if ($nom != "ListeAfpa_Seuils" && $_SESSION['utilisateur']->getRole() > 1) {
        echo '<li><a href="?page=ListeAfpa_Seuils">Seuils</a><li>';
    }
    // if ($nom != "Accueil") {
    // echo '<li><a href="?page=Accueil">Accueil</a><li>';
    // }
?>
<li><a href='?page=Accueil'>Accueil</a><li>
<li><a href='?page=ListeAfpa_Lumieres'>Lumieres</a><li>
<li><a href='?page=ListeAfpa_Objectifs'>Objectifs</a><li>
<li><a href='?page=ListeAfpa_Sons'>Sons</a><li>
<li><a href='?page=ListeAfpa_Temperatures'>Temperatures</a><li>


</ul>
</nav>
